update dbo.Person set FullName = (FirstName + ' ' + MiddleName + ' ' + LastName)
update dbo.Member set MemberTypeCode = (select Code from dbo.MemberType as mt where dbo.Member.MemberTypeId = mt.Id)
update dbo.Member set MemberTypeDescription = (select [Description] from dbo.MemberType as mt where dbo.Member.MemberTypeId = mt.Id)
update dbo.Member set JobCode = (select Code from dbo.Job as j where dbo.Member.JobId = j.Id)
update dbo.Member set JobDescription = (select [Description] from dbo.Job as j where dbo.Member.JobId = j.Id)

 update dbo.Member set GenderCode = 
(select top 1 pg.Code from dbo.PersonGender as pg
	   left outer join dbo.Person as p on p.GenderId = pg.Id
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND m.Id = dbo.Member.Id)


 update dbo.Member set PostalCode = 
(select top 1 po.Code from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Person as p on p.EntityId = e.Id
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND m.Id = dbo.Member.Id)

 update dbo.Member set CityName = 
(select top 1 po.CityName from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Person as p on p.EntityId = e.Id
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND m.Id = dbo.Member.Id)

 update dbo.Member set AddressLine = 
(select top 1 (AddressLine1 + ' ' + AddressLine2) from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Person as p on p.EntityId = e.Id
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND m.Id = dbo.Member.Id)

  update dbo.Member set PhoneCountryCode = 
(select top 1 phone.CountryCode from dbo.Phone as phone
	   left outer join dbo.Entity as e on e.Id = phone.EntityId
	   left outer join dbo.Person as p on p.EntityId = e.Id
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND phone.IsPrimary = 'TRUE' AND m.Id = dbo.Member.Id)

   update dbo.Member set Age = 
(select DATEDIFF(yy, p.Birthday, getdate())-1 from dbo.Person as p
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND m.Id = dbo.Member.Id)

  update dbo.Member set PhoneNumber = 
(select top 1 phone.Number from dbo.Phone as phone
	   left outer join dbo.Entity as e on e.Id = phone.EntityId
	   left outer join dbo.Person as p on p.EntityId = e.Id
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND phone.IsPrimary = 'TRUE' AND m.Id = dbo.Member.Id)

 update dbo.Member set CountryCode = 
(select top 1 c.Code from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Country as c on c.Id = a.CountryId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Person as p on p.EntityId = e.Id
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND m.Id = dbo.Member.Id)

  update dbo.Member set CountryName = 
(select top 1 c.Name from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Country as c on c.Id = a.CountryId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Person as p on p.EntityId = e.Id
	   left outer join dbo.Member as m on p.Id = m.PersonId
	   left outer join dbo.ChangeEvent as ce on m.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND m.Id = dbo.Member.Id)

update dbo.Member set LastSignOnId = (select top 1 so.Id from dbo.SignOn as so left outer join dbo.Member as m on so.MemberId = m.Id where m.Id = dbo.Member.Id order by so.Id desc)

update dbo.SignOn set ShipCode = (select top 1 s.Code from dbo.Ship as s left outer join dbo.Trip as t on t.ShipId = s.Id left outer join dbo.SignOn as so on so.TripId = t.Id where so.Id = dbo.SignOn.Id)

update dbo.SignOn set ShipName = (select top 1 s.Name from dbo.Ship as s left outer join dbo.Trip as t on t.ShipId = s.Id left outer join dbo.SignOn as so on so.TripId = t.Id where so.Id = dbo.SignOn.Id)



update dbo.Ship set PostalCode = 
(select top 1 po.Code from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Ship as s on e.Id = s.EntityId
	   left outer join dbo.ChangeEvent as ce on s.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND s.Id = dbo.Ship.Id)

update dbo.Ship set CityName = 
(select top 1 po.CityName from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Ship as s on e.Id = s.EntityId
	   left outer join dbo.ChangeEvent as ce on s.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND s.Id = dbo.Ship.Id)

update dbo.Ship set AddressLine = 
(select top 1 (AddressLine1 + ' ' + AddressLine2) from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Ship as s on e.Id = s.EntityId
	   left outer join dbo.ChangeEvent as ce on s.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND s.Id = dbo.Ship.Id)

update dbo.Ship set PhoneCountryCode = 
(select top 1 phone.CountryCode from dbo.Phone as phone
	   left outer join dbo.Entity as e on e.Id = phone.EntityId
	   left outer join dbo.Ship as s on e.Id = s.EntityId
	   left outer join dbo.ChangeEvent as ce on s.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND phone.IsPrimary = 'TRUE' AND s.Id = dbo.Ship.Id)

update dbo.Ship set PhoneNumber = 
(select top 1 phone.Number from dbo.Phone as phone
	   left outer join dbo.Entity as e on e.Id = phone.EntityId
	   left outer join dbo.Ship as s on e.Id = s.EntityId
	   left outer join dbo.ChangeEvent as ce on s.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND phone.IsPrimary = 'TRUE' AND s.Id = dbo.Ship.Id)

update dbo.Ship set CountryCode = 
(select top 1 c.Code from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Country as c on c.Id = a.CountryId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Ship as s on e.Id = s.EntityId
	   left outer join dbo.ChangeEvent as ce on s.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND s.Id = dbo.Ship.Id)

update dbo.Ship set CountryName = 
(select top 1 c.Name from dbo.Postal as po
	   INNER JOIN dbo.[Address] as a ON po.Id = a.PostalId
	   left outer join dbo.Country as c on c.Id = a.CountryId
	   left outer join dbo.Entity as e on e.Id = a.EntityId
	   left outer join dbo.Ship as s on e.Id = s.EntityId
	   left outer join dbo.ChangeEvent as ce on s.ChangeEventId = ce.Id 
 where (ce.DeletedOn IS NULL) AND a.IsPrimary = 'TRUE' AND s.Id = dbo.Ship.Id)