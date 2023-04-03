/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This widget is a splash screen that plays gorgeous animations and high-budget
 * special effects for an arbitrary and unnecessary 8 seconds before loading in the main program.
 * Because we wanted to. Bask in the glory of the Bjarne.
 *
 * Also, rotating the gif did not end up being implemented. Rotating individual frame's pixmaps
 * sounded like too much work. Alas, it was not to be. Probably.
 */

#ifndef SPLASHSCREENDIALOG_H
#define SPLASHSCREENDIALOG_H

#include "qmainwindow.h"
#include <QDialog>
#include <QTimer>

namespace Ui {
class SplashScreenDialog;
}

class SplashScreenDialog : public QDialog
{
    Q_OBJECT

public:
    explicit SplashScreenDialog();
    ~SplashScreenDialog();

private:
    Ui::SplashScreenDialog *ui;
    QTimer* timer; // timer to hide the widget and bring up the main form
    QTimer* rotateTimer; // timer for rotating the logo
    QTimer* gifTimer; // timer to rotate the gif
    QMainWindow* mainWindow; // reference to the main window to show it when timer elapses
    QPixmap logoPixmap; // pixmap for the logo; used in rotating it
    QImage logoImage; // image for the logo; made from the pixmap
    QMovie* gif; // the GIFFFFF!!!!!
    int x, y; // center coords for the logo
    int angle_logo; // rotation value for the logo
    int angle_gif; // rotation value for the gif

    /**
     * @brief startMainWindow - Starts the main window.
     */
    void startMainWindow();
    /**
     * @brief rotateLogo - Rotates the logo by an arbitrary constant.
     */
    void rotateLogo();
    /**
     * @brief rotateGif - Rotates the gif.
     */
    void rotateGif();

};

#endif // SPLASHSCREENDIALOG_H
