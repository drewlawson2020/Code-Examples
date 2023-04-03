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
 * This is an Input/Output widget embedded onto the Gate image in the Graphics view.
 * Its main puprose is to provide the user the ability to draw lines between gates.
 *
 */


#ifndef GATEIOWIDGET_H
#define GATEIOWIDGET_H

#include <QGraphicsEllipseItem>
#include <QBrush>
#include <QPen>

class GateIOWidget : public QObject, public QGraphicsEllipseItem
{
    Q_OBJECT

public:/**
     * @brief GateIOWidget - Constructs a Widget on the
     * @param x - Position embedded on parent.
     * @param y - Position embedded on the parent.
     * @param gateId - ID of the gate - controls
     * @param ioID - ID of the INPUT. -1 is an Output, otherwise 1 -n for Multi input gates.
     * @param gateType - AND, OR, NOT, XOR, NAND
     * @param parent
     */
    GateIOWidget(int x, int y, int gateId, int ioID, QString gateType, QGraphicsItem *parent);

    /**
     * @brief SetAllowedClick - Allows a user to select it after starting to draw a line.
     * @param value
     */
    void SetAllowedClick(bool value);

    /**
     * @brief ResetState - Resets all values as if the object was just created.
     */
    void ResetState();

    /**
     * @brief IsInput - Checks if this IO is an input, or an output.
     * @return
     */
    bool IsInput();

    /**
     * @brief GetGateId - Returns the GateId
     * @return
     */
    int GetGateId();

    /**
     * @brief GetIOId - Returns the IOId
     * @return
     */
    int GetIOId();

    /**
     * @brief Disable - Turns off all interactions.
     */
    void Disable();

    /**
     * @brief IsDisabled - Returns the value if its disabled.
     * @return
     */
    bool IsDisabled();

    /**
     * @brief GetGateType - Returns the string gate name.
     * @return
     */
    QString GetGateType();

    /**
     * @brief hoverEnterEvent - Overwrites events for changing the color of the button when a user attempts to interact with it.
     */
    void hoverEnterEvent(QGraphicsSceneHoverEvent*) override;
    void hoverLeaveEvent(QGraphicsSceneHoverEvent*) override;
    void mousePressEvent(QGraphicsSceneMouseEvent *event) override;

private:
    bool _clicked = false;
    bool _allowClicked = true;
    bool _disabled;

    QString _gateType;
    int _gateId;
    int _ioID;
    int _xPositionRelativeToParent;
    int _yPositionRelativeToParent;

signals:
    /**
     * @brief IOSelected - Emits when this is selected.
     * @param gateId
     * @param inputId
     */
    void IOSelected(int gateId, int inputId);

};

#endif // GATEIOWIDGET_H
