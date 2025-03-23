namespace container_manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Container Manager Application");

            // ----- Testing payload type consistency and capacity rules -----

            // Test 1: Liquid container – load non‑hazardous "Milk" then attempt to load a different payload "Fuel"
            LiquidContainer liquidContainer1 = new LiquidContainer(300, 2000, 600, 10000);
            try
            {
                // first load: non‑hazardous milk.
                liquidContainer1.LoadContainer('L', "Milk", 8000, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // Attempt to load a different payload ("Fuel") into the same container
            try
            {
                liquidContainer1.LoadContainer('L', "Fuel", 1000, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // test 2: Gas container – load hazardous "Helium" then try to load "Natural Gas"
            GasContainer gasContainer1 = new GasContainer(300, 2200, 600, 8000, 1.5);
            try
            {
                // First load: hazardous Helium
                gasContainer1.LoadContainer('G', "Helium", 3000, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //attempt to load a different payload ("Natural Gas") into the same container
            try
            {
                gasContainer1.LoadContainer('G', "Natural Gas", 500, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // test 3: Refrigerated container – load "Bananas" at a valid temperature
            // then try to load a different product "Fish"
            RefrigeratedContainer refrigeratedContainer1 = new RefrigeratedContainer(300, 2500, 600, 9000);
            try
            {
                refrigeratedContainer1.LoadContainer('C', "Bananas", 4000, 15);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // attempt to load a different product
            try
            {
                refrigeratedContainer1.LoadContainer('C', "Fish", 1000, 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // ----- Testing ship operations -----

            // create a ship with constraints: max speed 25 knots, up to 5 containers, and 50 tons max weight
            Ship ship1 = new Ship(25, 5, 50);
            
            // Add containers to the ship
            ship1.AddContainer(liquidContainer1);
            ship1.AddContainer(gasContainer1);
            ship1.AddContainer(refrigeratedContainer1);

            ship1.PrintShipInfo();

            // remove a container from the ship
            ship1.RemoveContainer(liquidContainer1.SerialNumber);

            // create a new liquid container and load it with Milk
            LiquidContainer liquidContainer2 = new LiquidContainer(300, 2000, 600, 10000);
            try
            {
                liquidContainer2.LoadContainer('L', "Milk", 7000, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //replace the gas container with the new liquid container
            ship1.ReplaceContainer(gasContainer1.SerialNumber, liquidContainer2);

            ship1.PrintShipInfo();

            //Create a second ship and transfer a container from ship1 to ship2
            Ship ship2 = new Ship(30, 4, 40);
            ship1.TransferContainerTo(ship2, refrigeratedContainer1.SerialNumber);

            Console.WriteLine("\nShip 1 info after transfer:");
            ship1.PrintShipInfo();
            Console.WriteLine("\nShip 2 info after transfer:");
            ship2.PrintShipInfo();

            // ----- Testing loading a list of containers onto a ship and unloading a specific container -----

            Console.WriteLine("\n--- Testing loading a list of containers and unloading a specific container ---");

            //create a new ship that will load multiple containers at once
            Ship ship3 = new Ship(20, 5, 60);

            // a list of containers
            List<Container> containerList = new List<Container>();

            LiquidContainer lc = new LiquidContainer(300, 2000, 600, 10000);
            try
            {
                lc.LoadContainer('L', "Water", 5000, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            GasContainer gc = new GasContainer(300, 2200, 600, 8000, 1.2);
            try
            {
                gc.LoadContainer('G', "Helium", 3000, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            RefrigeratedContainer rc = new RefrigeratedContainer(300, 2500, 600, 9000);
            try
            {
                //"Fish" requires a minimum temperature of 2°C -> Using 5°C is acceptable
                rc.LoadContainer('C', "Fish", 3000, 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            containerList.Add(lc);
            containerList.Add(gc);
            containerList.Add(rc);

            //load the entire list onto ship3
            ship3.AddContainers(containerList);

            ship3.PrintShipInfo();

            //unload a specific container
            Console.WriteLine($"\nUnloading container {lc.SerialNumber} on ship3:");
            lc.UnloadContainer();
            
            ship3.PrintContainerInfo(lc.SerialNumber);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
