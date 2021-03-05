declare @From datetime = '20140601'
declare @To datetime = '20150228'
declare @ShipId int = NULL

SELECT
	trip.Id,
	ship.Code as ShipCode,
	ship.Name as ShipName,
	trip.TRYG_BILAG as InsuranceNumber,
	trip.TRYG_KRHB as LaborInsurancePaid,
	trip.TripId as TripId,
	trip.[From] as "From",
	trip.[To] as "To",
	(DATEDIFF(d, trip.[From], trip.[To]) + 1) as "Days",
	trip.TRYG_ANT,
	(trip.LaborInsurance * (DATEDIFF(d, trip.[From], trip.[To]) + 1)) as LaborInsurance
FROM
	dbo.Trip as trip LEFT OUTER JOIN
	dbo.Ship as ship ON ship.Id = trip.ShipId LEFT OUTER JOIN
	ChangeEvent as ce ON trip.ChangeEventId = ce.Id
WHERE
	ce.DeletedOn IS NULL AND
	trip.[From] >= @From AND trip.[To] <= @To AND trip.TRYG_BILAG IS NOT NULL AND trip.TRYG_BILAG > 0 AND 
	TRYG_KRHB != (trip.LaborInsurance * (DATEDIFF(d, trip.[From], trip.[To]) + 1)) AND
	ship.Id = CASE WHEN @ShipId <> 0 THEN @ShipId ELSE ship.Id END
ORDER BY ShipCode