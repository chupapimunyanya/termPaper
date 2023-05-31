
using System.Text.Json;
using TermPaper;

namespace TermPaper
{
    public class PetrolColumn : IShowInfo
    {
        private int ColumnId { get; set; }
        private static int sourceId = 0;
        public List<FuelType> FuelTypes { get; set; }
        public bool IsFree { get; set; }
        public PetrolColumn()
        {
            ColumnId = sourceId++;
            FuelTypes = new List<FuelType>();
            IsFree = true;
        }

        public PetrolColumn(List<FuelType> fuelTypes)
        {
            ColumnId = sourceId++;
            FuelTypes = fuelTypes;
            IsFree = true;
        }

        public static void ShowWholeInfo(List<PetrolColumn> columns)
        {
            if (!columns.Any())
            {
                Console.WriteLine("---");
                return;
            }
            foreach (var column in columns)
            {
                column.ShowInfo();
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Petrol column #{ColumnId}.\n Types of petrol:");
            foreach (var fuelType in FuelTypes)
            {
                Console.Write($"{fuelType}\t");
            }
            Console.WriteLine();
        }

        public static void WriteToJsonFile(List<PetrolColumn> columns, string path)
        {
            try
            {
                string jsonstring = "";
                foreach (var p in columns)
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
            return JsonSerializer.Serialize<PetrolColumn>(this);
        }

        public static List<PetrolColumn> ReadFromJsonFile(List<PetrolColumn> columns, string path)
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
                if (null == columns)
                {
                    columns = new List<PetrolColumn>();
                }
                foreach (var item in lines)
                {
                    try //(TryToReadFromJson(item))
                    {
                        sourceId -= 1;
                        PetrolColumn t = new();
                        if (null != item)
                        {

                            t = t.ReadFromJson(item);//JsonSerializer.Deserialize<IPerson>(item);
                            //t.ColumnId -= 1;
                            columns.Add(t);
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
            return columns;
        }
        protected PetrolColumn ReadFromJson(string item)
        {
            PetrolColumn? column = JsonSerializer.Deserialize<PetrolColumn>(item);
            if (null == column)
            {
                throw new Exception($"Failed to read from .json: '{item}'");
            }
            return column;
        }
    }


    public enum FuelType { Diesel = 0, Petrol80, Petrol92, Petrol95, Petrol98 }
}
