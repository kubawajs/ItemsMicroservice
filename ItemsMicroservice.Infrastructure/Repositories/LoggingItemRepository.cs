using ItemsMicroservice.Core.Domain;
using Microsoft.Extensions.Logging;

namespace ItemsMicroservice.Infrastructure.Repositories;

internal sealed class LoggingItemRepository : IItemRepository
{
    private readonly IItemRepository _decoratedRepository;
    private readonly ILogger<LoggingItemRepository> _logger;

    public LoggingItemRepository(ILogger<LoggingItemRepository> logger, IItemRepository decoratedRepository)
    {
        _logger = logger;
        _decoratedRepository = decoratedRepository;
    }

    public async Task CreateAsync(Item item, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Creating item with code {item.Code} in database.");
        await _decoratedRepository.CreateAsync(item, cancellationToken);
        _logger.LogInformation($"Item {item.Code} successfully created.");
    }

    public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving items from database...");
        var items = await _decoratedRepository.GetAllAsync(cancellationToken);
        _logger.LogInformation($"Successfully retrieved {items.Count()} from database.");

        return items;
    }

    public async Task<Item?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Retrieving item with code {code} from database...");
        var item = await _decoratedRepository.GetByCodeAsync(code, cancellationToken);
        _logger.LogInformation($"Successfully retrieved item {code} from database.");

        return item;
    }

    public async Task<IEnumerable<Item>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Retrieving items from database... Page: {page} Page size: {pageSize}");
        var items = await _decoratedRepository.GetPaginatedAsync(page, pageSize, cancellationToken);
        _logger.LogInformation($"Successfully retrieved {items.Count()} from database. Page: {page} Page size: {pageSize}");

        return items;
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving total number of items in database...");
        var totalCount = await _decoratedRepository.GetTotalCountAsync(cancellationToken);
        _logger.LogInformation($"Total count: {totalCount}");

        return totalCount;
    }

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating item with code {item.Code} in database...");
        await _decoratedRepository.UpdateAsync(item, cancellationToken);
        _logger.LogInformation($"Item {item.Code} successfully updated.");
    }
}
