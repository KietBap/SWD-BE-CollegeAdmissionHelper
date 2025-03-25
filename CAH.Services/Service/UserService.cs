using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.ModelViews.UniMajorModelViews;
using CAH.ModelViews.UniversityModelViews;
using CAH.ModelViews.UserModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAH.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<BasePaginatedList<ListUserModelView>> GetAllUserAsync(int pageNumber, int pageSize, string id, string email, string phoneNumber)
        {
            var roleId = await _unitOfWork.GetRepository<Role>()
                .Entities
                .Where(r => r.Name == "User")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            var userQuery = from user in _unitOfWork.GetRepository<User>().Entities.AsNoTracking()
                            join userRole in _unitOfWork.GetRepository<UserRole>().Entities.AsNoTracking()
                                on user.Id equals userRole.UserId
                            where userRole.RoleId == roleId && !user.DeletedTime.HasValue
                            select user;

            if (!string.IsNullOrWhiteSpace(id))
            {
                userQuery = userQuery.Where(p => p.Id.ToString() == id);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                userQuery = userQuery.Where(p => p.Email.Contains(email));
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                userQuery = userQuery.Where(p => p.PhoneNumber.Contains(phoneNumber));
            }

            int totalCount = await userQuery.CountAsync();

            List<User> paginatedServices = await userQuery
                .OrderByDescending(p => p.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<ListUserModelView> userModelView = _mapper.Map<List<ListUserModelView>>(paginatedServices);

            return new BasePaginatedList<ListUserModelView>(userModelView, totalCount, pageNumber, pageSize);
        }



        public async Task<Object> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(Guid.Parse(id));
                if (user == null || user.DeletedTime.HasValue)
                {
                    return "can not find or user is deleted";
                }

                return _mapper.Map<ListUserModelView>(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
        public async Task<string> DeleteUserAccountAsync(string id)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;
                if (!Guid.TryParse(id, out Guid userGuid))
                {
                    return "Invalid User ID format";
                }

                var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(userGuid);
                if (user == null)
                {
                    return "can not find UserAccount";
                }

                user.DeletedTime = DateTimeOffset.UtcNow;
                user.DeletedBy = userId;

                await _unitOfWork.GetRepository<User>().UpdateAsync(user);
                await _unitOfWork.SaveAsync();

                return "UserAccount delete successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> UpdateUserAccountAsync(string id, ListUserModelView model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

                var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(id);
                if (user == null || user.DeletedTime.HasValue)
                {
                    return "can not find or UserAccount is deleted";
                }

                _mapper.Map(model, user);
                user.LastUpdatedBy = userId;
                user.LastUpdatedTime = DateTimeOffset.UtcNow;

                await _unitOfWork.GetRepository<User>().UpdateAsync(user);
                await _unitOfWork.SaveAsync();

                return "UserAccount updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
