namespace container_manager
{
    public class RefrigeratedContainer : Container
    {
        // predefined products and their required minimum temperatures (°C)
        public Dictionary<string, double> ProductMinTemperatures = new Dictionary<string, double>
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

        public int ContainerTemperature { get; set; }

        public RefrigeratedContainer(int height, int tareWeight, int depth, int maxPayload)
            : base("C", height, tareWeight, depth, maxPayload)
        {
        }

        // overloaded load method that includes a temperature parameter
        public void LoadContainer(char payloadType, string payloadName, int payloadMass, int temp)
        {

            if (LoadedPayload != null && LoadedPayload != payloadName)
            {
                Console.WriteLine(
                    $"Error: Refrigerated container {SerialNumber} already contains {LoadedPayload}. Cannot load {payloadName}.");
                return;
            }
            // check temperature requirement
            if (ProductMinTemperatures.ContainsKey(payloadName))
            {
                double requiredTemp = ProductMinTemperatures[payloadName];
                if (temp < requiredTemp)
                {
                    Console.WriteLine(
                        $"Error: Temperature {temp}°C is lower than required {requiredTemp}°C for {payloadName}.");
                    return;
                }
            }
            if (LoadedPayload == null)
            {
                LoadedPayload = payloadName;
            }
            ContainerTemperature = temp;
            
            if (CargoMass + payloadMass > MaxPayload)
            {
                throw new OverfillException(
                    $"Cannot load {payloadMass} kg of {payloadName} into refrigerated container {SerialNumber}. " +
                    $"Max payload is {MaxPayload} kg.");
            }

            CargoMass += payloadMass;
            Console.WriteLine(
                $"Loaded {payloadMass} kg of {payloadName} at {temp}°C into refrigerated container {SerialNumber}.");
        }

        // the abstract method to display an error when no temperature is provided
        public override void LoadContainer(char payloadType, string payloadName, int payloadMass, bool hazardous = false)
        {
            Console.WriteLine(
                "Error: Refrigerated container requires temperature specification. Use LoadContainer with temperature parameter.");
        }

        public override void PrintInfo()
        {
            var payloadDisplay = LoadedPayload ?? "(none)";
            Console.WriteLine(
                $"Refrigerated Container Serial: {SerialNumber}, CargoMass: {CargoMass} kg, MaxPayload: {MaxPayload} kg, Payload: {payloadDisplay}, Temperature: {ContainerTemperature}°C");
        }
    }
}
