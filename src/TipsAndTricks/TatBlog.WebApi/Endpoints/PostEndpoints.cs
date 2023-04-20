using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlugGenerator;
using System.Net;
using System.Runtime.CompilerServices;
using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApi.Models;
using TatBlog.WebApi.Models.Category;
using TatBlog.WebApi.Models.Post;

namespace TatBlog.WebApi.Endpoints
{
    public static class PostEndpoint
    {
        public static WebApplication MapPostEndpoint(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/posts");

            routeGroupBuilder.MapGet("/", GetPosts)
                .WithName("GetPosts")
                .Produces<ApiResponse<PaginationResult<PostDto>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetPostsDetailsById)
                .WithName("GetPostsDetailsById")
                .Produces<ApiResponse<PostDto>>();


            routeGroupBuilder.MapPost("/{id:int}/avatar", SetAvatarPost)
                .WithName("SetAvatarPost")
                .Accepts<IFormFile>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<string>>();



            routeGroupBuilder.MapDelete("/{id:int}", DeletePost)
                .WithName("DeletePost")
                .Produces(401)
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapGet("/get-posts-filter", GetFilteredPosts)
                .WithName("GetFilteredPost")
                .Produces<ApiResponse<PostDto>>();

            routeGroupBuilder.MapGet("/get-filter", GetFilter)
                .WithName("GetFilter")
                .Produces<ApiResponse<PostFilterModel>>();

            routeGroupBuilder.MapPost("/", AddPost)
                .WithName("AddNewPost")
                .Accepts<PostEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<PostItems>>();
            return app;
        }


        private static async Task<IResult> GetPosts([AsParameters] PostFilterModel model,
            IBlogRepository blogRepository, IMapper mapper)
        {
            var postQuery = mapper.Map<PostQuery>(model);


            var postList = await blogRepository.GetPagesPostsAsync(postQuery, model,
                posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(postList);

            return Results.Ok(ApiResponse.Success(paginationResult));

        }

        private static async Task<IResult> SetAvatarPost(
            int id, IFormFile imageFile, IBlogRepository blogRepository, IMediaManager mediaManager)
        {
            var imageUrl = await mediaManager.SaveFileAsync(
                imageFile.OpenReadStream(), imageFile.FileName, imageFile.ContentType);
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Lưu tập tin không thành công"));
            }

            await blogRepository.SetImageUrlAsync(id, imageUrl);

            return Results.Ok(ApiResponse.Success(imageFile));

        }

        private static async Task<IResult> GetPostsDetailsById(
       int id, IBlogRepository blogRepository, IMapper mapper
       )
        {
            var posts = await blogRepository.GetCachedPostById(id, true);

            var postQuery = mapper.Map<PostDetail>(posts);

            return postQuery == null
                ? Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound, $"Không tìm thấy bài đăng có mã số {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<PostDto>(postQuery)));

        }



        private static async Task<IResult> DeletePost(
            int id, IBlogRepository blogRepository)
        {
            return await blogRepository.DeletePostById(id)
                ? Results.Ok(ApiResponse.Success("Bài đang đã được xóa :3 ", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Không thể tìm được bài đăng"));
        }

        private static async Task<IResult> GetFilter(
            IAuthorRepository authorRepository,
            IBlogRepository blogRepository)
        {
            var model = new PostFilterModel()
            {
                AuthorList = (await authorRepository.GetAuthorsAsync())
            .Select(a => new SelectListItem()
            {
                Text = a.FullName,
                Value = a.Id.ToString()
            }),
                CategoryList = (await blogRepository.GetCategoriesAsync())
            .Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            };
            return Results.Ok(ApiResponse.Success(model));
        }

        private static async Task<IResult> GetFilteredPosts(
            [AsParameters] PostFilterModel model,
            IMapper mapper,
            IBlogRepository blogRepository)
        {
            var postQuery = mapper.Map<PostQuery>(model);
            var postsList = await blogRepository.GetPagesPostsAsync(postQuery, model, posts =>
            posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(postsList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> AddPost(
            HttpContext context,
            IBlogRepository blogRepository,
            IMapper mapper,
            IMediaManager mediaManager)
        {
            var model = await PostEditModel.BindAsync(context);
            var slug = model.Title.GenerateSlug();
            if (await blogRepository.IsPostSlugExistedAsync(model.Id, slug))
            {
                return Results.Ok(ApiResponse.Fail(
                   HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng cho bài viết khác"));
            }
            var post = model.Id > 0 ? await
    blogRepository.GetPostByIdAsync(model.Id) : null;
            if (post == null)
            {
                post = new Post()
                {
                    PostedDate = DateTime.Now
                };
            }
            post.Title = model.Title;
            post.AuthorId = model.AuthorId;
            post.CategoryId = model.CategoryId;
            post.ShortDescription = model.ShortDescription;
            post.Description = model.Description;
            post.Meta = model.Meta;
            post.Published = model.Published;
            post.ModifiedDate = DateTime.Now;
            post.UrlSlug = model.Title.GenerateSlug();
            if (model.ImageFile?.Length > 0)
            {
                string hostname =
                $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/",
                uploadedPath = await
                mediaManager.SaveFileAsync(model.ImageFile.OpenReadStream(),
                model.ImageFile.FileName,
                model.ImageFile.ContentType);
                if (!string.IsNullOrWhiteSpace(uploadedPath))
                {
                    post.ImageUrl = uploadedPath;
                }
            }
            await blogRepository.AddOrUpdatePostAsync(post, model.GetSelectedTags());

            return Results.Ok(ApiResponse.Success(
            mapper.Map<PostItems>(post), HttpStatusCode.Created));
        }
    }
}

