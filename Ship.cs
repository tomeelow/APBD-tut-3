namespace container_manager;

public class Ship
{
    public List<Container> Containers { get; set; }
    public int MaxSpeed { get; set; }
    public int MaxContainersNum { get; set; }
    public int MaxTotalWeight { get; set; }
        public Ship(int maxSpeed, int maxContainersNum, int maxTotalWeight)
    {
        MaxSpeed = maxSpeed;
        MaxContainersNum = maxContainersNum;
        MaxTotalWeight = maxTotalWeight;
        Containers = new List<Container>();
    }
    public bool AddContainer(Container container)
    {
        if (Containers.Count >= MaxContainersNum)
        {
            Console.WriteLine("Cannot add container: maximum container number reached.");
            return false;
        }
        if (GetTotalWeight() + container.TareWeight + container.CargoMass > MaxTotalWeight * 1000)
        {
            Console.WriteLine("Cannot add container: maximum total weight exceeded.");
            return false;
        }
        Containers.Add(container);
        Console.WriteLine($"Container {container.SerialNumber} added to ship.");
        return true;
    }

    // adds a list of containers
    public bool AddContainers(List<Container> containers)
    {
        bool allAdded = true;
        foreach (var container in containers)
        {
            bool added = AddContainer(container);
            if (!added)
                allAdded = false;
        }
        return allAdded;
    }

    // removes a container by its serial number
    public bool RemoveContainer(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            Containers.Remove(container);
            Console.WriteLine($"Container {serialNumber} removed from ship.");
            return true;
        }
        else
        {
            Console.WriteLine($"Container {serialNumber} not found on ship.");
            return false;
        }
    }

    // replaces a container with a new one after checking weight constraints
    public bool ReplaceContainer(string serialNumber, Container newContainer)
    {
        for (int i = 0; i < Containers.Count; i++)
        {
            if (Containers[i].SerialNumber == serialNumber)
            {
                int currentWeight = GetTotalWeight() - (Containers[i].TareWeight + Containers[i].CargoMass);
                if (currentWeight + newContainer.TareWeight + newContainer.CargoMass > MaxTotalWeight * 1000)
                {
                    Console.WriteLine("Cannot replace container: maximum total weight would be exceeded.");
                    return false;
                }
                Containers[i] = newContainer;
                Console.WriteLine($"Container {serialNumber} replaced with container {newContainer.SerialNumber}.");
                return true;
            }
        }
        Console.WriteLine($"Container {serialNumber} not found on ship.");
        return false;
    }

    // transfers a container from one ship to another ship
    public bool TransferContainerTo(Ship otherShip, string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            Console.WriteLine($"Container {serialNumber} not found on ship.");
            return false;
        }
        if (otherShip.AddContainer(container))
        {
            Containers.Remove(container);
            Console.WriteLine($"Container {serialNumber} transferred to another ship.");
            return true;
        }
        else
        {
            Console.WriteLine($"Transfer of container {serialNumber} failed due to constraints on target ship.");
            return false;
        }
    }

    // prints detailed information for a specific container
    public void PrintContainerInfo(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            container.PrintInfo();
        }
        else
        {
            Console.WriteLine($"Container {serialNumber} not found on ship.");
        }
    }

    // summary information for the ship and all its containers
    public void PrintShipInfo()
    {
        Console.WriteLine("Ship Information:");
        Console.WriteLine($"Max Speed: {MaxSpeed} knots, Max Containers: {MaxContainersNum}, Max Total Weight: {MaxTotalWeight} tons");
        Console.WriteLine($"Current Containers: {Containers.Count}");
        foreach (var container in Containers)
        {
            container.PrintInfo();
        }
        Console.WriteLine($"Total Weight on ship: {GetTotalWeight()} kg");
    }

    // calculate the total weight (tare + cargo) of all containers on the ship
    private int GetTotalWeight()
    {
        int total = 0;
        foreach (var container in Containers)
        {
            total += container.TareWeight + container.CargoMass;
        }
        return total;
    }
}

