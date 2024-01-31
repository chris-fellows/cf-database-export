using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using CFUtilities;
using CFUtilities.CSV;

namespace CFDatabaseExport
{
    class Database1Builder
    {
        public static void CreateDatabase1(string outputFolder)
        {
            Char delimiter = ',';
            CSVSettings settings = null;

            // Get data
            DataTable countriesData = GetCountriesData();
            DataTable productData = GetProductData();
            DataTable customerData = GetCustomersData(countriesData);
            DataTable orderData = GetOrderData(customerData);
            DataTable orderItemData = GetOrderItemData(orderData, productData);

            settings = new CSVSettings(Path.Combine(outputFolder, "Countries.txt"), delimiter, false, false);
            //settings.SetColumnValueQuoted(countriesData);
            CSVFile.WriteDataTable(settings, countriesData, true);

            settings = new CSVSettings(Path.Combine(outputFolder, "Products.txt"), delimiter, false, false);
            //settings.SetColumnValueQuoted(productData);            
            CSVFile.WriteDataTable(settings, productData, true);

            settings = new CSVSettings(Path.Combine(outputFolder, "Customers.txt"), delimiter, false, false);
            //settings.SetColumnValueQuoted(customerData);
            CSVFile.WriteDataTable(settings, customerData, true);

            settings = new CSVSettings(Path.Combine(outputFolder, "Orders.txt"), delimiter, false, false);
            //settings.SetColumnValueQuoted(orderData);
            CSVFile.WriteDataTable(settings, orderData, true);

            settings = new CSVSettings(Path.Combine(outputFolder, "OrderItems.txt"), delimiter, false, false);
            //settings.SetColumnValueQuoted(orderItemData);
            CSVFile.WriteDataTable(settings, orderItemData, true);
        }

