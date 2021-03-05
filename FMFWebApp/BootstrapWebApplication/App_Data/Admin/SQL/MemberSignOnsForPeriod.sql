declare @MemberId int = 217--13451
declare @From datetime = '19990601'--'20131001'
declare @To datetime = '20151016'--'20151001'

SELECT  
	dbo.Trip.Id,      
	dbo.Member.Id, 
	dbo.Person.FirstName, dbo.Person.MiddleName, dbo.Person.LastName, dbo.Person.Birthday, 
	so.[From], so.[To], so.PART, 
	dbo.Ship.Name AS ShipName, 
	dbo.Trip.TripId, dbo.Trip.CrewShareMoney, dbo.Trip.Crew, dbo.Trip.MinimumWage, dbo.Trip.MANN_PART_I,  dbo.Trip.MANN_PART, 
	dbo.Postal.CityName, 
	dbo.Job.Code AS JobCode,
	dbo.[Address].AddressLine1
	--dbo.Phone.RawNumber
FROM          dbo.SignOn as so INNER JOIN
                         dbo.Member ON so.MemberId = dbo.Member.Id INNER JOIN
                         dbo.Person ON dbo.Member.PersonId = dbo.Person.Id INNER JOIN
                         dbo.Trip ON so.TripId = dbo.Trip.Id INNER JOIN
                         dbo.Job ON dbo.Job.Id = so.JobWhileSignedOnId INNER JOIN
                         dbo.Ship ON dbo.Trip.ShipId = dbo.Ship.Id LEFT OUTER JOIN
                         dbo.[Address] ON dbo.Person.EntityId = dbo.[Address].EntityId LEFT OUTER JOIN
                         dbo.Postal ON dbo.[Address].PostalId = dbo.Postal.Id LEFT OUTER JOIN
						 dbo.ChangeEvent as ce ON so.ChangeEventId = ce.Id
						 --LEFT OUTER JOIN
       --                  dbo.Phone ON dbo.Person.EntityId = dbo.Phone.EntityId
WHERE        (dbo.Member.Id = @MemberId) AND (so.[From] >= @From) AND (so.[To] <= @To) AND (dbo.[Address].IsPrimary = 'TRUE') AND (ce.DeletedOn IS NULL)
--AND (dbo.Phone.IsPrimary = 'TRUE')
ORDER BY so.[From] ASC