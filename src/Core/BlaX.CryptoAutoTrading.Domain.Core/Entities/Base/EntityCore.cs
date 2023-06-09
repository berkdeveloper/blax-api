namespace BlaX.CryptoAutoTrading.Domain.Core.Entities.Base
{
    public class EntityCore
    {
        public Guid Id { get; init; }
        public EntityCore() => Id = Guid.NewGuid();
    }
}
