@echo off

if "%1" EQU "" goto usage

SET FullUpdateFile=%1

echo Write full update file header
type nul > fullUpdateTemp.tmp
echo. >> fullUpdateTemp.tmp

rem ******************** Create table index *********************
copy /Y /B fullUpdateTemp.tmp + "CreateScript\Index.sql" fullUpdateTemp.tmp || go errors

rem ******************** Create views *********************
copy /Y /B fullUpdateTemp.tmp + "CreateScript\View.sql" fullUpdateTemp.tmp || go errors

rem ******************** Create function scripts *********************
echo. >> fullFunctions.tmp
echo Create function scripts by name
rem File names are sorted by creation date
for /F %%f in ('dir "CreateScript\Function\*.sql" /B /O:N /A:-D-H-S') do (
	echo PRINT 'Create function %%f'
	copy /Y /B fullFunctions.tmp + "CreateScript\Function\%%f" fullFunctions.tmp || go errors
	echo. >> fullFunctions.tmp
)
copy /Y /B fullUpdateTemp.tmp + fullFunctions.tmp fullUpdateTemp.tmp || goto errors
del fullFunctions.tmp

rem ******************** Create StoredProcedure scripts *********************
echo. >> fullStoredProcedure.tmp
echo Create StoredProcedure scripts by name
rem File names are sorted by creation date
for /F %%f in ('dir "CreateScript\StoredProcedure\*.sql" /B /O:N /A:-D-H-S') do (
	echo PRINT 'Create StoredProcedure %%f'
	copy /Y /B fullStoredProcedure.tmp + "CreateScript\StoredProcedure\%%f" fullStoredProcedure.tmp || go errors
	echo. >> fullStoredProcedure.tmp
)
copy /Y /B fullUpdateTemp.tmp + fullStoredProcedure.tmp fullUpdateTemp.tmp || goto errors
del fullStoredProcedure.tmp

rem ******************** Final actions ********************
echo Full update file size = %~z1
echo Clear full update file before copying
type nul > "%FullUpdateFile%" || goto errors
echo New size = %~z1

echo Copy full update file to the destination
copy fullUpdateTemp.tmp "%FullUpdateFile%" || goto errors
echo Final size = %~z1

del fullUpdateTemp.tmp || goto errors

echo Full update created

goto finish

REM: error handler
:errors
echo.
echo WARNING! Error(s) were detected!
echo --------------------------------
echo Please evaluate the situation and, if needed,
echo restart this command file. You may need to
echo supply command parameters when executing
echo this command file.
echo.
pause
goto finish

REM: How to use screen
:usage
echo.
echo Usage: CreateDbSchema.cmd fullCreateName
echo.
echo Example: MyScript.cmd full_create_db_script.sql
echo.
pause
goto finish

:finish
echo.
echo Script execution is complete!
