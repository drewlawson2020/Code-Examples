/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This application is an educational app for teaching about logic gates.
 * It covers 5 logic gates and provides a space for the user to test out each
 * individual gate, as well as a sandbox to use how they wish. This file is the
 * entry point. It displays a splash screen for an arbitrary amount of time (5 sec)
 * before displaying the main window.
 */
#include "mainwindow.h"
#include "splashscreendialog.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    // splash screen
    SplashScreenDialog d;
    d.show();

    QTime dieTime= QTime::currentTime().addSecs(5);
        while (QTime::currentTime() < dieTime)
            QCoreApplication::processEvents(QEventLoop::AllEvents, 100);

    MainWindow w;
    w.show();
    return a.exec();
}
