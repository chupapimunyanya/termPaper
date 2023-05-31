using Newtonsoft.Json;
using TermPaper;

namespace AdministratorUnitTest
{
    [TestClass]
    public class AdministratorUnitTest1
    {
        [TestMethod]
        public void AddAdministraitor_Test()
        {
            List<Administrator> administrators = new()
            {
                new("Ben", 40, Gender.Male, 2500, "Hello there!")
            };
            Assert.IsTrue(administrators.Any());
        }

        [TestMethod]
        public void AddAdministratorsFromJsonFile_Test()
        {
            List<Administrator> administrators = new();
            string path = "adminsRead.json";

            administrators = Person.ReadFromJsonFile<Administrator>(administrators, path);
            Assert.IsTrue(administrators.Any());
        }

        [TestMethod]
        public void SaveAdministratorsToJsonFile_Test()
        {
            File.WriteAllText("admins.json", JsonConvert.SerializeObject(new object()));

            List<Administrator> administrators = new()
            {
                new Administrator("Alex", 18, Gender.Male, 300, "Hello there!")
            };
            string path = "admins.json";

            Person.WriteToJsonFile(administrators, path);

            Assert.IsTrue(new FileInfo("admins.json").Length != 0);
        }

        [TestMethod]
        public void FireWorker_Test()
        {
            List<Worker> workers = new()
            {
                new("Alex", 18, Gender.Male, 300, "Hello there!")
            };
            Administrator administrator = new("Liza", 23, Gender.Female, 320, "Hello there!");
            StringReader sr = new StringReader("0" + Environment.NewLine);
            Console.SetIn(sr);
            workers = administrator.FireWorker(workers);
            Assert.IsFalse(workers.Any());
        }

        [TestMethod]
        public void HireWorker_Test()
        {
            List<Worker> workers = new();
            Administrator administrator = new("Liza", 23, Gender.Female, 320, "Hello there!");
            StringReader sr = new StringReader("John" + Environment.NewLine +
                                           "19" + Environment.NewLine +
                                            "0" + Environment.NewLine +
                                            "300" + Environment.NewLine +
                                            "Hello!" + Environment.NewLine);
            Console.SetIn(sr);
            workers = administrator.HireWorker(workers);
            Assert.IsTrue(workers.Any());
        }

        [TestMethod]
        public void ChangeSalary_Test()
        {
            List<Worker> workers = new() {
                new("Alex", 18, Gender.Male, 300, "Hello there!")
            };
            Administrator administrator = new("Liza", 23, Gender.Female, 320, "Hello there!");
            StringReader sr = new("0" + Environment.NewLine + "400.5" + Environment.NewLine);
            Console.SetIn(sr);
            workers = administrator.ChangeSalary(workers);

            Assert.AreEqual(400.5, workers.ElementAt(0).Salary);
        }
    }
}