using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zhiji.Common.Domain
{
    public abstract partial class Enumeration
    {
        class Cached<T> where T : Enumeration
        {
            public static IEnumerable<T> Value { get; set; }
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            if (Cached<T>.Value is null)
            {
                var instance = Activator.CreateInstance(typeof(T), true);
                var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                Cached<T>.Value = fields.Select(f => f.GetValue(instance)).OfType<T>().ToArray();
            }

            return Cached<T>.Value;
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem is null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public static bool ContainsValue<T>(int value) where T : Enumeration
        {
            return GetAll<T>().Any(e => e.Id == value);
        }
    }
}
