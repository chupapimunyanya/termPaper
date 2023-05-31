using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TermPaper
{
    public class Administrator : Person
    {

        public Administrator() : base()
        {

        }

        public Administrator(string name, int age, Gender gender, double salary, string greetingMessage) : base(name, age, gender, salary, greetingMessage)
        {

        }

        public List<Worker> FireWorker(List<Worker> workers)
        {
            try
            {
                /*if (!workers.Any())
                {
                    Console.WriteLine("There are no workers yet.");
                    return workers;
                }*/

                Console.WriteLine("Here is the list of Workers: ");
                int i = 0;
                foreach (Worker worker in workers)
                {
                    Console.WriteLine(i++ + " - " + worker.Name);
                }
                Console.Write("Please choose the worker you want to fire from list above by his id: ");
                int.TryParse(Console.ReadLine(), out int inputId);
                workers.Remove(workers[inputId]);
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong. Please try again.");
            }
            Console.WriteLine("Worker was fired.");
            return workers;
        }

        public List<Worker> HireWorker(List<Worker> workers)
        {
            try
            {
                string name;
                int age;
                Gender gender;
                double salary;
                string greetingMessage;
                Worker newWorker = new();
                Console.Write("What's new worker's name?: ");
                name = Console.ReadLine();
                if (null == name)
                {
                    throw new Exception();
                }
                Console.Write("What's new worker's age?: ");
                int.TryParse(Console.ReadLine(), out age);
                Console.Write("What's new worker's gender (male '0' / female '1')?: ");
                Gender.TryParse(Console.ReadLine(), out gender);
                Console.Write("What's new worker's salary?: ");
                double.TryParse(Console.ReadLine(), out salary);
                Console.Write("How would new worker like to greet Customers?: ");
                greetingMessage = Console.ReadLine();
                if (null == greetingMessage)
                {
                    throw new Exception();
                }

                workers.Add(new(name, age, gender, salary, greetingMessage));
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong. Please try again.");
                //return Workers;
            }
            Console.WriteLine("New worker hired.");
            return workers;
        }

        public List<T> ChangeSalary<T>(List<T> people) where T : Person
        {
            try
            {
                bool adminOrWorker = people.GetType().Equals(typeof(Administrator));

                if (!people.Any() && adminOrWorker == false)
                {
                    Console.WriteLine("There are no workers yet.");
                    return people;
                }

                int i = 0;
                Console.WriteLine("Here is the list of " + (adminOrWorker ? "Administrators: " : "Workers: "));
                foreach (var person in people)
                {
                    Console.WriteLine(i++ + " - " + person.Name);
                }
                Console.Write("Please choose " + (adminOrWorker ? "administrator's" : "worker's") + " id to change his salary: ");
                int.TryParse(Console.ReadLine(), out int inputId);
                Console.Write("Set new salary for chosen person: ");
                double.TryParse(Console.ReadLine(), out double newSalary);

                people.ElementAt(inputId).Salary = newSalary;
                Console.WriteLine("Salary was changed.");
                return people;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong. Please try again.");
                return people;
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Position: 'Administrator', Name: {Name}, Gender: {Gender}, Salary: {Salary}, Greeting: '{GreetingMessage}'");
        }
        protected override Person ReadFromJson(string item)
        {

            Administrator? administrator = JsonSerializer.Deserialize<Administrator>(item);
            if (null == administrator)
            {
                throw new Exception($"Failed to read from .json: '{item}'");
            }
            return administrator;
        }
        protected override string WriteToJson()
        {
            return JsonSerializer.Serialize<Administrator>(this);
        }
    }
}

