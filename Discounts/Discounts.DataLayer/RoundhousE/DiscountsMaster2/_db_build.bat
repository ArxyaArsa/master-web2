@echo off
setlocal ENABLEEXTENSIONS

SET database.name="DiscountsMaster2"
SET sql.files.directory="."
SET server.database="DESKTOP-VBOFR98"
SET db.user="master-web-2"
SET db.pass="master-web-2"
SET version.file="_db_build.xml"
SET version.xpath="//buildInfo/version"
SET environment="Developer"
SET ConnString="Data Source=%server.database%;Initial Catalog=%database.name%;User ID=%db.user%;Password=%db.pass%"

rh.exe /d=%database.name% /f=%sql.files.directory% /s=%server.database% /c=%ConnString% /vf=%version.file% /vx=%version.xpath% /env=%environment% /silent

pause