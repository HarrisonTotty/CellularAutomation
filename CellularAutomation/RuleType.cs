using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CellularAutomation
{
    //NOT IMPLEMENTED
    public enum RuleType
    {
        /// <summary>
        /// Checks the number of living neighbors
        /// </summary>
        CheckLivingNeighbors,
        /// <summary>
        /// Checks the number of dead neighbors
        /// </summary>
        CheckDeadNeighbors,
        /// <summary>
        /// Checks the current state of the cell
        /// </summary>
        CheckState,
    }
}
