using System;

namespace Common
{
    public class CanPrivateOverride : Attribute
    {
        public string PropertyName { get; }

        public CanPrivateOverride(string value)
        {
            this.PropertyName = value;
        }
    }
}
