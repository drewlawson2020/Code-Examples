/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is a widget that displays information about the AND gate.
 */

#ifndef GATEINFO_AND_H
#define GATEINFO_AND_H

#include <QWidget>
#include <QFrame>

namespace Ui {
class GateInfo_AND;
}

class GateInfo_AND : public QFrame
{
    Q_OBJECT

public:
    explicit GateInfo_AND();
    ~GateInfo_AND();

private:
    Ui::GateInfo_AND *ui;
};

#endif // GATEINFO_AND_H
