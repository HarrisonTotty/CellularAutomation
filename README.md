# Cellular Automaton
A [cellular automaton](https://en.wikipedia.org/wiki/Cellular_automaton "Cellular Automaton Wikipedia Page") program I made for an object oriented programming class in C# (back in 2013). It is capable of simulating [Conways Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life "Conway's Game of Life Wikipedia Page") as well as generating random rulesets (which may be saved and loaded) and supports many neighbor search algorithms (many of which I made up). You can find some interesting rulesets that were randomly generated in "Pre-Saved Simulations.zip".

### COMPILING
This project does not have any dependencies other than the .NET framework, so compiling the source code shouldn't be a problem provided you have some version of Visual Studio. If you don't want to compile the source yourself, I have added a pre-compiled executable in "Compiled - x86_64.zip".

### HOW TO RUN
When the program loads, you can press the "START" button to seed the program with a random grid and run the simulation under the regular "Game of Life" ruleset. Selecting one of the options under the "Rule Sets" menu will run the simulation under a different ruleset. For a description of the current ruleset, check the "Rule Information" tab. The "Seed" menu will re-start the current ruleset with a particular pattern or fraction of active cells. You can click anywhere on the grid to turn that cell into an active cell (you can change the brush size under "Modify" > "Brush Size"). The "Interval" menu will change the number of milliseconds between each generation update. There are plenty of other hidden features as well.
