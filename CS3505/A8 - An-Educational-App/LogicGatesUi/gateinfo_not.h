/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is a widget that displays information about the NOT gate.
 */

#ifndef GATEINFO_NOT_H
#define GATEINFO_NOT_H

#include <QFrame>

namespace Ui {
class GateInfo_NOT;
}

class GateInfo_NOT : public QFrame
{
    Q_OBJECT

public:
    explicit GateInfo_NOT(QWidget *parent = nullptr);
    ~GateInfo_NOT();

private:
    Ui::GateInfo_NOT *ui;
};

#endif // GATEINFO_NOT_H
