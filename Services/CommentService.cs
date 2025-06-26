using AutoMapper;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Comments;

namespace CoScheduleOA.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CommentService(
            ICommentRepository commentRepository,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<CommentDto> CreateAsync(CommentCreateModel model)
        {
            var userId = _currentUser.Id ?? throw new UnauthorizedAccessException("No user context");

            var entity = new Comment
            {
                UserId = userId,
                Content = model.Content,
                ItemId = model.ItemId,
                CreatedUtc = DateTime.UtcNow
            };

            var added = await _commentRepository.AddAsync(entity);
            return _mapper.Map<CommentDto>(added);
        }

        public async Task<IEnumerable<CommentDto>> GetByItemAsync(int itemId)
        {
            var list = await _commentRepository.GetByItemAsync(itemId);
            return list.Select(c => _mapper.Map<CommentDto>(c));
        }

        public async Task<CommentDto?> FindAsync(int id)
        {
            var entity = await _commentRepository.FindByIdAsync(id);
            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<CommentDto>(entity);
        }

        private async Task<Comment?> GetOwnedCommentAsync(int id)
        {
            var userId = _currentUser.Id ?? throw new UnauthorizedAccessException("No user context");
            var comment = await _commentRepository.FindByIdAsync(id);

            if (comment == null)
            {
                return null;
            }

            if (comment.UserId != userId)
            {
                throw new UnauthorizedAccessException("Comment does not belong to current user.");
            }

            return comment;
        }

        public async Task<bool> UpdateAsync(CommentUpdateModel model)
        {
            var entity = await GetOwnedCommentAsync(model.Id);
            if (entity is null)
            {
                return false;
            }

            if (entity.Content == model.Content)
            {
                return true;
            }

            entity.Content = model.Content;
            entity.UpdatedUtc = DateTime.UtcNow;

            await _commentRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetOwnedCommentAsync(id);
            if (entity is null)
            {
                return false;
            }

            await _commentRepository.DeleteAsync(id);
            return true;
        }
    }
}
