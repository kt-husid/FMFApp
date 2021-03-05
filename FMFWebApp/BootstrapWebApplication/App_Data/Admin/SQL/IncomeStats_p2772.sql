declare @from datetime = '20141226'
declare @to datetime = '20150916'
declare @shipTypeCode int = 14

SELECT DISTINCT
	ship.Name as ShipName,
	ship.Code as ShipCode,
	st.[Description] as ShipTypeDescription,
	--From
	(SELECT TOP 1 trip.[From]
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id, trip.[From] ORDER BY trip.[From] asc) as "From",
	--To
	(SELECT TOP 1 trip.[To]
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id, trip.[To] ORDER BY trip.[To] desc) as "To",
	--Days
	(SELECT SUM((DATEDIFF(d, trip.[From], trip.[To]) + 1))
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id) as "Days",
	--TripCount
	(SELECT COUNT(*)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id) as TripCount,
	--CrewShare
	(SELECT ROUND(SUM(trip.CrewShareMoney / trip.CrewIncludingStayingAtHome), 4)--ROUND(SUM(trip.MANN_PART), 4)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id) as CrewShare,
	--Temp
	(SELECT ROUND(SUM(trip.MANN_PART), 4)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id) as Temp,
	--HolidayPay
	(SELECT ROUND((SUM(trip.CrewShareMoney / trip.CrewIncludingStayingAtHome) + SUM(trip.MinimumWage * (DATEDIFF(d, trip.[From], trip.[To]) + 1))) * 0.12, 4)
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id) as HolidayPay,
	--MinimumWage
	(SELECT SUM(trip.MinimumWage * (DATEDIFF(d, trip.[From], trip.[To]) + 1))
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id) as MinimumWage,
	--Total
	(SELECT 
		--ROUND(SUM(trip.MANN_PART), 4) + 
		ROUND(SUM(trip.CrewShareMoney / trip.CrewIncludingStayingAtHome), 4) +
		--ROUND((SUM(trip.MANN_PART) + SUM(trip.MinimumWage * (DATEDIFF(d, trip.[From], trip.[To]) + 1))) * 0.12, 4) + 
		ROUND((SUM(trip.CrewShareMoney / trip.CrewIncludingStayingAtHome) + SUM(trip.MinimumWage * (DATEDIFF(d, trip.[From], trip.[To]) + 1))) * 0.12, 4) + 
		SUM( trip.MinimumWage * (DATEDIFF(d, trip.[From], trip.[To]) + 1))
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id) as Total,
	--SalaryPerDay
	(SELECT 
		(
		ROUND(SUM(trip.CrewShareMoney / trip.CrewIncludingStayingAtHome), 4) +
		ROUND((SUM(trip.CrewShareMoney / trip.CrewIncludingStayingAtHome) + SUM(trip.MinimumWage * (DATEDIFF(d, trip.[From], trip.[To]) + 1))) * 0.12, 4) + 
		SUM( trip.MinimumWage * (DATEDIFF(d, trip.[From], trip.[To]) + 1))
		) 
		/
		SUM((DATEDIFF(d, trip.[From], trip.[To]) + 1))
	FROM dbo.Trip as trip INNER JOIN dbo.Ship as ship2 ON trip.ShipId = ship.Id LEFT OUTER JOIN dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN ChangeEvent as ce ON ce.Id = trip.ChangeEventId
	WHERE (st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ship2.Id = ship.Id AND ce.DeletedOn IS NULL GROUP BY ship2.Id) as SalaryPerDay
FROM
	dbo.Trip as trip INNER JOIN
    dbo.Ship as ship ON trip.ShipId = ship.Id LEFT OUTER JOIN
	dbo.ShipType as st ON trip.ShipTypeId = st.Id LEFT OUTER JOIN
	ChangeEvent as ce ON ce.Id = trip.ChangeEventId
WHERE
	(st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) AND ce.DeletedOn IS NULL