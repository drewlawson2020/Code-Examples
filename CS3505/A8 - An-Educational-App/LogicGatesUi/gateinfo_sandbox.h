/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is a widget that displays an intro message about the sandbox level, informing
 * the user that they are, in fact, in the sandbox.
 */

#ifndef GATEINFO_SANDBOX_H
#define GATEINFO_SANDBOX_H

#include <QFrame>

namespace Ui {
class GateInfo_Sandbox;
}

class GateInfo_Sandbox : public QFrame
{
    Q_OBJECT

public:
    explicit GateInfo_Sandbox(QWidget *parent = nullptr);
    ~GateInfo_Sandbox();

private:
    Ui::GateInfo_Sandbox *ui;
};

#endif // GATEINFO_SANDBOX_H
