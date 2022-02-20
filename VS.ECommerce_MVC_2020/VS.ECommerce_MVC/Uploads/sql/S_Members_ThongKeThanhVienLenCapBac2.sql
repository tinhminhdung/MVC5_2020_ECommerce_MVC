-- [S_Members_ThongKeThanhVienLenCapBac2] '','',N'','','',''
-- [S_Members_ThongKeThanhVienLenCapBac2] '02/01/2022','02/12/2022',N'','','',''
alter PROCEDURE [dbo].[S_Members_ThongKeThanhVienLenCapBac2]
	@TuNgay	nvarchar(500) ,
	@DenNgay	nvarchar(500) ,
	@Key nvarchar(500) ,
	@Status nvarchar(500),
	@IDThanhVien nvarchar(500),
	@CapBac nvarchar(500)
AS

DECLARE @PhoTongGiamDoc NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='PhoTongGiamDoc')
DECLARE @TienPhoTongGiamDoc NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='TienPhoTongGiamDoc')

DECLARE @GiamDocKinhDoanh NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='GiamDocKinhDoanh')
DECLARE @TienGiamDocKinhDoanh NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='TienGiamDocKinhDoanh')

DECLARE @PhoGiamDocKD NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='PhoGiamDocKD')
DECLARE @TienPhoGiamDocKD NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='TienPhoGiamDocKD')

DECLARE @PhoTruongPhong NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='PhoTruongPhong')
DECLARE @TienPhoTruongPhong NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='TienPhoTruongPhong')

DECLARE @TruongNhomKD NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='TruongNhomKD')
DECLARE @TienTruongNhomKD NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='TienTruongNhomKD')

DECLARE @TienChuyenVienKinhDoanh NCHAR(50) = (SELECT Value FROM Setting WHERE Properties='TienChuyenVienKinhDoanh')

select * from (

select CASE
WHEN (SumSoNhanVien>=@PhoTongGiamDoc ) and ( SumTienHopDong>=@TienPhoTongGiamDoc)
THEN N'6'

WHEN (SumSoNhanVien>=@GiamDocKinhDoanh ) and ( SumTienHopDong>=@TienGiamDocKinhDoanh)
THEN N'5'

WHEN (SumSoNhanVien>=@PhoGiamDocKD ) and ( SumTienHopDong>=@TienPhoGiamDocKD)
THEN N'5'

WHEN (SumSoNhanVien>=@PhoTruongPhong ) and ( SumTienHopDong>=@TienPhoTruongPhong)
THEN N'4'
--
WHEN (SumSoNhanVien>=@TruongNhomKD ) and ( SumTienHopDong>=@TienTruongNhomKD)
THEN N'3'

WHEN  ( SumTienHopDong>=@TienChuyenVienKinhDoanh)
THEN N'2'

ELSE'1'
END AS CapTrangThai

, CASE
WHEN (SumSoNhanVien>=@PhoTongGiamDoc ) and ( SumTienHopDong>=@TienPhoTongGiamDoc)
THEN N'Phó Tổng Giám Đốc Kinh Doanh'

WHEN (SumSoNhanVien>=@GiamDocKinhDoanh ) and ( SumTienHopDong>=@TienGiamDocKinhDoanh)
THEN N'Giám Đốc Kinh Doanh'

WHEN (SumSoNhanVien>=@PhoGiamDocKD ) and ( SumTienHopDong>=@TienPhoGiamDocKD)
THEN N'Phó Giám Đốc Kinh Doanh'

WHEN (SumSoNhanVien>=@PhoTruongPhong ) and ( SumTienHopDong>=@TienPhoTruongPhong)
THEN N'Phó Trưởng Phòng Kinh Doanh'
--
WHEN (SumSoNhanVien>=@TruongNhomKD ) and ( SumTienHopDong>=@TienTruongNhomKD)
THEN N'Trưởng Nhóm Kinh Doanh'

WHEN  ( SumTienHopDong>=@TienChuyenVienKinhDoanh)
THEN N'Chuyên viên Kinh Doanh'

ELSE'Nhân viên'
END AS  CapText

,* 

from (
select
*
,(select count(*) from [Members] ma where ma.GioiThieu = t.ID and (@TuNgay='' or convert(DATETIME,[NgayTao],131) >=@TuNgay ) AND (@DenNgay='' or convert(DATETIME,[NgayTao],131) <= @DenNgay)) as SumSoNhanVien
,(select ISNULL(sum(CAST(ma.SoTienHopDong AS decimal(18,0))),0)from [Members] ma where ma.GioiThieu = t.ID and (@TuNgay='' or convert(DATETIME,[NgayTao],131) >=@TuNgay ) AND (@DenNgay='' or convert(DATETIME,[NgayTao],131) <= @DenNgay)) as SumTienHopDong
 from [dbo].[Members]  as t
) as c
)as g

WHERE 1=1
--and (@TuNgay='' or convert(DATETIME,[NgayTao],131) >=@TuNgay )
--AND (@DenNgay='' or convert(DATETIME,[NgayTao],131) <= @DenNgay)
and (HoVaTen like N'%'+@Key+'%'  or DiaChi like N'%'+@Key+'%' or DienThoai like N'%'+@Key+'%' or Email like N'%'+@Key+'%') 
and(@Status='' or [TrangThai]=@Status )
and(@IDThanhVien='' or ID=@IDThanhVien )
and(@CapBac='' or g.CapTrangThai=@CapBac )

and SumSoNhanVien > 0
and SumTienHopDong > 0
order by g.CapTrangThai desc