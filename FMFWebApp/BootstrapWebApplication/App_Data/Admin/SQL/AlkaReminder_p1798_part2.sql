declare @fromPostalCode int = 188
declare @toPostalCode int = 188
declare @from int = 2003
declare @to int = 2014

SELECT p.FirstName, p.Birthday, postal.CityName as CityName, postal.Code as PostalCode, address.AddressLine1,
(SELECT TOP 1
	MAX(trip.[From]) as LastSignOnDate
FROM
	dbo.SignOn AS so INNER JOIN
	dbo.Member as m ON so.MemberId = m.Id INNER JOIN
	dbo.Person as p2 ON m.PersonId = p2.Id INNER JOIN
	dbo.Trip as trip on trip.Id = so.TripId INNER JOIN -- ON so.TripId = trip.Id INNER JOIN
	dbo.Ship as ship ON trip.ShipId = ship.Id-- LEFT OUTER JOIN
	--dbo.[Address] as address2 ON p2.EntityId = address2.EntityId LEFT OUTER JOIN
	--dbo.Postal as postal2 ON address2.PostalId = postal2.Id
WHERE        
	(p.Id = p2.Id) AND
	--(postal2.Code >= @fromPostalCode) AND (postal2.Code <= @toPostalCode) AND 
	(DATEPART(yyyy, trip.[From]) >= @from) AND (DATEPART(yyyy, trip.[To]) <= @to) --AND (address2.IsPrimary = 'TRUE')
GROUP BY trip.[From]
ORDER BY trip.[From] DESC) as LastSignOn 
FROM
	dbo.Person as p INNER JOIN
	DBO.[ADDRESS] AS ADDRESS ON P.ENTITYID = ADDRESS.ENTITYID INNER JOIN
	DBO.POSTAL AS POSTAL ON address.PostalId = postal.Id
WHERE
	--(postal.Code >= @fromPostalCode) AND (postal.Code <= @toPostalCode)
p.FirstName = 'Benjamin' AND p.LastName = 'Hammer' AND (address.IsPrimary = 'TRUE')