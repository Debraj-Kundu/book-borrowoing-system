# How to setup

- Open the .sln file

- Install all the necessary nuget packages from .csproj file

- Find the database connection string in **appsettigns.json** file and change the details accourding to your system it is using windows authentication for sql server

- Restore the db backup file

- Run the server on at port [7167]

---

- Open **book_borrowing_system_client**

- Install all npm packages using

```bash
npm install
```

- You can change the port of base url for the server in **environments** folder
  inside

```bash
baseurl: 'https://localhost:[7167]/api'
```

- To serve use

```bash
ng serve
```

Seeded user details

Username = "User1", Password = "User@123"

Username = "User2", Password = "User@123"

Username = "User3", Password = "User@123"

Username = "User4", Password = "User@123"
