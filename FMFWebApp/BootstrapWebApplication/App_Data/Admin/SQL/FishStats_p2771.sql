declare @from datetime = '20150101'--'20131226'
declare @to datetime = '20151021'--'20141217'
declare @shipTypeCode int = 3
declare @minWeight decimal = 10.0

SELECT DISTINCT
	fs.Id,
	fs.Name,
	(SELECT TOP 1 st2.[Description]
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE 
	(st2.Code = @shipTypeCode) AND (trip2.[From] >= @from) AND (trip2.[To] <= @to) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id, st2.[Description]) as ShipTypeDescription,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE 
	--(st2.Code = @shipTypeCode) AND (trip2.[From] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @from), 114) + '1226') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1225')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as Total,
	(st2.Code = @shipTypeCode) AND (trip2.[From] BETWEEN @from AND @to) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as Total,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	--WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @from), 114) + '1226') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0201')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as January,
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN @from AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0201')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as January,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0201') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0301')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as February,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0301') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0401')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as March,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0401') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0501')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as April,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0501') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0601')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as May,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0601') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0701')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as June,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0701') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0801')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as July,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0801') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0901')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as August,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '0901') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1001')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as September,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1001') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1101')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as October,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1101') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1201')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as November,
	(SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE (st2.Code = @shipTypeCode) AND (trip2.[To] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1201') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1225')) AND fs2.Id = fs.Id AND ce.DeletedOn IS NULL GROUP BY fs2.Id) as December
FROM
	dbo.FishSpecies as fs INNER JOIN
	dbo.TripLine as tl ON fs.Id = tl.FishSpeciesId LEFT OUTER JOIN
	dbo.Trip as trip ON tl.TripId = trip.Id LEFT OUTER JOIN
    dbo.Ship as ship ON trip.ShipId = ship.Id LEFT OUTER JOIN
	dbo.ShipType as st ON ship.ShipTypeId = st.Id LEFT OUTER JOIN 
	ChangeEvent as ce ON ce.Id = fs.ChangeEventId
WHERE
	(st.Code = @shipTypeCode) AND (trip.[From] >= @from) AND (trip.[To] <= @to) --AND fs.Name = 'toskur 1'
	AND ce.DeletedOn IS NULL
	AND 
	((SELECT SUM(tl2.Amount) / 1000
	FROM
		dbo.FishSpecies as fs2 LEFT OUTER JOIN
		dbo.TripLine as tl2 ON fs2.Id = tl2.FishSpeciesId LEFT OUTER JOIN
		dbo.Trip as trip2 ON tl2.TripId = trip2.Id LEFT OUTER JOIN
		dbo.Ship as ship2 ON trip2.ShipId = ship2.Id LEFT OUTER JOIN
		dbo.ShipType as st2 ON trip2.ShipTypeId = st2.Id LEFT OUTER JOIN 
		ChangeEvent as ce ON ce.Id = fs2.ChangeEventId
	WHERE 
	--(st2.Code = @shipTypeCode) AND (trip2.[From] BETWEEN (CONVERT(VARCHAR(4), DATEPART(YYYY, @from), 114) + '1226') AND (CONVERT(VARCHAR(4), DATEPART(YYYY, @to), 114) + '1225')) AND fs2.Id = fs.Id GROUP BY fs2.Id)) >= 10 AND ce.DeletedOn IS NULL
	(st2.Code = @shipTypeCode) AND (trip2.[From] BETWEEN @from AND @to) AND fs2.Id = fs.Id GROUP BY fs2.Id)) >= @minWeight AND ce.DeletedOn IS NULL
ORDER BY fs.Name-- fs.Id