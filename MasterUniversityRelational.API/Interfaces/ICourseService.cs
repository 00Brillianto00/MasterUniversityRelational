using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CoursesData>> GetAllAsync();
        Task<CoursesData> GetByIdAsync(Guid id);
        Task<CoursesData> Save(CoursesData rootcausecategory);
        Task<CoursesData> Update(CoursesData rootcausecategory);
        Task<bool> Delete(Guid id);
    }
}
