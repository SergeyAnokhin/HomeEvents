using System;

namespace Common.Config
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
