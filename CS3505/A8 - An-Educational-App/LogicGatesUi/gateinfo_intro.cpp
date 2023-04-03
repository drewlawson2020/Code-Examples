/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the GateInfo_Intro class.
 */

#include "gateinfo_intro.h"
#include "ui_gateinfo_intro.h"

GateInfo_Intro::GateInfo_Intro(QWidget *parent) :
    QFrame(parent),
    ui(new Ui::GateInfo_Intro)
{
    ui->setupUi(this);

    connect(ui->pushButton_accept, &QPushButton::clicked,
            this, &QWidget::hide);
}

GateInfo_Intro::~GateInfo_Intro()
{
    delete ui;
}
