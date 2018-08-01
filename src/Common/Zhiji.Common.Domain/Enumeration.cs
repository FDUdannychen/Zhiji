using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zhiji.Common.Domain
{
    public abstract partial class Enumeration : IComparable
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        protected Enumeration() { }

        protected Enumeration(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int CompareTo(object other) => this.Id.CompareTo(((Enumeration)other).Id);

        public override int GetHashCode() => this.Id.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is Enumeration another)
            {
                var typeMatches = this.GetType().Equals(obj.GetType());
                var valueMatches = this.Id.Equals(another.Id);
                return typeMatches && valueMatches;
            }

            return false;
        }

        public override string ToString() => this.Name;
    }
}
