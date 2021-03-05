declare @shipFrom varchar(8) = 'FD000';
declare @shipTo varchar(8) = 'ZZ999';

SELECT
	ship.Code as ShipCode, 
	ship.Name as ShipName,
	ship.ContactCompanyName,
	ship.CountryCode,
	ship.PostalCode, 
	ship.CityName,
	ship.AddressLine as AddressLine1
	--.Name as ShippingCompanyName
FROM            
	dbo.Ship as ship --LEFT OUTER JOIN
	--dbo.ShipType ON dbo.ShipType.Id = ship.ShipTypeId INNER JOIN
	--dbo.ShippingCompany as sc ON sc.Id = ship.ShippingCompanyId LEFT OUTER JOIN
	--ChangeEvent as ce ON ship.ChangeEventId = ce.Id
WHERE 
	(ship.Code >= @shipFrom) AND (ship.Code <= @shipTo) AND (ship.[Status] != 'iv') AND (ship.[Status] != 'in') AND ship.Code = 'FD1204' -- AND ce.DeletedOn IS NULL
ORDER BY ship.Code