Lọc thống kê theo tuần, tháng 
H:\Congty_APhuc\Nam2020\Thang09\VBNSHOP_NangCap

http://vbnshop.vn/admin.aspx?u=ThongKe

// lọc theo tuần
// Tuan này dạng 1,2,3,4,5 trong db
SELECT
Tuan,
SUM(CONVERT(float,SoCoin)) 
FROM HoaHongThanhVien
WHERE year(NgayTao)=2020 and month(NgayTao)=11
GROUP BY  Tuan



// lọc theo tuần
SELECT
  DATEPART(week, NgayTao) AS Tuan,
  COUNT(SoCoin) AS SoCoin
FROM HoaHongThanhVien
WHERE year(NgayTao)=2020 and month(NgayTao)=11
GROUP BY DATEPART(week, NgayTao)
ORDER BY DATEPART(week, NgayTao);


// Lọc theo tháng
SELECT SUM(CONVERT(float,SoCoin)) as TongTien,month(NgayTao) as month FROM HoaHongThanhVien 
where  year(NgayTao)=" + LocThang + " group by month(NgayTao) order by month(NgayTao) asc





// lọc theo các năm
SELECT
year(NgayTao) as year,
SUM(CONVERT(float,SoCoin))  as SoCoin
FROM HoaHongThanhVien
	// WHERE year(NgayTao)=2020 and month(NgayTao)=11
GROUP BY  year(NgayTao)
ORDER BY year(NgayTao)