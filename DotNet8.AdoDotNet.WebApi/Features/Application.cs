namespace DotNet8.AdoDotNet.WebApi.Features;

public class Application : IApplication
{
    private readonly AdoDotNetService _adoDotNetService =
        new AdoDotNetService(SqlConnectionString.sqlConnectionStringBuilder.ConnectionString);

    public async Task<List<BlogDataModel>> GetBlogs()
    {
        string query = Queries.GetQuery;
        var list = await _adoDotNetService.Query<BlogDataModel>(query);
        return list;
    }

    public async Task<BlogDataModel> GetBlogById(int id)
    {
        string query = Queries.GetByIdQuery;
        var item = await _adoDotNetService.QueryFirstOrDefault<BlogDataModel>(query);
        return item;
    }

    public async Task<int> CreateBlog(BlogDataModel reqModel)
    {
        string query = Queries.CreateQuery;
        var item = await _adoDotNetService.Execute(query,
            new AdoDotNetParameter("@BlogTitle", reqModel.BlogTitle),
            new AdoDotNetParameter("@BlogAuthor", reqModel.BlogAuthor),
            new AdoDotNetParameter("@BlogContent", reqModel.BlogContent));
        return item;
    }

    public async Task<int> PutBlog(int id, BlogDataModel reqModel)
    {
        string getQuery = Queries.GetByIdQuery;
        var item = await _adoDotNetService.QueryFirstOrDefault<BlogDataModel>(getQuery);
        if (item is null)
        {
            throw new Exception("Invalid Id");
        }

        string query = Queries.CreateQuery;
        var result = await _adoDotNetService.Execute(query,
            new AdoDotNetParameter("@BlogId", id),
            new AdoDotNetParameter("@BlogTitle", reqModel.BlogTitle!),
            new AdoDotNetParameter("@BlogAuthor", reqModel.BlogAuthor!),
            new AdoDotNetParameter("@BlogContent", reqModel.BlogContent!)
        );
        return result;
    }

    public async Task<int> PatchBlog(int id, BlogDataModel reqModel)
    {
        string getQuery = Queries.GetByIdQuery;
        var item = await _adoDotNetService.QueryFirstOrDefault<BlogDataModel>(getQuery);
        if (item is null)
        {
            throw new Exception("Invalid Id");
        }

        var parameters = new List<AdoDotNetParameter>();
        var conditions = Queries.Conditions(id, reqModel, parameters);
        var query = Queries.PatchQuery(conditions);
        var result = await _adoDotNetService.Execute(query,
            parameters.ToArray()
        );
        return result;
    }

    public async Task<int> DeleteBlog(int id)
    {
        var query = Queries.DeleteQuery;
        var result = await _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));
        return result;
    }
}