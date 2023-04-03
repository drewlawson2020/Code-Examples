/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief Declarations and include statements for GateIoWidget.h
 *
 * Acts as an image representing a Gate. GateIOWidgets are embedded on this.
 *
 */

#ifndef GateWidget_H
#define GateWidget_H

#include <QGraphicsPixmapItem>
#include <QPen>
#include <QPainter>
#include "GateIOWidget.h"

using std::vector;

class GateWidget : public QGraphicsPixmapItem
{
public:
    /**
     * @brief GateWidget - Constructs the gate based on the GateType. Acts as the image that a user drags onto the canvas.
     * @param gateId
     * @param gateType
     * @param parent
     */
    GateWidget(int gateId, QString gateType, QGraphicsItem *parent);

private:
    int _gateId;

};

#endif // GateWidget_H
