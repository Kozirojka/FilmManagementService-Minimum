# FilmManagementService

## Movie Management System 🎥

A simple CRUD app for managing movies built with .NET Core and React.  
Users can view, add, edit, and delete movies, as well as perform sorting, filtering, and searching.

- **Backend**: ASP.NET Core (Minimal API) + EF Core + MediatR 
- **Architecture**: Clean Architecture + Vertical Slice Architecture
- **Frontend**: React + Vite
- **Database**: Postgres
- **Testing**: Unit tests with the xUnit library, using an in-memory database for mocks
- **Docs**: Swagger UI

---

## Versions
>*Backend*: .NET 9 (SDK .NET 9)

>*Frontend*: React 18.3.1

## Installation

**Clone the Git repository**:  
 **>** `git clone https://github.com/Kozirojka/FilmManagementService-Minimum.git`

### Backend

1. Go to the server folder and restore packages and dependencies   
**>** `dotnet restore`.

2. Provide your connection string in the appsettings.json or in a password manager

3. Execute the EF migrations  
**>** `dotnet ef database update`

4. Run the backend:
   Your backend should be running at https://localhost:7091.

---

### Frontend

1. Navigate to the client folder FilmMS-Client

2. Install dependencies with command  
**>** `npm install`

3. Run server  
**>** `npm run dev`

Your frontend should be running at
http://localhost:5173

## How to Use the Application

![alt text](./User-Attachments/image-6.png)
1. Open modal window for adding film when you press on PLUS

![alt text](./User-Attachments/image-1.png)
2. Possibility to edit and delete

![alt text](./User-Attachments/ezgif-47ff1d5ad4c4d.gif)
3. Pagination (.GIF)

![alt text](./User-Attachments/image-2.png)

4. Possibility filter by "Director" or "Title"  
(work with data that fetched from the server)


![alt text](./User-Attachments/image-3.png)
5. When we enter film ID in this field, it send a request to the server.  
As responce we receive information about the film with this ID


![alt text](./User-Attachments/ezgif-2c24e76aecf74.gif)
6. When we try to enter invalid data in "Rating" or   something not allowed in inputs, we receive an error (.GIF)  

![alt text](./User-Attachments/image-5.png)
7. Possibility to delete the film