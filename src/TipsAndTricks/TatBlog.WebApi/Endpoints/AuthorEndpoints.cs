﻿using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Filters;
using TatBlog.WebApi.Models;
using TatBlog.WebApi.Models.Author;
using TatBlog.WebApi.Models.Post;

namespace TatBlog.WebApi.Endpoints
{
    public static class AuthorEndpoints
    {
        public static WebApplication MapAuthorEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/authors");

            routeGroupBuilder.MapGet("/", GetAuthors)
              .WithName("GetAuthors")
              .Produces<ApiResponse<PaginationResult<AuthorItem>>>();


            routeGroupBuilder.MapGet("/{id:int}", GetAuthorDetails)
              .WithName("GetAuthorByID")
              .Produces<ApiResponse<AuthorItem>>();
              


            routeGroupBuilder.MapGet("/{id:int}/posts", GetPostsByAuthorId)
              .WithName("GetPostsByAuthorId")
              .Produces<PaginationResult<PostDto>>();


            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9_-]+$)}/posts", GetPostsByAuthorSlug)
              .WithName("GetPostsByAuthorSlug")
              .Produces<ApiResponse<PaginationResult<PostDto>>>();


            routeGroupBuilder.MapPost("/", AddAuthor)
              .WithName("AddAuthor")
              .AddEndpointFilter<ValidatorFilter<AuthorEditModel>>()
              .Produces(401)
              .Produces<ApiResponse<AuthorItem>>();
              

            routeGroupBuilder.MapPost("/{id:int}/avatar", SetAuthorPicture)
              .WithName("SetAuthorPicture")
              .RequireAuthorization()
              .Accepts<IFormFile>("multipart/form-data")
              .Produces(401)
              .Produces<ApiResponse<string>>();


            routeGroupBuilder.MapPut("/{id:int}", UpdateAuthor)
              .WithName("UpdateAnAuthor")
              .RequireAuthorization()
              .Produces(401)
              .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeleteAuthor)
              .WithName("DeleteAnAuthor")
              .RequireAuthorization()
              .Produces(401)
              .Produces<ApiResponse<string>>();

            return app;
        }

        private static async Task<IResult> GetAuthors(
          [AsParameters] AuthorFilterModel model,
          IAuthorRepository authorRepository,
          ILogger<IResult> logger)
        {
            var authors = await authorRepository.GetPagedAuthorsAsync(model, model.Name);
            var paginationResult = new PaginationResult<AuthorItem>(authors);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetAuthorDetails(
          int id,
          IAuthorRepository authorRepository,
          IMapper mapper,
          ILogger<IResult> logger)
        {
            var author = await authorRepository.GetCachedAuthorByIdAsync(id);

            return author == null
              ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"khong tim thay tac gia nao co ma so {id}"))
              : Results.Ok(ApiResponse.Success(mapper.Map<AuthorItem>(author)));
        }

        private static async Task<IResult> GetPostsByAuthorId(
          int id,
          [AsParameters] PagingModel pagingModel,
          IBlogRepository blogRepository,
          ILogger<IResult> logger)
        {
            var postQuery = new PostQuery()
            {
                AuthorId = id,
                PublishedOnly = true,
            };
            var posts = await blogRepository.GetPagesPostsAsync(
              postQuery, pagingModel,
              posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(posts);

            return Results.Ok(ApiResponse.Success(paginationResult));   
        }

        private static async Task<IResult> GetPostsByAuthorSlug(
            [FromRoute] string slug,
            [AsParameters] PagingModel pagingModel,
            IBlogRepository blogRepository,
            ILogger<IResult> logger)
        {
            var postQuery = new PostQuery()
            {
                AuthorSlug = slug,
                PublishedOnly = true,
            };
            var posts = await blogRepository.GetPagesPostsAsync(
              postQuery, pagingModel,
              posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(posts);
            
            return Results.Ok(paginationResult);
        }

        private static async Task<IResult> AddAuthor(
            AuthorEditModel model,
            IAuthorRepository authorRepository,
            IMapper mapper,
            ILogger<IResult> logger)
        {
            if (await authorRepository.IsAuthorSlugExistedAsync(0, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            var author = mapper.Map<Author>(model);
            await authorRepository.AddOrUpdateAsync(author);

            return Results.Ok(ApiResponse.Success(mapper.Map<AuthorItem>(author), HttpStatusCode.Created)); 
        }

        private static async Task<IResult> SetAuthorPicture(
           int id,
           IFormFile imagefile,
           IAuthorRepository authorRepository,
           IMediaManager mediaManager,
           ILogger<IResult> logger)
        {
            var imageUrl = await mediaManager.SaveFileAsync(
              imagefile.OpenReadStream(),
              imagefile.FileName, imagefile.ContentType);

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Không lưu được tập tin"));
            }
            await authorRepository.SetImageUrlAsync(id, imageUrl);
            return Results.Ok(ApiResponse.Success(imageUrl));
        }

        private static async Task<IResult> UpdateAuthor(
          int id, AuthorEditModel model,
          IValidator<AuthorEditModel> validator,
          IAuthorRepository authorRepository,
          IMapper mapper)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, validationResult));
            }

            if (await authorRepository.IsAuthorSlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            var author = mapper.Map<Author>(model);
            author.Id = id;

            return await authorRepository.AddOrUpdateAsync(author)
                ? Results.Ok(ApiResponse.Success("Author is updated", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Could not find author"));
        }

        private static async Task<IResult> DeleteAuthor(
          int id,
          IAuthorRepository authorRepository)
        {
            return await authorRepository.DeleteAuthorAsync(id)
              ? Results.Ok(ApiResponse.Success("Author is deleted", HttpStatusCode.NoContent))
              : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Could not found author with ID = {id}"));
        }
    }
}