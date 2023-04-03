/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the SplashScreenDialog class.
 */

#include "splashscreendialog.h"
#include "ui_splashscreendialog.h"
#include <QTimer>
#include <QTransform>
#include <QMovie>

SplashScreenDialog::SplashScreenDialog() :
    QDialog(nullptr),
    ui(new Ui::SplashScreenDialog),
    logoPixmap(":/Resources/bb.png"),
    logoImage(logoPixmap.toImage())
{
    ui->setupUi(this);

    x = logoImage.rect().center().x();
    y = logoImage.rect().center().y();
    angle_logo = 0;

    this->setWindowFlags(Qt::SplashScreen);
    rotateTimer = new QTimer();
    rotateTimer->setTimerType(Qt::PreciseTimer);
    connect(rotateTimer, &QTimer::timeout,
            this, &SplashScreenDialog::rotateLogo);
    rotateTimer->start(1000/60);

    QMovie* movie = new QMovie(":/Resources/bjarne.gif");
    ui->label_gif->setMovie(movie);
    movie->start();

    gifTimer = new QTimer();
    gifTimer->setSingleShot(true);
    connect(gifTimer, &QTimer::timeout,
            this, &SplashScreenDialog::rotateGif);
    gifTimer->start(5000);
    angle_gif = 0;

    // setup a timer to kill this widget and display the main window.
    timer = new QTimer(this);
    connect(timer, &QTimer::timeout,
            this, &SplashScreenDialog::startMainWindow);
    timer->start(5000);
}

void SplashScreenDialog::rotateGif(){
    QTransform t;
    QPoint p = ui->label->rect().center();
    t.translate(p.x(), p.y());
    t.rotate(angle_gif += 30);

}

void SplashScreenDialog::rotateLogo(){
    QTransform t;
    t.translate(x, y);
    t.rotate(angle_logo += 1);
    QImage i = logoImage.transformed(t);
    ui->label->setPixmap(QPixmap::fromImage(i));
}

void SplashScreenDialog::startMainWindow(){
    timer->stop();
    rotateTimer->stop();
    this->hide();
}

SplashScreenDialog::~SplashScreenDialog()
{
    delete ui;
    delete timer;
    delete rotateTimer;
}
