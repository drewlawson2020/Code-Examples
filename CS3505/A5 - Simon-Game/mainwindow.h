#ifndef MAINWINDOW_H
#define MAINWINDOW_H
#include "simon.h"
#include <QMainWindow>
// Drew Lawson
// October 26th. 2022
// Professor Johnson
// CS 3505
// This is the header file for mainwindow.h
QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(simon& model, QWidget *parent = nullptr);
    ~MainWindow();

private:
    Ui::MainWindow *ui;
    int timer = 2000;
    int currIndexInUI;
    int currMove;
    QVector<int> tempMoves;

public slots:
    void FlashColorsDriverMethod(QVector<int>);
    void redButtonPress();
    void blueButtonPress();
    void enableButtons();
    void disableButtons();
    void disableStartButton();
    void FlashColors();
    void undoflashColors();
    void enableStartButton();
    void timerScaleMethod();
    void timerReset();
    void enableRandomButton();
    void disableRandomButton();
    void enableGameOverText();
    void disableGameOverText();

signals:
    void redButtonPressSignal(int);
    void blueButtonPressSignal(int);
};

#endif // MAINWINDOW_H
