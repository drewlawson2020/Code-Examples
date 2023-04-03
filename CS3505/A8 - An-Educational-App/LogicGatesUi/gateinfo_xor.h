/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is a widget that displays information about the XOR gate.
 */

#ifndef GATEINFO_XOR_H
#define GATEINFO_XOR_H

#include <QFrame>

namespace Ui {
class GateInfo_XOR;
}

class GateInfo_XOR : public QFrame
{
    Q_OBJECT

public:
    explicit GateInfo_XOR();
    ~GateInfo_XOR();

private:
    Ui::GateInfo_XOR *ui;
};

#endif // GATEINFO_XOR_H
