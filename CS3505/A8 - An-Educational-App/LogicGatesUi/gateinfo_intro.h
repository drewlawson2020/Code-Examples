/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is a widget that displays an introduction page about the application.
 */

#ifndef GATEINFO_INTRO_H
#define GATEINFO_INTRO_H

#include <QFrame>

namespace Ui {
class GateInfo_Intro;
}

class GateInfo_Intro : public QFrame
{
    Q_OBJECT

public:
    explicit GateInfo_Intro(QWidget *parent = nullptr);
    ~GateInfo_Intro();

private:
    Ui::GateInfo_Intro *ui;
};

#endif // GATEINFO_INTRO_H
