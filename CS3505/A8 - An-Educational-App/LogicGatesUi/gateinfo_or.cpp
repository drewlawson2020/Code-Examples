/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the GateInfo_OR class.
 */

#include "gateinfo_or.h"
#include "ui_gateinfo_or.h"

GateInfo_OR::GateInfo_OR() :
    QFrame(nullptr, Qt::SplashScreen),
    ui(new Ui::GateInfo_OR)
{
    ui->setupUi(this);

    setFrameStyle(QFrame::Shape::WinPanel | QFrame::Shadow::Plain);

    connect(ui->pushButton_accept, &QPushButton::clicked,
            this, &QWidget::hide);
}

GateInfo_OR::~GateInfo_OR()
{
    delete ui;
}
