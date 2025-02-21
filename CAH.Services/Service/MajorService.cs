using AutoMapper;
using CAH.Contract.Repositories.Entity;
using CAH.Contract.Repositories.Interface;
using CAH.Contract.Services.Interface;
using CAH.Core;
using CAH.Core.Base;
using CAH.ModelViews.MajorModelViews;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;

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
				throw new Exception($"Error: {ex.Message}");
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
				throw new Exception($"Error: {ex.Message}");
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

				_mapper.Map(model, major);
				major.LastUpdatedBy = model.UserId;
				major.LastUpdatedTime = DateTimeOffset.UtcNow;

				await _unitOfWork.GetRepository<Major>().UpdateAsync(major);
				await _unitOfWork.SaveAsync();

				return "Major updated successfully";
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}

		public async Task<BasePaginatedList<MajorModelView>> GetAllMajorAsync(int pageNumber, int pageSize)
		{
			IQueryable<Major> majorQuery = _unitOfWork.GetRepository<Major>().Entities
				.Where(p => !p.DeletedTime.HasValue)
				.OrderByDescending(s => s.CreatedTime);

			int totalCount = await majorQuery.CountAsync();

			List<Major> paginatedServices = await majorQuery
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			List<MajorModelView> majorModelView = _mapper.Map<List<MajorModelView>>(paginatedServices);

			return new BasePaginatedList<MajorModelView>(majorModelView, totalCount, pageNumber, pageSize);
		}


		public async Task<MajorModelView> GetMajorByIdAsync(string id)
		{
			try
			{
				var major = await _unitOfWork.GetRepository<Major>().GetByIdAsync(id);

				return _mapper.Map<MajorModelView>(major);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}");
			}
		}
	}
}
