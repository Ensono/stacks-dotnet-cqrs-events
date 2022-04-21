using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Data.Documents;
using Amido.Stacks.DynamoDB.Abstractions;
using FluentAssertions;
using NSubstitute;
using Xunit;
using xxAMIDOxx.xxSTACKSxx.Application.Integration;
using xxAMIDOxx.xxSTACKSxx.Domain;
using xxAMIDOxx.xxSTACKSxx.Domain.Entities;
using xxAMIDOxx.xxSTACKSxx.Infrastructure.Repositories;

namespace xxAMIDOxx.xxSTACKSxx.Infrastructure.UnitTests;

public class DynamoDbMenuRepositoryTests
{
    private readonly DynamoDbMenuRepository repository;
    private readonly IDynamoDbObjectStorage<Menu> fakeMenuRepository;
    private Menu menu;

    public DynamoDbMenuRepositoryTests()
    {
        fakeMenuRepository = SetupFakeMenuRepository();
        repository = new DynamoDbMenuRepository(fakeMenuRepository);
    }

    [Fact]
    public void DynamoDbMenuRepository_Should_ImplementIMenuRepository()
    {
        // Arrange
        // Act
        // Assert
        typeof(DynamoDbMenuRepository).Should().Implement<IMenuRepository>();
    }

    [Fact]
    public async Task GetByIdAsync()
    {
        // Arrange
        // Act
        var result = await repository.GetByIdAsync(Guid.Empty);

        // Assert
        await fakeMenuRepository.Received().GetByIdAsync(Arg.Any<string>());

        result.Should().BeOfType<Menu>();
        result.Should().Be(menu);
    }

    [Fact]
    public async Task SaveAsync()
    {
        // Arrange
        // Act
        var result = await repository.SaveAsync(menu);

        // Assert
        await fakeMenuRepository.Received().SaveAsync(menu);

        result.Should().Be(true);
    }

    [Fact]
    public async Task DeleteAsync()
    {
        // Arrange
        // Act
        var result = await repository.DeleteAsync(Guid.Empty);

        // Assert
        await fakeMenuRepository.Received().DeleteAsync(Arg.Any<string>());

        result.Should().Be(true);
    }

    private IDynamoDbObjectStorage<Menu> SetupFakeMenuRepository()
    {
        var menuRepository = Substitute.For<IDynamoDbObjectStorage<Menu>>();
        menu = new Menu(Guid.Empty, "testName", Guid.Empty, "testDescription", true, new List<Category>());
        var fakeTypeResponse = new OperationResult<Menu>(true, menu, new Dictionary<string, string>());
        var fakeNonTypeResponse = new OperationResult(true, new Dictionary<string, string>());

        menuRepository.GetByIdAsync(Arg.Any<string>())
            .Returns(Task.FromResult(fakeTypeResponse));
        menuRepository.SaveAsync(menu)
            .Returns(Task.FromResult(fakeTypeResponse));
        menuRepository.DeleteAsync(Arg.Any<string>())
            .Returns(Task.FromResult(fakeNonTypeResponse));

        return menuRepository;
    }
}
