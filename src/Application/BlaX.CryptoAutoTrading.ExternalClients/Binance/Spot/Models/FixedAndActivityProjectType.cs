namespace BlaX.CryptoAutoTrading.ExternalClients.Binance.Spot.Models
{
    public struct FixedAndActivityProjectType
    {
        private FixedAndActivityProjectType(string value)
        {
            Value = value;
        }

        public static FixedAndActivityProjectType ACTIVITY { get => new FixedAndActivityProjectType("ACTIVITY"); }
        public static FixedAndActivityProjectType CUSTOMIZED_FIXED { get => new FixedAndActivityProjectType("CUSTOMIZED_FIXED"); }

        public string Value { get; private set; }

        public static implicit operator string(FixedAndActivityProjectType enm) => enm.Value;

        public override string ToString() => Value.ToString();
    }
}