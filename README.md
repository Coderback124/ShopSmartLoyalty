# ShopSmart Loyalty

A C# console application that simulates a retail loyalty management system. The application allows customer registration, purchase tracking, reward point calculation, membership tier management, reporting using LINQ, and branch-to-head-office communication through TCP networking.

---

## Features

- Customer registration and management
- Purchase history tracking
- Reward points calculation
- Automatic membership tier assignment
- Customer validation and exception handling
- Duplicate customer prevention
- Customer search, update, and removal
- Managerial reports using LINQ
- TCP server and client networking
- Branch alert messaging
- Alert filtering using LINQ

---

## Technologies

- C#
- .NET 8
- Visual Studio 2022
- LINQ
- TCP Networking (TcpListener & TcpClient)
- Generic Collections
- Object-Oriented Programming

---

## Reports

The application generates reports including:

- Top customers by spending
- Spending by province
- Spending by branch
- Customers grouped by membership tier
- Monthly sales performance

---

## Networking

The application includes a TCP server that receives branch alerts from multiple simulated clients. Alerts include:

- Branch Code
- Staff Member
- Alert Type
- Message Content
- Timestamp

Alerts can be filtered and displayed using LINQ.

---

## Future Improvements

- Persistent database storage using SQL Server
- User authentication
- Graphical user interface (WPF or WinForms)
- REST API integration
- Real-time dashboard
- Unit testing with xUnit

---

## Author

**Alistair Josephs**
