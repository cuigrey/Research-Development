USE my_imdb;
GO
--ALTER DATABASE my_imdb	SET ONLINE WITH ROLLBACK IMMEDIATE;
--ALTER DATABASE my_db		SET ONLINE WITH ROLLBACK IMMEDIATE;

--ALTER DATABASE my_imdb	SET OFFLINE WITH ROLLBACK IMMEDIATE;
--ALTER DATABASE my_db		SET OFFLINE WITH ROLLBACK IMMEDIATE;

--select count(*), 5000000 - count(*) from dbo.rtbl1m;


select * from (select *, POWER(x - 128, 2) - POWER(y - 256, 2) as [r1] from [dbo].[rtbl1m] where t like '%0%') t1 
where t1.r1 > 0 and SQRT(POWER(x - 128, 2) - POWER(y - 256, 2)) > 42
ORDER BY x DESC;
GO




