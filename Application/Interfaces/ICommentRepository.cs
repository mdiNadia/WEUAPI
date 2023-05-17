using Domain.Entities;


namespace Application.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<bool> CheckIfHasChildren(int commentId);

    }
}
