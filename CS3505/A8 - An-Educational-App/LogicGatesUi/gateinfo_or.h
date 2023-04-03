/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is a widget that displays information about the OR gate.
 */

#ifndef GATEINFO_OR_H
#define GATEINFO_OR_H

#include <QFrame>

namespace Ui {
class GateInfo_OR;
}

class GateInfo_OR : public QFrame
{
    Q_OBJECT

public:
    explicit GateInfo_OR();
    ~GateInfo_OR();

private:
    Ui::GateInfo_OR *ui;
};

#endif // GATEINFO_OR_H
