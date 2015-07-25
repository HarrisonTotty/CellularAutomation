using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomation
{
    /// <summary>
    /// New varient of autosim
    /// NOT IMPLEMENTED
    /// </summary>
    public class Automaton
    {
        /// <summary>
        /// Represents the current grid of cells
        /// </summary>
        public bool[][] Grid
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the list of rules governing this simulation
        /// </summary>
        public List<Rule> Rules
        {
            get;
            set;
        }

        /// <summary>
        /// The number of rules that must be valid in order for this cell to become "true"
        /// </summary>
        public int RequiredValidity
        {
            get;
            set;
        }

        public bool DetermineStatus(int RCoord, int CCoord)
        {
            int NumberValid = 0; //Holds the current number of valid rules

            //For each rule:
            foreach (Rule R in this.Rules)
            {
                //Determine if the rule is valid
                switch (R.Type)
                {
                    case RuleType.CheckDeadNeighbors:
                        if (R.CheckRule(DetermineNumNeighbors(false, R.SearchRadius, RCoord, CCoord))) NumberValid++;
                        break;

                    case RuleType.CheckLivingNeighbors:
                        if (R.CheckRule(DetermineNumNeighbors(true, R.SearchRadius, RCoord, CCoord))) NumberValid++;;
                        break;

                    case RuleType.CheckState:
                        if (R.CheckStatus(this.Grid[RCoord][CCoord])) NumberValid++;
                        break;

                    default:
                        continue;
                }
            }

            return (NumberValid >= this.RequiredValidity);
        }

        public void Update()
        {
            //Get a new grid
            bool[][] NewGrid = new bool[Grid.Length][];

            for (int i = 0; i < this.Grid.Length; i++)
            {
                NewGrid[i] = new bool[Grid[i].Length];
            }

            //Determine the status of each point
            Parallel.For(0, Grid.Length, i =>
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    bool status = DetermineStatus(i, j);
                    lock (NewGrid)
                    {
                        NewGrid[i][j] = status;
                    }
                }
            });

            //Save the new grid
            this.Grid = NewGrid;
        }

        public int DetermineNumNeighbors(bool Type, int SearchRadius, int RCoord, int CCoord)
        {
            int IndexCursorR = RCoord - SearchRadius;
            int IndexCursorC = CCoord - SearchRadius;

            //Count the number of living neighbors
            int Neighbors = 0;
            for (int i = IndexCursorR; i <= (IndexCursorR + (2 * SearchRadius)); i++)
            {
                //if we are off the grid, skip this coordinate
                if (i < 0 || i >= this.Grid.Length) continue;

                for (int j = IndexCursorC; j <= (IndexCursorC + (2 * SearchRadius)); j++)
                {
                    //If we are off the grid, skip this coordinate
                    if (j < 0 || j >= this.Grid[i].Length) continue;

                    //if this coordinate is our origional, skip it
                    if (i == RCoord && j == CCoord) continue;

                    //If the coordinate is the type we are looking for, add it to the total
                    if (this.Grid[i][j] == Type) Neighbors++;
                }
            }

            return Neighbors;
        }
    }
}
