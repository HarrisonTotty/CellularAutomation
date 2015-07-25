using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CellularAutomation
{
    /// <summary>
    /// Simple format used to write basic rule sets to files
    /// </summary>
    [Serializable()]
    public class SerializableRuleSet
    {
        public int BirthCount
        {
            get;
            set;
        }

        public int DeathCount
        {
            get;
            set;
        }

        public int OvercrowdingCount
        {
            get;
            set;
        }

        public int SearchRadius
        {
            get;
            set;
        }

        public NeighborType NeighborSearchAlgorithm
        {
            get;
            set;
        }
    }
}
