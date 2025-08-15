using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category>> CreateAsync(CreateCategoriesRequest request);
    Task<Response<Category>> UpdateAsync(UpdateCategoriesRequest request);
    Task<Response<Category>> DeleteAsync(DeleteCategoriesRequest request);
    Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<Response<List<Category>>> GetAllAsync(GetAllCaregoriesRequest request);
}