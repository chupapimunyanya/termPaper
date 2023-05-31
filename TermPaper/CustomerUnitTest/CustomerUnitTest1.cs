using Newtonsoft.Json;
using TermPaper;

namespace CustomerUnitTest
{
    [TestClass]
    public class CustomerUnitTest1
    {
        [TestMethod]
        public void AddCustomer_Test()
        {
            Queue<Customer> customers = new();
            customers.Enqueue(new("Sam", FuelType.Petrol92, VehicleType.Car));
            Assert.IsTrue(customers.Any());
        }

        [TestMethod]
        public void AddCustomersFromJsonFile_Test()
        {
            Queue<Customer> customers = new();
            string path = "customersRead.json";

            customers = Customer.ReadFromJsonFile(customers, path);
            Assert.IsTrue(customers.Any());
        }

        [TestMethod]
        public void SaveCustomersToJsonFile_Test()
        {
            File.WriteAllText("customers.json", JsonConvert.SerializeObject(new object()));

            Queue<Customer> customers = new();

            customers.Enqueue(new Customer("Max", FuelType.Petrol92, VehicleType.Car));

            string path = "customers.json";

            Customer.WriteToJsonFile(customers, path);

            Assert.IsTrue(new FileInfo("customers.json").Length != 0);
        }
    }
}