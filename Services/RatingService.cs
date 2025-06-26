using AutoMapper;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Ratings;

namespace CoScheduleOA.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public RatingService(
            IRatingRepository ratingRepository,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<RatingDto> CreateAsync(RatingCreateModel model)
        {
            var userId = _currentUser.Id ?? throw new UnauthorizedAccessException("No user context");

            var entity = new Rating
            {
                UserId = userId,
                Value = model.Value,
                ItemId = model.ItemId,
                CreatedUtc = DateTime.UtcNow
            };

            var added = await _ratingRepository.AddAsync(entity);
            return _mapper.Map<RatingDto>(added);
        }

        public async Task<IEnumerable<RatingDto>> GetByItemAsync(int itemId)
        {
            var list = await _ratingRepository.GetByItemAsync(itemId);
            return list.Select(r => _mapper.Map<RatingDto>(r));
        }

        public async Task<RatingDto?> FindAsync(int id)
        {
            var entity = await _ratingRepository.FindByIdAsync(id);
            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<RatingDto>(entity);
        }

        private async Task<Rating?> GetOwnedRatingAsync(int id)
        {
            var userId = _currentUser.Id ?? throw new UnauthorizedAccessException("No user context");
            var rating = await _ratingRepository.FindByIdAsync(id);

            if (rating == null)
            { 
                return null;
            }

            if (rating.UserId != userId)
            {
                throw new UnauthorizedAccessException("Rating does not belong to current user.");
            }

            return rating;
        }

        public async Task<bool> UpdateAsync(RatingUpdateModel model)
        {
            var entity = await GetOwnedRatingAsync(model.Id);
            if (entity is null)
            {
                return false;
            }

            if (entity.Value == model.Value)
            {
                return true;
            }

            entity.Value = model.Value;
            entity.UpdatedUtc = DateTime.UtcNow;
            await _ratingRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exists = await GetOwnedRatingAsync(id);
            if (exists is null)
            {
                return false;
            }

            await _ratingRepository.DeleteAsync(id);
            return true;
        }
    }
}
