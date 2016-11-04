
REM  https://msdn.microsoft.com/en-us/library/ms162806.aspx 
osql -E -S WSWINCNHZ1126\MSSQLSVRINS -d my_imdb -i "F:\TFS_gkgk.cui@outlook.com\CodeBench_Mainly4Experiment\HTRD\SQLDBUnitTestProject\SqlQuery_1.sql" -w 5000 -n

bcp "SELECT * FROM my_imdb.dbo.rtbl1m WHERE x < 10 FOR XML RAW, XMLSCHEMA, ELEMENTS" queryout F:\a-wn.xml -x -N -T -S WSWINCNHZ1126\MSSQLSVRINS  