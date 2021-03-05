declare @MemberFinancialAidId int = (SELECT TOP(1) mfa2.Id from MemberFinancialAid as mfa2 where mfa2.MemberId = 1318)

SELECT        TOP (1) p.FirstName, p.MiddleName, p.LastName, p.Birthday, a.AddressLine1, dbo.Postal.Code, dbo.Postal.CityName, m.Id, mfa.[From], 
                         mfa.[To], ba.AccountNumber, b.RegNumber, b.Name AS BankName, mfa.SocialServicePayment, mfa.PaymentPerDay, 
                         mfa.Days, mfa.OurPayment, mfa.Id AS MemberFinancialAidId, mfa.PrintedOn, mfa.PrintedById
FROM            dbo.Person as p INNER JOIN
                dbo.Member as m ON p.Id = m.PersonId LEFT JOIN
				dbo.Entity as entity ON entity.Id = p.EntityId LEFT JOIN
                dbo.Address as a ON entity.Id = a.EntityId LEFT JOIN
                dbo.Postal ON a.PostalId = dbo.Postal.Id LEFT JOIN
                dbo.MemberFinancialAid as mfa ON m.Id = mfa.MemberId LEFT JOIN
                dbo.BankAccount as ba ON mfa.BankAccountId = ba.Id LEFT JOIN
                dbo.Bank as b ON ba.BankId = b.Id LEFT OUTER JOIN
				ChangeEvent as ce ON m.ChangeEventId = ce.Id
WHERE        (mfa.Id = @MemberFinancialAidId) AND (a.IsPrimary = 'TRUE') AND ce.DeletedOn IS NULL