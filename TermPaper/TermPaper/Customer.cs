
using System.Text.Json;

namespace TermPaper
{
    public class Customer : IShowInfo, ITalk
    {
        public string Name { get; set; }
        public FuelType FuelType { get; set; }
        public VehicleType VehicleType { get; set; }

        public Customer()
        {
            Name = string.Empty;
            FuelType = 0;
            VehicleType = 0;
        }
        public Customer(string name, FuelType fuelType, VehicleType vehicleType)
        {
            Name = name;
            FuelType = fuelType;
            VehicleType = vehicleType;
        }

        public void Greeting()
        {
            Console.WriteLine($"Hi there, i need to fuel my {VehicleType} with {FuelType}.");
        }

        public void Farewell()
        {
            Console.WriteLine("Thanks, bye.");
        }
        public static void ShowWholeInfo(Queue<Customer> customers)
        {
            if (!customers.Any())
            {
                Console.WriteLine("---");
                return;
            }
            foreach (var customer in customers)
            {
                customer.ShowInfo();
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Customer, Name: {Name}, Fuel Type: {FuelType} , Vehicle: {VehicleType}");
        }

        public static void WriteToJsonFile(Queue<Customer> customers, string path)
        {
            try
            {
                string jsonstring = "";
                foreach (var p in customers)
                {
                    jsonstring += p.WriteToJson();
                    jsonstring += "\n";
                }
                File.WriteAllText(path, jsonstring);
                Console.WriteLine($"\nCheck out the JSON file at: {Path.GetFullPath(path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected string WriteToJson()
        {
            return JsonSerializer.Serialize<Customer>(this);
        }

        public static Queue<Customer> ReadFromJsonFile(Queue<Customer> customers, string path)
        {
            try
            {
                int i = 0;
                List<string> lines = new();
                lines = File.ReadAllLines(path).ToList();

                /*Console.WriteLine("\nContents of JSON Account file:");
                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }
                */
                if (null == customers)
                {
                    customers = new Queue<Customer>();
                }
                foreach (var item in lines)
                {
                    try //(TryToReadFromJson(item))
                    {
                        Customer t = new();
                        if (null != item)
                        {

                            t = t.ReadFromJson(item);//JsonSerializer.Deserialize<IPerson>(item);

                            customers.Enqueue(t);
                        }
                        i++;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }
                Console.WriteLine("Total added: " + i);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"\nReading JSON file error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine("\n" + ex.Message + "\nFix the file text and try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
            return customers;
        }
        protected Customer ReadFromJson(string item)
        {
            Customer? customer = JsonSerializer.Deserialize<Customer>(item);
            if (null == customer)
            {
                throw new Exception($"Failed to read from .json: '{item}'");
            }
            return customer;
        }
    }
    public enum VehicleType
    {
        Truck = 0, Car = 1, Motorcycle
    }
}
