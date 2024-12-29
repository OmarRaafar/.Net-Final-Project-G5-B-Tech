Project Overview
This project is an e-commerce platform inspired by the B-Tech website. It aims to replicate its functionalities and user experience while introducing modern technologies and practices.

Features
Admin Dashboard: Built with ASP.NET Core MVC for managing products, categories, users, orders, and other backend functionalities.
Customer Frontend: Developed with Angular, consuming APIs for seamless interaction and a dynamic user interface.
RESTful APIs: Built using ASP.NET Core Web API to serve data to the Angular frontend.
Authentication & Authorization: Includes secure authentication using JWT tokens, with role-based access control.
Responsive Design: Ensures a user-friendly experience across all devices using modern CSS frameworks.
Product Search & Filters: Allows customers to search and filter products by categories, brands, and price ranges.
Shopping Cart: Includes 'Add to Cart' and 'Checkout' functionalities.
Order Management: Customers can view order history and track order statuses.
Payment Integration: Supports payment processing through third-party services (if applicable).
Technologies Used
Backend:
ASP.NET Core 6.0
Web API for frontend integration.
MVC Core for admin dashboard.
Entity Framework Core: Database management and migrations.
SQL Server: Database for storing application data.
Frontend:
Angular 15
Modular components for a scalable UI.
Two-way data binding for seamless interaction.
Bootstrap: For responsive and modern design.
Additional Tools:
JWT Authentication: Secure user authentication.
AutoMapper: For mapping models and DTOs.
Setup Instructions
Prerequisites
.NET 6 SDK
Node.js (for Angular frontend)
SQL Server
Backend Setup
Clone the repository:
bash
Copy code
git clone https://github.com/yourusername/your-repo.git
Navigate to the backend project directory:
bash
Copy code
cd YourBackendProject
Install dependencies:
bash
Copy code
dotnet restore
Configure the database connection string in appsettings.json.
Run the migrations:
bash
Copy code
dotnet ef database update
Start the server:
bash
Copy code
dotnet run
Frontend Setup
Navigate to the frontend directory:
bash
Copy code
cd YourFrontendProject
Install dependencies:
bash
Copy code
npm install
Start the development server:
bash
Copy code
ng serve
Access the application at http://localhost:4200.
