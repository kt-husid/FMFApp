--declare @SignOnFrom varchar(8) = '20130101'
--declare @SignOnTo varchar(8) = '20141231'
--declare @genderId int = 1
--declare @today datetime = getdate()

--update dbo.Member set Age = (select DATEDIFF(year, p.Birthday, @today) from dbo.Person as p join dbo.Member as m on p.Id = m.PersonId where m.Id = dbo.Member.Id)-1
/*
SELECT DISTINCT
	Age,
	cnt.colCount
	--(select top 1 AVG(m.Age) 
	--from dbo.Member as m INNER JOIN
	--	dbo.Person as p ON m.PersonId = p.Id LEFT OUTER JOIN
	--	dbo.PersonGender as gender ON p.GenderId = gender.Id LEFT OUTER JOIN
	--	dbo.SignOn as so ON so.MemberId = m.Id
	--WHERE (gender.Id = @genderId) AND Age = (DATEDIFF(year, p.Birthday, @today) - 1) and (DATEDIFF(year, p.Birthday, @today) is not null) AND (so.[From] >= @SignOnFrom) AND (so.[From] <= @SignOnTo) GROUP BY m.Id) as AvgAge
	--LSO.AvgAge
	--COUNT(Age) as "Count",
	--MIN(Age) as "MinAge",
	--MAX(Age) as "MaxAge",
FROM
	dbo.Member as m INNER JOIN
	dbo.Person as p ON m.PersonId = p.Id LEFT OUTER JOIN
	dbo.PersonGender as gender ON p.GenderId = gender.Id LEFT OUTER JOIN
	dbo.SignOn as so ON so.MemberId = m.Id LEFT OUTER JOIN
	ChangeEvent as ce ON m.ChangeEventId = ce.Id
	LEFT OUTER JOIN (
           select distinct (DATEDIFF(year, p2.Birthday, @today) - 1) as Age2, count(*) as colCount
           from dbo.Person as p2
			group by p2.Birthday
           ) as cnt on cnt.Age2 = Age
	--LEFT JOIN (SELECT TOP 1 m2.Age as Age2, AVG(m2.Age) as AvgAge, MIN(DATEDIFF(year, p2.Birthday, @today)) as MinAge, MAX(DATEDIFF(year, p2.Birthday, @today)) as MaxAge FROM dbo.Person as p2 INNER JOIN dbo.Member as m2 ON m2.PersonId = p2.Id INNER JOIN dbo.SignOn as so2 ON so2.MemberId = m2.Id WHERE (so2.[From] >= @SignOnFrom) AND (so2.[To] <= @SignOnTo) GROUP BY m2.Age ORDER BY m2.Age DESC) as LSO ON LSO.Age2 = m.Age
WHERE        
	ce.DeletedOn IS NULL AND (gender.Id = @genderId) and (DATEDIFF(year, p.Birthday, @today) is not null) AND (so.[From] >= @SignOnFrom) AND (so.[From] <= @SignOnTo) --AND Age = cnt.Age2
group by m.Id, m.Age, cnt.colCount--, LSO.AvgAge
order by Age
*/
SELECT DISTINCT
	Age,
	count(p.id) as colCount
FROM
	dbo.Member as m INNER JOIN
	dbo.Person as p ON m.PersonId = p.Id inner JOIN
	dbo.SignOn as so ON so.MemberId = m.Id inner JOIN
	ChangeEvent as ce ON m.ChangeEventId = ce.Id
WHERE        
	ce.DeletedOn IS NULL AND (p.GenderId = @genderId) and (so.[From] >= @SignOnFrom) AND (so.[From] <= @SignOnTo) 
group by m.Age
order by Age
