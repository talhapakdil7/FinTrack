using FinTrack.API.DTOs;

namespace FinTrack.API.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync(int userId);

    Task<CategoryDto> CreateAsync(CreateCategoryDto dto, int userId);
    Task<CategoryDto?> UpdateAsync(int id, CreateCategoryDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}