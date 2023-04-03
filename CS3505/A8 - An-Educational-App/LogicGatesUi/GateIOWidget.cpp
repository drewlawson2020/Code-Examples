/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the GateIOWidget class.
 */

#include "GateIOWidget.h"

GateIOWidget::GateIOWidget(int x, int y, int gateId, int ioID, QString gateType, QGraphicsItem *parent) : QGraphicsEllipseItem(x, y, 50,50, parent),
    _gateType(gateType),
    _gateId{gateId},
    _ioID{ioID},
    _xPositionRelativeToParent{x},
    _yPositionRelativeToParent{y}
{
    setAcceptHoverEvents(true);
    setBrush(QBrush(Qt::white, Qt::SolidPattern));
    setPen(QPen(Qt::black, 15, Qt::SolidLine));
}

void GateIOWidget::hoverEnterEvent(QGraphicsSceneHoverEvent*)
{
    if(_allowClicked && !_disabled)
    {
        if(!_clicked)
        {
            setBrush(QBrush(Qt::black, Qt::SolidPattern));
        }
    }
}

void GateIOWidget::hoverLeaveEvent(QGraphicsSceneHoverEvent*)
{
    if(_allowClicked && !_disabled)
    {
        if(!_clicked)
        {
            setBrush(QBrush(Qt::white, Qt::SolidPattern));
        }
    }
}

void GateIOWidget::mousePressEvent(QGraphicsSceneMouseEvent*)
{
    if(_allowClicked && !_disabled)
    {
        _clicked = !_clicked;

        if(_clicked)
        {
            setPen(QPen(Qt::blue, 15, Qt::SolidLine));

            emit IOSelected(_gateId, _ioID);
        }
        else
        {
            setPen(QPen(Qt::black, 15, Qt::SolidLine));
        }
    }
}



void GateIOWidget::SetAllowedClick(bool value)
{
    _allowClicked = value;

    if(value)
    {
        setPen(QPen(Qt::green, 15, Qt::SolidLine));
    }
    else
    {
        setPen(QPen(Qt::red, 15, Qt::SolidLine));
    }
}

bool GateIOWidget::IsInput() {
    return _ioID > 0;
}

int GateIOWidget::GetGateId()
{
    return _gateId;
}

int GateIOWidget::GetIOId() {
    return _ioID;
}

void GateIOWidget::ResetState()
{
    _allowClicked = true;
    _clicked = false;
    setBrush(QBrush(Qt::white, Qt::SolidPattern));
    setPen(QPen(Qt::black, 15, Qt::SolidLine));
}

void GateIOWidget::Disable() {
    _disabled = true;

    setPen(QPen(Qt::black, 15, Qt::SolidLine));
}

bool GateIOWidget::IsDisabled() {
    return _disabled;
}

QString GateIOWidget::GetGateType()
{
    return _gateType;
}
