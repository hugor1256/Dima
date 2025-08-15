using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class UpdateCategoriesRequest : Request
{
    [Required(ErrorMessage = "Titulo Inválido")]
    [MaxLength(80, ErrorMessage = "O titulo deve conter no maximo 80 caracteres")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Descrição Inválida")]
    public string Description { get; set; } = string.Empty;
}