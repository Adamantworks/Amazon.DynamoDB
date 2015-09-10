nuget Update -self
MsBuild.exe Adamantworks.Amazon.sln /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild
nuget pack DynamoDB/DynamoDB.nuspec