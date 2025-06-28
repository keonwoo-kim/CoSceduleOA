using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Items;

namespace CoScheduleOA.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<int> SaveIfNotExistsAndReturnIdAsync(ItemCreateModel model)
        {
            var existing = await _itemRepository.FindAsync(model.Source, model.ExternalId);
            if (existing != null)
            {
                return existing.Id;
            }
            var entity = new Item
            {
                Source = model.Source,
                ExternalId = model.ExternalId,
                Title = model.Title,
                Url = model.Url,
                CreatedUtc = DateTime.UtcNow
            };
            await _itemRepository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<Item?> FindAsync(string source, string externalId)
        {
            return await _itemRepository.FindAsync(source, externalId);
        }
    }
}
