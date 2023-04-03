#include "mainwindow.h"
#include "spritemakermodel.h"
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    SpriteMakerModel model;
    MainWindow w(model);
    w.show();
    return a.exec();
}
