using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossOptions.Models
{
    public class MariaDBOptions
    {
        public Version MariaDbVersion { get; set; }

        public bool RowFormat { get; set; }
    }
}
