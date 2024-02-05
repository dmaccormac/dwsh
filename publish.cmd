dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true
copy dwsh\bin\Release\net8.0\win-x64\publish\dwsh.exe dwsh\dist\