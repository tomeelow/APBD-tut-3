namespace container_manager;

public class RefrigeratedContainer : Container
{
    public Dictionary<string, double> productTemperatures = new Dictionary<string, double>
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18.0 },
        { "Fish", 2.0 },
        { "Meat", -15.0 },
        { "Ice cream", -18.0 },
        { "Frozen pizza", -30.0 },
        { "Cheese", 7.2 },
        { "Sausages", 5.0 },
        { "Butter", 20.5 },
        { "Eggs", 19.0 }
    };


    public RefrigeratedContainer(string serialNumber, int Height, int TareWeight, int Depth, int MaxPayload) : base(serialNumber, Height, TareWeight, Depth, MaxPayload)
    {
    }

    public void LoadContainer(char PayloadType, string PayloadName, int PayloadMass, int Temp)
    {
        
    }
}