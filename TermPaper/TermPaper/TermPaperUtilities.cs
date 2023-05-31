

namespace TermPaper
{
    public static class TermPaperUtilities
    {
        public static void IsNeededToClear()
        {
            Console.ReadLine();
            Console.Clear();
        }

        public static List<string> GetFiles(bool toRead)
        {
            string toSplit;
            if (toRead)
            {
                Console.WriteLine("Input information about gas station. (Ex 'name,workers.json,admins.json,columns.json,customers.json')");
            }
            else
            {
                Console.WriteLine("Input files' names to where write information. (Ex 'workersWrite.json,adminsWrite.json,columnsWrite.json,customersWrite.json')");
            }
            toSplit = Console.ReadLine();
            List<string> info = toSplit.Split(',').ToList();
            return info;
        }

        public static int ChooseGasStation(List<GasStation> gasStations)
        {
            try
            {
                int Id;
                if (gasStations.Count == 1)
                {
                    return 0;
                }
                else if (gasStations.Count > 1)
                {
                    Console.WriteLine("Choose the gas station you want to interact with: ");
                    foreach (var gasStation in gasStations)
                    {
                        Console.WriteLine($"{gasStation.Id} - {gasStation.Name}");
                    }

                    _ = int.TryParse(Console.ReadLine(), out Id);
                    if (Id > gasStations.Count || Id < 0)
                    {
                        throw new Exception("Something went wrong.Please try again.");
                    }
                    return Id;
                }
                else
                {
                    throw new Exception("You don't have any gas stations opened yet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public static bool CheckIfGasStExists(List<GasStation> gasStations)
        {
            return (gasStations.Any() ? true : false);
        }

        public static bool CheckIfAdminExists(List<GasStation> gasStations, int gasStId)
        {

            return (gasStations.ElementAt(gasStId).Administrators.Any() ? true : false);
        }

        public static bool CheckIfWorkerExists(List<GasStation> gasStations, int gasStId)
        {
            return (gasStations.ElementAt(gasStId).Workers.Any() ? true : false);
        }

        public static int ChooseAdmin(List<Administrator> administrators)
        {
            try
            {
                if (administrators.Any() || administrators.Count > 0)
                {
                    Console.WriteLine("\nChoose the administrator you want to manage staff with: ");
                    int i = 0;
                    foreach (Administrator administrator in administrators)
                    {
                        Console.WriteLine(i++ + " - " + administrator.Name);
                    }
                    _ = int.TryParse(Console.ReadLine(), out int Id);
                    if (Id > administrators.Count || Id < 0)
                    {
                        throw new Exception("Something went Wrong.Please try again.");
                    }
                    return Id;
                }
                else
                {
                    throw new Exception("You don't have any administrators hired yet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                return -1;
            }
        }


        public static void ChangeSalaryCheck(GasStation gasStation, int adminId)
        {
            if (!gasStation.Administrators.Any())
            {
                Console.WriteLine("You can't change salaries without administrators.");
                return;
            }
            try
            {
                Console.Write("Whose salary you would like to change (worker '0' / administrator '1')?: ");
                int adminOrWorker = int.Parse(Console.ReadLine());
                if (adminOrWorker == 0)
                {
                    gasStation.Workers = gasStation.Administrators.ElementAt(adminId).ChangeSalary(gasStation.Workers);
                }
                else if (adminOrWorker == 1)
                {
                    gasStation.Administrators = gasStation.Administrators.ElementAt(adminId).ChangeSalary(gasStation.Administrators);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Something went wrong. Please try again.");
            }
        }

        public static void WriteAllToJson(GasStation gasStation)
        {
            List<string> fileNames = GetFiles(false);
            Worker.WriteToJsonFile(gasStation.Workers, fileNames.ElementAt(0));
            Administrator.WriteToJsonFile(gasStation.Administrators, fileNames.ElementAt(1));
            PetrolColumn.WriteToJsonFile(gasStation.PetrolColumns, fileNames.ElementAt(2));
            Customer.WriteToJsonFile(gasStation.Customers, fileNames.ElementAt(3));
        }
    }
}
