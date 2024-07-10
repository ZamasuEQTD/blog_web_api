
namespace SharedKernel.Abstractions
{
    public abstract class EntityId:ValueObject
    {
        public Guid Value {get;private set;}
        
        protected  EntityId(){}
        public EntityId(Guid value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }
    }
}