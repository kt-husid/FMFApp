declare @from datetime = '20150520'
declare @to datetime = '20150617'
declare @shipTypeCode int = 3

--update dbo.Member set Age = (select DATEDIFF(year, p.Birthday, getdate()) from dbo.Person as p join dbo.Member as m on p.Id = m.PersonId where m.Id = dbo.Member.Id)-1

SELECT DISTINCT
	ship.Name as ShipName,
	ship.Code as ShipCode,
	st.[Description] as ShipTypeDescription,
	--Days
	(SELECT SUM((DATEDIFF(d, trip.[From], trip.[To]) + 1))
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as "Days",
	--TripCount
	(SELECT COUNT(*)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as TripCount,
	--CrewAmount
	(SELECT AVG(trip.Crew)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as CrewAmount,
	--TripLine Amount
	(SELECT ROUND(SUM(tl.Amount) / 1000, 4)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.TripLine as tl ON trip.Id = tl.TripId LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = tl.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND tl.FishSpeciesId != (SELECT Id FROM FishSpecies WHERE (Code = 'ju')) AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as TripLinesAmount,
	--TripLines
	(SELECT ROUND(SUM(tl.Amount * tl.UnitPrice) / 1000, 4)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.TripLine as tl ON trip.Id = tl.TripId LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = tl.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND tl.FishSpeciesId != (SELECT Id FROM FishSpecies WHERE (Code = 'ju')) AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as TripLinesPrice,
	--TripLine Avg Price
	(SELECT ROUND(SUM(tl.Amount * tl.UnitPrice) / 1000, 4) / ROUND(SUM(tl.Amount) / 1000, 4)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.TripLine as tl ON trip.Id = tl.TripId LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = tl.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND tl.FishSpeciesId != (SELECT Id FROM FishSpecies WHERE (Code = 'ju')) AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as TripLineAverage,
	--MinAge
	(SELECT MIN(m.Age)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.SignOn as so ON trip.Id = so.TripId INNER JOIN dbo.Member as m ON so.MemberId = m.Id LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as MinAge,
	--AvgAge
	(SELECT AVG(m.Age)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.SignOn as so ON trip.Id = so.TripId INNER JOIN dbo.Member as m ON so.MemberId = m.Id LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as AvgAge,
	--MaxAge
	(SELECT MAX(m.Age)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN dbo.SignOn as so ON trip.Id = so.TripId INNER JOIN dbo.Member as m ON so.MemberId = m.Id LEFT OUTER JOIN dbo.ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE ce.DeletedOn IS NULL AND (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id GROUP BY ship2.Id) as MaxAge
FROM
	dbo.Trip as trip INNER JOIN
    dbo.Ship as ship ON trip.ShipId = ship.Id LEFT OUTER JOIN
	dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN
	ChangeEvent as ce ON trip.ChangeEventId = ce.Id
WHERE
	(st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ce.DeletedOn IS NULL