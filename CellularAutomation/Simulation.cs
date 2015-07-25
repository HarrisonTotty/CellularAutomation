using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CellularAutomation
{
    /// <summary>
    /// Contains all of the static methods available for simulation
    /// </summary>
    public class Simulation
    {
        public static Random RandomGen = new Random();

        /// <summary>
        /// Seeds a simulation with randomly flipped coordinates given the specified on/off ratio
        /// </summary>
        public static bool[][] SeedSimulation(int Size, double OnOffRatio)
        {
            //Create a new return grid
            bool[][] ReturnGrid = new bool[Size][];

            for (int i = 0; i < Size; i++)
            {
                ReturnGrid[i] = new bool[Size];
            }

            //For each row:
            for (int i = 0; i < Size; i++)
            {
                //For each column:
                for (int j = 0; j < Size; j++)
                {
                    //Roll a number between 0 and 1
                    double Roll = GenerateRandomDouble(0, 1);

                    //If the roll is less than the on/off ratio:
                    if (Roll <= OnOffRatio)
                    {
                        //Lets turn the cell on
                        ReturnGrid[i][j] = true;
                    }
                    else //else we turn the cell off
                    {
                        ReturnGrid[i][j] = false;
                    }
                }
            }

            return ReturnGrid;
        }

        /// <summary>
        /// Returns a blank grid of a specified size
        /// </summary>
        public static bool[][] GetBlankGrid(int Size)
        {
            //Create a new return grid
            bool[][] ReturnGrid = new bool[Size][];

            for (int i = 0; i < Size; i++)
            {
                ReturnGrid[i] = new bool[Size];
            }

            return ReturnGrid;
        }

        /// <summary>
        /// Returns a random double value between two double values
        /// </summary>
        public static double GenerateRandomDouble(double Lowerbound, double Upperbound)
        {
            return RandomGen.NextDouble() * (Upperbound - Lowerbound) + Lowerbound;
        }


    }
}
