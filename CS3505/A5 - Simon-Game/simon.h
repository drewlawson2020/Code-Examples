#ifndef SIMON_H
#define SIMON_H

#include <QObject>
// Drew Lawson
// October 26th. 2022
// Professor Johnson
// CS 3505
// This is the header file for simon.h
class simon : public QObject
{
    Q_OBJECT

public:
    explicit simon(QObject *parent = nullptr);

public slots:
     void sendNumOfMoves();
     void startGame();
     void checkInput(int);
     void toggleRandomizedMoves();

signals:
     void updateProgBar(double);
     void sendMovesToFlashColors(QVector<int>);
     void enableButtons();
     void disableButtons();
     void disableStartButton();
     void enableStartButton();
     void timerScale();
     void timerReset();
     void enableRandomButton();
     void disableRandomButton();
     void enableGameOverText();
     void disableGameOverText();

private:
    QVector<int> moves;
    int currIndex;
    void addRandomMove();
    void roundLoop();
    bool randomizedMoves;
    void randomizeMoves();
};

#endif // SIMON_H
