NAnt -t:net-3.5  -f:build.nant -D:msbuild.logger=Kobush.Build.Logging.XmlLogger,Kobush.Build.dll

if NOT ERRORLEVEL 0 exit

