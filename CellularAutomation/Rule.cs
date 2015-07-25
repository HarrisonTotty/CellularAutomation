using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CellularAutomation
{
    /// <summary>
    /// Represents a single automaton rule
    /// NOT IMPLEMENTED
    /// </summary>
    [Serializable()]
    public class Rule
    {
        public Rule(RuleType T, Comparitor C, int V)
        {
            this.Type = T;
            this.Compare = C;
            this.Value = V;
            this.SearchRadius = 1;
        }

        /// <summary>
        /// Represents the type of comparison to be made
        /// </summary>
        public RuleType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the type of comparison to perform
        /// </summary>
        public Comparitor Compare
        {
            get;
            set;
        }

        /// <summary>
        /// The value to compare to
        /// </summary>
        public int Value
        {
            get;
            set;
        }

        public int SearchRadius
        {
            get;
            set;
        }

        /// <summary>
        /// Checks a value for validity
        /// </summary>
        public bool CheckRule(int Comparison)
        {
            switch (this.Compare)
            {
                case Comparitor.Equals:
                    return (Comparison == this.Value);

                case Comparitor.GreaterThan:
                    return (Comparison > this.Value);

                case Comparitor.GreaterThanEquals:
                    return (Comparison >= this.Value);

                case Comparitor.LessThan:
                    return (Comparison < this.Value);

                case Comparitor.LessThanEquals:
                    return (Comparison <= this.Value);

                case Comparitor.NotEquals:
                    return (Comparison != this.Value);

                default:
                    return false;
            }
        }

        public bool CheckStatus(bool Comparison)
        {
            bool ValToBool = false;
            if (this.Value > 0)
            {
                ValToBool = true;
            }

            return (Comparison == ValToBool);
        }
    }
}

