using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellularAutomation
{
    public partial class MainForm : Form
    {
        public static AutoSim Sim;
        public static long GenerationCount;
        public static int Size = 200;
        public static int BrushSize = 1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            UpdateTimer.Stop();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, 0.7);

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void ScaleGrid()
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -5;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -5;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = Sim.Grid.Length + 5;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = Sim.Grid.Length + 5;
        }

        private void PrintGrid()
        {
            try
            {
                PrimaryGrid.Series[0].Points.Clear();
                for (int i = 0; i < Sim.Grid.Length; i++)
                {
                    for (int j = 0; j < Sim.Grid[i].Length; j++)
                    {
                        if (Sim.Grid[i][j] == true)
                        {
                            PrimaryGrid.Series[0].Points.AddXY(i, j);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void PrintGridV2()
        {
            for (int i = 0; i < Sim.Grid.Length; i++)
            {
                for (int j = 0; j < Sim.Grid[i].Length; j++)
                {
                    if (Sim.Grid[i][j] == true)
                    {
                        bool found = false;
                        foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint x in PrimaryGrid.Series[0].Points)
                        {
                            if (x.XValue == i && x.YValues[0] == j)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found) PrimaryGrid.Series[0].Points.AddXY(i, j);
                    }
                    else
                    {
                        foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint x in PrimaryGrid.Series[0].Points)
                        {
                            if (x.XValue == i && x.YValues[0] == j)
                            {

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimer.Stop();

            Sim.Update();

            if (Tabs.SelectedTab == Tab_Grid) PrintGrid();
            if (Tabs.SelectedTab == Tab_RuleInformation) PrintRuleInfo();

            GenerationCount++;
            PrimaryGrid.Titles[0].Text = "Generation " + GenerationCount;

            UpdateTimer.Start();
        }

        private void PrintRuleInfo()
        {
            InfoBox.Clear();

            string NL = Environment.NewLine;
            InfoBox.Text = "";
            InfoBox.Text += "-----INFO-----" + NL;
            InfoBox.Text += "Generation:       " + GenerationCount + NL;
            InfoBox.Text += "Search Algorithm: " + Sim.NeighborSearchAlgorithm.ToString() + NL;
            InfoBox.Text += "Search Radius:    " + Sim.SearchRadius + NL;
            InfoBox.Text += "Grid Size:        " + Size + NL;

            InfoBox.Text += NL;
            InfoBox.Text += "----------RULES----------" + NL;
            InfoBox.Text += "1. An active cell becomes inactive if surrounded by fewer than " + Sim.DeathCount + " neighbors." + NL;
            InfoBox.Text += "2. An active cell remains active if surrounded by between " + Sim.DeathCount + " and " + Sim.OvercrowdingCount + " neighbors inclusive." + NL;
            InfoBox.Text += "3. An active cell becomes inactive if surrounded by more then " + Sim.OvercrowdingCount + " neighbors." + NL;
            InfoBox.Text += "4. An inactive cell becomes active if surrounded by exactly " + Sim.BirthCount + " neighbors." + NL;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UpdateTimer.Interval = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UpdateTimer.Interval = 50;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            UpdateTimer.Interval = 100;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            UpdateTimer.Interval = 500;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            UpdateTimer.Interval = 1000;
        }

        private void searchRadius2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, 0.7);
            Sim.SearchRadius = 2;
            Sim.OvercrowdingCount = 7;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void sR2DC4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, 0.7);
            Sim.SearchRadius = 2;
            Sim.OvercrowdingCount = 7;
            Sim.BirthCount = 4;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void sR3DC4OC7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, 0.7);
            Sim.SearchRadius = 3;
            Sim.OvercrowdingCount = 13;
            Sim.BirthCount = 4;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void sR3DC4OC42ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, 0.7);
            Sim.SearchRadius = 3;
            Sim.OvercrowdingCount = 13;
            Sim.BirthCount = 4;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void PrimaryGrid_MouseClick(object sender, MouseEventArgs e)
        {
            bool wason = UpdateTimer.Enabled;
            if (wason) UpdateTimer.Stop();
                int x = (int)Math.Round(PrimaryGrid.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X));
                int y = (int)Math.Round(PrimaryGrid.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y));

                for (int i = x - BrushSize; i <= (x + BrushSize); i++)
                {
                    for (int j = y - BrushSize; j <= (y + BrushSize); j++)
                    {
                        try
                        {
                            Sim.Grid[i][j] = true;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                PrintGrid();
            if (wason) UpdateTimer.Start();
        }

        private void rANDOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(1, 4);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.BirthCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);

            int roll = Simulation.RandomGen.Next(0, 7);
            if (roll == 0) Sim.NeighborSearchAlgorithm = NeighborType.Moore;
            if (roll == 1) Sim.NeighborSearchAlgorithm = NeighborType.Neumann;
            if (roll == 2) Sim.NeighborSearchAlgorithm = NeighborType.Totty;
            if (roll == 3) Sim.NeighborSearchAlgorithm = NeighborType.Diagonal;
            if (roll == 4) Sim.NeighborSearchAlgorithm = NeighborType.AntiDiagonal;
            if (roll == 5) Sim.NeighborSearchAlgorithm = NeighborType.CheckeredSelection;
            if (roll == 6) Sim.NeighborSearchAlgorithm = NeighborType.Shell;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void sR1OC7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, 0.1);
            Sim.OvercrowdingCount = 7;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void tRUERANDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(1, 10);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.BirthCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));

            int roll = Simulation.RandomGen.Next(0, 5);
            if (roll == 0) Sim.NeighborSearchAlgorithm = NeighborType.Moore;
            if (roll == 1) Sim.NeighborSearchAlgorithm = NeighborType.Neumann;
            if (roll == 2) Sim.NeighborSearchAlgorithm = NeighborType.Totty;
            if (roll == 3) Sim.NeighborSearchAlgorithm = NeighborType.Diagonal;
            if (roll == 4) Sim.NeighborSearchAlgorithm = NeighborType.AntiDiagonal;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Size = 200;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Size = 100;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Size = 300;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Size = 500;
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            UpdateTimer.Start();
        }

        private void Open_AutoSim_Click(object sender, EventArgs e)
        {
            //If the timer is on, stop it
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            OFD_AutoSim.ShowDialog();
        }

        private void Open_RuleSet_Click(object sender, EventArgs e)
        {
            //If the timer is on, stop it
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            OFD_RuleSet.ShowDialog();
        }

        private void Save_AutoSim_Click(object sender, EventArgs e)
        {
            //If the timer is on, stop it
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            SFD_AutoSim.ShowDialog();
        }

        private void Save_RuleSet_Click(object sender, EventArgs e)
        {
            //If the timer is on, stop it
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            SFD_RuleSet.ShowDialog();
        }

        private void OFD_RuleSet_FileOk(object sender, CancelEventArgs e)
        {
            //Load the file to memory
            SerializableRuleSet Rules = Serialization.DeserializeObject<SerializableRuleSet>(OFD_RuleSet.FileName);

            //Create a new sim
            Sim = new AutoSim(Size);

            //Copy over the rule information
            Sim.BirthCount = Rules.BirthCount;
            Sim.DeathCount = Rules.DeathCount;
            Sim.OvercrowdingCount = Rules.OvercrowdingCount;
            Sim.SearchRadius = Rules.SearchRadius;
            Sim.NeighborSearchAlgorithm = Rules.NeighborSearchAlgorithm;

            //Now re-seed the simulation
            Sim.Grid = Simulation.SeedSimulation(Size, 0.7);

            MessageBox.Show("Automaton simulation ruleset successfully loaded...", "Load Successful!", MessageBoxButtons.OK);
            GenerationCount = 0;
            UpdateTimer.Start();
        }

        private void OFD_AutoSim_FileOk(object sender, CancelEventArgs e)
        {
            //Load the file to memory
            Sim = Serialization.DeserializeObject<AutoSim>(OFD_AutoSim.FileName);

            MessageBox.Show("Automaton simulation successfully loaded...", "Load Successful!", MessageBoxButtons.OK);
            GenerationCount = 0;
            UpdateTimer.Start();
        }

        private void SFD_RuleSet_FileOk(object sender, CancelEventArgs e)
        {
            //Create a ruleset object
            SerializableRuleSet SaveObject = new SerializableRuleSet();
            SaveObject.BirthCount = Sim.BirthCount;
            SaveObject.DeathCount = Sim.DeathCount;
            SaveObject.OvercrowdingCount = Sim.OvercrowdingCount;
            SaveObject.SearchRadius = Sim.SearchRadius;
            SaveObject.NeighborSearchAlgorithm = Sim.NeighborSearchAlgorithm;

            //Save the object to file
            Serialization.SerializeObject<SerializableRuleSet>(SFD_RuleSet.FileName, SaveObject);

            MessageBox.Show("Automaton simulation ruleset successfully saved...", "Save Successful!", MessageBoxButtons.OK);
        }

        private void SFD_AutoSim_FileOk(object sender, CancelEventArgs e)
        {
            //Save the simulation to file
            Serialization.SerializeObject<AutoSim>(SFD_AutoSim.FileName, Sim);
            MessageBox.Show("Automaton simulation successfully saved...", "Save Successful!", MessageBoxButtons.OK);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            ReSeedSim(0.1);
        }

        private void ReSeedSim(double SeedVal)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Make sure the sim isn't null
            if (Sim == null) Sim = new AutoSim(Size);

            //Re-seed the simulation
            Sim.Grid = Simulation.SeedSimulation(Size, SeedVal);

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            ReSeedSim(0.3);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            ReSeedSim(0.5);
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            ReSeedSim(0.7);
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            ReSeedSim(0.9);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Sim.SearchRadius = 1;
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Sim.SearchRadius = 2;
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            Sim.SearchRadius = 3;
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            Sim.SearchRadius = 4;
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            Sim.SearchRadius = 5;
        }

        private void mooreDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sim.NeighborSearchAlgorithm = NeighborType.Moore;
        }

        private void neumannToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sim.NeighborSearchAlgorithm = NeighborType.Neumann;
        }

        private void tottyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sim.NeighborSearchAlgorithm = NeighborType.Totty;
        }

        private void randomMoorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(1, 4);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.BirthCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.NeighborSearchAlgorithm = NeighborType.Moore;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void randomNeumannToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(1, 4);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.BirthCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.NeighborSearchAlgorithm = NeighborType.Neumann;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void randomTottyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(2, 4);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.BirthCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.NeighborSearchAlgorithm = NeighborType.Totty;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void randomDiagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(1, 4);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.BirthCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.NeighborSearchAlgorithm = NeighborType.Diagonal;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void diagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sim.NeighborSearchAlgorithm = NeighborType.Diagonal;
        }

        private void antiDiagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sim.NeighborSearchAlgorithm = NeighborType.AntiDiagonal;
        }

        private void randomAntiDiagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(2, 4);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.BirthCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.NeighborSearchAlgorithm = NeighborType.AntiDiagonal;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void randomSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sim.NeighborSearchAlgorithm = NeighborType.CheckeredSelection;
        }

        private void randomRandomSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(1, 4);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.BirthCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.NeighborSearchAlgorithm = NeighborType.CheckeredSelection;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void randomShellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Create a new autosim
            Sim = new AutoSim(Size, Simulation.GenerateRandomDouble(0.1, 0.9));

            Sim.SearchRadius = Simulation.RandomGen.Next(2, 4);
            Sim.OvercrowdingCount = Simulation.RandomGen.Next(1, (Sim.SearchRadius * 2) * (Sim.SearchRadius * 2));
            Sim.DeathCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.BirthCount = Simulation.RandomGen.Next(1, Sim.OvercrowdingCount + 1);
            Sim.NeighborSearchAlgorithm = NeighborType.Shell;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void shellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sim.NeighborSearchAlgorithm = NeighborType.Shell;
        }

        private void File_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void File_New_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Just press the start button, I'm too lazy to impliment anything...", "jhasdbfkjasdhvfb", MessageBoxButtons.OK);
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Make sure the sim isn't null
            if (Sim == null) Sim = new AutoSim(Size);

            //Re-seed the simulation
            Sim.Grid = Simulation.GetBlankGrid(Size);

            Sim.Grid[Sim.Grid.Length / 2][Sim.Grid.Length / 2] = true;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Make sure the sim isn't null
            if (Sim == null) Sim = new AutoSim(Size);

            //Re-seed the simulation
            Sim.Grid = Simulation.GetBlankGrid(Size);

            Sim.Grid[0][Sim.Grid.Length / 2] = true;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Make sure the sim isn't null
            if (Sim == null) Sim = new AutoSim(Size);

            //Re-seed the simulation
            Sim.Grid = Simulation.GetBlankGrid(Size);

            Sim.Grid[0][0] = true;

            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void centerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Make sure the sim isn't null
            if (Sim == null) Sim = new AutoSim(Size);

            //Re-seed the simulation
            Sim.Grid = Simulation.GetBlankGrid(Size);

            int centerval = Sim.Grid.Length / 2;
            Sim.Grid[centerval][centerval] = true;
            Sim.Grid[centerval + 1][centerval] = true;
            Sim.Grid[centerval][centerval + 1] = true;
            Sim.Grid[centerval + 1][centerval + 1] = true;


            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void topLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Make sure the sim isn't null
            if (Sim == null) Sim = new AutoSim(Size);

            //Re-seed the simulation
            Sim.Grid = Simulation.GetBlankGrid(Size);

            int centerval = Sim.Grid.Length / 2;
            Sim.Grid[0][centerval] = true;
            Sim.Grid[1][centerval] = true;
            Sim.Grid[0][centerval + 1] = true;
            Sim.Grid[1][centerval + 1] = true;


            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Make sure the sim isn't null
            if (Sim == null) Sim = new AutoSim(Size);

            //Re-seed the simulation
            Sim.Grid = Simulation.GetBlankGrid(Size);

            int centerval = 0;
            Sim.Grid[0][0] = true;
            Sim.Grid[centerval + 1][centerval] = true;
            Sim.Grid[centerval][centerval + 1] = true;
            Sim.Grid[centerval + 1][centerval + 1] = true;


            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void centerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Stop the sim if it's running
            if (UpdateTimer.Enabled) UpdateTimer.Stop();

            //Make sure the sim isn't null
            if (Sim == null) Sim = new AutoSim(Size);

            //Re-seed the simulation
            Sim.Grid = Simulation.GetBlankGrid(Size);

            int centerval = Sim.Grid.Length / 2;
            Sim.Grid[centerval][centerval] = true;
            Sim.Grid[centerval][centerval - 1] = true;
            Sim.Grid[centerval][centerval + 1] = true;
            Sim.Grid[centerval + 1][centerval] = true;
            Sim.Grid[centerval - 1][centerval] = true;
            Sim.Grid[centerval - 1][centerval - 1] = true;
            Sim.Grid[centerval + 1][centerval + 1] = true;
            Sim.Grid[centerval + 1][centerval - 1] = true;
            Sim.Grid[centerval - 1][centerval + 1] = true;


            //Scale the grid
            ScaleGrid();

            //Reset the generation count
            GenerationCount = 0;

            //Start the timer
            UpdateTimer.Start();
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            BrushSize = 1;
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            BrushSize = 3;
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            BrushSize = 5;
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            BrushSize = 7;
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            BrushSize = 9;
        }

        private void centerToolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }
    }
}
