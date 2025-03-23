namespace container_manager
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public LiquidContainer(int height, int tareWeight, int depth, int maxPayload)
            : base("L", height, tareWeight, depth, maxPayload)
        {
        }

        public override void LoadContainer(char payloadType, string payloadName, int payloadMass, bool hazardous = false)
        {

            if (LoadedPayload != null && LoadedPayload != payloadName)
            {
                Console.WriteLine(
                    $"Error: Liquid container {SerialNumber} already contains {LoadedPayload}. Cannot load {payloadName}.");
                return;
            }

            if (IsHazardousCargo != null && IsHazardousCargo != hazardous)
            {
                Console.WriteLine(
                    $"Error: Liquid container {SerialNumber} already loaded with {(IsHazardousCargo.Value ? "hazardous" : "non-hazardous")} cargo. Cannot load cargo with a different hazardous flag.");
                return;
            }

            if (LoadedPayload == null)
            {
                LoadedPayload = payloadName;
                IsHazardousCargo = hazardous;
            }
            
            int allowed = hazardous
                ? (MaxPayload * 50 / 100)
                : (MaxPayload * 90 / 100);

            if (CargoMass + payloadMass > allowed)
            {
                throw new OverfillException(
                    $"Cannot load {payloadMass} kg of {payloadName} into liquid container {SerialNumber}. " +
                    $"Allowed capacity is {allowed} kg.");
            }

            CargoMass += payloadMass;
            Console.WriteLine(
                $"Loaded {payloadMass} kg of {payloadName} into liquid container {SerialNumber}.");

            if (hazardous)
            {
                Notify();
            }
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
                $"Liquid Container Serial: {SerialNumber}, CargoMass: {CargoMass} kg, MaxPayload: {MaxPayload} kg, Payload: {payloadDisplay}, {hazardDisplay}");
        }
    }
}
