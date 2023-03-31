using Mapster;
using MapsterMapper;
using System.Net;
using System.Runtime.CompilerServices;
using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
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

    }
}
