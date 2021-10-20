

	SELECT *
	FROM users a
	LEFT JOIN
	(
		SELECT   ROW_NUMBER() OVER (PARTITION BY IDThanhVien ORDER BY NgayMua DESC) AS RowNumber
		,IDThanhVien
		FROM KhoaHocThanhVien
	) b
	ON a.iuser_id = b.IDThanhVien
	WHERE b.RowNumber = 1