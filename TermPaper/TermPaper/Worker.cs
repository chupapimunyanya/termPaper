
using System.Data.Common;
using System.Text.Json;

namespace TermPaper
{
    public delegate void Notify();
    public class Worker : Person
    {
        public static event Notify FuelingCompleted;

        public bool IsFree { get; set; }
        private Customer? currentCustomer;
        private PetrolColumn? currentColumn;
        public Worker() : base()
        {
            IsFree = true;
        }
        public Worker(string name, int age, Gender gender, double salary, string greetingMessage) : base(name, age, gender, salary, greetingMessage)
        {
            IsFree = true;
        }


        public bool FuelTheVehicle(Customer customer, List<PetrolColumn> columns)
        {
            if (!IsFree)
            {
                return false;
            }

            foreach (PetrolColumn column in columns)
            {
                if (column.IsFree)
                {
                    foreach (var fuelType in column.FuelTypes)
                    {
                        if (fuelType == customer.FuelType)
                        {
                            IsFree = false;
                            currentColumn = column;
                            currentCustomer = customer;
                            currentColumn.IsFree = false;
                            //Thread.Sleep(3000);
                            //Console.WriteLine($"Customer {customer.Name} was successfully served!");
                            FuelingCompleted += FuelingIsFinished;
                            //FuelingCompleted.Invoke();

                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void OnIteration()
        {
            FuelingCompleted?.Invoke();
        }

        protected void FuelingIsFinished()
        {
            //Thread.Sleep(3000);
            Console.WriteLine($"Customer {currentCustomer.Name} was successfully served!");
            currentColumn.IsFree = true;
            IsFree = true;
            currentColumn = null;
            currentCustomer = null;
            FuelingCompleted -= FuelingIsFinished;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Position: 'Worker', Name: {Name}, Gender: {Gender}, Salary: {Salary}, Greeting: '{GreetingMessage}'");
        }
        protected override Person ReadFromJson(string item)
        {
            Worker? worker = JsonSerializer.Deserialize<Worker>(item);
            if (null == worker)
            {
                throw new Exception($"Failed to read from .json: '{item}'");
            }
            return worker;
        }

        protected override string WriteToJson()
        {
            return JsonSerializer.Serialize<Worker>(this);
        }
    }
}
