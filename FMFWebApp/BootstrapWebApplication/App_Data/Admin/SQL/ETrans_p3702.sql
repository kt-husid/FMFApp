declare @From datetime = '20150401'
--declare @To datetime = '20160331'
declare @WeekFrom datetime = '20151011'
declare @WeekTo datetime = '20151018'

SELECT
hp.EmployerNumber,
(SELECT COUNT(*) FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL  AND (hp2.TransferDate >= @From) AND (hp2.TransferDate <= @WeekTo)) AS Pcs,
(SELECT SUM(hp2.Amount) - ISNULL((SELECT SUM(hp3.Amount) FROM HolidayPay_HOVD AS hp3 LEFT OUTER JOIN ChangeEvent AS ce3 ON ce3.Id = hp3.ChangeEventId WHERE ce3.DeletedOn IS NULL AND ((hp3.ART = '9') OR (hp3.ART = 'R')) AND (hp3.TransferDate >= @From) AND (hp3.TransferDate <= @WeekTo)), 0)
FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.ART <> '9') AND (hp2.ART <> 'R') AND (hp2.TransferDate >= @From) AND (hp2.TransferDate <= @WeekTo)) AS AmountTotal,
(SELECT SUM(hp2.Amount) - ISNULL((SELECT SUM(hp3.Amount) FROM HolidayPay_HOVD AS hp3 LEFT OUTER JOIN ChangeEvent AS ce3 ON ce3.Id = hp3.ChangeEventId WHERE ce3.DeletedOn IS NULL AND ((hp3.ART = '9') OR (hp3.ART = 'R')) AND (hp3.TransferDate >= @WeekFrom) AND (hp3.TransferDate <= @WeekTo)), 0)
FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.ART <> '9') AND (hp2.ART <> 'R') AND (hp2.TransferDate >= @WeekFrom) AND (hp2.TransferDate <= @WeekTo)) AS WeekAmountTotal,
(SELECT TOP 1 TransferDate FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.EmployerNumber = hp.EmployerNumber) AND (TransferDate >= @From) AND (TransferDate <= @WeekTo) ORDER BY TransferDate ASC) AS DateFrom,
(SELECT TOP 1 TransferDate FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.EmployerNumber = hp.EmployerNumber) AND (TransferDate >= @From) AND (TransferDate <= @WeekTo) ORDER BY TransferDate DESC) AS DateTo,
sc.Name,
Pcs.[Count] AS WeekCount, 
Pcs.Amount AS WeekAmount,
PcsDeduction.[Count] AS WeekDeductionCount, 
PcsDeduction.Amount AS WeekDeductionAmount, 
Total.[Count] AS TotalCount, 
Total.Amount AS TotalAmount,
TotalDeduction.[Count] AS TotalDeductionCount, 
TotalDeduction.Amount AS TotalDeductionAmount,
WeekDeduction.Amount AS WeekDeductionAmount,
WeekDeduction.[Count] AS WeekDeductionCount
FROM HolidayPay_HOVD AS hp  LEFT OUTER JOIN
ChangeEvent as ce ON hp.ChangeEventId = ce.Id
LEFT OUTER JOIN ShippingCompany AS sc ON sc.EmployerNumber = hp.EmployerNumber 
LEFT OUTER JOIN ChangeEvent as ce2 ON ce2.Id = sc.ChangeEventId
LEFT OUTER JOIN (SELECT DISTINCT hp2.EmployerNumber, COUNT(*) AS "Count", SUM(hp2.Amount) AS Amount FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.ART = '1') AND (hp2.TransferDate >= @WeekFrom) AND (hp2.TransferDate <= @WeekTo) GROUP BY hp2.EmployerNumber) AS Pcs ON hp.EmployerNumber = Pcs.EmployerNumber
LEFT OUTER JOIN (SELECT DISTINCT hp2.EmployerNumber, COUNT(*) AS "Count", SUM(hp2.Amount) AS Amount FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.ART <> '1') AND (hp2.TransferDate >= @WeekFrom) AND (hp2.TransferDate <= @WeekTo) GROUP BY hp2.EmployerNumber) AS PcsDeduction ON hp.EmployerNumber = PcsDeduction.EmployerNumber
LEFT OUTER JOIN (SELECT DISTINCT hp2.EmployerNumber, COUNT(*) AS "Count", SUM(hp2.Amount) AS Amount FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.ART = '1') AND (hp2.TransferDate >= @From) AND (hp2.TransferDate <= @WeekTo) GROUP BY hp2.EmployerNumber) AS Total ON hp.EmployerNumber = Total.EmployerNumber
LEFT OUTER JOIN (SELECT DISTINCT hp2.EmployerNumber, COUNT(*) AS "Count", SUM(hp2.Amount) AS Amount FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.ART = 'R') AND (hp2.TransferDate >= @From) AND (hp2.TransferDate <= @WeekTo) GROUP BY hp2.EmployerNumber) AS TotalDeduction ON hp.EmployerNumber = TotalDeduction.EmployerNumber
LEFT OUTER JOIN (SELECT DISTINCT hp2.EmployerNumber, COUNT(*) AS "Count", SUM(hp2.Amount) AS Amount FROM HolidayPay_HOVD AS hp2 LEFT OUTER JOIN ChangeEvent AS ce2 ON ce2.Id = hp2.ChangeEventId WHERE ce2.DeletedOn IS NULL AND (hp2.ART = 'R') AND (hp2.TransferDate >= @WeekFrom) AND (hp2.TransferDate <= @WeekTo) GROUP BY hp2.EmployerNumber) AS WeekDeduction ON hp.EmployerNumber = WeekDeduction.EmployerNumber
WHERE ce.DeletedOn IS NULL AND ce2.DeletedOn IS NULL AND (hp.TransferDate >= @From) AND (hp.TransferDate <= @WeekTo) AND (pcs.[Count] IS NOT NULL) --AND hp.ART <> '9'AND hp.ART <> 'R' --AND ((PcsDeduction.Amount IS NULL) OR (Pcs.Amount - PcsDeduction.Amount > 0))
GROUP BY hp.EmployerNumber, sc.Name, Pcs.[Count], Pcs.Amount, Total.[Count], Total.Amount, TotalDeduction.[Count], TotalDeduction.Amount, PcsDeduction.[Count], PcsDeduction.Amount, WeekDeduction.[Count], WeekDeduction.Amount
ORDER BY hp.EmployerNumber