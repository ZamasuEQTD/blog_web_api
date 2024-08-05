namespace SharedKernel.Abstractions
{
    public abstract class Entity 
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public ICollection<IDomainEvent> DomainEvents => _domainEvents;
        protected void Raise(IDomainEvent IDomainEvent) {
            _domainEvents.Add(IDomainEvent);
        }

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }    

        protected Entity() {}
    }

    public abstract class Entity<TId> : Entity where TId:EntityId{
        public DateTime CreatedAt {get; protected set;} = DateTime.UtcNow;
        public TId Id {get;protected set;}
        protected Entity(){}
        protected Entity(TId id) :base() {
            Id = id;
        }
    }

    public class BusinessRuleValidationException : Exception {
        public IBusinessRule BrokenRule { get; }

        public string Details { get; }

        public BusinessRuleValidationException(IBusinessRule brokenRule)
            : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            this.Details = brokenRule.Message;
        }

        public override string ToString()
        {
            return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        }
    }

    public interface IBusinessRule {
        bool IsBroken();

        string Message { get; }

    }
}