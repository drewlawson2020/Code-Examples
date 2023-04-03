/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the WireWidget class.
 */

#include "wirewidget.h"

WireWidget::WireWidget(GateIOWidget* pinA, GateIOWidget* pinB) : QGraphicsLineItem()
{
    _pinA = pinA;
    _pinB = pinB;

    this->setPen(QPen(Qt::black, 3));

    this->setFlags(QGraphicsItem::ItemIsSelectable);

    UpdatePosition();
}

GateIOWidget* WireWidget::GetPinA() {
    return _pinA;
}

GateIOWidget* WireWidget::GetPinB() {
    return _pinB;
}

void WireWidget::UpdatePosition()
{
    QPointF wireStartPosition = _pinA->mapToScene(_pinA->rect().center());

    QPointF wireEndPosition = _pinB->mapToScene(_pinB->rect().center());

    this->setLine(QLineF(wireStartPosition, wireEndPosition));
}


void WireWidget::paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget) {

    UpdatePosition();

    QGraphicsLineItem::paint(painter, option, widget);
}
