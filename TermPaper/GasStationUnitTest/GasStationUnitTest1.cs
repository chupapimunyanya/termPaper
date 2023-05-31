using TermPaper;

namespace GasStationUnitTest
{
    [TestClass]
    public class GasStationUnitTest1
    {
        [TestMethod]
        public void AddGasEmptyStation_Test()
        {
            List<GasStation> gasStations = new()
            {
                new("name")
            };
            Assert.IsTrue(gasStations.Any());
        }

        [TestMethod]
        public void AddGasStationJson_Test()
        {
            StringReader sr = new("name,workersRead.json,adminsRead.json,columnsRead.json,customersRead.json" + Environment.NewLine);
            Console.SetIn(sr);

            GasStation gasStation = new(TermPaperUtilities.GetFiles(true));
            Assert.IsNotNull(gasStation);
        }

        [TestMethod]
        public void AddPetrolColumn_Test()
        {
            List<PetrolColumn> columns = new();
            List<FuelType> fuelTypes = new()
            {
                FuelType.Diesel,
                FuelType.Petrol92
            };
            columns.Add(new(fuelTypes));
            Assert.IsTrue(columns.Any());
        }
    }
}