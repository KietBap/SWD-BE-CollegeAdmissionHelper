using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core.Base;
using CAH.ModelViews.MajorModelViews;

namespace CAH.Services.Service
{
	public class MajorService : IMajorService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

        public MajorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

		public async Task<string> CreateMajorAsync(CreateMajorModelView model)
		{
			try
			{
				Major newMajor = _mapper.Map<Major>(model);

				newMajor.CreatedTime = DateTimeOffset.UtcNow;
				newMajor.CreatedBy = model.UserId;

				await _unitOfWork.GetRepository<Major>().InsertAsync(newMajor);
				await _unitOfWork.SaveAsync();

				return "Major added successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				return ex.Message;
			}
			catch (Exception ex)
			{
				return "An error occurred while adding the major.";
			}
		}

		public async Task<string> DeleteMajorAsync(string id, string userId)
		{
			try
			{
				var major = await _unitOfWork.GetRepository<Major>().GetByIdAsync(id);
				if (major == null)
				{
					return "can not find major";
				}

				major.DeletedTime = DateTimeOffset.UtcNow;
				major.DeletedBy = userId;

				await _unitOfWork.GetRepository<Major>().UpdateAsync(major);
				await _unitOfWork.SaveAsync();

				return "Major delete successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				return ex.Message;
			}
			catch (Exception ex)
			{
				return "An error occurred while delete the major.";
			}
		}

		public async Task<string> UpdateMajorAsync(string id, CreateMajorModelView model)
		{
			try
			{
				var major = await _unitOfWork.GetRepository<Major>().GetByIdAsync(id);
				if (major == null)
				{
					return "can not find major";
				}

				major = _mapper.Map<Major>(model);
				major.LastUpdatedBy = model.UserId;
				major.LastUpdatedTime = DateTimeOffset.UtcNow;

				await _unitOfWork.GetRepository<Major>().UpdateAsync(major);
				await _unitOfWork.SaveAsync();

				return "Major updated successfully";
			}
			catch (BaseException.BadRequestException ex)
			{
				return ex.Message;
			}
			catch (Exception ex)
			{
				return "An error occurred while updating the major.";
			}
		}
	}
}
