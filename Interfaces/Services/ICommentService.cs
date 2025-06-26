using CoScheduleOA.Models.Comments;

namespace CoScheduleOA.Interfaces.Services
{
    public interface ICommentService
    {
        /// <summary>
        /// Creates a new comment based on the given input model and returns the created comment DTO.
        /// </summary>
        Task<CommentDto> CreateAsync(CommentCreateModel model);

        /// <summary>
        /// Retrieves all comments associated with the specified item ID.
        /// </summary>
        Task<IEnumerable<CommentDto>> GetByItemAsync(int itemId);

        /// <summary>
        /// Finds and returns a comment DTO by its ID, or null if not found.
        /// </summary>
        Task<CommentDto?> FindAsync(int id);

        /// <summary>
        /// Updates the content of a comment with the given ID. Returns true if the update was successful.
        /// </summary>
        Task<bool> UpdateAsync(CommentUpdateModel model);

        /// <summary>
        /// Deletes the comment with the specified ID. Returns true if the deletion was successful.
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}