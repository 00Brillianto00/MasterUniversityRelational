using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface ILecturerService
    {
        Task<IEnumerable<LecturerDetailData>> GetAllAsync();
        Task<LecturerDetailData>GetByIdAsync(Guid id);
        Task<LecturerDetailData>Save(LecturerDetailData LecturerData);
        Task<LecturerDetailData>Update(LecturerDetailData LecturerData);
        Task<bool> Delete(Guid id);
    }
}
