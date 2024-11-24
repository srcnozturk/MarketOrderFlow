using AutoFixture;
using FluentAssertions;
using MarketOrderFlow.Infrastructure;
using MarketOrderFlow.Infrastructure.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MarketOrderFlow.API;
using MarketOrderFlow.API.Features.Products.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using MarketOrderFlow.API.Endpoints;
using MarketOrderFlow.API.Features.LogisticCenter.Commands;

namespace MarketOrderFlow.Api.Tests;

public class Tests
{
    #region Setup

    Fixture f;
    ServiceProvider serviceProvider;
    IMediator? mediatr;
    ApplicationDbContext? db;
    Guid?  TestProductGlobalId, TestLogisticCenterGlobalId;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var services = new ServiceCollection();
        services.AddMapsters();
        services.AddAPIServices();
        var a=services.AddScoped<ApplicationDbContext>();

        serviceProvider = services.BuildServiceProvider();
        mediatr = serviceProvider.GetService<IMediator>();
        db = serviceProvider.GetService<ApplicationDbContext>();


        TestProductGlobalId = db?.Products.FirstOrDefault()?.GlobalId;
        TestLogisticCenterGlobalId = db?.LogisticsCenters.FirstOrDefault()?.GlobalId;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        serviceProvider?.Dispose();
        db?.Dispose();
        mediatr = null;
    }

    [SetUp]
    public void Setup() => f = new Fixture();

    [TearDown]
    public void TearDown()
    {
        var products = db?.Products.Where(c => c.Name.StartsWith("Name") && c.Name.Length == 40);
        if (products?.Count() == 0) return;

        db?.Products.RemoveRange(products);
        db?.SaveChanges();
    }

    #endregion

    [Test]
    public async Task AddNewProduct_ShouldBeCreated()
    {
        var logisticCmd=f.Build<CreateLogisticCenterCommand>()
            .With(p => p.GlobalId, TestLogisticCenterGlobalId)
            .Create();
        var productCmd = f.Build<CreateProductCommand>()
            .With(lc=> lc.CreateLogisticCenter,logisticCmd)
            .Create();

        var sut = await ProductEndpoints.AddProduct(productCmd, mediatr);

        sut.Result.Should().BeOfType<Created>();
    }
}