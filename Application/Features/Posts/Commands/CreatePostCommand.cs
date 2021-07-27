using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreatePostCommand : IRequest<Response<int>>
    {
        public string Text { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? UserId { get; set; }
        public string ImageURL {get;set;}

        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Response<int>>
        {
            private readonly IPostRepositoryAsync _TimeSlotRepository;
            public CreatePostCommandHandler(IPostRepositoryAsync TimeSlotRepository)
            {
                _TimeSlotRepository = TimeSlotRepository;
            }
            public async Task<Response<int>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
            {
                var post = new Domain.Entities.Post();
         
                post.Text = command.Text;
                post.UserId = command.UserId;
                post.Latitude = command.Latitude;
                post.Longitude = command.Longitude;
                post.ImageURL = command.ImageURL;
                await _TimeSlotRepository.AddAsync(post);
                return new Response<int>(post.Id);

            }
        }
    }
}
