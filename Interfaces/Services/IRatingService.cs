using CoScheduleOA.Models.Ratings;

namespace CoScheduleOA.Interfaces.Services
{
    public interface IRatingService
    {
        /// <summary>
        /// Creates a new rating using the provided input model and returns the resulting rating DTO.
        /// </summary>
        Task<RatingDto> CreateAsync(RatingCreateModel model);

        /// <summary>
        /// Retrieves all ratings associated with the specified item ID.
        /// </summary>
        Task<IEnumerable<RatingDto>> GetByItemAsync(int itemId);

        /// <summary>
        /// Finds and returns a rating by its ID.
        /// </summary>
        Task<RatingDto?> FindAsync(int id);

        /// <summary>
        /// Updates the value of the rating with the specified ID. Returns true if the update was successful.
        /// </summary>
        Task<bool> UpdateAsync(RatingUpdateModel model);

        /// <summary>
        /// Deletes the rating with the given ID. Returns true if the deletion was successful.
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}