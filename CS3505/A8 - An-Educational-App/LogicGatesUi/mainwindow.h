/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief Declarations and include statements for mainwindow.h
 */

#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "gamemodel.h"
#include "logicgatecanvas.h"
#include "logicgatelist.h"
#include "gateinfo_and.h"
#include "gateinfo_or.h"
#include "gateinfo_not.h"
#include "gateinfo_nand.h"
#include "gateinfo_xor.h"
#include "gateinfo_intro.h"
#include "gateinfo_sandbox.h"

#include <QMainWindow>
#include <QLabel>
#include <QListWidgetItem>
#include <QDebug>
#include <Box2D/Box2D.h>
#include <QTimer>
#include <QTime>
#include <QMessageBox>

using std::vector;

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

    void transitionToCanvas();
    
public slots:
    void tick();
    void LevelComplete();

private slots:
    /**
     * @brief Transitions animation and hides menu widget.
     */
    void on_newGameButton_clicked();

    /**
     * @brief Transitions to chapter menu.
     */
    void on_chapterButton_clicked();

    /**
     * @brief Transitions then closes entire QT application.
     */
    void on_quitButton_clicked();

    /**
     * @brief Transitions out of level select back to main menu.
     */
    void on_backButton_clicked();

    /**
     * @brief Logic to load level 1-6.
     */
    void on_levelOneButton_clicked();
    void on_levelTwoButton_clicked();
    void on_levelThreeButton_clicked();
    void on_levelFourButton_clicked();
    void on_levelFiveButton_clicked();
    void on_levelSixButton_clicked();

    /**
     * @brief Restarts round and updates model.
     */
    void on_clearButton_clicked();

private:
    Ui::MainWindow *ui;
    LogicGateCanvas* _canvas;
    LogicGateList* _logicGateList;
    GameModel _gameModel;
    QTimer timer;

    b2World world;
    b2Body* bodyTitle;
    b2Body* bodyNewGame;
    b2Body* bodyChapters;
    b2Body* bodyQuit;
    b2Body* bodyLevels;
    b2Body* groundBody;

    // Gate info widgets
    GateInfo_AND gateInfo_AND;
    GateInfo_OR gateInfo_OR;
    GateInfo_NOT gateInfo_NOT;
    GateInfo_NAND gateInfo_NAND;
    GateInfo_XOR gateInfo_XOR;
    GateInfo_Intro gateInfo_intro;
    GateInfo_Sandbox gateInfo_sandbox;

    // Define the dynamic body. We set its position and call the body factory.
    b2BodyDef bodyDefTitle;
    b2BodyDef bodyDefNewGame;
    b2BodyDef bodyDefChapters;
    b2BodyDef bodyDefQuit;
    b2BodyDef bodyDefLevels;


    /**
     * @brief Delays all QT events from executing for the number of seconds.
     *        Note: This does not stop any other outside process.
     * @param Number of seconds you want to delay (suggested value: 2)
     */
    void delay(int seconds);

    /**
     * @brief Initializes box2dphysics with the necessary bodies.
     */
    void ConfigureBox2dPhysics();

    /**
     * @brief Sets the stylesheet and default visibility on initialization.
     */
    void ConfigureMainMenuStyles();

    /**
     * @brief Initializes slots and signals
     */
    void ConfigureSlotsAndSignals();

    /**
     * @brief Restarts main menu body physics.
     */
    void resetMainMenuPhysics();

    /**
     * @brief Shows the appropriate tooltip based on current level.
     */
    void PresentTutorial();
};
#endif // MAINWINDOW_H
