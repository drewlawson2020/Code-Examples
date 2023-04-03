/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the LogicGateList class.
 */

#include "logicgatelist.h"

LogicGateList::LogicGateList(QWidget *parent)
{
    setIconSize(QSize(75,75));

    setParent(parent);

    setMinimumHeight(parent->height());

    setMinimumWidth(parent->width());

    setDragEnabled(true);

    setSelectionBehavior(SelectionBehavior::SelectItems);

    setSelectionMode(SelectionMode::SingleSelection);

    setDropIndicatorShown(true);

    setDefaultDropAction(Qt::CopyAction);

    setViewMode(QListView::ListMode);

}

void LogicGateList::SetGates(vector<QString> addGates)
{
    this->clear();

    for(int i = 0; i < addGates.size(); i++)
    {
        QString resource = ":/Resources/" + addGates[i] + "Gate.png";

        QListWidgetItem *a = new QListWidgetItem(addGates[i]);
        a->setIcon(QIcon(resource));
        addItem(a);
    }


}
