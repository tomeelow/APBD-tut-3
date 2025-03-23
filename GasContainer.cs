namespace container_manager
{
    public class GasContainer : Container, IHazardNotifier
    {
        // additional property for gas pressure (in atmospheres)
        public double Pressure { get; set; }

        public GasContainer(int height, int tareWeight, int depth, int maxPayload, double pressure)
            : base("G", height, tareWeight, depth, maxPayload)
        {
            Pressure = pressure;
        }

        public override void LoadContainer(char payloadType, string payloadName, int payloadMass, bool hazardous = false)
        {
            if (LoadedPayload != null && LoadedPayload != payloadName)
            {
                Console.WriteLine(
                    $"Error: Gas container {SerialNumber} already contains {LoadedPayload}. Cannot load {payloadName}.");
                return;
            }

            if (IsHazardousCargo != null && IsHazardousCargo != hazardous)
            {
                Console.WriteLine(
                    $"Error: Gas container {SerialNumber} already loaded with {(IsHazardousCargo.Value ? "hazardous" : "non-hazardous")} cargo. Cannot load cargo with a different hazardous flag.");
                return;
            }
            
            if (LoadedPayload == null)
            {
                LoadedPayload = payloadName;
                IsHazardousCargo = hazardous;
            }
            
            if (CargoMass + payloadMass > MaxPayload)
            {
                throw new OverfillException(
                    $"Cannot load {payloadMass} kg of {payloadName} into gas container {SerialNumber}. " +
                    $"Max payload is {MaxPayload} kg.");
            }

            CargoMass += payloadMass;
            Console.WriteLine($"Loaded {payloadMass} kg of {payloadName} into gas container {SerialNumber}.");

            if (hazardous)
            {
                Notify();
            }
        }

        // when unloading a gas container 5% of its cargo remains
        public override void UnloadContainer()
        {
            int remain = (int)Math.Ceiling(CargoMass * 0.05);
            CargoMass = remain;


            if (CargoMass == 0)
            {
                LoadedPayload = null;
            }

            Console.WriteLine(
                $"Gas container {SerialNumber} unloaded. 5% remains: {remain} kg.");
        }

        public void Notify()
        {
            Console.WriteLine($"Hazardous operation in progress in container {SerialNumber}.");
        }

        public override void PrintInfo()
        {
            var payloadDisplay = LoadedPayload ?? "(none)";
            var hazardDisplay = IsHazardousCargo.HasValue && IsHazardousCargo.Value ? "Hazardous" : "Non-hazardous";
            Console.WriteLine(
                $"Gas Container Serial: {SerialNumber}, CargoMass: {CargoMass} kg, MaxPayload: {MaxPayload} kg, Pressure: {Pressure} atm, Payload: {payloadDisplay}, {hazardDisplay}");
        }
    }
}
