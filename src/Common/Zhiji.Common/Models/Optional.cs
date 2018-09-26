using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.Common.Models
{
    public struct Optional<T> : IEquatable<Optional<T>>
    {
        public static readonly Optional<T> None = new Optional<T>();
        
        private readonly T _value;

        public Optional(T value)
        {
            _value = value;
            this.HasValue = true;
        }

        public bool HasValue { get; }

        public T Value => this.HasValue 
            ? _value 
            : throw new InvalidOperationException($"{typeof(Optional<T>)} doesn't have a value");

        public T GetValueOrDefault(T defaultValue = default)
            => this.HasValue ? _value : defaultValue;

        public override int GetHashCode()
        {
            return this.HasValue.GetHashCode()
                + typeof(T).GetHashCode() * 3
                + (_value == null ? 1 : _value.GetHashCode()) * 5;
        }

        public override bool Equals(object obj)
            => obj is Optional<T> other && this.Equals(other);

        public bool Equals(Optional<T> other)
        {
            return this.HasValue == other.HasValue
                && (!this.HasValue
                || Object.Equals(this.GetValueOrDefault(), other.GetValueOrDefault()));
        }
    }
}
