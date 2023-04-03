#include "simon.h"
#include <QTimer>
#include <time.h>
// Drew Lawson
// October 26th. 2022
// Professor Johnson
// CS 3505
// This file contains all the essiential code to provide the backbone
// for how the simon game functions, and uses signals to communicate with
// and update the gui code in mainwindow.cpp.
simon::simon(QObject *parent) : QObject(parent) {
}
// Starts the game. Sets a srand, the currIndex, and clears out the moves list.
// Then, resets the timer back to 2000 ms, and starts the loop.
void simon::startGame()
{
    // Disable start and random buttons
    emit disableStartButton();
    emit disableRandomButton();
    emit disableGameOverText();
    srand((unsigned int)time(NULL));
    currIndex = 0;
    moves.clear();
    emit timerReset();
    roundLoop();
}
// The main logic driver. Does everything that runs the game by emitting and taking signals
// in and out from mainwindow.cpp.
void simon::roundLoop()
{
    // Adds a random move to the vector. 0 is red, 1 is blue.
    addRandomMove();

    // Checks if completely random moves are enabled.
    if (randomizedMoves)
    {
        randomizeMoves();
    }
    //Sends the number of moves to progress bar.
    sendNumOfMoves();

    //Calls the timer scaler for the timer to update.
    emit timerScale();

    //Emits the move list to FlashColors.
    emit sendMovesToFlashColors(moves);
}
// The main check for moves, taken from a signal from the buttons in mainwindow.cpp.
// If the move does not match, it will reset the game back to its original state.
// If it does, thenn it will iterate the index and loop the roundLoop.
void simon::checkInput(int buttonPressed)
{
    // Checks if the move doesn't match
    if(buttonPressed != moves.at(currIndex))
    {
        // Reset UI elements.
        emit disableButtons();
        emit enableStartButton();
        emit enableRandomButton();
        emit updateProgBar(0);
        // Enables gameover text
        emit enableGameOverText();
    }
    else
    {
        // Iterates index
        currIndex++;
        // sendsNumOfMoves for the progress bar to update.
        sendNumOfMoves();
        // Checks if Index is equal to the size.
        if (currIndex == moves.size())
        {
            // Disables the buttons again and resets index for roundLoop.
            emit disableButtons();
            currIndex = 0;
            QTimer::singleShot(2000, this, &simon::roundLoop);
        }
    }
}
// Adds random moves to the vector.
void simon::addRandomMove()
{
    // Rand between 0 and 1. Red is 0, blue is 1.
    int randNum = rand() % 2;
    // Adds to end of vector.
    moves.push_back(randNum);
}
// Toggles the randomized moves feature.
void simon::toggleRandomizedMoves()
{
    randomizedMoves = !randomizedMoves;
}
// Custom code for a custom feature. Randomizes every round of moves.
 void simon::randomizeMoves()
{
     // Save a temp size since moves will be cleared.
     int tempSize = moves.size();
     moves.clear();
     // Fill up again with brand new moves.
     for (int i = 0; i < tempSize; i++)
     {
        addRandomMove();
     }
}
// Main method for the progress bar's logic.
void simon::sendNumOfMoves()
{
    // The math is basically the current index divided by size of the vector. The 100.0 is there to make it a whole number instead of a decimal.
   double numToSend = ((double)currIndex / (double)moves.size()) * 100.0;
   // Sends the number over to the progress bar.
   emit updateProgBar(numToSend);
}
