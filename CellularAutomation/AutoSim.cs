using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CellularAutomation
{
    /// <summary>
    /// Represents the data neccessary for starting a simulation
    /// </summary>
    [Serializable()]
    public class AutoSim
    {
        public static Random Gen = new Random();

        public AutoSim(int Size)
        {
            //Initialize the rows
            this.Grid = new bool[Size][];

            for (int i = 0; i < this.Grid.Length; i++)
            {
                this.Grid[i] = new bool[Size];
            }

            //We assume the default rules
            this.OvercrowdingCount = 3;
            this.DeathCount = 2;
            this.BirthCount = 3;
            this.SearchRadius = 1;
            this.NeighborSearchAlgorithm = NeighborType.Moore;
        }

        public AutoSim(int Size, double OnOffRatio)
        {
            //Simply seed the sim
            this.Grid = Simulation.SeedSimulation(Size, OnOffRatio);

            //We assume the default rules
            this.OvercrowdingCount = 3;
            this.DeathCount = 2;
            this.BirthCount = 3;
            this.SearchRadius = 1;
            this.NeighborSearchAlgorithm = NeighborType.Moore;
        }

        //Grid[ROW][COLUMN]
        public bool[][] Grid
        {
            get;
            set;
        }

        /// <summary>
        /// A cell surrounded by fewer than this number of neighbors dies
        /// </summary>
        public int DeathCount
        {
            get;
            set;
        }

        /// <summary>
        /// A cell surrounded by more than this number of neighbors dies.
        /// </summary>
        public int OvercrowdingCount
        {
            get;
            set;
        }

        /// <summary>
        /// A dead cell surrounded by this many neighbors (exactly) becomes alive.
        /// </summary>
        public int BirthCount
        {
            get;
            set;
        }

        /// <summary>
        /// The radius to include "neighbors" in the search algorithm.
        /// </summary>
        public int SearchRadius
        {
            get;
            set;
        }

        /// <summary>
        /// The search algorithm used to determine what is considered a neighbor.
        /// </summary>
        public NeighborType NeighborSearchAlgorithm
        {
            get;
            set;
        }

        /// <summary>
        /// Determines the next generation of a cell.
        /// </summary>
        public bool DetermineStatus_Neumann(int RCoord, int CCoord)
        {
            int IndexCursorR = RCoord - this.SearchRadius;
            int IndexCursorC = CCoord - this.SearchRadius;
                
            //Count the number of living neighbors
            int LivingNeighbors = 0;
            for (int i = IndexCursorR; i <= (IndexCursorR + (2 * this.SearchRadius)); i++)
            {
                //if we are off the grid, skip this coordinate
                if (i < 0 || i >= this.Grid.Length) continue;

                //if this coordinate is our origional, skip it
                if (i == RCoord) continue;

                //If the coordinate is a living neighbor, add it to the total
                if (this.Grid[i][CCoord] == true) LivingNeighbors++;
            }

            for (int j = IndexCursorC; j <= (IndexCursorC + (2 * this.SearchRadius)); j++)
            {
                //If we are off the grid, skip this coordinate
                if (j < 0 || j >= this.Grid[RCoord].Length) continue;

                //if this coordinate is our origional, skip it
                if (j == CCoord) continue;

                //If the coordinate is a living neighbor, add it to the total
                if (this.Grid[RCoord][j] == true) LivingNeighbors++;
            }

            //Determine the status of this particle
            if (this.Grid[RCoord][CCoord] == true) //If we are living
            {
                if (LivingNeighbors >= this.DeathCount && LivingNeighbors <= this.OvercrowdingCount) return true;
            }
            else //otherwise if we are dead
            {
                if (LivingNeighbors == this.BirthCount) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines the next generation of a cell.
        /// </summary>
        public bool DetermineStatus_Diagonal(int RCoord, int CCoord)
        {
            int IndexCursorR = RCoord - this.SearchRadius;
            int IndexCursorC = CCoord - this.SearchRadius;

            //Count the number of living neighbors
            int LivingNeighbors = 0;
            for (int i = IndexCursorR; i <= (IndexCursorR + (2 * this.SearchRadius)); i++)
            {
                //if we are off the grid, skip this coordinate
                if (i < 0 || i >= this.Grid.Length) continue;

                for (int j = IndexCursorC; j <= (IndexCursorC + (2 * this.SearchRadius)); j++)
                {
                    //If we are off the grid, skip this coordinate
                    if (j < 0 || j >= this.Grid[i].Length) continue;

                    //if this coordinate is our origional, skip it
                    if (i == RCoord && j == CCoord) continue;

                    if ((i - RCoord) == (j - CCoord) || (i - RCoord) == -(j - CCoord))
                    {
                        //If the coordinate is a living neighbor, add it to the total
                        if (this.Grid[i][j] == true) LivingNeighbors++;
                    }
                }
            }

            //Determine the status of this particle
            if (this.Grid[RCoord][CCoord] == true) //If we are living
            {
                if (LivingNeighbors >= this.DeathCount && LivingNeighbors <= this.OvercrowdingCount) return true;
            }
            else //otherwise if we are dead
            {
                if (LivingNeighbors == this.BirthCount) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines the next generation of a cell.
        /// </summary>
        public bool DetermineStatus_AntiDiagonal(int RCoord, int CCoord)
        {
            int IndexCursorR = RCoord - this.SearchRadius;
            int IndexCursorC = CCoord - this.SearchRadius;

            //Count the number of living neighbors
            int LivingNeighbors = 0;
            for (int i = IndexCursorR; i <= (IndexCursorR + (2 * this.SearchRadius)); i++)
            {
                //if we are off the grid, skip this coordinate
                if (i < 0 || i >= this.Grid.Length) continue;

                for (int j = IndexCursorC; j <= (IndexCursorC + (2 * this.SearchRadius)); j++)
                {
                    //If we are off the grid, skip this coordinate
                    if (j < 0 || j >= this.Grid[i].Length) continue;

                    //if this coordinate is our origional, skip it
                    if (i == RCoord && j == CCoord) continue;

                    if ((i - RCoord) == (j - CCoord) || (i - RCoord) == -(j - CCoord))
                    {

                    }
                    else
                    {
                        //If the coordinate is a living neighbor, add it to the total
                        if (this.Grid[i][j] == true) LivingNeighbors++;
                    }
                }
            }

            //Determine the status of this particle
            if (this.Grid[RCoord][CCoord] == true) //If we are living
            {
                if (LivingNeighbors >= this.DeathCount && LivingNeighbors <= this.OvercrowdingCount) return true;
            }
            else //otherwise if we are dead
            {
                if (LivingNeighbors == this.BirthCount) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines the next generation of a cell.
        /// </summary>
        public bool DetermineStatus_Moore(int RCoord, int CCoord)
        {
            int IndexCursorR = RCoord - this.SearchRadius;
            int IndexCursorC = CCoord - this.SearchRadius;

            //Count the number of living neighbors
            int LivingNeighbors = 0;
            for (int i = IndexCursorR; i <= (IndexCursorR + (2 * this.SearchRadius)); i++)
            {
                //if we are off the grid, skip this coordinate
                if (i < 0 || i >= this.Grid.Length) continue;

                for (int j = IndexCursorC; j <= (IndexCursorC + (2 * this.SearchRadius)); j++)
                {
                    //If we are off the grid, skip this coordinate
                    if (j < 0 || j >= this.Grid[i].Length) continue;

                    //if this coordinate is our origional, skip it
                    if (i == RCoord && j == CCoord) continue;

                    //If the coordinate is a living neighbor, add it to the total
                    if (this.Grid[i][j] == true) LivingNeighbors++;
                }
            }

            //Determine the status of this particle
            if (this.Grid[RCoord][CCoord] == true) //If we are living
            {
                if (LivingNeighbors >= this.DeathCount && LivingNeighbors <= this.OvercrowdingCount) return true;
            }
            else //otherwise if we are dead
            {
                if (LivingNeighbors == this.BirthCount) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines the next generation of a cell.
        /// </summary>
        public bool DetermineStatus_Shell(int RCoord, int CCoord)
        {
            int IndexCursorR = RCoord - this.SearchRadius;
            int IndexCursorC = CCoord - this.SearchRadius;

            //Count the number of living neighbors
            int LivingNeighbors = 0;
            for (int i = IndexCursorR; i <= (IndexCursorR + (2 * this.SearchRadius)); i++)
            {
                //if we are off the grid, skip this coordinate
                if (i < 0 || i >= this.Grid.Length) continue;

                for (int j = IndexCursorC; j <= (IndexCursorC + (2 * this.SearchRadius)); j++)
                {
                    //If we are off the grid, skip this coordinate
                    if (j < 0 || j >= this.Grid[i].Length) continue;

                    //if this coordinate is our origional, skip it
                    if (i == RCoord && j == CCoord) continue;

                    //If we are within the grid, skip it
                    if (i > IndexCursorR && i < (IndexCursorR + (2 * this.SearchRadius)))
                    {
                        if (j > IndexCursorC && j < (IndexCursorC + (2 * this.SearchRadius)))
                        {
                            continue;
                        }
                    }

                    //If the coordinate is a living neighbor, add it to the total
                    if (this.Grid[i][j] == true) LivingNeighbors++;
                }
            }

            //Determine the status of this particle
            if (this.Grid[RCoord][CCoord] == true) //If we are living
            {
                if (LivingNeighbors >= this.DeathCount && LivingNeighbors <= this.OvercrowdingCount) return true;
            }
            else //otherwise if we are dead
            {
                if (LivingNeighbors == this.BirthCount) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines the next generation of a cell.
        /// </summary>
        public bool DetermineStatus_CheckeredSelection(int RCoord, int CCoord)
        {
            int IndexCursorR = RCoord - this.SearchRadius;
            int IndexCursorC = CCoord - this.SearchRadius;

            //Count the number of living neighbors
            int LivingNeighbors = 0;
            bool flip = true;
            for (int i = IndexCursorR; i <= (IndexCursorR + (2 * this.SearchRadius)); i++)
            {
                //if we are off the grid, skip this coordinate
                if (i < 0 || i >= this.Grid.Length) continue;

                for (int j = IndexCursorC; j <= (IndexCursorC + (2 * this.SearchRadius)); j++)
                {
                    //If we are off the grid, skip this coordinate
                    if (j < 0 || j >= this.Grid[i].Length) continue;

                    //if this coordinate is our origional, skip it
                    if (i == RCoord && j == CCoord) continue;
                    
                    if (flip)
                    {
                        //If the coordinate is a living neighbor, add it to the total
                        if (this.Grid[i][j] == true) LivingNeighbors++;
                    }
                    
                    flip = !flip;
                }
            }

            //Determine the status of this particle
            if (this.Grid[RCoord][CCoord] == true) //If we are living
            {
                if (LivingNeighbors >= this.DeathCount && LivingNeighbors <= this.OvercrowdingCount) return true;
            }
            else //otherwise if we are dead
            {
                if (LivingNeighbors == this.BirthCount) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines the next generation of a cell.
        /// </summary>
        public bool DetermineStatus_Totty(int RCoord, int CCoord)
        {
            int IndexCursorR = RCoord - this.SearchRadius;
            int IndexCursorC = CCoord - this.SearchRadius;

            //Count the number of living neighbors
            int LivingNeighbors = 0;
            for (int i = IndexCursorR; i <= (IndexCursorR + (2 * this.SearchRadius)); i++)
            {
                //if we are off the grid, skip this coordinate
                if (i < 0 || i >= this.Grid.Length) continue;

                //if this coordinate is our origional, skip it
                if (i == RCoord) continue;

                for (int j = IndexCursorC; j <= (IndexCursorC + (2 * this.SearchRadius)); j++)
                {
                    //If we are off the grid, skip this coordinate
                    if (j < 0 || j >= this.Grid[i].Length) continue;

                    //if this coordinate is our origional, skip it
                    if (j == CCoord) continue;

                    //If the coordinate is a living neighbor, add it to the total
                    if (this.Grid[i][j] == true) LivingNeighbors++;
                }
            }

            //Determine the status of this particle
            if (this.Grid[RCoord][CCoord] == true) //If we are living
            {
                if (LivingNeighbors >= this.DeathCount && LivingNeighbors <= this.OvercrowdingCount) return true;
            }
            else //otherwise if we are dead
            {
                if (LivingNeighbors == this.BirthCount) return true;
            }

            return false;
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
                    //Update the cell based on the given search algorithm
                    bool status = false;
                    if (this.NeighborSearchAlgorithm == NeighborType.Moore) status = DetermineStatus_Moore(i, j);
                    if (this.NeighborSearchAlgorithm == NeighborType.Neumann) status = DetermineStatus_Neumann(i, j);
                    if (this.NeighborSearchAlgorithm == NeighborType.Totty) status = DetermineStatus_Totty(i, j);
                    if (this.NeighborSearchAlgorithm == NeighborType.Diagonal) status = DetermineStatus_Diagonal(i, j);
                    if (this.NeighborSearchAlgorithm == NeighborType.AntiDiagonal) status = DetermineStatus_AntiDiagonal(i, j);
                    if (this.NeighborSearchAlgorithm == NeighborType.CheckeredSelection) status = DetermineStatus_CheckeredSelection(i, j);
                    if (this.NeighborSearchAlgorithm == NeighborType.Shell) status = DetermineStatus_Shell(i, j);
                    lock (NewGrid)
                    {
                        NewGrid[i][j] = status;
                    }
                }
            });

            //Save the new grid
            this.Grid = NewGrid;
        }
    }
}
