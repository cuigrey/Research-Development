USE my_db;
GO
--select count(*) from rtbl1m;


select * from (select *, POWER(x - 128, 2) - POWER(y - 256, 2) as [r1] from [dbo].[rtbl1m] where t like '%0%') t1 
where t1.r1 > 0 and SQRT(POWER(x - 128, 2) - POWER(y - 256, 2)) > 42
ORDER BY x DESC;
GO