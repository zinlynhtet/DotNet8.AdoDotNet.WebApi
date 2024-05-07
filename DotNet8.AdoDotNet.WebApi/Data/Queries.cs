namespace DotNet8.AdoDotNet.WebApi;

public static class Queries
{
    public static string GetQuery { get; } = @"Select * from Tbl_Blogs";
    public static string GetByIdQuery { get; } = @"Select * from Tbl_Blogs where BlogId = @BlogId";
    public static string CreateQuery { get; } = @"INSERT INTO [dbo].[Tbl_Blogs]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
		   @BlogAuthor,
		   @BlogContent)";

    public static string UpdateQuery { get; } = @"UPDATE [dbo].[Tbl_Blogs]
     SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
     WHERE BlogId = @BlogId";
    public static string DeleteQuery { get; } = @"DELETE FROM [dbo].[Tbl_Blogs]
    WHERE BlogId = @BlogId";
    
    public static string PatchQuery(string conditions)
    {
        string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";
        return query;
    }
    public static string Conditions(int id, BlogDataModel reqModel, List<AdoDotNetParameter> parameters)
    {
        var conditions = string.Empty;
        if (!string.IsNullOrEmpty(reqModel.BlogTitle))
        {
            conditions += " [BlogTitle] = @BlogTitle, ";
            parameters.Add(new AdoDotNetParameter("@BlogTitle", reqModel.BlogTitle));
        }

        if (!string.IsNullOrEmpty(reqModel.BlogAuthor))
        {
            conditions += " [BlogAuthor] = @BlogAuthor, ";
            parameters.Add(new AdoDotNetParameter("@BlogAuthor", reqModel.BlogAuthor));
        }

        if (!string.IsNullOrEmpty(reqModel.BlogContent))
        {
            conditions += " [BlogContent] = @BlogContent, ";
            parameters.Add(new AdoDotNetParameter("@BlogContent", reqModel.BlogContent));
        }

        if (conditions.Length == 0)
        {
            throw new Exception("Invalid conditions");
        }

        parameters.Add(new AdoDotNetParameter("@BlogId", id));
        conditions = conditions.Substring(0, conditions.Length - 2);
        return conditions;
    }

}