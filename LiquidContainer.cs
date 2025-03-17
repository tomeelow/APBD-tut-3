namespace container_manager;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(string serialNumber, int Height, int TareWeight, int Depth, int MaxPayload) : base(serialNumber, Height, TareWeight, Depth, MaxPayload)
    {
    }
}