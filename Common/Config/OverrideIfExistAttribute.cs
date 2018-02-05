using System;

namespace Common
{
    public class OverrideIfExistAttribute : Attribute
    {
        private readonly object value;

        public OverrideIfExistAttribute(object value)
        {
            this.value = value;
        }
    }
}
