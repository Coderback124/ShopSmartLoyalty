# ShopSmart Loyalty

A C# console application for managing a retail loyalty programme. The application supports customer registration, purchase tracking, reward point calculation, membership tier management, managerial reporting using LINQ, and branch-to-head-office communication through TCP networking.

---

## Features

- Register and manage customers
- Record customer purchases
- Calculate reward points
- Automatically determine membership tiers
- Validate customer and purchase information
- Prevent duplicate customer registrations
- Search, update, and remove customer records
- Generate managerial reports using LINQ
- TCP server and client networking
- Branch alert management
- Filter alerts using LINQ
- Console-based user interface

---

## Reports

The application generates reports including:

- Top customers by total spending
- Spending by province
- Spending by branch
- Customers grouped by membership tier
- Monthly sales performance

---

## Networking

The networking component enables branch staff to send alerts to a central server.

Each alert contains:

- Branch Code
- Staff Member Name
- Alert Type
- Message Content
- Timestamp

Alerts are received, stored, displayed in timestamp order, and can be filtered using LINQ.

---

## Technologies

**Language**

- C#

**Framework**

- .NET 8

**Development Environment**

- Visual Studio 2022

**Concepts**

- Object-Oriented Programming
- Generic Collections
- LINQ
- TCP Networking
- Exception Handling

---

## Project Structure

```text
ShopSmartLoyalty
│
├── Program.cs
├── Customer.cs
├── Purchase.cs
├── BranchAlert.cs
├── ShopSmartSystem.cs
├── AlertServer.cs
└── AlertClient.cs
```

---

## Planned Enhancements

- SQL Server database integration
- User authentication
- Web dashboard
- REST API integration
- Mobile companion application
- Automated unit testing

---

## Author

**Alistair Josephs**
