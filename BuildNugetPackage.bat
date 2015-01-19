nuget Update -self
MsBuild.exe Adamantworks.Amazon.sln /p:Configuration=Release /p:Platform="Any CPU"
nuget pack DynamoDB/DynamoDB.nuspec