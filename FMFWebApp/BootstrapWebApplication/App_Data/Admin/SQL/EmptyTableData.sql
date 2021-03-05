delete from BankAccount
--DBCC CHECKIDENT ('BankAccount', RESEED, 0)
delete from Person
--DBCC CHECKIDENT ('Person', RESEED, 0)
delete from Member
--DBCC CHECKIDENT ('Member', RESEED, 0)
delete from MemberType
--DBCC CHECKIDENT ('MemberType', RESEED, 0)
delete from [Status]
--DBCC CHECKIDENT ('Status', RESEED, 0)
delete from PersonGender
delete from PersonTitle
delete from ChangeEvent
--DBCC CHECKIDENT ('ChangeEvent', RESEED, 0)