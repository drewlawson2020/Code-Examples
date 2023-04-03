/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the GateInfo_Sandbox class.
 */

#include "gateinfo_sandbox.h"
#include "ui_gateinfo_sandbox.h"

GateInfo_Sandbox::GateInfo_Sandbox(QWidget *parent) :
    QFrame(parent, Qt::SplashScreen),
    ui(new Ui::GateInfo_Sandbox)
{
    ui->setupUi(this);

    connect(ui->pushButton_accept, &QPushButton::clicked,
            this, &QWidget::hide);
}

GateInfo_Sandbox::~GateInfo_Sandbox()
{
    delete ui;
}
