/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief The implementation of the MainWindow.cpp class. Member documentation can be found
 * in the MainWindow.h file.
 */

#include "mainwindow.h"
#include "logicgatecanvas.h"
#include "logicgatelist.h"
#include "ui_mainwindow.h"
#include "gateinfo_and.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
    , timer(this)
    , world(b2Vec2(0.0f, 12.0f))
{
    ui->setupUi(this);

    _logicGateList = new LogicGateList(ui->logicGateListFrame);
    _canvas = new LogicGateCanvas(ui->logicGateCanvasFrame);
    ui->clearButton->hide();
    ui->clearButton->setDisabled(true);
    ConfigureMainMenuStyles();
    ConfigureBox2dPhysics();
    ConfigureSlotsAndSignals();
}

/**
 * @brief Initializes slots and signals
 */
void MainWindow::ConfigureSlotsAndSignals() {
    connect(&_gameModel, &GameModel::SetUpLevel, _canvas, &LogicGateCanvas::AddItemToCanvas);
    connect(&_gameModel, &GameModel::LevelComplete, this, &MainWindow::LevelComplete);
    connect(&_gameModel, &GameModel::AllowGates, _logicGateList, &LogicGateList::SetGates);
    connect(&_gameModel, &GameModel::ResetBoard, _canvas, &LogicGateCanvas::ClearCanvas);

    connect(&_gameModel, &GameModel::SetUpLevel, this, &MainWindow::PresentTutorial);
    connect(_canvas, &LogicGateCanvas::GateConnected, &_gameModel, &GameModel::GateConnected);
    connect(_canvas, &LogicGateCanvas::GateCreated, &_gameModel, &GameModel::GateCreated);
}

/**
 * @brief Shows the appropriate tooltip based on current level.
 */
void MainWindow::PresentTutorial() {
    int chapter = _gameModel.GetChapter();

    if(chapter == 1)
    {
        gateInfo_AND.show();
    }
    else if(chapter == 2)
    {
        gateInfo_OR.show();
    }
    else if(chapter == 3)
    {
        gateInfo_NOT.show();
    }
    else if(chapter == 4)
    {
        gateInfo_NAND.show();
    }
    else if(chapter == 5)
    {
        gateInfo_XOR.show();
    }
    else if(chapter == 6)
    {
        gateInfo_sandbox.show();
    }
}

/**
 * @brief Sets the stylesheet and default visibility on initialization.
 */
void MainWindow::ConfigureMainMenuStyles() {
    ui->MenuWidget->setStyleSheet("background-color: #f2f2f2;");
    ui->newGameButton->setStyleSheet("background-color:#d3d3d3  ;");
    ui->chapterButton->setStyleSheet("background-color:#d3d3d3  ;");
    ui->quitButton->setStyleSheet("background-color:#d3d3d3 ;");
    ui->levelWidget->setVisible(false);
}

/**
 * @brief Initializes box2dphysics with the necessary bodies.
 */
