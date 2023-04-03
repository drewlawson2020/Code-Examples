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
 * QGraphicsScene that allows a User to 'paint' gates together to create logical outputs.
 *
 */

#ifndef LOGICGATECANVAS_H
#define LOGICGATECANVAS_H

#include "GateWidget.h"
#include "wirewidget.h"
#include <QGraphicsView>
#include <QDebug>
#include <QLabel>
#include <QMouseEvent>
#include <QGraphicsPixmapItem>
#include <QTransform>
#include <QDragEnterEvent>
#include <QDropEvent>
#include <QMimeData>
#include <QListWidgetItem>


class LogicGateCanvas : public QGraphicsView
{
    Q_OBJECT

public:
    /**
     * @brief LogicGateCanvas - Constructs the Canvas on a Parent element, i.e. a Frame.
     * @param parent
     */
    LogicGateCanvas(QWidget *parent = nullptr);


    /**
     * @brief LogicGateCanvas::dropEvent
     *
     * https://stackoverflow.com/questions/1723989/how-to-decode-application-x-qabstractitemmodeldatalist-in-qt-for-drag-and-drop
     *
     * @param event
     */
    void dropEvent(QDropEvent *event) override;
    /**
     * @brief dragMoveEvent - Allows the User to drag an image on.
     * @param event
     */
    void dragMoveEvent(QDragMoveEvent *event) override;
    /**
     * @brief dragEnterEvent - Allows the User to drag an image on.
     * @param event
     */
    void dragEnterEvent(QDragEnterEvent *event) override;

    /**
     * @brief focusOutEvent - Overwrites to control the play stopping from drawing lines.
     * @param event
     */
    void focusOutEvent(QFocusEvent *event) override;
    /**
     * @brief focusOutEvent - Overwrites to control the play stopping from drawing lines.
     * @param event
     */
    void mousePressEvent(QMouseEvent *event) override;


private:
    int _gatesOnCanvas;
    int _selectingEndpointForWire;
    int _selectedGate;
    int _selectedIO;

    void DisableInvalidWireDestinations(int gateId, int inputId);
    void DrawLineToDestination(int endGate, int endInput);
    GateIOWidget* GetGate(int gateId, int inputId);
    bool CanGatesConnect(QString gateType1, QString gateType2);
    void ResetAllGatesState();

public slots:
    /**
     * @brief IOSelected - When a slot is selected, connects the Multiple IO points together.
     * @param gateId
     * @param inputId
     */
    void IOSelected(int gateId, int inputId);

    /**
     * @brief AddItemToCanvas - Adds a a Gate, based on its name, to the canvas at the X,Y positions.
     * @param x
     * @param y
     * @param itemName
     */
    void AddItemToCanvas(int x, int y, QString itemName);

    /**
     * @brief ClearCanvas - Removes all the elements from the canvas.
     */
    void ClearCanvas();

signals:
    /**
     * @brief GateCreated - Emits a Gate is created.
     */
    void GateCreated(QString gateType);
    /**
     * @brief GateCreated - Emits when two IOs are drawn together.
     */
    void GateConnected(int firstGateId, int firstIOID, int secondGateId, int secondIOID);

};

#endif // LOGICGATECANVAS_H
