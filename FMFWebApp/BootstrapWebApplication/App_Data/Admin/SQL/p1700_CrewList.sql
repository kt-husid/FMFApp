declare @PostalCodeFrom int = 100
declare @PostalCodeTo int = 340
declare @SignOnFrom int = 2014
declare @SignOnTo int = 2015
declare @MemberTypeCode varchar(2) = 'ff'

SELECT
	m.Id,
	m.MemberTypeCode, 
	m.MemberTypeDescription, 
	m.JobCode, 
	m.JobDescription, 
	m.CountryCode, 
	m.CountryName, 
	m.PostalCode, 
	m.CityName,
	m.AddressLine, 
	m.PhoneCountryCode, 
	m.PhoneNumber, 
	m.GenderCode,
	p.FirstName, p.MiddleName, p.LastName,
	p.FullName, 
	p.Birthday,
	so.[From] as SignOnFrom,
	so.[To] as SignOnTo,
	so.ShipCode,
	so.ShipName,
	DATEDIFF(year, p.Birthday, getdate()) as Age
FROM
	dbo.Member as m INNER JOIN
	dbo.Person as p ON m.PersonId = p.Id INNER JOIN
	dbo.ChangeEvent as ce ON m.ChangeEventId = ce.Id INNER JOIN
	dbo.SignOn as so ON so.Id = m.LastSignOnId
WHERE
	(m.PostalCode >= @PostalCodeFrom) AND 
	(m.PostalCode <= @PostalCodeTo) AND 
	(m.MemberTypeCode = @MemberTypeCode) AND 
	(ce.DeletedOn IS NULL) AND 
	(p.IsAlive = 'TRUE') AND
	DATEPART(yyyy, so.[To]) >= @SignOnFrom AND DATEPART(yyyy, so.[To]) <= @SignOnTo
ORDER BY m.PostalCode, p.Birthday