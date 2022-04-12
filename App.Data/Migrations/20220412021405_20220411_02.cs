using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class _20220411_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string script = @"CREATE OR ALTER VIEW dbo.UrlView AS 
								WITH urlGroup AS (
									SELECT 
									m.UrlId,
									COUNT(*) Clicks
									FROM URLMETRIC m
									GROUP BY m.UrlId
									)
								SELECT 
									u.Id,
									u.ShortUrl, 
									u.OriginalUrl,
									u.DateCreated,
									ug.Clicks
								FROM Url u 
								INNER JOIN urlGroup ug ON ug.UrlId=u.Id";
			migrationBuilder.Sql(script);
		
	}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW dbo.UrlView");
		}
    }
}