void MainWindow::ConfigureBox2dPhysics(){
    // Define the ground body.
    b2BodyDef groundBodyDef;
    groundBodyDef.position.Set(200.0f, 400.0f);

    // Call the body factory which allocates memory for the ground body
    // from a pool and creates the ground box shape (also from a pool).
    // The body is also added to the world.
    groundBody = world.CreateBody(&groundBodyDef);

    // Define the ground box shape.
    b2PolygonShape groundBox;

    // The extents are the half-widths of the box.
    groundBox.SetAsBox(800.0f, 10.0f);

    // Add the ground fixture to the ground body.
    groundBody->CreateFixture(&groundBox, 0.0f);

    bodyDefTitle.type = b2_dynamicBody;
    bodyDefNewGame.type = b2_dynamicBody;
    bodyDefChapters.type = b2_dynamicBody;
    bodyDefQuit.type = b2_dynamicBody;
    bodyDefLevels.type = b2_dynamicBody;

    //position is set off screen for the "fall" effect
    bodyDefTitle.position.Set(270.0f, -280.0f);
    bodyDefNewGame.position.Set(270.0f, -210.0f);
    bodyDefChapters.position.Set(270.0f, -140.0f);
    bodyDefQuit.position.Set(270.0f, -70.0f);

    bodyDefLevels.position.Set(270.0f, -140.0f);

    bodyTitle = world.CreateBody(&bodyDefTitle);
    bodyNewGame = world.CreateBody(&bodyDefNewGame);
    bodyChapters = world.CreateBody(&bodyDefChapters);
    bodyQuit = world.CreateBody(&bodyDefQuit);
    bodyLevels = world.CreateBody(&bodyDefLevels);

    // Define another box shape for our dynamic body.
    b2PolygonShape dynamicBox;
    dynamicBox.SetAsBox(250.0f, 30.0f);

    // Define the dynamic body fixture.
    b2FixtureDef fixtureDef;
    fixtureDef.shape = &dynamicBox;

    // Set the box density to be non-zero, so it will be dynamic.
    fixtureDef.density = 1.0f;

    // Override the default friction.
    fixtureDef.friction = 0.3f;

    //sets the "bounciness" of a fixture
    fixtureDef.restitution = 0.7;

    //alternate fixture choose level screen. Used to reduce bounciness of default fixture.
    b2FixtureDef fixtureDefLevels;
    fixtureDefLevels.shape = &dynamicBox;
    fixtureDefLevels.density = 1.0f;
    fixtureDefLevels.friction = 0.3f;
    fixtureDefLevels.restitution = 0.3f;

    // Add the shape to the body.
    bodyTitle->CreateFixture(&fixtureDef);
    bodyNewGame->CreateFixture(&fixtureDef);
    bodyChapters->CreateFixture(&fixtureDef);
    bodyQuit->CreateFixture(&fixtureDef);
    bodyLevels->CreateFixture(&fixtureDefLevels);

    //this is set as inactive so not to interfere with main menu
    bodyLevels->SetActive(false);

    connect(&timer, &QTimer::timeout, this, &MainWindow::tick);
    timer.start(1);
}

/**
 * @brief Default Destructor
 */
MainWindow::~MainWindow()
{
    delete ui;
    delete _logicGateList;
    delete _canvas;
}

/**
 * @brief Updates all physics and movement information at a rate of:
 *        60 times per second.
 */
void MainWindow::tick()
{
   world.Step(1.0/60.0, 6, 2);

   //moves the y-axis of ui components based on their respective physics model.
   world.Step(1.0/60.0, 6, 2);
   //qDebug() << (bodyTitle->GetPosition().y);
   ui->titleLabel->move(ui->titleLabel->x(), bodyTitle->GetPosition().y);
   ui->newGameButton->move(ui->newGameButton->x(), bodyNewGame->GetPosition().y);
   ui->chapterButton->move(ui->chapterButton->x(), bodyChapters->GetPosition().y);
   ui->quitButton->move(ui->quitButton->x(), bodyQuit->GetPosition().y);

   //moves y-axis + has offset for widget size
   ui->levelWidget->move(ui->levelWidget->x(), bodyLevels->GetPosition().y - 220);

   update();
}

/**
 * @brief Transitions animation and hides menu widget.
 */
void MainWindow::on_newGameButton_clicked()
{
    transitionToCanvas();
    gateInfo_AND.show();
    gateInfo_intro.show();
    _gameModel.StartNewGame();
}

/**
 * @brief Transitions to chapter menu.
 */
void MainWindow::on_chapterButton_clicked()
{
    groundBody->SetActive(false);

    delay(2);

    groundBody->SetActive(true);
    bodyLevels->SetActive(true);

    ui->levelWidget->setEnabled(true);
    ui->levelWidget->setVisible(true);

    bodyTitle->SetActive(false);
    bodyNewGame->SetActive(false);
    bodyChapters->SetActive(false);
    bodyQuit->SetActive(false);
}

/**
 * @brief Delays all QT events from executing for the number of seconds.
 *        Note: This does not stop any other outside process.
 * @param Number of seconds you want to delay (suggested value: 2)
 */
void MainWindow::delay(int seconds)
{
    QTime dieTime= QTime::currentTime().addSecs(seconds);
    while (QTime::currentTime() < dieTime)
        QCoreApplication::processEvents(QEventLoop::AllEvents, 100);
}

/**
 * @brief Transitions then closes entire QT application.
 */
void MainWindow::on_quitButton_clicked()
{
    groundBody->SetActive(false);

    delay(2);

    close();
}

/**
 * @brief Gives the user the option of returning to main menu or going to the next level.
 */
