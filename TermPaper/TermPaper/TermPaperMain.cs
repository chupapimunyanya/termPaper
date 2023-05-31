namespace TermPaper
{

    enum StartMenu { AddGasSt = 1, InteractWithGasSt, DeleteGasSt, Exit = 0 };
    enum AddGasStMenu { AddEmpty = 1, AddFromJson, Exit = 0 };
    enum InteractionWithGasStMenu { HireAdministrator = 1, FireAdministrator, ManageStaffViaAdmin, AddCustomer, StartGasSt, ShowInfo, WriteToJson, Exit = 0 };
    enum ManageStaffMenu { HireWorker = 1, FireWorker, ChangeSalary, Exit = 0 };


    class TermPaperMain
    {

        static StartMenu StartMenuFun()
        {
            StartMenu item;
            do
            {
                Console.WriteLine("\n1 - Open new gas station\n2 - Interact with gas station\n3 - Close gas station permanently\n0 - Exit");
            } while (!(StartMenu.TryParse(Console.ReadLine(), out item) && Enum.IsDefined(typeof(StartMenu), item)));
            return item;
        }

        static AddGasStMenu AddGasStMenuFun()
        {
            AddGasStMenu item;
            do
            {
                Console.WriteLine("\n1 - Add empty gas station\n2 - Add gas station by reading .json\n0 - Exit");
            } while (!(AddGasStMenu.TryParse(Console.ReadLine(), out item) && Enum.IsDefined(typeof(AddGasStMenu), item)));
            return item;
        }

        static InteractionWithGasStMenu InteractionWithGasStMenuFun()
        {
            InteractionWithGasStMenu item;
            do
            {
                Console.WriteLine("\n1 - Hire administrator\n2 - Fire administrator\n3 - Manage staff as administrator\n4 - Add new customers\n5 - Start gas station\n6 - Show full information\n7 - Write information to .json file\n0 - Exit");
            } while (!(InteractionWithGasStMenu.TryParse(Console.ReadLine(), out item) && Enum.IsDefined(typeof(InteractionWithGasStMenu), item)));
            return item;
        }

        static ManageStaffMenu ManageStaffMenuFun()
        {
            ManageStaffMenu item;
            do
            {
                Console.WriteLine("\n1 - Hire worker\n2 - Fire worker\n3 - Change salary\n0 - Exit");
            } while (!(ManageStaffMenu.TryParse(Console.ReadLine(), out item) && Enum.IsDefined(typeof(ManageStaffMenu), item)));
            return item;
        }


        static void Main(string[] args)
        {
            //TermPaperMain termPaperMain = new(); 
            List<GasStation> gasStations = new();

            StartMenu startMenuItem;
            AddGasStMenu addGasStMenuItem;
            InteractionWithGasStMenu interactionWithGasStMenuItem;
            ManageStaffMenu manageStaffMenuItem;

            do
            {
                startMenuItem = StartMenuFun();
                switch (startMenuItem)
                {
                    //AddGasStManu
                    case StartMenu.AddGasSt:
                        do
                        {
                            addGasStMenuItem = AddGasStMenuFun();
                            switch (addGasStMenuItem)
                            {
                                //add empty gas station

                                case AddGasStMenu.AddEmpty:
                                    Console.WriteLine();
                                    Console.Write("Input the name of the gas station: ");
                                    gasStations.Add(new(Console.ReadLine()));
                                    TermPaperUtilities.IsNeededToClear();
                                    break;

                                //add gas station by reading .json files

                                case AddGasStMenu.AddFromJson:
                                    Console.WriteLine();
                                    gasStations.Add(new(TermPaperUtilities.GetFiles(true)));
                                    TermPaperUtilities.IsNeededToClear();
                                    break;

                            }

                        } while (addGasStMenuItem != AddGasStMenu.Exit);
                        Console.Clear();
                        break;

                    //InteractionWithGasStMenu

                    case StartMenu.InteractWithGasSt:

                        //check if there are any gas stations

                        if (!TermPaperUtilities.CheckIfGasStExists(gasStations))
                        {
                            Console.WriteLine();
                            Console.WriteLine("There are no opened gas stations yet.");
                            TermPaperUtilities.IsNeededToClear();
                            break;
                        }
                        do
                        {
                            //choose gas station

                            interactionWithGasStMenuItem = InteractionWithGasStMenuFun();
                            Console.WriteLine();
                            int gasStId = TermPaperUtilities.ChooseGasStation(gasStations);
                            if (gasStId >= 0)
                            {
                                switch (interactionWithGasStMenuItem)
                                {
                                    case InteractionWithGasStMenu.HireAdministrator:
                                        Console.WriteLine();
                                        gasStations.ElementAt(gasStId).HireAdministrator();
                                        TermPaperUtilities.IsNeededToClear();
                                        break;

                                    case InteractionWithGasStMenu.FireAdministrator:
                                        Console.WriteLine();
                                        gasStations.ElementAt(gasStId).FireAdministrator();
                                        TermPaperUtilities.IsNeededToClear();
                                        break;

                                    case InteractionWithGasStMenu.ManageStaffViaAdmin:

                                        //check if there are any admins

                                        if (!TermPaperUtilities.CheckIfAdminExists(gasStations, gasStId))
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("There are no administrators working on this gas station.");
                                            TermPaperUtilities.IsNeededToClear();
                                            break;
                                        }
                                        do
                                        {
                                            manageStaffMenuItem = ManageStaffMenuFun();
                                            if(manageStaffMenuItem == ManageStaffMenu.Exit)
                                            {
                                                Console.Clear();
                                                break;
                                            }
                                            Console.WriteLine();
                                            int adminId = TermPaperUtilities.ChooseAdmin(gasStations.ElementAt(gasStId).Administrators);
                                            if (adminId >= 0)
                                            {
                                                switch (manageStaffMenuItem)
                                                {
                                                    case ManageStaffMenu.HireWorker:
                                                        Console.WriteLine();
                                                        gasStations.ElementAt(gasStId).Administrators.ElementAt(adminId).HireWorker(gasStations.ElementAt(gasStId).Workers);
                                                        TermPaperUtilities.IsNeededToClear();
                                                        break;

                                                    case ManageStaffMenu.FireWorker:
                                                        if(!TermPaperUtilities.CheckIfWorkerExists(gasStations, gasStId))
                                                        {
                                                            Console.WriteLine();
                                                            Console.WriteLine("There are no workers working on this gas station.");
                                                            TermPaperUtilities.IsNeededToClear();
                                                            break;
                                                        }
                                                        Console.WriteLine();
                                                        gasStations.ElementAt(gasStId).Administrators.ElementAt(adminId).FireWorker(gasStations.ElementAt(gasStId).Workers);
                                                        TermPaperUtilities.IsNeededToClear();
                                                        break;

                                                    case ManageStaffMenu.ChangeSalary:
                                                        Console.WriteLine();
                                                        TermPaperUtilities.ChangeSalaryCheck(gasStations.ElementAt(gasStId), adminId);
                                                        TermPaperUtilities.IsNeededToClear();
                                                        break;
                                                }
                                            }

                                            //TermPaperUtilities.IsNeededToClear();
                                        } while (manageStaffMenuItem != ManageStaffMenu.Exit);

                                        //TermPaperUtilities.IsNeededToClear();
                                        break;

                                    case InteractionWithGasStMenu.AddCustomer:
                                        Console.WriteLine();
                                        gasStations.ElementAt(gasStId).AddCustomer();
                                        TermPaperUtilities.IsNeededToClear();
                                        break;

                                    case InteractionWithGasStMenu.StartGasSt:
                                        Console.WriteLine();
                                        bool moreWork;
                                        do
                                        {
                                            moreWork = gasStations.ElementAt(gasStId).FuelTheVehicle();
                                            Thread.Sleep(3000);
                                            //Worker.FuelingCompleted.Invoke();
                                            Worker.OnIteration();

                                        } while (moreWork);
                                        Console.WriteLine();
                                        Console.WriteLine("All customers are served.");
                                        TermPaperUtilities.IsNeededToClear();
                                        break;

                                    case InteractionWithGasStMenu.ShowInfo:
                                        Console.WriteLine();
                                        gasStations.ElementAt(gasStId).ShowInfo();
                                        TermPaperUtilities.IsNeededToClear();
                                        break;

                                    case InteractionWithGasStMenu.WriteToJson:
                                        TermPaperUtilities.WriteAllToJson(gasStations.ElementAt(gasStId));
                                        TermPaperUtilities.IsNeededToClear();
                                        break;
                                }
                            }

                        } while (interactionWithGasStMenuItem != InteractionWithGasStMenu.Exit);
                        Console.Clear();
                        break;

                    case StartMenu.DeleteGasSt:
                        if (!TermPaperUtilities.CheckIfGasStExists(gasStations))
                        {
                            Console.WriteLine();
                            Console.WriteLine("There are no opened gas stations yet.");
                            TermPaperUtilities.IsNeededToClear();
                            break;
                        }
                        int gasStDelId = TermPaperUtilities.ChooseGasStation(gasStations);
                        gasStations.RemoveAt(gasStDelId);
                        Console.WriteLine();
                        Console.WriteLine("Gas station was closed.");
                        TermPaperUtilities.IsNeededToClear();
                        break;
                }

            } while (startMenuItem != StartMenu.Exit);
        }
    }
}