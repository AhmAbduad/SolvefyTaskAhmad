\- Node.js (LTS version). Check with node -v

\- Angular CLI: npm install -g @angular/cli

1\. Open a terminal in the frontend folder:
	cd src\AngularFrontend\solvefy-task

2\. Install the dependencies (first time only):
 	npm install

3\. Start the app:
	ng serve --open

4\. Open http://localhost:4200 in your browser.

The page reloads automatically when files change. Stop the server with Ctrl+C.
\### Notes

\- The frontend talks to the API at https://localhost:5001. Start the API first (see "Running the Backend" above).

\- If npm install fails, delete the node_modules folder and package-lock.json, run npm cache clean --force, and try again.


## Connecting Everything Together

The app has three parts that talk to each other:

**Angular (browser)** → **ASP.NET Core API** → **SQL Server**

### 1. Database (SQL Server)

The API uses Entity Framework Core to talk to SQL Server. The connection string is in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME\\SQLEXPRESS;Database=SolvefyTaskDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

**Before you run the API:**
1. Install SQL Server Express (or use LocalDB) and make sure it is running.
2. Replace `YOUR_SERVER_NAME\\SQLEXPRESS` with your own server name. For LocalDB, use `Server=(localdb)\\MSSQLLocalDB`.
3. Create the database tables:
   
   OPEN package Manger Console 
   Type Command (Add-Migration InitialMigration)
   then Type Command (update-database)

	it will automatically create the tables in the database 

The database is registered in `Program.cs` with `AddDbContext<ApplicationDbContext>` and `UseSqlServer(...)`, and it reads the connection string above.

### 2. Backend (ASP.NET Core API)

1. Start the API:

2. Look at the terminal for the address it is listening on, for example `https://localhost:7135`.
3. Open `https://localhost:7135/swagger` to test the API without the frontend. Swagger only works in Development mode.

**How the API is organised:** the controller receives the request, the service handles the logic, and the repository saves to the database. All three are registered in `Program.cs`.

**CORS:** the frontend runs on a different address from the API, so the browser blocks the calls unless the API allows them. `Program.cs` has a policy called `AngularPolicy` that allows `http://localhost:4200`, and it is turned on with `app.UseCors("AngularPolicy")`.

JUST change the program.cs file and appsetings.json file according to your browser requirements 

### 3. Frontend (Angular)

1. Open the Angular environment file and set the API address. It must match the address from step 2:
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'https://localhost:7135/api'
   };
   ```
2. Start Angular:
   ```
   cd src\AngularFrontend\solvefy-task
   npm install   or npm install --legacy-peer-deps 
   ng serve --open
   ```
3. Open http://localhost:4200.

The Angular service uses `HttpClient` to call the API: `GET /api/Equipment` loads the list, and `POST /api/Equipment` adds an item.

### Quick Check

1. SQL Server is running and the tables exist.
2. The API is running (check Swagger).
3. Angular is running and the Equipment page shows the list.

### If Something Goes Wrong

- **Can't connect to the database:** check the server name in the connection string and that SQL Server is running.
- **Table not found:** run `dotnet ef database update`.
- **CORS error in the browser:** check that the API is running and the policy allows `http://localhost:4200`.
- **Network error from Angular:** check that `apiUrl` uses the same port that the API prints when it starts. If the browser complains about the HTTPS certificate, run `dotnet dev-certs https --trust` once.


### Time Spent

- **I have worked on weekends (saturday + sunday )    7 hours each and (monday and Tuesday) 3 hours each because i have to go to my company because i am on notice 
- ** notice period


### Tools Used
- **I have used chatgpt ,  claude , cursor 