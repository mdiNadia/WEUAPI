using Application.Dtos.Comment;
using Application.Dtos.Common;
using Application.Errors;
using Application.ExtensionMethods;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Comment.Queries
{
    public class GetAllCommentsByAdId : IRequest<IEnumerable<GetCommentDto>>
    {
        /// <summary>
        /// ConfirmedResult Id
        /// </summary>
        public int Id { get; set; }
        public class GetAllCommentsByAdIdHandler : IRequestHandler<GetAllCommentsByAdId, IEnumerable<GetCommentDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCommentsByAdIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetCommentDto>> Handle(GetAllCommentsByAdId query, CancellationToken cancellationToken)
            {
                var getAdId = await _unitOfWork.Comments.GetQueryList()
                    .Where(c => c.ConfirmResultId == query.Id).AsNoTracking().FirstOrDefaultAsync();
                if (getAdId == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                var comments = await _unitOfWork.Comments.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Parent).ThenInclude(c => c.Children)
                    .Include(c => c.ConfirmResult)
                    .Include(c => c.Author).ThenInclude(c => c.Avatar)
                    .Where(c => c.ConfirmResult.Id == getAdId.ConfirmResultId)
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
                    }).ToListAsync();
                if (comments == null) throw new RestException(HttpStatusCode.BadRequest, "پیام وجود ندارد!");

                return comments;


            }
        }
    }
}