void MainWindow::LevelComplete() {
    QMessageBox qDialog ;

    qDialog.setWindowTitle("Level Complete!");
    qDialog.setInformativeText("You completed the level! Go to the next level or back to main menu?");
    QPushButton *button = qDialog.addButton("Main Menu", QMessageBox::AcceptRole);
    QPushButton *button2 = qDialog.addButton("Next Level", QMessageBox::AcceptRole);
    qDialog.exec();
    if (qDialog.clickedButton() == button) {
        ui->MenuWidget->show();
        groundBody->SetActive(true);
        resetMainMenuPhysics();
        timer.start();
        ui->clearButton->hide();
        ui->clearButton->setDisabled(true);

    }
    else
    {
        _gameModel.NextLevel();
    }

}

/**
 * @brief Restarts main menu body physics.
 */
void MainWindow::resetMainMenuPhysics()
{
    bodyTitle->SetActive(true);
    bodyNewGame->SetActive(true);
    bodyChapters->SetActive(true);
    bodyQuit->SetActive(true);

    //Resets the main menu physics objects to their default location and velocity.
    bodyTitle->SetTransform(b2Vec2(270.0f, -280.0f),0);
    bodyTitle->SetLinearVelocity(b2Vec2(0,0));

    bodyNewGame->SetTransform(b2Vec2(270.0f, -210.0f),0);
    bodyNewGame->SetLinearVelocity(b2Vec2(0,0));

    bodyChapters->SetTransform(b2Vec2(270.0f, -140.0f),0);
    bodyChapters->SetLinearVelocity(b2Vec2(0,0));

    bodyQuit->SetTransform(b2Vec2(270.0f, -70.0f),0);
    bodyQuit->SetLinearVelocity(b2Vec2(0,0));
 }

/**
 * @brief Transitions out of level select back to main menu.
 */
void MainWindow::on_backButton_clicked()
{

   ui->levelWidget->setEnabled(false);

   groundBody->SetActive(false);

   delay(2);

   ui->levelWidget->setVisible(false);
   groundBody->SetActive(true);

   bodyLevels->SetTransform(b2Vec2(270.0f, 0.0f),0);
   bodyLevels->SetLinearVelocity(b2Vec2(0,0));

   bodyLevels->SetActive(false);

   //restarts main menu body physics.
   resetMainMenuPhysics();
}

/**
 * @brief Logic to load level one.
 */
void MainWindow::transitionToCanvas()
{
    groundBody->SetActive(false);
    delay(2);
    timer.stop();
    bodyLevels->SetTransform(b2Vec2(270.0f, -50.0f),0);
    bodyLevels->SetLinearVelocity(b2Vec2(0,0));
    bodyLevels->SetActive(false);
    ui->levelWidget->setEnabled(false);
    ui->levelWidget->setVisible(false);
    ui->MenuWidget->hide();
    groundBody->SetActive(true);
    ui->clearButton->show();
    ui->clearButton->setDisabled(false);
}

/**
 * @brief Logic to load level one.
 */
void MainWindow::on_levelOneButton_clicked()
{
    _gameModel.SetChapter(1);
    transitionToCanvas();
    gateInfo_AND.show();
}

/**
 * @brief Logic to load level two.
 */
void MainWindow::on_levelTwoButton_clicked()
{
    _gameModel.SetChapter(2);
    transitionToCanvas();
    gateInfo_OR.show();
}

/**
 * @brief Logic to load level three.
 */
void MainWindow::on_levelThreeButton_clicked()
{
    _gameModel.SetChapter(3);
    transitionToCanvas();
    gateInfo_NOT.show();
}

/**
 * @brief Logic to load level four.
 */
void MainWindow::on_levelFourButton_clicked()
{
    _gameModel.SetChapter(4);
    transitionToCanvas();
    gateInfo_NAND.show();
}

/**
 * @brief Logic to load level five.
 */
void MainWindow::on_levelFiveButton_clicked()
{
    _gameModel.SetChapter(5);
    transitionToCanvas();
    gateInfo_XOR.show();
}

/**
 * @brief Logic to load level six.
 */
void MainWindow::on_levelSixButton_clicked()
{
    _gameModel.SetChapter(6);
    transitionToCanvas();
    gateInfo_sandbox.show();
}

/**
 * @brief Restarts round and updates model.
 */
void MainWindow::on_clearButton_clicked()
{
    _gameModel.RestartRound();
}
