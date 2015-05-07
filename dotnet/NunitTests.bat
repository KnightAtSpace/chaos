@echo off
SETLOCAL
Set wd=%~dp0
Set orange=%~dp0\..\external\tools\nunit-orange-2-1\NUnitOrange.exe
Set nunit=%wd%..\external\tools\Nunit\nunit-console-x86.exe
Set resultDir=%~dp0Build\Debug\
Set nunitWorkParam=/work=%resultDir%
Set nunitConfDir=%wd%Test\Unit

echo --------------------------------------------------------------------------------
echo Running unit tests
echo --------------------------------------------------------------------------------
echo Execution dir: %~dp0

%nunit% %nunitConfDir%\Common.nunit /xml=CommonTest.xml %nunitWorkParam%
%nunit% %nunitConfDir%\UnitTests.nunit /xml=UnitTests.xml %nunitWorkParam%

echo --------------------------------------------------------------------------------
echo Generating HTML report
echo --------------------------------------------------------------------------------

%orange% %resultDir%

ENDLOCAL
@echo on