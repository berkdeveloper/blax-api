using BlaX.CryptoAutoTrading.Domain.Core.Enums;
using BlaX.CryptoAutoTrading.Domain.Core.Extensions;
using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Domain.Core.Entities.Base
{
    public abstract class EntityBase : EntityCore
    {
        [JsonIgnore]
        public DateTime CreatedDate { get; protected set; }
        [JsonIgnore]
        public Guid CreatedBy { get; protected set; }
        [JsonIgnore]
        public DateTime? UpdatedDate { get; protected set; }
        [JsonIgnore]
        public Guid? UpdatedBy { get; protected set; }
        [JsonIgnore]
        public StatusTypeEnum StatusType { get; protected set; }

        public EntityBase()
        {
            CreatedDate = DateTime.UtcNow;
            StatusType = StatusTypeEnum.Active;
        }

        public virtual void SetCreatedAt(DateTime createdDate) => CreatedDate = createdDate;
        public virtual void SetCreatedBy(Guid createdBy) => CreatedBy = createdBy;
        public virtual void SetUpdatedAt(DateTime updatedDate) => UpdatedDate = updatedDate;
        public virtual void SetUpdatedBy(Guid? updatedBy) => UpdatedBy = updatedBy;
        public virtual void SetStatusType(StatusTypeEnum value) => StatusType = value;
    }
}
