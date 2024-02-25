using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Products.Application.Interfaces;
using Products.Domain;
using Products.Domain.Dtos;
using Products.Infrastructure.Repository.Interfaces;

namespace Products.Application.Tests;

public class ProductServiceTests
{
    private readonly IProductService _sut;
    private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
    private readonly IMapper _mapperMock = Substitute.For<IMapper>();
    private readonly ILogger<ProductService> _logger = Substitute.For<ILogger<ProductService>>();

    public ProductServiceTests()
    {
        _sut = new ProductService(_productRepository, _mapperMock, _logger);
    }


    [Fact]
    public async void ListAll_ShouldNotReturnEmpty_When_ThereIsAProduct_test()
    {
        //Arrange
        Product product = new()
        {
            ProductId = 98800,
            ProductName = "Milan 2009",
            ProductType = "Jersey",
            Price = 230.98,
            Quantity = 144
        };
        
        List<Product> productList = new()
        {
            product
        };

        _productRepository.GetAllAsync().Returns(productList);

        //Act
        var result = await _sut.ListAll();

        //Assert
        result.Should().NotBeEmpty();
    }
}


//         public Task<IList<ProductDto>> ListAll();
//         public Task<ProductDto> ListById(int id);
//        public Task UpdateQuantityProduct(int id, int qtd);