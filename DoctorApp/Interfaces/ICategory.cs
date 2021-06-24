using DoctorApp.DTO_s;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface ICategory
    {
        Task<IList<CategoryDTO>> GetCategories();

        Task<CategoryDTO> GetCategory(int id);

        void NewCategory(CategoryDTO category);

        void UpdateCategory(CategoryDTO category);

        void DeleteCategory(int id);
    }
}