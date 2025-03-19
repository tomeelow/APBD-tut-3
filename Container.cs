namespace container_manager;

public abstract class Container
{
    private int CargoMass { get; set; }
    private int Height { get; set; }
    private int TareWeight { get; set; }
    private int Depth { get; set; }
    private string SerialNumber { get; set; }
    private int MaxPayload { get; set; }
    
    private int ContainerIndex { get; set; }

    protected Container(String serialNumber, int Height, int TareWeight, int Depth, int MaxPayload)
    {
        SerialNumber = serialNumber;
        Height = this.Height;
        TareWeight = this.TareWeight;
        Depth = this.Depth;
        MaxPayload = this.MaxPayload;
        
        ContainerIndex += 1;
        SerialNumber += ContainerIndex.ToString();
    }

    public void UnloadContainer()
    {
        CargoMass = 0;
    }

    public virtual void LoadContainer(char PayloadType, string PayloadName, int PayloadMass)
    {

        if (CargoMass + PayloadMass > MaxPayload)
        {
            throw new OverfillException("Container is full");
        }

        if (CargoMass <= 0)
        {
            throw new EmptyCargoException("The mass of the cargo is too small");
        }
        
        CargoMass += PayloadMass;
        Console.WriteLine($"Loaded {PayloadMass} kg of {PayloadName} in container {SerialNumber}");
        
    }
}