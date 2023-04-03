#include "events.h"

Events::Events()
{

}

CustomGraphicsView::CustomGraphicsView(QWidget *widget) : QGraphicsView(widget)
{
    setAcceptDrops(true);
}

void CustomGraphicsView::dropEvent(QDropEvent *event)
{
    if (event->source() == this) return;
    QListWidget *listwidget = qobject_cast<QListWidget*>(event->source());

    emit itemDrop(listwidget->currentItem()->text());
}

void CustomGraphicsView::dragEnterEvent(QDragEnterEvent *event)
{
    event->accept();
    event->acceptProposedAction();
}

void CustomGraphicsView::dragLeaveEvent(QDragLeaveEvent *event)
{
    event->accept();
}

void CustomGraphicsView::dragMoveEvent(QDragMoveEvent *event)
{
    event->accept();
    event->acceptProposedAction();
}

