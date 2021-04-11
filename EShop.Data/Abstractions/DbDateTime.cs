using System;
using EShop.Data.Helpers;

namespace EShop.Data.Abstractions
{
    public class DbDateTime
    {
        private DateTime _value;

        public DateTime Value
        {
            get => _value.EnsureUniversalTime();
            set => _value = value.EnsureUniversalTime();
        }
        
        public DbDateTime(){}

        public DbDateTime(DateTime value)
        {
            Value = value;
        }
    }
}