
using System.Text.Json;

namespace TermPaper
{
    public abstract class Person : IShowInfo, ITalk //IPerson
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public double Salary { get; set; }
        public string GreetingMessage { get; set; }
        public string farewellMessage = "Have a nice day!";
        public Person()
        {

            Name = string.Empty;
            Age = 0;
            Gender = Gender.Male;
            Salary = 0;
            GreetingMessage = string.Empty;
            //farewellMessage = string.Empty;
        }

        public Person(string name, int age, Gender gender, double salary, string greetingMessage)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Salary = salary;
            GreetingMessage = greetingMessage;
            //farewellMessage = "farewellMassege";
        }

        public void Greeting()
        {
            Console.WriteLine(GreetingMessage);
        }
        public void Farewell()
        {
            Console.WriteLine(farewellMessage);
        }

        public static void ShowWholeInfo<T>(List<T> people) where T : Person
        {
            if (!people.Any())
            {
                Console.WriteLine("---");
                return;
            }
            foreach (var person in people)
            {
                person.ShowInfo();
            }
        }
        public abstract void ShowInfo();

        public static void WriteToJsonFile<T>(List<T> people, string path) where T : Person, new()
        {
            try
            {
                string jsonstring = "";
                foreach (var p in people)
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

        protected abstract string WriteToJson();

        public static List<T> ReadFromJsonFile<T>(List<T> people, string path) where T : Person, new()
        {
            try
            {
                int i = 0;
                List<string> lines = new();
                lines = File.ReadAllLines(path).ToList();
                /*
                Console.WriteLine("\nContents of JSON Account file:");
                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }
                */
                if (null == people)
                {
                    people = new List<T>();
                }
                foreach (var item in lines)
                {
                    try //(TryToReadFromJson(item))
                    {
                        T t = new();
                        if (null != item)
                        {

                            t = (T)t.ReadFromJson(item);//JsonSerializer.Deserialize<IPerson>(item);

                            people.Add(t);
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
            return people;
        }

        protected abstract Person ReadFromJson(string item);
    }
    public enum Gender
    {
        Male, Female
    };
}
