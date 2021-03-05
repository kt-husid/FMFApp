declare @MemberId int = 12010
declare @From datetime = '20030101'
declare @To datetime = '20141231'

SELECT        
m.Id,
hp.KOYR_DATO, hp.TransferDate, hp.Amount, hp.EmployerNumber, hp.REG_NR_FF, hp.KONTO_FF, hp.ART,
p.FirstName, p.MiddleName, p.LastName, p.Birthday, 
m.AddressLine as AddressLine1,
m.PostalCode as Code,
m.CityName,
m.CountryCode,
dbo.ShippingCompany.Name
FROM            dbo.HolidayPay_HOVD as hp INNER JOIN
                dbo.Member as m ON hp.MemberId = m.Id LEFT OUTER JOIN
                dbo.Person as p ON m.PersonId = p.Id LEFT OUTER JOIN
                dbo.ShippingCompany ON hp.EmployerNumber = dbo.ShippingCompany.EmployerNumber LEFT OUTER JOIN
				ChangeEvent as ce ON hp.ChangeEventId = ce.Id
WHERE        (m.Id = @MemberId) AND (hp.TransferDate >= @From) AND (hp.TransferDate < @To) AND (ce.DeletedOn IS NULL)