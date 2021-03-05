declare @fromPostalCode int = 100
declare @toPostalCode int = 999
declare @SignOnTo datetime = '20150529'
declare @daysUntilInsuranceExpires int = 360
--declare @SignOnFrom datetime = DATEADD(day, -@daysUntilInsuranceExpires, @SignOnTo)
declare @IncludeLessThan30days bit = 'TRUE'

SELECT 
m.LastSignOnId,
--(SELECT Id FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 2) as NSM_Id,
(SELECT DATEDIFF(day, [From], [To]) as SignOnDays  FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 2) as NSM_Days,
DATEDIFF(day,
(SELECT [To] FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 2),
(SELECT [From] FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 1)
) as DaysBetweenSignOn, 
--DATEADD(day, -@daysUntilInsuranceExpires, @SignOnTo) as SignOnFrom,
--DATEDIFF(day, so.[From], so.[To]) as "SignOnDays",
Temp.SignOnDays,
(Temp.SignOnDays + DATEDIFF(day,
(SELECT [To] FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 2),
(SELECT [From] FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 1)
)) as TotalDaysTemp,
--DATEDIFF(day, so.[To], @SignOnTo) as "DaysSinceLastSignOn",
Temp.DaysSinceLastSignOn as DaysSinceLastSignOn,
so.[From], so.[To],
DATEADD(d, 180, so.[To]) as InsuranceEndDate,
p.Fullname, p.FirstName, p.MiddleName, p.LastName, p.Birthday, 
m.NR, m.CityName, m.PostalCode, m.CountryName, m.CountryCode, m.AddressLine, 
so.ShipCode, so.ShipName
FROM
	dbo.SignOn AS so INNER JOIN
	dbo.Member as m ON so.Id = m.LastSignOnId INNER JOIN
	dbo.Person as p ON m.PersonId = p.Id LEFT OUTER JOIN 
	dbo.ChangeEvent as ce ON ce.Id = so.ChangeEventID LEFT OUTER JOIN
	(SELECT DISTINCT 
		so.Id, 
		DATEDIFF(day, so.[From], so.[To]) as SignOnDays, 
		DATEDIFF(day, so.[To], @SignOnTo) as DaysSinceLastSignOn 
	FROM Signon AS so 
	WHERE 
		so.[To] >= DATEADD(day, -@daysUntilInsuranceExpires, @SignOnTo)
		AND so.[To] <= @SignOnTo
		--AND DATEDIFF(day, so.[From], @SignOnTo) > @daysUntilInsuranceExpires
	) AS Temp ON so.Id = Temp.Id
WHERE
	ce.DeletedOn IS NULL AND
	-- Filter by postal code and only those members who have insurance
	m.PostalCode >= @fromPostalCode AND m.PostalCode <= @toPostalCode AND so.HasInsurance = 'TRUE'
	-- Only select 180 days back from current SignOnTo date
	AND so.[To] >= DATEADD(day, -360, @SignOnTo)	AND so.[To] <= @SignOnTo
	--														 SignOn Days     + Days Between SignOn
	AND ((Temp.DaysSinceLastSignOn > 180 AND Temp.SignOnDays > 30 AND @IncludeLessThan30days = 'FALSE') 
		OR (Temp.DaysSinceLastSignOn > 180 AND Temp.SignOnDays < 30 AND (Temp.SignOnDays + DATEDIFF(day,
		(SELECT [To] FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 2),
		(SELECT [From] FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 1)
		) > 30) 
		AND (SELECT DATEDIFF(day, [From], [To]) as SignOnDays  FROM (SELECT ROW_NUMBER() OVER (ORDER BY so.Id DESC) AS RowNum, * FROM dbo.SignOn as so WHERE so.MemberId = m.Id) sub WHERE RowNum = 2) IS NOT NULL
		AND @IncludeLessThan30days = 'TRUE'
		))
ORDER BY m.NR
--ORDER BY DaysSinceLastSignOn -- m.NR