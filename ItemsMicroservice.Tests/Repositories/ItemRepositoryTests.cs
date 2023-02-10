using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.Database;
using ItemsMicroservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ItemsMicroservice.Tests.Repositories;

[TestFixture]
public class ItemRepositoryTests
{
    private ItemRepository _itemRepository;
    private ItemsMicroserviceDbContext _context;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ItemsMicroserviceDbContext>()
            .UseInMemoryDatabase(databaseName: "ItemRepositoryTests")
            .Options;

        _context = new ItemsMicroserviceDbContext(options);
        _itemRepository = new ItemRepository(_context);
        SeedDatabase();
    }

    [Test]
    public async Task GetAllAsync_ReturnsAllItems()
    {
        // Arrange
        // Act
        var result = await _itemRepository.GetAllAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.IsTrue(result.Any());
    }

    [Test]
    public async Task GetPaginatedAsync_ReturnsCorrectPage()
    {
        // Arrange
        var page = 1;
        var pageSize = 2;

        // Act
        var result = await _itemRepository.GetPaginatedAsync(page, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(pageSize));
    }

    [Test]
    public async Task GetByCodeAsync_ReturnsCorrectItem()
    {
        // Arrange
        var code = "ITEM1";

        // Act
        var result = await _itemRepository.GetByCodeAsync(code);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Code, Is.EqualTo(code));
    }

    [Test]
    public async Task UpdateAsync_UpdatesItem()
    {
        // Arrange
        var item = new Item { Code = "ITEM2", Name = "Item X", Color = "green", Notes = "Test notes." };

        // Act
        _context.ChangeTracker.Clear();
        await _itemRepository.UpdateAsync(item);

        // Assert
        var updatedItem = await _itemRepository.GetByCodeAsync(item.Code);
        Assert.That(updatedItem, Is.Not.Null);
        Assert.That(updatedItem.Code, Is.EqualTo(item.Code));
        Assert.That(updatedItem.Name, Is.EqualTo(item.Name));
        Assert.That(updatedItem.Color, Is.EqualTo(item.Color));
        Assert.That(updatedItem.Notes, Is.EqualTo(item.Notes));
    }

    [Test]
    public async Task UpdateAsync_DoesNotUpdateItem_IfCodeNotExists()
    {
        // Arrange
        var item = new Item { Code = "RandomCode", Name = "Item X", Color = "green", Notes = "Test notes." };

        // Act
        _context.ChangeTracker.Clear();

        // Assert
        Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _itemRepository.UpdateAsync(item));
        var updatedItem = await _itemRepository.GetByCodeAsync(item.Code);
        Assert.That(updatedItem, Is.Null);
    }

    [Test]
    public async Task CreateAsync_CreatesItem()
    {
        // Arrange
        var item = new Item { Code = "ITEMY", Name = "Item Y", Color = "red", Notes = "Test notes."};

        // Act
        _context.ChangeTracker.Clear();
        await _itemRepository.CreateAsync(item);

        // Assert
        var createdItem = await _itemRepository.GetByCodeAsync(item.Code);
        Assert.That(createdItem, Is.Not.Null);
        Assert.That(createdItem.Code, Is.EqualTo(item.Code));
        Assert.That(createdItem.Name, Is.EqualTo(item.Name));
        Assert.That(createdItem.Color, Is.EqualTo(item.Color));
        Assert.That(createdItem.Notes, Is.EqualTo(item.Notes));
    }

    private void SeedDatabase()
    {
        var items = new List<Item>();
        var colors = new string[] { "red", "blue", "green" };
        var random = new Random();
        for(var index = 1; index <= 100; index++)
        {
            items.Add(new Item { Code = $"ITEM{index}", Name = $"Item {index}", Color = colors[random.Next(colors.Length)], Notes = "Test notes." });
        }

        _context.Items.AddRange(items);
        _context.SaveChangesAsync();
    }
}