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

        public async Task<string> CreateAdmissionInforAsync(AdmissionInforModelView model)
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


        public async Task<BasePaginatedList<ListAdInforModelView>> GetAllAdmissionInforAsync(int pageNumber, int pageSize, string? uniName, string? majorName)
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
                    MethodName = string.Join(", ", p.InforMethods.Select(im => im.AdmissionMethod.MethodName)), // Lấy danh sách phương thức tuyển sinh
                    AdmisstionDate = p.AdmisstionDate
                })
                .ToListAsync();

            return new BasePaginatedList<ListAdInforModelView>(adInforModelView, totalCount, pageNumber, pageSize);
        }
    }
}
