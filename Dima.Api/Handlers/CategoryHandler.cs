using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoriesRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };
        
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        
            return new Response<Category?>(category);
        }
        catch
        {
            return new Response<Category?>(null, 500, "Erro ao criar a categoria");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = context.Categories.FirstOrDefault(s => s.Id == request.Id && s.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");
        
            category.Title = request.Title;
            category.Description = request.Description;
            

            context.Update(category);
            await context.SaveChangesAsync();
            
            return new Response<Category?>(category, 201, "Categoria alterado com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Erro ao alterar a categoria");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = context.Categories.FirstOrDefault(s => s.Id == request.Id && s.UserId == request.UserId);
        
            if (category == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");
        
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            
            return new Response<Category?>(category, 200, "Categoria excluida com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Erro excluir a categoria");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.Id &&  s.UserId == request.UserId);

            return category == null
                    ? new Response<Category?>(null, 404, "Categoria não encotrada")
                    : new Response<Category?>(category);
        }
        catch
        {
            return new Response<Category?>(null, 500, "Erro ao buscar a categria");
        }
        
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCaregoriesRequest request)
    {
        try
        {
            var query = context.Categories
                .AsNoTracking()
                .Where(s => s.UserId == request.UserId)
                .OrderBy(s => s.Title);
        
            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();
        
            return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Category>>(null, 500, "Erro ao buscar as categorias");
        }
    }
}