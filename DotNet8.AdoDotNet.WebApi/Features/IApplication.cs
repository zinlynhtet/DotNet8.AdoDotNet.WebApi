namespace DotNet8.AdoDotNet.WebApi.Features;

public interface IApplication
{
    Task<List<BlogDataModel>> GetBlogs();
    Task<BlogDataModel> GetBlogById(int id);
    Task<int> CreateBlog(BlogDataModel reqModel);
    Task<int> PutBlog(int id, BlogDataModel reqModel);
    Task<int> PatchBlog(int id, BlogDataModel reqModel);
    Task<int> DeleteBlog(int id);
}