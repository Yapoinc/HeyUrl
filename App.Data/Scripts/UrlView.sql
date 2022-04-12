CREATE OR ALTER VIEW dbo.UrlView AS 
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
									ISNULL(ug.Clicks,0) Clicks
								FROM Url u 
								LEFT JOIN urlGroup ug ON ug.UrlId=u.Id