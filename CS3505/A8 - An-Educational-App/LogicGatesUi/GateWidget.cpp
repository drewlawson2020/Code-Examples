/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the GateWidget class.
 */

#include "GateWidget.h"

GateWidget::GateWidget(int gateId, QString gateType, QGraphicsItem *parent) : QGraphicsPixmapItem(parent), _gateId{gateId}
{
    QString resource = ":/Resources/" + gateType + "Gate.png";

    QPixmap image = QPixmap::fromImage(QImage{resource});

    setPixmap(image);

    if(gateType == "AND" || gateType == "OR" || gateType == "NAND" || gateType == "XOR")
    {
        new GateIOWidget(10, 140, _gateId, 1, gateType, this);

        new GateIOWidget(10, 310, _gateId, 2, gateType, this);
        new GateIOWidget(440, 230, _gateId, -1, gateType, this); //output
    }
    else if (gateType == "NOT")
    {
        new GateIOWidget(10, 230, _gateId, 1, gateType, this);//input
        new GateIOWidget(440, 230, _gateId, -1, gateType, this); //output
    }
    else if (gateType == "InputOn" || gateType == "InputOff")
    {
        new GateIOWidget(440, 230, _gateId, -1, gateType, this);
    }
    else if (gateType == "OutputOn" || gateType == "OutputOff")
    {
        new GateIOWidget(10, 230, _gateId, 1, gateType, this);
    }
}

