using TermPaper;

namespace GeneralTermPaperTest
{
    [TestClass]
    public class GeneralUnitTest1
    {
        [TestMethod]
        public void Greeting_Test()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            Worker worker = new("Alex", 18, Gender.Male, 300, "Hello there!");
            worker.Greeting();
            string expected = "Hello there!" + Environment.NewLine;

            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void Farewell_Test()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            Administrator administrator = new("Alex", 18, Gender.Male, 300, "Have a nice day!");
            administrator.Farewell();
            string expected = "Have a nice day!" + Environment.NewLine;

            Assert.AreEqual(expected, sw.ToString());
        }

        [TestMethod]
        public void ShowInfo_Test()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            Worker worker = new("Alex", 18, Gender.Male, 300, "Hello there!");
            worker.ShowInfo();

            string expected = "Position: 'Worker', Name: Alex, Gender: Male, Salary: 300, Greeting: 'Hello there!'" + Environment.NewLine;

            Assert.AreEqual(expected, sw.ToString());
        }
    }
}