# PowerPoint Remote Web API
## Running the server locally
### Step 1. Have a MySQL database (or run the command below to run a MySQL docker instance)
```
docker run --name mysql -e MYSQL_ROOT_PASSWORD=my-secret-pw -d -p 3306:3306 mysql 
```
### Step 2. In command prompt, change the working directory to the project
```
cd ./PowerPointRemote/WebApi/PowerPointRemote.WebApi
```
### Step 3. Set secrets for development (Note: this will only work for the development build)
```
dotnet user-secrets set "MySql:User" "root"
dotnet user-secrets set "MySql:Password" "my-secret-pw"
dotnet user-secrets set "MySql:Server" "localhost"
dotnet user-secrets set "MySql:Database" "ppremote"
dotnet user-secrets set "Jwt:Secret" "SomeReallyStrongCrypticSecret"

```
### Step 4. Install Entity Framework Tools
```
dotnet tool install --global dotnet-ef
```
### Step 5. Update the database
```
dotnet ef database update
```