

namespace TermPaper

{
    public interface ITalk
    {
        void Greeting();
        void Farewell();
    }
}
    /*
    public interface IPerson
    {
        string Name => throw new NotImplementedException();
        int Age => throw new NotImplementedException();
        Gender Gender => throw new NotImplementedException();
        double Salary => throw new NotImplementedException();
        string GreetingMessage => throw new NotImplementedException();
        void Greeting();
        void Farewell();
        void ChangeSalary(double newSalary);
        void ShowInfo();
        IPerson ReadFromJson(string item);
        string WriteToJson();
    }
    
    interface IAdministrator : IPerson
    {
        List<Worker> FireWorker(List<Worker> Workers, int id);
        List<Worker> HireWorker(List<Worker> Workers);
        List<Administrator> ReadFromFileJson(List<Administrator> Administrators, string path);
        void SaveToFileJson(List<Administrator> Administrators, string path);
    }

    interface IWorker : IPerson
    {
        Queue<Customer> FuelTheVehicle(Queue<Customer> Customers);
        List<Worker> ReadFromFileJson(List<Worker> Workers, string path);
        //static void SaveToFileJson(List<Worker> Workers, string path);
    }

    interface ICustomer
    {
        string Name => throw new NotImplementedException();
        FuelType FuelType => throw new NotImplementedException();
        int PumpId => throw new NotImplementedException();
        VehicleType VehicleType => throw new NotImplementedException();
        void Greeting();
        void Farewell();
        void ShowInfo();
    }
    
}*/
