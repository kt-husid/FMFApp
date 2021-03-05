declare @memberTypeCode varchar(2) = 'ff';
declare @postalFrom int = 100;
declare @postalTo int = 999;
declare @SignOnFrom datetime = '20140101';
declare @SignOnTo datetime = '20150914';
declare @MemberJobCodeFrom varchar(1) = 'd'
declare @MemberJobCodeTo varchar(1) = 'l'

SELECT
	(row_number() OVER (ORDER BY (select NULL))-1) MyDataIndex,
	p.FirstName, p.MiddleName, p.LastName, p.FullName, m.AddressLine, m.CountryCode, m.CountryName, m.PostalCode, m.CityName, so.[From], m.JobCode, m.MemberTypeCode
FROM
	dbo.Member as m INNER JOIN
	dbo.ChangeEvent as ce ON m.ChangeEventId = ce.Id LEFT OUTER JOIN
	dbo.MemberType ON dbo.MemberType.Id = m.MemberTypeId LEFT OUTER JOIN
	dbo.Job ON dbo.Job.Id = m.JobId LEFT OUTER JOIN
	dbo.Person as p ON m.PersonId = p.Id LEFT OUTER JOIN
	dbo.SignOn as so ON m.LastSignOnId = so.Id 
WHERE 
	(m.PostalCode >= @postalFrom) AND 
	(m.PostalCode <= @postalTo) AND 
	(dbo.MemberType.Code = @memberTypeCode) AND 
	ce.DeletedOn IS NULL AND
	(
		((@memberTypeCode <> 'al') AND so.[From] >= @SignOnFrom AND so.[To] <= @SignOnTo)
		OR
		((@memberTypeCode = 'al') AND (ISNULL(so.[From], 1) = 1 OR so.[From] >= '19000101'))
	)
	AND m.JobCode >= @MemberJobCodeFrom AND m.JobCode <= @MemberJobCodeTo
ORDER BY m.JobCode, m.PostalCode