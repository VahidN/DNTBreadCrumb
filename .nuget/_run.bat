"%~dp0NuGet.exe" pack "..\DNTBreadCrumb\DNTBreadCrumb.csproj" -Prop Configuration=Release
copy "%~dp0*.nupkg" "%localappdata%\NuGet\Cache"

pause