        private static DataTable GetCustomersData(DataTable countriesData)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CustomerID", typeof(Int32));
            dataTable.Columns.Add("Name", typeof(String));
            dataTable.Columns.Add("Email", typeof(String));
            dataTable.Columns.Add("CountryID", typeof(Int16));
            dataTable.Columns.Add("Address1", typeof(String));
            dataTable.Columns.Add("Address2", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Postcode", typeof(String));
            dataTable.Columns.Add("Telephone", typeof(String));
            dataTable.Columns.Add("DateCreated", typeof(DateTime));

            Random random = new Random();
            int countryId = 1;

            for (int index = 0; index < 1000; index++)
            {
                DataRow row = dataTable.NewRow();
                row["CustomerID"] = index + 1;
                row["Name"] = string.Format("Customer {0}", index + 1);
                row["Email"] = string.Format("customer{0}@hotmail.co.uk", index + 1);
                row["CountryID"] = countryId;
                row["DateCreated"] = DateTime.Now.AddDays(-index * DateTime.Now.Second);
                row["Address1"] = GetRandomAddress1(random);
                row["Address2"] = "";
                row["State"] = GetRandomState(random, countryId);
                row["Postcode"] = "";
                row["Telephone"] = GetRandomPhoneNumber(random);
               
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private static string GetRandomState(Random random, int countryId)
        {         
            List<string> states = new List<string>();
            switch (countryId)
            {
                case 1:
                    states.Add("Berkshire");
                    states.Add("Buckinghamshire");
                    states.Add("Surrey");
                    break;      
                default:
                    states.Add("");
                    break;
            }



            return states[random.Next(0, states.Count - 1)];
        }

        private static string GetRandomAddress1(Random random)
        {         
            StringBuilder address = new StringBuilder("");
            address.Append(string.Format("{0} Some Road", random.Next(1, 300)));          
         
            return address.ToString();
        }

        private static string GetRandomPhoneNumber(Random random)
        {
            StringBuilder number = new StringBuilder("");
            while (number.Length < 10)
            {
                int digit = random.Next(0, 9);
                number.Append(digit.ToString());
            }

            return number.ToString();
        }

        private static DataTable GetCountriesData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CountryID", typeof(Int32));
            dataTable.Columns.Add("Name", typeof(String));
            dataTable.Columns.Add("Currency", typeof(String));

            dataTable.Rows.Add(DataTableUtilities.GetNewRow(dataTable, 1, "UK", "GBP"));
            dataTable.Rows.Add(DataTableUtilities.GetNewRow(dataTable, 2, "France", "EUR"));
            dataTable.Rows.Add(DataTableUtilities.GetNewRow(dataTable, 3, "Germany", "EUR"));
            dataTable.Rows.Add(DataTableUtilities.GetNewRow(dataTable, 4, "Italy", "EUR"));
            dataTable.Rows.Add(DataTableUtilities.GetNewRow(dataTable, 5, "Spain", "EUR"));
            dataTable.Rows.Add(DataTableUtilities.GetNewRow(dataTable, 6, "Sweden", "EUR"));
            dataTable.Rows.Add(DataTableUtilities.GetNewRow(dataTable, 7, "USA", "USD"));

            return dataTable;
        }

        private static DataTable GetUsersData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UserID", typeof(Int32));
            dataTable.Columns.Add("Name", typeof(String));
            dataTable.Columns.Add("Email", typeof(String));
            dataTable.Columns.Add("DateCreated", typeof(DateTime));

            for (int index = 0; index < 50; index++)
            {
                DataRow row = dataTable.NewRow();
                row["UserID"] = index + 1;
                row["Name"] = string.Format("User {0}", index + 1);
                row["Email"] = string.Format("user{0}@hotmail.co.uk", index + 1);
                row["DateCreated"] = DateTime.Now.AddDays(-index * DateTime.Now.Second);

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private static DataTable GetProductData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ProductID", typeof(Int32));
            dataTable.Columns.Add("Name", typeof(String));
            dataTable.Columns.Add("ItemCost", typeof(Double));

            Random random = new Random();
            for (int index = 0; index < 5000; index++)
            {
                double itemCost = random.Next(1, 100) - 0.01;
                DataRow row = dataTable.NewRow();
                row["ProductID"] = index + 1;
                row["Name"] = string.Format("Product {0}", row["ProductID"]);
                row["ItemCost"] = itemCost;

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private static DataTable GetOrderItemData(DataTable orderData, DataTable productData)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("OrderID", typeof(Int32));
            dataTable.Columns.Add("ProductID", typeof(Int32));
            dataTable.Columns.Add("Quantity", typeof(Int16));
            dataTable.Columns.Add("ItemCost", typeof(Double));

            Random random = new Random();
            for (int orderIndex = 0; orderIndex < orderData.Rows.Count; orderIndex++)
            {                
                int orderLineCount = random.Next(1, 5);     // Get number of lines for order
                List<int> productIndexUsed = new List<int>();
               
                for (int orderLine = 0; orderLine < orderLineCount; orderLine++)
                {
                    // Get product, prevent duplicates for same order
                    int productIndex = 0;
                    do
                    {
                        productIndex = random.Next(0, productData.Rows.Count - 1);   // Get product
                    } while (productIndexUsed.Contains(productIndex));

                    int quantityNumber = random.Next(1, 100);
                    int quantity = 1;
                    if (quantityNumber >= 97)
                    {
                        quantity = 3;
                    }
                    else if (quantityNumber >= 90)
                    {
                        quantity = 1;
                    }

                    DataRow row = dataTable.NewRow();
                    row["OrderID"] = orderData.Rows[orderIndex]["OrderID"];
                    row["ProductID"] = productData.Rows[productIndex]["ProductID"];
                    row["Quantity"] = quantity;
                    row["ItemCost"] = productData.Rows[productIndex]["ItemCost"];

                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        private static DataTable GetOrderData(DataTable customerData)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("OrderID", typeof(Int32));
            dataTable.Columns.Add("CustomerID", typeof(Int32));
            dataTable.Columns.Add("OrderDate", typeof(DateTime));
            dataTable.Columns.Add("Status", typeof(Int16));
            dataTable.Columns.Add("DispatchDate", typeof(DateTime));
            dataTable.Columns.Add("CancelDate", typeof(DateTime));
            dataTable.Columns.Add("Currency", typeof(String));

            Random random = new Random();
            for (int index = 0; index < 2000; index++)
            {
                int customerIndex = random.Next(0, customerData.Rows.Count - 1);

                DateTime orderDate = DateTime.Now.AddSeconds(-random.Next(100, 500000));

                DataRow row = dataTable.NewRow();
                row["OrderID"] = index + 1;
                row["CustomerID"] = customerData.Rows[customerIndex]["CustomerID"];
                row["OrderDate"] = orderDate;
                row["Currency"] = "GBP";

                // 0=Pending, 1=Dispatched, 2=Cancelled
                int orderStatus = 0;
                int orderNumber = random.Next(1, 100);
                if (orderNumber >= 95)
                {
                    orderStatus = 2;
                }
                else if (orderNumber >= 25)
                {
                    orderStatus = 1;
                }

                row["Status"] = orderStatus;
                switch (orderStatus)
                {
                    case 0:
                        break;
                    case 1:
                        DateTime dispatchDate = DateTime.Now;
                        do
                        {
                            dispatchDate = DateTime.Now.AddMinutes(-random.Next(0, 50000));
                        } while (dispatchDate <= orderDate);
                        row["DispatchDate"] = dispatchDate;
                        break;
                    case 2:
                        DateTime cancelDate = DateTime.Now;
                        do
                        {
                            cancelDate = DateTime.Now.AddMinutes(-random.Next(0, 50000));
                        } while (cancelDate <= orderDate);
                        row["CancelDate"] = cancelDate;
                        break;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

    }
}
