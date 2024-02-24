using Products.Domain.Dtos;

namespace Products.Application.Interfaces
{
    public interface IProductService
    {
        Task<IList<ProductDto>> ListAll();
        Task<ProductDto> ListById(int id);
        Task UpdateQuantityProduct(int id, int qtd);
    }
}