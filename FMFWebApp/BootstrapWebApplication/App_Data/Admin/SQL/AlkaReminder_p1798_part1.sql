declare @fromPostalCode int = 100
declare @toPostalCode int = 100
declare @from int = 2003
declare @to int = 2014

SELECT 
p.FirstName, p.Birthday, 
postal.CityName as CityName, postal.Code as PostalCode, 
address.AddressLine1,
--LSO.LastSignOnDate
----trip.[From],
--ship.Code as ShipCode,
--ship.Name as ShipName
--DATEDIFF(day, LSO.LastSignOnDate, getdate()) as DaysSinceLastSignOn
--,
(SELECT TOP 1
	MAX(trip.[From]) as LastSignOnDate
FROM
	dbo.SignOn AS so INNER JOIN
	dbo.Member as m ON so.MemberId = m.Id INNER JOIN
	dbo.Person as p2 ON m.PersonId = p2.Id INNER JOIN
	dbo.Trip as trip on trip.Id = so.TripId INNER JOIN
	dbo.Ship as ship ON trip.ShipId = ship.Id
WHERE        
	(p.Id = p2.Id) AND (DATEPART(yyyy, trip.[From]) >= @from) AND (DATEPART(yyyy, trip.[To]) <= @to)
GROUP BY trip.[From]
ORDER BY trip.[From] DESC) as LastSignOn
--DATEDIFF(day, LastSignOn, getdate()) as DaysSinceLastSignOn
FROM
	dbo.Person as p INNER JOIN
	dbo.Member as m ON m.PersonId = p.Id INNER JOIN
	dbo.[ADDRESS] AS ADDRESS ON P.ENTITYID = ADDRESS.ENTITYID INNER JOIN
	dbo.Postal AS postal ON address.PostalId = postal.Id --LEFT JOIN
	-- Use this instead of subquery above
	--(SELECT TOP 1 so.MemberId, so.TripId, MAX(so.[From]) as LastSignOnDate FROM dbo.SignOn as so GROUP BY so.MemberId, so.TripId, so.[From] ORDER BY so.[From] DESC) as LSO ON LSO.MemberId = m.Id --LEFT JOIN
	--dbo.Trip as trip on LSO.TripId = trip.Id INNER JOIN
	--dbo.Ship as ship on trip.ShipId = ship.Id
WHERE
	--(postal.Code >= @fromPostalCode) AND (postal.Code <= @toPostalCode) AND (address.IsPrimary = 'TRUE')
	p.FirstName = 'Benjamin' AND p.LastName = 'Hammer' AND (address.IsPrimary = 'TRUE')
ORDER BY postal.Code, p.FirstName