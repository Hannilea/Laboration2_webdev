TownSquare - A Local Events Boaard 

Overview
TownSquare is a web application designed to help community memmebrs to discover and share local events. 

Technologies Used:
- ASP.NET Core MVC: used to build dynamic web applications
- Entity FrameWork Core: ORM for handling database operations (CRUD)
- ASP.NET Identity: User authentication and authorization
- Bootstrap: FE framework for building responsive design
- External API integration - integration with external public APIs, Picsum

Feartures
1. Event Management
- CRUD Operations: Register Users can create, read, update, and delete their own events.
- Event Details: Events include title, description, date, time, location, and category.
- Filter: Users can filter events by totals attending or date.
- Anonymous can views the event, how many person gonna join the event, but not RSVP to the events, can either create och edit the events

2. User Authentication and Profiles
- Registration and Login: Users can register and log in using ASP.NET Identity.
- User Profile: Each user has a personal profile to manage their created events.
- Roles: There are two roles – Admin and User. Admins can remove users och delete events, while users can manage their own events.

3. RSVP System
- Anonymous Viewing: Users can view events but cannot RSVP unless they are logged in.
- RSVP Management: Logged-in users can RSVP and manage their RSVP status.
- Notifications: Event creators will receive notifications when someone RSVPs to their event.

4. External API Integration
- The app integrates external APIs (Picsum), a random pic will show in the events datails

5. User-Friendly Interface
- Custom UI design using Bootstrap to ensure a responsive, user-friendly interface.

6. Roles and Administration
- Admin Features: Admins have the ability to remove users, and delete events (events created by deleted users). ANd also able to create och edit their own events likes usual user. 
- User can create, edit their own events, SRVP other evtents or cancel if they change their mind.

Setup introductions
To run this project locally, you 'll need:
- Vs, VScode or any preferred IDE
- .NET 6.0 or later
- SQLite

1. Clone the repository
   git clone https://github.com/Hannilea/Laboration2_webdev.git
   cd Laboration2_webdev
   cd TownSquareAuth
2. Install Dependencies
   Before running the application, run the following command to restore dependencies (NuGet Packages)
   dotnet restore
3. Databbase
   This application is using SQLite so you dont need to worry about configure the connection string to point SQLite. 
4. Run Migrations
   Run the following commands to apply database migrations
   dotnet ef migrations add InitialCreate
   dotnet ef database update
5. Running the application
   dotnet run





