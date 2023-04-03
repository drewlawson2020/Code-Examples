#include<iostream>
#include<math.h>
using namespace std;
// Assignment: A1: An Ecological Simulation
// Author: Drew Lawson
// Class: CS3505
// Professor: David Johnson
// Description: This assignment calculates the Lotka-Volterra equation, and plots it out using characters in the terminal.

// Declare functions so that they're properly able to be accessed at compile time.
void runSimulation(int, double, double);
void updatePopulations(double, double, double, double, double, double&, double&);
void plotCharacter(int, char);
void plotPopulations(double, double, double);
void incrementCounter(int*);

/*runSimulation: Takes in an int iterator, and the number of rabbits and foxes. From there, it declares
local static consts for every variable necessary for the Lotka-Volterra equation. After this, a for loop
based on the number of iteratios runs, cycling through plotPopulations, updatePopulations, and the incrementCounter
functions until either the rabbit or fox population drops below 1, or until the max number of iterations is hit, defaulting at
500.*/
void runSimulation(int iterations, double numRabbits, double numFoxes)
{
//Declare consts and i iterator
static const double rabbitGrowth = 0.2;
static const double predationRate = 0.0022;
static const double foxPreyConversion = 0.6;
static const double foxMortalityRate = 0.2;
static const double carryCapacity = 1000.0;
int i;

for (i = 0; i < iterations;)
{
  if(numRabbits < 1 || numFoxes < 1)
  {
    exit(0);
  }
    plotPopulations(numRabbits, numFoxes, .1);
    cout << endl;
    updatePopulations(rabbitGrowth, predationRate, foxPreyConversion, 
    foxMortalityRate, carryCapacity, numRabbits, numFoxes);
    // Increment i with pointer
    int* pointer = &i;
    incrementCounter(pointer);
}

}

/*main: Starts the code necessary for the simulation. Declares numRabbits and numFoxes, and then takes
user input to change the values of each respective variable. Then, runs runSimulation for 500 iterations.*/
int main()
{
double numRabbits = 1.0;
double numFoxes = 1.0;



cout << "Enter the population of foxes: " << endl;

if(!(cin >> numFoxes))
{
    cout << "Error: Cannot input fox pop. data that isn't a double. Please try again." << endl;
    return -1;
}

cout << "Enter the population of rabbits: " << endl;

if(!(cin >> numRabbits))
{
    cout << "Error: Cannot input rabbit pop. data that isn't a double. Please try again." << endl;
    return -1;
}
runSimulation(500, numRabbits, numFoxes);

}
/*updatePopulations: The mathematical backbone to the code. Calculates the Lotka-Volterra equation, and adds the resulting delta
to the num of rabbits and foxes.*/
void updatePopulations(double rabbitGrowth, double predationRate, double foxPreyConversion, 
double foxMortalityRate, double carryCapacity, double& numRabbits, double& numFoxes)
{
    double deltaRabbit  = rabbitGrowth * numRabbits * (1 - (numRabbits / carryCapacity)) - predationRate * numRabbits * numFoxes;
    double deltaFox = foxPreyConversion * predationRate * numRabbits * numFoxes - foxMortalityRate * numFoxes;

    numRabbits += deltaRabbit;
    numFoxes += deltaFox;
}

/*plotCharacter: The helper method to plotPopulations that allows for the plotting of characters. Outputs spaces and characters
based on scaleNum, and checks if it's not less than or equal to 1. If it is, then we insert spaces
over and over again until we reach the limit, at which point we then print out the respective letter.*/
void plotCharacter(int scaleNum, char letter)
{   
  string temp = "";
    while (scaleNum >= 1)
    {
        temp += " ";
        scaleNum--;
    }
  temp += letter;
  cout << temp;
}
/*plotPopulations: The method that allows for the math related to plotting. foxScale and rabbitScale are calculated using
the provided scale, which in turn, changes how far apart the data is. This is then floored to ensure that the double
conversion doesn't drastically change the results. From there, it is checked if they're equal (to plot '*'), rabbitScale is
greater than foxScale, and an else for the opposite case.*/
void plotPopulations(double numFoxes, double numRabbits, double scale)
{
  int foxScale = floor(numFoxes * scale);
  int rabbitScale = floor(numRabbits * scale);

  if(foxScale == rabbitScale){
    plotCharacter(foxScale, '*');
  }

  else if(foxScale < rabbitScale){
    plotCharacter(foxScale, 'r');
  // Subtract to account for the spaces already printed
    rabbitScale -= foxScale;
    plotCharacter(rabbitScale - 1, 'F');
  }

  else{
    plotCharacter(rabbitScale, 'F');
  // Subtract to account for the spaces already printed
    foxScale -= rabbitScale;
    plotCharacter(foxScale - 1, 'r');
  }
}
/*incrementCounter: Uses a pointer ref to increment i for the main loop. Simple enough.*/
void incrementCounter(int* pointer)
{
    *pointer = *pointer + 1;
}
