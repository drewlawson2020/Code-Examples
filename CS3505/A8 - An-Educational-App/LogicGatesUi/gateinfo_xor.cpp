/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the GateInfo_XOR class.
 */

#include "gateinfo_xor.h"
#include "ui_gateinfo_xor.h"

GateInfo_XOR::GateInfo_XOR() :
    QFrame(nullptr, Qt::SplashScreen),
    ui(new Ui::GateInfo_XOR)
{
    ui->setupUi(this);

    setFrameStyle(QFrame::Shape::WinPanel | QFrame::Shadow::Plain);

    connect(ui->pushButton_accept, &QPushButton::clicked,
            this, &QWidget::hide);
}

GateInfo_XOR::~GateInfo_XOR()
{
    delete ui;
}
