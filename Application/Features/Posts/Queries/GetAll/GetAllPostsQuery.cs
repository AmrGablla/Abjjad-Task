using Application.Filters;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetAllPostsQuery : IRequest<PagedResponse<IEnumerable<GetAllPostsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PagedResponse<IEnumerable<GetAllPostsViewModel>>>
    {
        private readonly IPostRepositoryAsync _PostService;
        private readonly IMapper _mapper;
        public GetAllPostsQueryHandler(IPostRepositoryAsync PostService, IMapper mapper)
        {
            _PostService = PostService;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPostsViewModel>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var result = await _PostService.GetAllAsync();
            var output = _mapper.Map<IEnumerable<GetAllPostsViewModel>>(result);
            return new PagedResponse<IEnumerable<GetAllPostsViewModel>>(output, 1, 10);
        }
    }
}
