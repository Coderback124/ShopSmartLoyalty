using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopSmartLoyalty
{
    public class Purchase
    {
        public string PurchaseID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string BranchCode { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }

        public Purchase(
            string purchaseID,
            DateTime purchaseDate,
            string branchCode,
            string category,
            decimal amount)
        {
            PurchaseID = purchaseID;
            PurchaseDate = purchaseDate;
            BranchCode = branchCode;
            Category = category;
            Amount = amount;
        }
    }

    public class BranchAlert
    {
        public string BranchCode { get; set; }
        public string StaffMemberName { get; set; }
        public string AlertType { get; set; }
        public string MessageContent { get; set; }
        public DateTime TimeStamp { get; set; }

        public BranchAlert(
            string branchCode,
            string staffMemberName,
            string alertType,
            string messageContent)
        {
            BranchCode = branchCode;
            StaffMemberName = staffMemberName;
            AlertType = alertType;
            MessageContent = messageContent;
            TimeStamp = DateTime.Now;
        }
    }

    public class Customer
    {
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string BranchCode { get; set; }
        public string MembershipTier { get; set; }
        public DateTime RegistrationDate { get; set; }

        public List<Purchase> PurchaseHistory { get; set; }

        public Customer(
            string customerID,
            string name,
            string province,
            string branchCode,
            DateTime registrationDate)
        {
            CustomerID = customerID;
            Name = name;
            Province = province;
            BranchCode = branchCode;
            RegistrationDate = registrationDate;
            MembershipTier = "Bronze";
            PurchaseHistory = new List<Purchase>();
        }

        public decimal CalculateTotalSpend()
        {
            decimal totalSpend = 0;

            foreach (Purchase purchase in PurchaseHistory)
            {
                totalSpend += purchase.Amount;
            }

            return totalSpend;
        }

        public int CalculateRewardPoints()
        {
            return (int)(CalculateTotalSpend() / 10);
        }

        public string DetermineMembershipTier()
        {
            decimal totalSpend = CalculateTotalSpend();

            if (totalSpend >= 5000)
            {
                return "Gold";
            }
            else if (totalSpend >= 1000)
            {
                return "Silver";
            }
            else
            {
                return "Bronze";
            }
        }

        public void AddPurchase(Purchase purchase)
        {
            try
            {
                if (purchase.Amount <= 0)
                {
                    throw new Exception("Purchase Amount must be greater than zero.");
                }

                PurchaseHistory.Add(purchase);

                MembershipTier = DetermineMembershipTier();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Purchase Error: " + ex.Message);
            }
        }
    }

    public class ShopSmartSystem
    {
        private Dictionary<string, Customer> customers;
        private List<BranchAlert> alerts;

        public ShopSmartSystem()
        {
            customers = new Dictionary<string, Customer>();
            alerts = new List<BranchAlert>();
        }

        public void RegisterCustomer(Customer customer)
        {
            try
            {
                if (customers.ContainsKey(customer.CustomerID))
                {
                    throw new Exception("Customer with this ID already exists.");
                }

                customers.Add(customer.CustomerID, customer);
                Console.WriteLine("Customer registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registration Error: " + ex.Message);
            }
        }

        public Customer FindCustomer(string customerID)
        {
            if (customers.ContainsKey(customerID))
            {
                return customers[customerID];
            }

            return null;
        }

        public void RecordPurchase(string customerID, Purchase purchase)
        {
            try
            {
                Customer customer = FindCustomer(customerID);

                if (customer == null)
                {
                    throw new Exception("Customer not found.");
                }

                customer.AddPurchase(purchase);

                Console.WriteLine("Purchase recorded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Purchase Error: " + ex.Message);
            }
        }

        public void RemoveCustomer(string customerID)
        {
            try
            {
                if (!customers.ContainsKey(customerID))
                {
                    throw new Exception("Customer not found.");
                }

                customers.Remove(customerID);

                Console.WriteLine("Customer removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Removal Error: " + ex.Message);
            }
        }

        public void UpdateCustomer(
            string customerID,
            string newName,
            string newProvince,
            string newBranchCode)
        {
            try
            {
                Customer customer = FindCustomer(customerID);

                if (customer == null)
                {
                    throw new Exception("Customer not found.");
                }

                customer.Name = newName;
                customer.Province = newProvince;
                customer.BranchCode = newBranchCode;

                Console.WriteLine("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error: " + ex.Message);
            }
        }

        public void TopCustomersReport()
        {
            var topCustomers = customers.Values
                .OrderByDescending(c => c.CalculateTotalSpend());

            Console.WriteLine("\n===== Top Customers Report =====");

            foreach (var customer in topCustomers)
            {
                Console.WriteLine(
                    customer.CustomerID + " | " +
                    customer.Name + " | R" +
                    customer.CalculateTotalSpend());
            }
        }

        public void SpendingByProvinceReport()
        {
            var provinceReport = customers.Values
                .GroupBy(c => c.Province)
                .Select(g => new
                {
                    Province = g.Key,
                    TotalSpend = g.Sum(c => c.CalculateTotalSpend())
                });

            Console.WriteLine("\n===== Spending By Province =====");

            foreach (var province in provinceReport)
            {
                Console.WriteLine(
                    province.Province + " | R" +
                    province.TotalSpend);
            }
        }

        public void SpendingByBranchReport()
        {
            var branchReport = customers.Values
                .GroupBy(c => c.BranchCode)
                .Select(g => new
                {
                    Branch = g.Key,
                    TotalSpend = g.Sum(c => c.CalculateTotalSpend())
                });

            Console.WriteLine("\n===== Spending By Branch =====");

            foreach (var branch in branchReport)
            {
                Console.WriteLine(
                    branch.Branch + " | R" +
                    branch.TotalSpend);
            }
        }

        public void CustomersByMembershipTierReport()
        {
            var tierReport = customers.Values
                .GroupBy(c => c.MembershipTier);

            Console.WriteLine("\n===== Customers By Membership Tier =====");

            foreach (var tier in tierReport)
            {
                Console.WriteLine("\n" + tier.Key);

                foreach (var customer in tier)
                {
                    Console.WriteLine(
                        customer.CustomerID + " | " +
                        customer.Name);
                }
            }
        }

        public void MonthlyPerformanceReport()
        {
            var monthlyReport = customers.Values
                .SelectMany(c => c.PurchaseHistory)
                .GroupBy(p => p.PurchaseDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSales = g.Sum(p => p.Amount),
                    PurchaseCount = g.Count()
                });

            Console.WriteLine("\n===== Monthly Performance Report =====");

            foreach (var month in monthlyReport)
            {
                Console.WriteLine(
                    "Month: " + month.Month +
                    " | Sales: R" + month.TotalSales +
                    " | Purchases: " + month.PurchaseCount);
            }
        }
        public void AddAlert(BranchAlert alert)
        {
            try
            {
                alerts.Add(alert);

                Console.WriteLine("Alert received successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alert Error: " + ex.Message);
            }
        }

        public void DisplayAlerts()
        {
            var sortedAlerts = alerts
                .OrderBy(a => a.TimeStamp);

            Console.WriteLine("\n===== Branch Alerts =====");

            foreach (var alert in sortedAlerts)
            {
                Console.WriteLine(
                    alert.TimeStamp + " | " +
                    alert.BranchCode + " | " +
                    alert.StaffMemberName + " | " +
                    alert.AlertType + " | " +
                    alert.MessageContent);
            }
        }

        public void FilterAlertsByType(string alertType)
        {
            var filteredAlerts = alerts
                .Where(a => a.AlertType == alertType);

            Console.WriteLine("\n===== Filtered Alerts =====");

            foreach (var alert in filteredAlerts)
            {
                Console.WriteLine(
                    alert.TimeStamp + " | " +
                    alert.BranchCode + " | " +
                    alert.AlertType + " | " +
                    alert.MessageContent);
            }
        }
    }

    public class AlertServer
    {
        public void StartServer()
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Loopback, 5000);

                server.Start();

                Console.WriteLine("Server started...");

                for (int i = 0; i < 2; i++)
                {
                    TcpClient client = server.AcceptTcpClient();

                    NetworkStream stream = client.GetStream();

                    byte[] buffer = new byte[1024];

                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine("Alert Received: " + message);

                    client.Close();
                }

                server.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Server Error: " + ex.Message);
            }
        }
    }

    public class AlertClient
    {
        public void SendAlert(string message)
        {
            try
            {
                TcpClient client = new TcpClient();

                client.Connect("127.0.0.1", 5000);

                NetworkStream stream = client.GetStream();

                byte[] data = Encoding.UTF8.GetBytes(message);

                stream.Write(data, 0, data.Length);

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client Error: " + ex.Message);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AlertServer server = new AlertServer();

            Task.Run(() => server.StartServer());

            Thread.Sleep(1000);

            AlertClient client1 = new AlertClient();
            AlertClient client2 = new AlertClient();

            client1.SendAlert("JHB0001 | Sipho | Network | Tower outage");
            client2.SendAlert("PE001 | Sarah | Security | Suspicious activity");
        }
    }
}