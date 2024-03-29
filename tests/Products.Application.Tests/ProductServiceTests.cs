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

    [Fact]
    public async void ListAll_ShouldReturnEmpty_When_ThereIsNotAProduct_test()
    {
        //Arrange
        Product product = new();
        
        List<Product> productList = new()
        {
            product
        };

        //Act
        var result = await _sut.ListAll();

        //Assert
        result.Should().BeEmpty();
    } 

    [Fact]
    public async void ListById_ShouldReturnsAProduct_When_ThereIsAProductIdValid_test()
    {
        //Arrange
        var productId = 1;
        var productName = "Milan 2009";
        var productType = "Jersey";
        var price = 230.98;
        var quantity = 144;
        var product = new Product{ProductId = productId, ProductName = productName, ProductType = productType, Price = price, Quantity = quantity};

        var productDto = new ProductDto{ProductId = productId, ProductName = productName, ProductType = productType, Price = price, Quantity = quantity};

        _productRepository.GetByIdAsync(productId).Returns(product);

        //Act
        var result = await _sut.ListById(productId);

        //Assert
        result.Should().Be(productDto);
    }

    [Fact]
    public async void ListById_ShouldNotReturnsAProduct_When_ThereIsNotAProductIdValid_test()
    {
        //Arrange
        int productId = 0;
        var product = new Product();

        _productRepository.GetByIdAsync(productId).Returns(product);

        //Act
        var result = await _sut.ListById(productId);

        //Assert
        result.Should().NotBe(productId);
    }

}