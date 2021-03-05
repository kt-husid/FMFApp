declare @SignOnFrom varchar(8) = '20141226'
declare @SignOnTo varchar(8) = '20151231'
declare @GenderCode varchar(1) = 'M'
declare @AgeFrom int = 15
declare @AgeTo int = 70

update dbo.Member set Age = (select DATEDIFF(year, p.Birthday, getdate()) from dbo.Person as p join dbo.Member as m on p.Id = m.PersonId where m.Id = dbo.Member.Id)-1

--Min, Max and Avg year of members
SELECT
	MIN(m.Age) as MinAge,
	MAX(m.Age) as MaxAge,
	AVG(m.Age) as AvgAge
FROM           
	dbo.Member as m JOIN
	dbo.Person as p ON m.PersonId = p.Id JOIN
	dbo.SignOn as so ON so.MemberId = m.Id LEFT OUTER JOIN
	ChangeEvent as ce ON m.ChangeEventId = ce.Id
WHERE        
	ce.DeletedOn IS NULL AND m.Age >= @AgeFrom AND m.Age <= @AgeTo AND (m.GenderCode = @genderCode) AND (DATEDIFF(year, p.Birthday, getdate()) is not null) AND (so.[From] >= @SignOnFrom) AND (so.[To] <= @SignOnTo) AND so.HasInsurance = 'TRUE'