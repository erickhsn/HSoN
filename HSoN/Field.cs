using System;
using System.Collections.Generic;
using System.Text;

namespace HSoN
{
    public class Field : IField
    {
        public string Id { get; }

        public Field(string id)
        {
            Id = id;
        }
    }
}
