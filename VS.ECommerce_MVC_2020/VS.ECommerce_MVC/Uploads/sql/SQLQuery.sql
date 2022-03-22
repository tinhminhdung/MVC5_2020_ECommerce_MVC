

--exec [List_NewsBy_Category] '1','1','11'
create PROCEDURE [dbo].[List_NewsBy_Category]
@categoryId int,
@SkipCount int,
@MaxResultCount	int
AS

select * from 
(
SELECT  *,ROW_NUMBER() OVER ( ORDER BY id DESC) AS rn FROM  (
 SELECT 
 inid as id
 ,Title as Tieude
 ,Brief as Mota
 ,a.Create_Date as NgayTao
 ,'https://minhlongfinance.vn'+ CAST( a.Images as nvarchar(MAX))  as ImageUrl
 ,b.ID as CategoryId
 , b.Name as CategoryName
 FROM [dbo].[News]  as a
  left join menu  as b on a.icid=b.ID
 )
 AS A 
) as t
WHERE t.rn BETWEEN @SkipCount AND @MaxResultCount
and t.CategoryId=@categoryId
order by t.NgayTao desc

