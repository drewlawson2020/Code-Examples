/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is a widget that displays information about the NAND gate.
 */

#ifndef GATEINFO_NAND_H
#define GATEINFO_NAND_H

#include <QFrame>

namespace Ui {
class GateInfo_NAND;
}

class GateInfo_NAND : public QFrame
{
    Q_OBJECT

public:
    explicit GateInfo_NAND();
    ~GateInfo_NAND();

private:
    Ui::GateInfo_NAND *ui;
};

#endif // GATEINFO_NAND_H
