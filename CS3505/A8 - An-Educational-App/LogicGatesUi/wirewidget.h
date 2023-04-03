/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief Declarations and include statements for gamemodel.h
 */

#ifndef WIREWIDGET_H
#define WIREWIDGET_H

#include "GateIOWidget.h"
#include <QGraphicsLineItem>

class WireWidget : public QGraphicsLineItem
{
public:
    /**
     * @brief WireWidget - Connection between Two Pins.
     * @param pinA
     * @param pinB
     */
    WireWidget(GateIOWidget* pinA, GateIOWidget* pinB);

    /**
     * @brief GetPinA - Returns the Pointer to GateIOWidget.
     * @return
     */
    GateIOWidget* GetPinA();

    /**
     * @brief GetPinB - Return the pointer to GateIOWidget.
     * @return
     */
    GateIOWidget* GetPinB();

    /**
     * @brief paint - Overwrites paint, to continuously draw the line.
     * @param painter
     * @param option
     * @param widget
     */
    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget = nullptr) override;

private:
    GateIOWidget* _pinA;
    GateIOWidget* _pinB;
    void UpdatePosition();

};

#endif // WIREWIDGET_H
