/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the LogicGateCanvas class.
 */

#include "logicgatecanvas.h"

LogicGateCanvas::LogicGateCanvas(QWidget *parent) : QGraphicsView(parent), _gatesOnCanvas(0), _selectingEndpointForWire{false}, _selectedGate{0}, _selectedIO{0}
{
    setFrameStyle(QFrame::Sunken | QFrame::StyledPanel);

    QGraphicsScene* scene = new QGraphicsScene(0,0, parent->width(), parent->height(), this);

    setScene(scene);

    setAcceptDrops(true);

    setGeometry(QRect(0, 0, parent->width(), parent->height() ));

    show();
}

void LogicGateCanvas::dragEnterEvent(QDragEnterEvent *event)
{
    event->acceptProposedAction();
}

void LogicGateCanvas::dragMoveEvent(QDragMoveEvent *event)
{
    event->acceptProposedAction();
}

void LogicGateCanvas::dropEvent(QDropEvent *event)
{
    QByteArray encoded = event->mimeData()->data("application/x-qabstractitemmodeldatalist");

    QDataStream stream(&encoded, QIODevice::ReadOnly);

    QStringList decodedString;

    while (!stream.atEnd())
    {
        int row, col;
        QMap<int,  QVariant> roleDataMap;
        stream >> row >> col >> roleDataMap;
        decodedString << roleDataMap[Qt::DisplayRole].toString();
    }

    QString gateType = decodedString[0];

    QPoint position = event->position().toPoint();

    QPointF gridPosition = mapFromScene(position);

    AddItemToCanvas(gridPosition.x(), gridPosition.y(), gateType);
}

void LogicGateCanvas::AddItemToCanvas(int x, int y, QString itemName)
{
    int nextGateId = _gatesOnCanvas;

    GateWidget* item = new GateWidget(nextGateId, itemName, nullptr);

    item->setFlags(QGraphicsItem::ItemIsMovable | QGraphicsItem::ItemIsSelectable);

    item->setShapeMode(QGraphicsPixmapItem::BoundingRectShape);

    item->setTransform(QTransform().scale(0.2, 0.2));

    item->setPos(x, y);

    scene()->addItem(item);

    _gatesOnCanvas++;

    QList<QGraphicsItem*> items = item->childItems();

    for(int i = 0; i < items.size(); i++)
    {
        GateIOWidget *gateIoWidget = static_cast<GateIOWidget*>(items[i]);

        connect(&(*gateIoWidget), &GateIOWidget::IOSelected, this, &LogicGateCanvas::IOSelected);
    }

    emit GateCreated(itemName);
}

void LogicGateCanvas::IOSelected(int gateId, int inputId)
{
    if(!_selectingEndpointForWire) {
        DisableInvalidWireDestinations(gateId, inputId);
    }
    else
    {
        DrawLineToDestination(gateId, inputId);
    }
}


GateIOWidget* LogicGateCanvas::GetGate(int gateId, int inputId) {
    QList<QGraphicsItem*> items = scene()->items();

    for(int i = 0; i < items.size(); i++)
    {
        GateIOWidget *gateIoWidget = qgraphicsitem_cast<GateIOWidget*>(items[i]);

        if(gateIoWidget)
        {
            int currentGateId = gateIoWidget->GetGateId();
            int currentInputID = gateIoWidget->GetIOId();

            if(gateId == currentGateId && inputId == currentInputID)
            {
                return gateIoWidget;
            }
        }
    }
}

