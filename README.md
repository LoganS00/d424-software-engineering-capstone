README intro
Card Shop Inventory Management System

A web application for a local trading card shop to track inventory, take customer hold requests, and generate reports. It is built with Blazor Server, .NET 8, Entity Framework Core, and SQLite.

Live demo: https://cardshopinventory.onrender.com (free tier, so the first load after inactivity can take about a minute, and data resets on redeploy)

Features

Customer side (no login required)

Search inventory by name, set, card number, or manufacturer, with paginated results
View price, quantity, condition, and photos
Request a hold on any item (single cards or sealed product)

Admin side (login required)

First-run setup screen that creates the admin account
Add, edit, and delete trading cards and sealed products, with image upload
Review and fulfill or cancel customer hold requests
Inventory report with title, timestamp, multiple columns, and totals
Design highlights
OOP: an abstract InventoryItem base class with Card and SealedProduct subclasses, demonstrating inheritance, polymorphism (GetDetails()), and encapsulation (price and quantity can never go negative)
Security: hashed admin passwords, cookie authentication, server-side authorization checks, and server-side validation of image uploads
Layered architecture: UI components, service interfaces, and a data layer connected through dependency injection
Testing: xUnit tests covering encapsulation, polymorphism, and CRUD, run against an isolated in-memory SQLite database
Tech stack

Blazor Server · .NET 8 · EF Core · SQLite · xUnit · Docker · Render
