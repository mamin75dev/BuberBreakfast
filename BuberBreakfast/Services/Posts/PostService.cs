﻿using BuberBreakfast.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _appDbContext;

        public PostService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ErrorOr<Created>> CreatePost(Post post)
        {
            _appDbContext.Posts.Add(post);
            await _appDbContext.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeletePost(Guid id)
        {
            var entity = new Post { Id = id };

            _appDbContext.Attach(entity);
            _appDbContext.Remove(entity);

            await _appDbContext.SaveChangesAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<List<Post>>> GetAllPosts()
        {
            List<Post> List = await _appDbContext.Posts.Join(_appDbContext.AppUsers, post => post.UserId, appUser => appUser.Id, (post, user) => new
            {
                id = post.Id,
                title = post.Title,
                description = post.Description,
                imageUrl = post.ImageUrl,
                createdDate = post.CreatedDate,
                updatedDate = post.UpdatedDate,
                user = user.Id
            }).Select(p => new Post
            {
                Id = p.id,
                Title = p.title,
                Description = p.description,
                ImageUrl = p.imageUrl,
                CreatedDate = p.createdDate,
                UpdatedDate = p.updatedDate,
                UserId = p.user
            }).ToListAsync();

            if (List.Count == 0)
            {
                return Errors.Post.NotFound;
            }

            return List;
        }

        public async Task<ErrorOr<List<Post>>> GetUserPosts(Guid userId)
        {
            List<Post> List = await _appDbContext.Posts.Join(_appDbContext.AppUsers, post => post.UserId, appUser => appUser.Id, (post, user) => new
            {
                id = post.Id,
                title = post.Title,
                description = post.Description,
                imageUrl = post.ImageUrl,
                createdDate = post.CreatedDate,
                updatedDate = post.UpdatedDate,
                user = user.Id
            }).Where(p => p.user == userId).Select(p => new Post
            {
                Id = p.id,
                Title = p.title,
                Description = p.description,
                ImageUrl = p.imageUrl,
                CreatedDate = p.createdDate,
                UpdatedDate = p.updatedDate,
                UserId = p.user
            }).ToListAsync();

            if (List.Count == 0)
            {
                return Errors.Post.NotFound;
            }

            return List;
        }

        public async Task<ErrorOr<UpdatedPostResult>> UpdatePost(Post post)
        {
            var entity = await _appDbContext.Posts.FirstOrDefaultAsync(post => post.Id == post.Id);

            bool IsNewlyCreated = false;
            if (entity == null)
            {
                IsNewlyCreated = true;
                _appDbContext.Posts.Add(post);
            }
            else
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.ImageUrl = post.ImageUrl;
                entity.CreatedDate = post.CreatedDate;
                entity.UpdatedDate = DateTime.UtcNow;
            }

            await _appDbContext.SaveChangesAsync();

            return new UpdatedPostResult(IsNewlyCreated);
        }
    }
}
