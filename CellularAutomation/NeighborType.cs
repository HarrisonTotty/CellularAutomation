using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CellularAutomation
{
    /// <summary>
    /// The search algorithm used for neighbors
    /// </summary>
    public enum NeighborType
    {
        /// <summary>
        /// The classical neighbor scheme
        /// </summary>
        Moore,

        /// <summary>
        /// The cross neighbor scheme
        /// </summary>
        Neumann,

        /// <summary>
        /// Anti-cross neighbor scheme
        /// </summary>
        Totty,

        /// <summary>
        /// Basic diagonal scheme
        /// </summary>
        Diagonal,

        /// <summary>
        /// Opposite of the diagonal scheme
        /// </summary>
        AntiDiagonal,

        /// <summary>
        /// Checkered selection
        /// </summary>
        CheckeredSelection,

        /// <summary>
        /// Selects only neighbors on the edge of the search radius
        /// </summary>
        Shell
    }
}
