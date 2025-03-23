namespace container_manager
{
    public abstract class Container
    {
        public int CargoMass { get; set; }
        public int Height { get; set; }
        public int TareWeight { get; set; }
        public int Depth { get; set; }
        public string SerialNumber { get; set; }
        public int MaxPayload { get; set; }
        
        protected string? LoadedPayload { get; set; } = null;
        protected bool? IsHazardousCargo { get; set; } = null;

        // counters for unique serial numbers per container type
        private static Dictionary<string, int> containerCounters = new Dictionary<string, int>()
        {
            {"L", 0}, {"G", 0}, {"C", 0}
        };

        protected Container(string typeCode, int height, int tareWeight, int depth, int maxPayload)
        {
            Height = height;
            TareWeight = tareWeight;
            Depth = depth;
            MaxPayload = maxPayload;
            CargoMass = 0;

            if (!containerCounters.ContainsKey(typeCode))
                containerCounters[typeCode] = 0;

            containerCounters[typeCode]++;
            SerialNumber = $"KON-{typeCode}-{containerCounters[typeCode]}";
        }
        
        public virtual void UnloadContainer()
        {
            CargoMass = 0;
            LoadedPayload = null;
            Console.WriteLine($"Container {SerialNumber} cargo unloaded.");
        }
        
        public abstract void LoadContainer(char payloadType, string payloadName, int payloadMass, bool hazardous = false);
        
        public virtual void PrintInfo()
        {
            var payloadDisplay = LoadedPayload ?? "(none)";
            var hazardDisplay = IsHazardousCargo.HasValue && IsHazardousCargo.Value ? "Hazardous" : "Non-hazardous";

            Console.WriteLine(
                $"Container Serial: {SerialNumber}, CargoMass: {CargoMass} kg, MaxPayload: {MaxPayload} kg, Payload: {payloadDisplay}, {hazardDisplay}");
        }
    }
}
