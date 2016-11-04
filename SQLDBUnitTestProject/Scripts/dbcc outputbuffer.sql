
PRINT 'ABC';
SELECT @@PROCID AS [Process ID], @@SPID AS [Session ID];
DBCC OUTPUTBUFFER (@@SPID);
DBCC INPUTBUFFER (@@SPID);
GO --
SELECT dest.*
FROM sys.dm_exec_requests AS der
     CROSS APPLY sys.dm_exec_sql_text (der.sql_handle) AS dest
WHERE session_id = @@spid;


GO --
DECLARE @OutputFile NVARCHAR(100) ,    @FilePath NVARCHAR(100) ,    @bcpCommand NVARCHAR(1000)
SET @bcpCommand = 'bcp "SELECT * FROM sys.objects " queryout '
SET @FilePath = 'C:\test\'
SET @OutputFile = 'FileName1.txt'
SET @bcpCommand = @bcpCommand + @FilePath + @OutputFile + ' -c -t, -T -S'+ @@servername
exec master..xp_cmdshell @bcpCommand;
GO -- Case Sensitive.
BEGIN TRY
	SELECT 1/0;
END TRY
BEGIN CATCH
END CATCH
BEGIN TRY
	SELECT 1/0;
END TRY
BEGIN CATCH
END CATCH
BEGIN TRY
	PRINT 'EBC';
END TRY
BEGIN CATCH
END CATCH
--CREATE TABLE #ErrorLog (LogDate date, ProcInfo varchar(max), TextContent varchar(max));
TRUNCATE TABLE #ErrorLog;
INSERT INTO #ErrorLog EXEC sp_readerrorlog 0, 1, 'Error:', 'Severity:';
SELECT * FROM #ErrorLog;
GO --
SELECT DISTINCT Operation FROM fn_dblog(NULL, NULL);
SELECT DISTINCT * FROM fn_dblog(NULL, NULL);
SELECT [Current LSN], Operation, Context, [Transaction ID], [Begin time] FROM sys.fn_dblog(NULL, NULL);
SELECT [begin time], [rowlog contents 1], [Transaction Name], Operation FROM sys.fn_dblog(NULL, NULL);
GO --
SELECT * FROM sys.database_files;
--	Primary			The recommended file name extension for primary data files is .mdf.
--	Transaction Log	The recommended file name extension for transaction logs is .ldf.
SELECT * FROM fn_dump_dblog(NULL,NULL,'DISK',1
,'F:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSVRINS\MSSQL\DATA\mastlog.ldf'--'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Backup\AdventureWorks2012.bak'
,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL
,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL
,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL
,NULL,NULL,NULL,NULL,NULL,NULL);