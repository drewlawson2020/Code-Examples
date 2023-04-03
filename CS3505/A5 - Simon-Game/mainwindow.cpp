#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QTimer>
// Drew Lawson
// October 26th. 2022
// Professor Johnson
// CS 3505
// This file contains all the essiential code to drive the UI and update it, while also
// sending and receiving signals from simon.cpp.
MainWindow::MainWindow(simon& model, QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
//  UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP UI SETUP

    ui->setupUi(this);
    //Set progress to zero
    ui->moveProgressBar->setValue(0);
    //Disable red/blue buttoms by default
    ui->redPushButton->setDisabled(true);
    ui->bluePushButton->setDisabled(true);
    // Disable gameover label
    ui->gameoverLabel->setVisible(false);
    // Set styles for buttons.
    ui->redPushButton->setStyleSheet( QString("QPushButton {background-color: rgb(200,50,50);} QPushButton:pressed {background-color: rgb(255,150,150);}"));
    ui->bluePushButton->setStyleSheet( QString("QPushButton {background-color: rgb(50,50,200);} QPushButton:pressed {background-color: rgb(150,150,255);}"));

// CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS CONNECTIONS

    // Start Game Connection
    connect(ui->startGameButton, &QPushButton::clicked, &model, &simon::startGame);

    // Red Button Connection
    connect(ui->redPushButton, &QPushButton::clicked, this, &MainWindow::redButtonPress);

    // Blue Button Connection
    connect(ui->bluePushButton, &QPushButton::clicked, this, &MainWindow::blueButtonPress);

    //Connect Progress Bar To NumMoves
    connect (&model, &simon::updateProgBar, ui->moveProgressBar, &QProgressBar::setValue);

    //Connect signals to enable buttons
    connect(&model, &simon::enableButtons, this, &MainWindow::enableButtons);

    //Connect signals to disable buttons
    connect(&model, &simon::disableButtons, this, &MainWindow::disableButtons);

    //Connect signals to enable start button
    connect(&model, &simon::enableStartButton, this, &MainWindow::enableStartButton);

    //Connect signals to disable start button
    connect(&model, &simon::disableStartButton, this, &MainWindow::disableStartButton);

    //Connect signals to flash buttons
    connect(&model, &simon::sendMovesToFlashColors, this, &MainWindow::FlashColorsDriverMethod);

    //Connect redButton to checkInput
    connect(this, &MainWindow::redButtonPressSignal, &model, &simon::checkInput);

    //Connect blueButton to checkInput
    connect(this, &MainWindow::blueButtonPressSignal, &model, &simon::checkInput);

    //Connect timeScale signal
    connect(&model, &simon::timerScale, this, &MainWindow::timerScaleMethod);

    //Connect timerReset signal
    connect(&model, &simon::timerReset, this, &MainWindow::timerReset);

    //Connect randomizeMoves signal
    connect(ui->randomButton, &QRadioButton::clicked, &model, &simon::toggleRandomizedMoves);

    //Connect signals to disable random button
    connect(&model, &simon::disableRandomButton, this, &MainWindow::disableRandomButton);

    //Connect signals to enable random button
    connect(&model, &simon::enableRandomButton, this, &MainWindow::enableRandomButton);

    //Connect signals to disable gameover text button
    connect(&model, &simon::disableGameOverText, this, &MainWindow::disableGameOverText);

    //Connect signals to enable gameover text button
    connect(&model, &simon::enableGameOverText, this, &MainWindow::enableGameOverText);

}

// SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS SLOTS

// Enables the colored buttons
void MainWindow::enableButtons()
{
    ui->redPushButton->setDisabled(false);
    ui->bluePushButton->setDisabled(false);
}
// Disables the colored buttons
void MainWindow::disableButtons()
{
    ui->redPushButton->setDisabled(true);
    ui->bluePushButton->setDisabled(true);
}
// Enables the start game button
void MainWindow::enableStartButton()
{
    ui->startGameButton->setDisabled(false);
}
// Disables the start game button
void MainWindow::disableStartButton()
{
    ui->startGameButton->setDisabled(true);
}
// Enables the randomize button
void MainWindow::enableRandomButton()
{
    ui->randomButton->setDisabled(false);
}
// Disables the randomize button
void MainWindow::disableRandomButton()
{
    ui->randomButton->setDisabled(true);
}

// Enables Gameover text
void MainWindow::enableGameOverText()
{
    ui->gameoverLabel->setVisible(true);
}
// Disables the randomize button
void MainWindow::disableGameOverText()
{
    ui->gameoverLabel->setVisible(false);
}


// Driver method for the FlashColors method. Takes in the moves vector via emit from simon, and
// saves its own temp move list, index, and currMove.
void MainWindow::FlashColorsDriverMethod(QVector<int> moves)
{
    // Surface copy of moves
    tempMoves = moves;
    // Set index for the UI
    currIndexInUI = 0;
    // The current move at the currIndex.
    currMove = tempMoves.at(currIndexInUI);
    // Call the FlashColors method based on a timer scaled delay.
    QTimer::singleShot(timer, this, &MainWindow::FlashColors);
}
// Flashes the colors of the buttons by checking the current move.
void MainWindow::FlashColors()
{
    //Flash the colors. 0 for red, 1 for blue.
    if (currMove == 0)
    {
        ui->redPushButton->setStyleSheet("QPushButton {background-color: rgb(255,100,100);} QPushButton:pressed {background-color: rgb(255,150,150);}");
    }
    else
    {
        ui->bluePushButton->setStyleSheet("QPushButton {background-color: rgb(100,100,255);} QPushButton:pressed {background-color: rgb(150,150,255)");
    }
    // Undo the flashes
    QTimer::singleShot(timer, this, &MainWindow::undoflashColors);
}
// Undos the flashes the colors of the buttons by checking the current move.
// Then, after this, it will re-call FlashColors, stopping when the list's size limit has been reached.
void MainWindow::undoflashColors()
{
    //Flash the colors. 0 for red, 1 for blue.
    if (currMove == 0)
    {
        ui->redPushButton->setStyleSheet("QPushButton {background-color: rgb(200,50,50);} QPushButton:pressed {background-color: rgb(255,150,150);}");
    }
    else
    {
        ui->bluePushButton->setStyleSheet("QPushButton {background-color: rgb(50,50,200);} QPushButton:pressed {background-color: rgb(150,150,255);}");
    }
    // Add to index.
    currIndexInUI++;
    // Check if index is not equal to the size of vector.
    if(currIndexInUI != tempMoves.size())
    {
        // Set currMove to new index.
        currMove = tempMoves.at(currIndexInUI);
        // Call FlashColors again on a timer.
        QTimer::singleShot(timer, this, &MainWindow::FlashColors);
    }
    // Otherwise, Reset the currMove, currIndexInUI, and re-enable the flashing buttons for I/O.
    else
    {
        currMove = 0;
        currIndexInUI = 0;
        enableButtons();
    }
}
// Scales the speed at which the buttons will flash. Reduces the delay by 15% each turn, before capping out at around 200 ms.
void MainWindow::timerScaleMethod()
{
    if (timer >= 200)
    {
        timer *= .85;
    }
}

// Resets the timer back to the default value of 2000.
void MainWindow::timerReset()
{
    timer = 2000;
}
// Default deletion
MainWindow::~MainWindow()
{
    delete ui;
}

// SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS SIGNALS

// Tells simon.cpp that the red button was pressed
void MainWindow::redButtonPress()
{
    emit redButtonPressSignal(0);
}
// Tells simon.cpp that the blue button was pressed
void MainWindow::blueButtonPress()
{
    emit blueButtonPressSignal(1);
}


