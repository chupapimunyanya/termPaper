

using System.Runtime.InteropServices;

namespace TermPaper
{
    public class GasStation : IShowInfo
    {
        public List<PetrolColumn> PetrolColumns { get; set; }
        public List<Worker> Workers { get; set; }
        public List<Administrator> Administrators { get; set; }
        public Queue<Customer> Customers { get; }
        public static int CustomersCounter { get; set; }

        public string Name { get; set; }
        public int Id { get; }

        private static int sourceId = 0;

        public GasStation(string name)
        {
            Name = name;
            Id = sourceId++;
            PetrolColumns = new List<PetrolColumn>();
            Workers = new List<Worker>();
            Administrators = new List<Administrator>();
            Customers = new Queue<Customer>();
            CustomersCounter = 0;
            Console.WriteLine();
            Console.WriteLine($"You've successfully opened new gas station '{name}'.");
        }

        public GasStation(List<string> info)
        {
            try
            {
                Name = info.ElementAt(0);
                Id = sourceId++;
                Workers = Person.ReadFromJsonFile(Workers, info.ElementAt(1));
                Administrators = Person.ReadFromJsonFile(Administrators, info.ElementAt(2));
                PetrolColumns = PetrolColumn.ReadFromJsonFile(PetrolColumns, info.ElementAt(3));
                Customers = Customer.ReadFromJsonFile(Customers, info.ElementAt(4));
                CustomersCounter = 0;
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Something went Wrong. Please try again.");
                return;
            }
            Console.WriteLine();
            Console.WriteLine($"You've successfully opened new gas station '{this.Name}'.");
        }

        public void HireAdministrator()
        {
            try
            {
                string name;
                string greetingMessage;
                Administrator newAdministrator = new();
                Console.Write("What's new administrotor's name?: ");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception();
                }
                Console.Write("What's new administrotor's age (all staff should be at least 17 y.o.)?: ");
                _ = int.TryParse(Console.ReadLine(), out int age);
                if (age < 17)
                {
                    throw new Exception();
                }
                Console.Write("What's new administrotor's gender (male '0' / female '1')?: ");
                _ = Gender.TryParse(Console.ReadLine(), out Gender gender);
                Console.Write("What's new administrotor's salary?: ");
                _ = double.TryParse(Console.ReadLine(), out double salary);
                if (salary < 0)
                {
                    throw new Exception();
                }
                Console.Write("How would new administrotor like to greet customers?: ");
                greetingMessage = Console.ReadLine();
                if (string.IsNullOrEmpty(greetingMessage))
                {
                    throw new Exception();
                }

                Administrators.Add(new(name, age, gender, salary, greetingMessage));
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Something went wrong. Please try again.");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Something went wrong. Please try again.");
                return;
            }
            Console.WriteLine();
            Console.WriteLine("New administrator hired.");
        }

        public void FireAdministrator()
        {
            if (!Administrators.Any())
            {
                Console.WriteLine("There are no administrotors on this gas station yet.");
                return;
            }
            try
            {
                Console.WriteLine("Here is the list of Administrators: ");
                int i = 0;
                foreach (Administrator administrator in Administrators)
                {
                    Console.WriteLine(i++ + " - " + administrator.Name);
                }
                Console.Write("Please choose the administrator you want to fire from list above by his id: ");
                _ = int.TryParse(Console.ReadLine(), out int inputId);
                Administrators.Remove(Administrators[inputId]);
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Something went wrong. Please try again.");
            }
            Console.WriteLine();
            Console.WriteLine("Administrator fired.");
        }

        public void AddCustomer()
        {
            if (!Workers.Any())
            {
                Console.WriteLine("You can't add customers until there are no workers.");
                return;
            }
            try
            {
                string name;
                Customer newCustomer = new();
                Console.Write("What's new customer's name?: ");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception();
                }
                Console.Write("What's new customer's fuel type? (Diesel '0', Petrol80 '1', Petrol92 '2', Petrol95 '3', Petrol98 '4'): ");
                _ = FuelType.TryParse(Console.ReadLine(), out FuelType fuelType);
                Console.Write("What's new customer's vehicle type? (Truck '0', Car '1', Motorcycle '2'): ");
                _ = VehicleType.TryParse(Console.ReadLine(), out VehicleType vehicle);
                Customers.Enqueue(new(name, fuelType, vehicle));
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Something went wrong. Please try again.");
                //return Workers;
            }
            Console.WriteLine();
            Console.WriteLine("New customer arrived.");
        }
        public void ShowInfo()
        {
            Console.WriteLine($"Gas station #{Id}:");
            Console.WriteLine($"Name: '{Name}'");
            Console.WriteLine("Administrators:");
            Person.ShowWholeInfo(Administrators);
            Console.WriteLine("Workers:");
            Person.ShowWholeInfo(Workers);
            Console.WriteLine("Columns:");
            PetrolColumn.ShowWholeInfo(PetrolColumns);
            Console.WriteLine("Current customer queue:");
            Customer.ShowWholeInfo(Customers);
            Console.WriteLine($"Customers served in total : {CustomersCounter}");
        }

        public bool FuelTheVehicle()
        {
            bool someCustomerServed = false;
            if (!Workers.Any())
            {
                Console.WriteLine("There are no workers to serve customers");
                return false;
            }

            foreach (Worker worker in Workers)
            {
                if (worker.IsFree)
                {
                    if (Customers.Any())
                    {
                        if (CheckIfFuelTypeMatch())
                        {
                            if (worker.FuelTheVehicle(Customers.First(), PetrolColumns))
                            {
                                Console.WriteLine($"Customer {Customers.First().Name} is being served...");
                                someCustomerServed = true;
                                Customers.Dequeue();
                                CustomersCounter++;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Customer {Customers.First().Name} did not find the right kind of fuel and left the gas station.");
                            someCustomerServed = true;
                            Customers.Dequeue();
                        }
                    }
                }
            }
            if (someCustomerServed && Customers.Any())
            {
                Console.WriteLine();
                Console.WriteLine("All workers or pumps are busy at the moment.");
            }
            return Customers.Any() || someCustomerServed;
        }

        private bool CheckIfFuelTypeMatch()
        {
            foreach (var column in PetrolColumns)
            {
                foreach (var fuelType in column.FuelTypes)
                {
                    if (fuelType == Customers.First().FuelType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
