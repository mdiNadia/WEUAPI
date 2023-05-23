using Application.Dtos.Common;
using Application.Errors;
using Application.ExtensionMethods;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Comment.Queries
{
    public class GetAllComments : IRequest<IEnumerable<GetCommentDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllComments(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllCommentsHandler : IRequestHandler<GetAllComments, IEnumerable<GetCommentDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCommentsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetCommentDto>> Handle(GetAllComments query, CancellationToken cancellationToken)
            {

                var commentList = await _unitOfWork.Comments.GetQueryList()
               .Include(c => c.ConfirmResult)
              .Include(c => c.Author).ThenInclude(c => c.Avatar)
              .Include(c => c.Parent).ThenInclude(c => c.Children)
               .AsNoTracking()
               .OrderByDescending(c => c.Id)
              .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
              .Take(query._filter.PageSize)
                   .Select(c => new GetCommentDto()
                   {
                       Id = c.Id,
                       Username = c.Author.Username,
                       Photo = c.Author.Avatar != null ? c.Author.Avatar.FileName : null,
                       Message = c.Message,
                       IsActive = c.IsActive,
                       IsVisited = c.IsVisited,
                       Advertising = new GetNameAndId()
                       {
                           Id = c.ConfirmResultId,
                           Name = c.ConfirmResult.Name,
                           CreationDate = c.ConfirmResult.CreationDate,
                       },
                       ParentId = c.ParentId,
                       CreationDate = c.CreationDate.TimeAgo(),
                       Children = c.Children.Select(c1 => new GetCommentDto()
                       {
                           Id = c1.Id,
                           ParentId = c1.ParentId,
                           CreationDate = c1.CreationDate.TimeAgo(),
                           Photo = c1.Author.Avatar != null ? c1.Author.Avatar.FileName : null,
                           Username = c1.Author.Username,
                           Message = c1.Message,
                           IsActive = c1.IsActive,
                           IsVisited = c1.IsVisited,
                           Advertising = new GetNameAndId()
                           {
                               Id = c1.ConfirmResultId,
                               Name = c1.ConfirmResult.Name,
                               CreationDate = c1.ConfirmResult.CreationDate,
                           },
                           Children = c1.Children.Select(c2 => new GetCommentDto()
                           {
                               Id = c2.Id,
                               ParentId = c2.ParentId,
                               CreationDate = c2.CreationDate.TimeAgo(),
                               Photo = c2.Author.Avatar != null ? c2.Author.Avatar.FileName : null,
                               Username = c2.Author.Username,
                               Message = c2.Message,
                               IsActive = c2.IsActive,
                               IsVisited = c2.IsVisited,
                               Advertising = new GetNameAndId()
                               {
                                   Id = c2.ConfirmResultId,
                                   Name = c2.ConfirmResult.Name,
                                   CreationDate = c2.ConfirmResult.CreationDate,
                               },
                               Children = null
                           }).ToList()
                       }).ToList(),
                   })
                    .ToListAsync();
                if (commentList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "پیام وجود ندارد!");
                }
                return commentList;


            }
        }
    }
}