void LogicGateCanvas::DrawLineToDestination(int endGate, int endInput)
{
    QList<QGraphicsItem*> items = scene()->items();

    GateIOWidget *startingGateWidget = nullptr;
    GateIOWidget *endingGateWidget = nullptr;

    for(int i = 0; i < items.size(); i++)
    {
        GateIOWidget *gateIoWidget = qgraphicsitem_cast<GateIOWidget*>(items[i]);

        if(gateIoWidget)
        {
            gateIoWidget->ResetState();

            int gateId = gateIoWidget->GetGateId();
            int ioID = gateIoWidget->GetIOId();

            if(endGate == gateId && endInput == ioID)
            {
                endingGateWidget = gateIoWidget;
            }
            else if(_selectedGate == gateId && _selectedIO == ioID)
            {
                startingGateWidget = gateIoWidget;
            }
        }
    }

    WireWidget* wireWidget = new WireWidget(startingGateWidget, endingGateWidget);
    scene()->addItem(wireWidget);

    _selectingEndpointForWire = false;
    _selectedGate = 0;
    _selectedIO = 0;

    if(startingGateWidget->IsInput())
    {
        startingGateWidget->Disable();
    }

    if(endingGateWidget->IsInput())
    {
        endingGateWidget->Disable();
    }


    emit GateConnected(startingGateWidget->GetGateId(), startingGateWidget->GetIOId(), endingGateWidget->GetGateId(), endingGateWidget->GetIOId());
}

void LogicGateCanvas::DisableInvalidWireDestinations(int gateId, int inputId) {

    bool inputSelected = inputId > 0;

    QList<QGraphicsItem*> items = scene()->items();

    GateIOWidget* selectedGate = GetGate(gateId, inputId);

    QString gateType = selectedGate->GetGateType();

    for(int i = 0; i < items.size(); i++)
    {
        GateIOWidget *gateIoWidget = qgraphicsitem_cast<GateIOWidget*>(items[i]);

        if(gateIoWidget)
        {
            //If an input is Selected, and this is an input, disable it.
            //If an outputIsSelected, and this is an input, enable it.
            //If its the same gate, turn off all the inputs. DOn't let them draw wires to itself.
            bool isNotSameGate = gateId != gateIoWidget->GetGateId();

            bool isInput = gateIoWidget->IsInput();

            bool canConnectToGate = CanGatesConnect(gateType, gateIoWidget->GetGateType());

            bool allowClick = canConnectToGate && isNotSameGate && ((isInput && !inputSelected) || (inputSelected && !isInput));

            gateIoWidget->SetAllowedClick(allowClick);
        }
    }

   _selectingEndpointForWire = true;
   _selectedGate = gateId;
   _selectedIO = inputId;
}


bool LogicGateCanvas::CanGatesConnect(QString gateType1, QString gateType2) {

    bool canConnectToGate = true;

    if(gateType1 == "InputOn" || gateType1 == "InputOff" || gateType1 == "OutputOn" || gateType1 == "OutputOff") {
        //If Gate one is an input or an Output.
        //Return false if gateType is an input or an output.

        if(gateType2 == "InputOn" || gateType2 ==  "InputOff" || gateType2 ==  "OutputOn" || gateType2 == "OutputOff")
        {
            canConnectToGate = false;
        }
    }

    return canConnectToGate;
}

void LogicGateCanvas::ClearCanvas()
{
    _selectingEndpointForWire = false;;
    _selectedGate = 0;
    _selectedIO = 0;
    _gatesOnCanvas = 0;
    scene()->clear();
}

void LogicGateCanvas::focusOutEvent(QFocusEvent *event)
{
    ResetAllGatesState();

    _selectingEndpointForWire = false;;
    _selectedGate = 0;
    _selectedIO = 0;
    QGraphicsView::focusOutEvent(event);
}

void LogicGateCanvas::mousePressEvent(QMouseEvent *event)
{
    QGraphicsItem *item = scene()->itemAt(mapToScene(event->pos()), QTransform());

    if(item == nullptr)
    {
        _selectingEndpointForWire = false;;
        _selectedGate = 0;
        _selectedIO = 0;

        ResetAllGatesState();
    }

    QGraphicsView::mousePressEvent(event);
}

void LogicGateCanvas::ResetAllGatesState()
{
    QList<QGraphicsItem*> items = scene()->items();

    for(int i = 0; i < items.size(); i++)
    {
        GateIOWidget *gateIoWidget = qgraphicsitem_cast<GateIOWidget*>(items[i]);

        if(gateIoWidget)
        {
            gateIoWidget->ResetState();
        }
    }

}
