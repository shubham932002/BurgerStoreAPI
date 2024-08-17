


# BurgerStoreAPI Documentation

**Author:** Shubham Shejwal

## Project Overview

BurgerStoreAPI is a web application designed to manage a burger store's operations, including managing menus, orders, and user accounts. The application is built using ASP.NET Core, Entity Framework Core, and includes both frontend and backend components.

## Installation and Setup

### Prerequisites

- .NET SDK 6.0 or later
- SQL Server (optional, for using with Entity Framework)
- Visual Studio 2022 or any other code editor

### Steps to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/ShubhamShejwal/BurgerStoreAPI.git
   cd BurgerStoreAPI
   ```

2. Restore the required packages:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the project:
   ```bash
   dotnet run
   ```

5. Access the application by navigating to `https://localhost:5001` in your browser.

## Project Structure

- **BurgerStoreAPI.sln**: The solution file for the project.
- **BurgerStoreAPI/**: The main project directory containing the API source code.
  - **Controllers/**: Contains API controllers for managing various resources like Admins, Cart Items, Menus, Orders, and Users.
  - **BusinessLayer/**: Contains service interfaces and implementations for business logic.
  - **Data/**: Contains the Entity Framework Core DbContext and migrations.
  - **Models/**: Contains data models representing the entities in the database.
  - **wwwroot/**: Contains static files for the frontend (HTML, CSS, JS, images).
- **BurgerStoreAPI.Test/**: Contains unit tests for the API.

## API Endpoints

### Admins Controller

- **GET /api/admins**: Retrieves a list of all admins.
- **POST /api/admins**: Adds a new admin.
- **PUT /api/admins/{id}**: Updates an existing admin.
- **DELETE /api/admins/{id}**: Deletes an admin.

### CartItems Controller

- **GET /api/cartitems**: Retrieves all cart items for a user.
- **POST /api/cartitems**: Adds a new item to the cart.
- **DELETE /api/cartitems/{id}**: Removes an item from the cart.

### Menus Controller

- **GET /api/menus**: Retrieves the menu of burgers.
- **POST /api/menus**: Adds a new burger to the menu.
- **PUT /api/menus/{id}**: Updates an existing burger.
- **DELETE /api/menus/{id}**: Deletes a burger from the menu.

### Orders Controller

- **GET /api/orders**: Retrieves all orders placed by users.
- **POST /api/orders**: Places a new order.
- **PUT /api/orders/{id}**: Updates an existing order.
- **DELETE /api/orders/{id}**: Cancels an order.

### Users Controller

- **GET /api/users**: Retrieves all users.
- **POST /api/users**: Registers a new user.
- **PUT /api/users/{id}**: Updates an existing user's information.
- **DELETE /api/users/{id}**: Deletes a user.

## Business Logic and Services

The business logic is encapsulated in service classes located in the `BusinessLayer/` directory. Each service implements an interface and is responsible for handling the core operations for a specific entity (e.g., Admins, CartItems, Menus).

## Frontend Integration

The static files for the frontend are located in the `wwwroot/` directory. This includes HTML, CSS, and JavaScript files that interact with the API. Each page corresponds to a specific functionality in the burger store:
- **mainpage.html**: The landing page where users can browse the menu.
- **menupage.html**: Displays the available burgers.
- **order.html**: Allows users to place an order.

## Testing

Unit tests are located in the `BurgerStoreAPI.Test/` directory. These tests ensure that the API endpoints and business logic work as expected. The tests cover various scenarios, including adding, updating, deleting, and retrieving data.

### Running Tests

To run the tests, use the following command:
```bash
dotnet test
```

## Credits

This project was created and documented by **Shubham Shejwal**. If you find any issues or have suggestions for improvements, feel free to reach out or submit a pull request.

---

**Note:** This documentation is intended to provide a clear understanding of the project's structure and functionality. Please refer to the code for more detailed implementation details.

