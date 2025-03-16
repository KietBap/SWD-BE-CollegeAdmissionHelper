using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.Core.Base;
using CAH.ModelViews.AdmissionInforModelViews;
using CAH.ModelViews.UniversityModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
    public class AdmissionInforService : IAdmissionInforService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public AdmissionInforService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContext = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> CreateAdmissionInforAsync(CreateAdInforModelView model)
        {
            try
            {
                var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;


                // Tạo AdmissionInfor
                AdmissionInfor admissionInfor = _mapper.Map<AdmissionInfor>(model);
                admissionInfor.CreatedBy = userId;
                admissionInfor.CreatedTime = DateTimeOffset.UtcNow;

                await _unitOfWork.GetRepository<AdmissionInfor>().InsertAsync(admissionInfor);
                await _unitOfWork.SaveAsync();

                //// Tạo từng InforMethod với thông tin riêng biệt
                //foreach (var inforMethodModel in model.InforMethods)
                //{
                //    var inforMethod = new InforMethod
                //    {
                //        AdmissionInforId = admissionInfor.Id,
                //        AdmissionMethodId = inforMethodModel.AdmissionMethodId,
                //        ScoreType = inforMethodModel.ScoreType,
                //        ScoreRequirement = inforMethodModel.ScoreRequirement,
                //        PercentageOfQuota = inforMethodModel.PercentageOfQuota
                //    };

                //    await _unitOfWork.GetRepository<InforMethod>().InsertAsync(inforMethod);
                //}

                //await _unitOfWork.SaveAsync();

                return "Admission information added successfully";
            }
            catch (BaseException.BadRequestException ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<BasePaginatedList<ListAdInforModelView>> GetAllAdmissionInforAsync(int pageNumber, int pageSize, string? uniName, string? majorName, int? year)
        {
            IQueryable<AdmissionInfor> query = _unitOfWork.GetRepository<AdmissionInfor>().Entities
                .Where(p => !p.DeletedTime.HasValue)
                .Include(p => p.UniMajor) // Lấy liên kết đến UniMajor
                    .ThenInclude(um => um.University) // Lấy University từ UniMajor
                .Include(p => p.UniMajor.Major) // Lấy Major từ UniMajor
                .Include(p => p.InforMethods) // Lấy danh sách phương thức tuyển sinh
                    .ThenInclude(im => im.AdmissionMethod)
                .OrderByDescending(p => p.CreatedTime);

            // Lọc theo tên trường đại học
            if (!string.IsNullOrEmpty(uniName))
            {
                query = query.Where(p => p.UniMajor.University.Name.Contains(uniName));
            }

            // Lọc theo tên ngành
            if (!string.IsNullOrEmpty(majorName))
            {
                query = query.Where(p => p.UniMajor.Major.Name.Contains(majorName));
            }

            if (year.HasValue)
            {
                query = query.Where(p => p.AdmisstionDate.Year == year.Value);
            }

            int totalCount = await query.CountAsync();

            // Lấy dữ liệu phân trang
            List<ListAdInforModelView> adInforModelView = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ListAdInforModelView
                {
                    Id = p.Id,
                    UniversityName = p.UniMajor.University.Name,
                    MajorName = p.UniMajor.Major.Name,
                    MethodName = string.Join(", ", p.InforMethods.Select(im => im.AdmissionMethod.MethodName)),
                    AdmisstionDate = p.AdmisstionDate
                })
                .ToListAsync();

            return new BasePaginatedList<ListAdInforModelView>(adInforModelView, totalCount, pageNumber, pageSize);
        }


        public async Task<string> DeleteAdmissionInforAsync(string id)
        {
            try
            {
                var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

                var admissionInfor = await _unitOfWork.GetRepository<AdmissionInfor>().Entities
                    .Include(a => a.InforMethods) 
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (admissionInfor == null)
                {
                    return "Can not find admissionInfor";
                }

                foreach (var inforMethod in admissionInfor.InforMethods)
                {
                    inforMethod.DeletedTime = DateTimeOffset.UtcNow;
                    inforMethod.DeletedBy = userId;
                    await _unitOfWork.GetRepository<InforMethod>().UpdateAsync(inforMethod);
                }

                admissionInfor.DeletedTime = DateTimeOffset.UtcNow;
                admissionInfor.DeletedBy = userId;

                await _unitOfWork.GetRepository<AdmissionInfor>().UpdateAsync(admissionInfor);
                await _unitOfWork.SaveAsync();

                return "AdmissionInfor and related InforMethods deleted successfully";
            }
            catch (BaseException.BadRequestException ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<Object> GetAdInforByIdAsync(string id)
        {
            try
            {
                var admissionInfor = await _unitOfWork.GetRepository<AdmissionInfor>().Entities
                    .Where(a => a.Id == id && !a.DeletedTime.HasValue)
                    .Include(a => a.InforMethods)
                    .ThenInclude(im => im.AdmissionMethod)
                    .FirstOrDefaultAsync();

                if (admissionInfor == null)
                {
                    return "Can not find or AdmissionInfor is deleted";
                }

                var result = new AdmissionInforModelView
                {
                    Id = admissionInfor.Id.ToString(),
                    Quota = admissionInfor.Quota,
                    AdmisstionDate = admissionInfor.AdmisstionDate,
                    Deadline = admissionInfor.Deadline,
                    InforMethods = admissionInfor.InforMethods
                        .Where(im => !im.DeletedTime.HasValue)
                        .Select(im => new ListInforMethodModelView
                        {
                            InforMethodId = im.Id,
                            MethodName = im.AdmissionMethod.MethodName,
                            ScoreType = im.ScoreType,
                            ScoreRequirement = im.ScoreRequirement,
                            PercentageOfQuota = im.PercentageOfQuota
                        }).ToList()
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> MarkWishlistAsync(string adInforId)
        {
            try
            {
                var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

                Wishlist wishlist = new Wishlist
                {
                    UserId = Guid.Parse(userId),
                    AdmissionInforId = adInforId,
                    CreatedBy = userId,
                    CreatedTime = DateTime.UtcNow,
                };

                await _unitOfWork.GetRepository<Wishlist>().InsertAsync(wishlist);
                await _unitOfWork.SaveAsync();

                return "Marked!";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<BasePaginatedList<ListAdInforModelView>> GetWishlistAsync(int pageNumber, int pageSize)
        {
            var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new BasePaginatedList<ListAdInforModelView>(new List<ListAdInforModelView>(), 0, pageNumber, pageSize);
            }

            IQueryable<Wishlist> query = _unitOfWork.GetRepository<Wishlist>().Entities
                .Where(w => w.UserId.ToString() == userId && !w.DeletedTime.HasValue)
                .Include(w => w.AdmissionInfor) // Bao gồm AdmissionInfor
                    .ThenInclude(p => p.UniMajor) // Lấy UniMajor từ AdmissionInfor
                        .ThenInclude(um => um.University) // Lấy University từ UniMajor
                .Include(w => w.AdmissionInfor.UniMajor.Major) // Lấy Major từ UniMajor
                .Include(w => w.AdmissionInfor.InforMethods) // Lấy danh sách phương thức tuyển sinh
                    .ThenInclude(im => im.AdmissionMethod)
                .OrderByDescending(w => w.AdmissionInfor.CreatedTime);

            int totalCount = await query.CountAsync();

            List<ListAdInforModelView> adInforModelView = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(w => new ListAdInforModelView
                {
                    Id = w.AdmissionInfor.Id,
                    UniversityName = w.AdmissionInfor.UniMajor.University.Name,
                    MajorName = w.AdmissionInfor.UniMajor.Major.Name,
                    MethodName = string.Join(", ", w.AdmissionInfor.InforMethods.Select(im => im.AdmissionMethod.MethodName)),
                    AdmisstionDate = w.AdmissionInfor.AdmisstionDate
                })
                .ToListAsync();

            return new BasePaginatedList<ListAdInforModelView>(adInforModelView, totalCount, pageNumber, pageSize);
        }

        public async Task<string> UnMarkWishlistAsync(string admissionInforId)
        {
            try
            {
                var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

                var wishlist = await _unitOfWork.GetRepository<Wishlist>()
                                                    .Entities.Where(w => w.AdmissionInforId ==  admissionInforId && w.UserId.ToString() == userId)
                                                    .FirstOrDefaultAsync();
                wishlist.DeletedTime = DateTime.Now;
                wishlist.DeletedBy = userId;

                await _unitOfWork.GetRepository<Wishlist>().UpdateAsync(wishlist);
                await _unitOfWork.SaveAsync();

                return "UnMarked!";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        //public async Task<string> UpdateAdmissionInforAsync(string id, UniversityModelView model)
        //{
        //    try
        //    {
        //        var userId = _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

        //        string imageUrl = string.Empty;
        //        if (model.Image != null)
        //        {
        //            var imageHelper = new Core.Utils.Firebase.ImageHelper(_configuration);
        //            imageUrl = await imageHelper.Upload(model.Image);
        //        }

        //        var university = await _unitOfWork.GetRepository<University>().GetByIdAsync(id);
        //        if (university == null || university.DeletedTime.HasValue)
        //        {
        //            return "can not find or university is deleted";
        //        }

        //        _mapper.Map(model, university);
        //        university.LastUpdatedBy = userId;
        //        university.LastUpdatedTime = DateTimeOffset.UtcNow;
        //        university.Image = imageUrl;

        //        await _unitOfWork.GetRepository<University>().UpdateAsync(university);
        //        await _unitOfWork.SaveAsync();

        //        return "University updated successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error: {ex.Message}");
        //    }
        //}




    }
}
