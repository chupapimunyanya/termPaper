using Newtonsoft.Json;
using TermPaper;

namespace WorkerUnitTest
{
    [TestClass]
    public class WorkerUnitTest1
    {
        [TestMethod]
        public void AddWorker_Test()
        {
            List<Worker> workers = new List<Worker>();
            workers.Add(new("Alex", 18, Gender.Male, 300, "Hello there!"));
            Assert.IsTrue(workers.Any());
        }

        [TestMethod]
        public void AddWorkersFromJsonFile_Test()
        {
            List<Worker> workers = new();
            string path = "workersRead.json";

            workers = Person.ReadFromJsonFile<Worker>(workers, path);
            Assert.IsTrue(workers.Any());
            //object emptyObject = new object();
        }

        [TestMethod]
        public void SaveWorkersToJsonFile_Test()
        {
            File.WriteAllText("workers.json", JsonConvert.SerializeObject(new object()));

            List<Worker> workers = new()
            {
                new Worker("Alex", 18, Gender.Male, 300, "Hello there!")
            };
            string path = "workers.json";

            Person.WriteToJsonFile(workers, path);

            Assert.IsTrue(new FileInfo("workers.json").Length != 0);
        }

        [TestMethod]
        public void FuelTheVehicle_Test()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            /*
            List<Administrator> customers = new()
            {
                new("Liza", 23, Gender.Female, 320, "Hello there!"),
                new("Liza", 23, Gender.Female, 320, "Hello there!"),
                new("Liza", 23, Gender.Female, 320, "Hello there!")
            };

            List<Worker> workers = new() {
                new("Alex", 18, Gender.Male, 300, "Hello there!"),
                new("Alex", 18, Gender.Male, 300, "Hello there!"),
                new("Alex", 18, Gender.Male, 300, "Hello there!")
            };

            Queue<Customer> customers = new();
            customers.Enqueue(new("Alex", FuelType.Diesel, VehicleType.Truck));
            customers.Enqueue(new("Alex", FuelType.Petrol80, VehicleType.Motorcycle));
            customers.Enqueue(new("Alex", FuelType.Petrol92, VehicleType.Car));

            List<FuelType> fuelTypes = new() { 
            FuelType.Petrol95,
            FuelType.Petrol92,
            FuelType.Diesel,
            };

            

            List<PetrolColumn> petrolColumns = new() {
                new(0, fuelTypes),
                new(1, fuelTypes2),
            };

            Administrator.WriteToJsonFile(customers, "admins.test");
            Worker.WriteToJsonFile(workers, "workers.test");
            Customer.WriteToJsonFile(customers, "customers.test");
            PetrolColumn.WriteToJsonFile(petrolColumns, "columns.test");
            */

            List<FuelType> fuelTypes = new() {
            FuelType.Petrol95,
            FuelType.Petrol80,
            FuelType.Diesel,
            FuelType.Petrol92
            };

            List<PetrolColumn> columns = new()
            {
                new(fuelTypes)
            };
            //PetrolColumn.WriteToJsonFile(columns, "columnsRead.json");


            StringReader sr = new("name,workersRead.json,adminsRead.json,columnsRead.json,customersRead.json" + Environment.NewLine);
            Console.SetIn(sr);

            GasStation gasStation = new(TermPaperUtilities.GetFiles(true));

            //gasStation.PetrolColumns = columns;

            //Assert.AreEqual(gasStation.FuelTheVehicle(), "eror3");

            Assert.IsTrue(gasStation.FuelTheVehicle());
        }
    }
}