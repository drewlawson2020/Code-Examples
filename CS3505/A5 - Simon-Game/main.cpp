#include "mainwindow.h"

#include <QApplication>
// Drew Lawson
// October 26th. 2022
// Professor Johnson
// CS 3505
// This file is the main driver.
int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    simon model;
    MainWindow w(model);
    w.show();
    return a.exec();
}
