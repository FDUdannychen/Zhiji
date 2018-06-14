using System;

namespace Zhiji.Common.Domain
{
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }

        public virtual bool IsTransient => this.Id == default;

        public override int GetHashCode()
        {
            return this.IsTransient ? base.GetHashCode() : this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj)
                || obj is Entity entity
                && !ReferenceEquals(entity, null)
                && this.GetType() == entity.GetType()
                && !this.IsTransient
                && !entity.IsTransient
                && this.Id == entity.Id;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right) => !(left == right);
    }
}
