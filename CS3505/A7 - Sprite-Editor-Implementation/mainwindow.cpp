#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "spritemakermodel.h"
#include <QPixmap>
#include <QColor>
#include <QImage>
#include <QProcess>
#include <iostream>
#include <QFileDialog>
#include <QInputDialog>
#include <QColorDialog>
#include <vector>
#include <QTimer>
#include <QHoverEvent>
#include <QMessageBox>

/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Larson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Documentation of class implementation is in the associated header file.
 */
MainWindow::MainWindow(SpriteMakerModel& model, QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
    , colorDialog(parent) {
    ui->setupUi(this);
    currentModel = &model;

    connect(ui->toolButton_AddLayer,&QPushButton::clicked,&model,&SpriteMakerModel::addLayer);
    connect(ui->toolButton_DeleteLayer,&QPushButton::clicked,&model,&::SpriteMakerModel::deleteLayer);
    connect(ui->moveLayerRightButton,&QPushButton::clicked,&model,&::SpriteMakerModel::moveLayerRight);
    connect(ui->moveLayerLeftButton,&QPushButton::clicked,&model,&::SpriteMakerModel::moveLayerLeft);
    connect(ui->horizontalSlider_layerSelector,&QSlider::valueChanged,&model,&::SpriteMakerModel::selectLayer);
    connect(ui->toolButton_AddFrame,&QPushButton::clicked,&model,&SpriteMakerModel::addFrame);
    connect(ui->toolButton_DeleteFrame,&QPushButton::clicked,&model,&::SpriteMakerModel::deleteFrame);
    connect(ui->moveFrameRightButton,&QPushButton::clicked,&model,&::SpriteMakerModel::moveFrameRight);
    connect(ui->moveFrameLeftButton,&QPushButton::clicked,&model,&::SpriteMakerModel::moveFrameLeft);
    connect(ui->horizontalSlider_frameSelector,&QSlider::valueChanged,&model,&::SpriteMakerModel::selectFrame);
    connect(&model,&SpriteMakerModel::updateCurrentLayer,ui->horizontalSlider_layerSelector, qOverload<int>(&QSlider::setSliderPosition));
    connect(&model,&SpriteMakerModel::updateMaxLayer,ui->horizontalSlider_layerSelector,qOverload<int>(&QSlider::setMaximum));
    connect(&model,&SpriteMakerModel::updateCurrentFrame,ui->horizontalSlider_frameSelector,qOverload<int>(&QSlider::setSliderPosition));
    connect(&model,&SpriteMakerModel::updateMaxFrame,ui->horizontalSlider_frameSelector,qOverload<int>(&QSlider::setMaximum));
    connect(&model, &SpriteMakerModel::emitAnimationCycle, this,  &MainWindow::receiveAnimationCycle);
    connect (ui->pushButton, &QPushButton::clicked, &model, &SpriteMakerModel::animationCyclePlayback);
    connect(ui->toolButton_togglePlayAnimation, &QPushButton::clicked, this, &MainWindow::toggleAnimation);
    connect(&colorDialog, &QColorDialog::colorSelected, this, &MainWindow::newColorSelected);
    connect(this, &MainWindow::colorPicked, &model, &SpriteMakerModel::setColor);
    connect(ui->toolButton_togglePlayAnimation, &QToolButton::triggered,this, &MainWindow::toggleAnimation);
    connect(ui->horizontalSlider_frameSelector, &QSlider::sliderMoved,&model, &SpriteMakerModel::canvasFrameChanged);
    connect(this, &MainWindow::canvasPaint, &model, &SpriteMakerModel::canvasClicked);
    connect(&model, &SpriteMakerModel::canvasDrawn, this, &MainWindow::redrawCanvas);
    connect(this, &MainWindow::brushSelected, &model, &SpriteMakerModel::brushSelected);
    connect(this, &MainWindow::eraserSelected, &model, &SpriteMakerModel::eraserSelected);
    connect(timer, &QTimer::timeout, &model, &SpriteMakerModel::animationCyclePlayback);
    connect(ui->horizontalSlider_animationSpeed, &QSlider::valueChanged, this, &MainWindow::adjustTimerSlider);

    connect(this, &MainWindow::toggleGrid, &model, &SpriteMakerModel::redrawCanvas);

    connect(this, &MainWindow::stampSelected, &model, qOverload<bool>(&SpriteMakerModel::stampSelected));
    connect(this, &MainWindow::setStamp, &model, &SpriteMakerModel::setStamp);

    ui->canvasButton->setMouseTracking(true);
    ui->canvasButton->installEventFilter(this);
    ui->canvasButton->setAutoRepeat(true);
    ui->canvasButton->setAutoRepeatDelay(100);
    ui->canvasButton->setStyleSheet(QString("border:1px solid black;"));
    ui->groupBoxStamps->setVisible(false);

    vector<vector<QColor>> color;
    redrawCanvas(color);
}

MainWindow::~MainWindow() {
    delete ui;
    delete timer;
}

bool MainWindow::eventFilter(QObject *obj, QEvent *event) {
    if (event->type() == QEvent::MouseMove)
    {
        tuple<int,int> coordinates = calculateCoordinate();

        int xCoordinate = get<0>(coordinates);

        int yCoordinate = get<1>(coordinates);

        bool haveCoordinatesUpdated = coordiantesUpdated();
        if(haveCoordinatesUpdated)
        {
            lastEmittedCoordinates = coordinates;
            emit hoverCanvas(xCoordinate, yCoordinate);
        }
    }
    else {
        return QMainWindow::eventFilter(obj, event);
    }
}

void MainWindow::adjustTimerSlider() {
    if (timer->isActive()){
        timer->stop();
        timer->start(ui->horizontalSlider_animationSpeed->value() * 100);
    }
}

void MainWindow::receiveAnimationCycle(std::vector<std::vector<QColor>> sentFrame) {
    int size_width = sentFrame.size();
    int size_height = sentFrame[0].size();
    QImage frame(size_width, size_height, QImage::QImage::Format_RGBA8888);

    for (int i = 0; i < size_width; i++)
    {
        for (int j = 0; j < size_height; j++)
        {
            frame.setPixel(i, j, sentFrame[i][j].rgba());
        }
    }

    QPixmap previewPixmap;
    previewPixmap = previewPixmap.fromImage(frame);

    int widthDivided = 0;
    int maintainedUpscaledWidth = ui->animationPreviewWindow->width();
    int maintainedUpscaledHeight = ui->animationPreviewWindow->height();

    if (size_width != 0 && size_width < ui->animationPreviewWindow->width())
    {
        widthDivided =  ui->animationPreviewWindow->height() / size_width;
        maintainedUpscaledWidth = widthDivided * size_width;
    }


    int heightDivided = 0;
    if (size_height != 0 && size_height < ui->animationPreviewWindow->height())
    {
        heightDivided = ui->animationPreviewWindow->width() / size_height;
        maintainedUpscaledHeight = heightDivided * size_height;
    }

    ui->animationPreviewWindow->setPixmap(previewPixmap.scaled(maintainedUpscaledWidth, maintainedUpscaledHeight, Qt::KeepAspectRatio, Qt::FastTransformation));
}

void MainWindow::toggleAnimation() {
    if (!timer->isActive()){
        timer->start(ui->horizontalSlider_animationSpeed->value() * 100);
    }
    else timer->stop();
}

void MainWindow::save() {
    currentModel->saveFile();
}

void MainWindow::on_actionNew_triggered() {
    QInputDialog qDialog ;
    QStringList items;
    items << QString("2");
    items << QString("4");
    items << QString("5");
    items << QString("8");
    items << QString("10");
    items << QString("16");
    items << QString("20");
    items << QString("25");
    items << QString("40");
    items << QString("50");

    qDialog.setOptions(QInputDialog::UseListViewForComboBoxItems);
    qDialog.setComboBoxItems(items);
    qDialog.setWindowTitle("Create New");
    qDialog.setLabelText(QString("Select a Sprite Size. The Size is in Pixels^2."));

    QObject::connect(&qDialog, &QInputDialog::textValueSelected, currentModel, &SpriteMakerModel::createNew);

    qDialog.exec();
}


void MainWindow::on_actionOpen_triggered() {

    QString path = QFileDialog::getOpenFileName(this, "Open Sprite File", QString(), "SpriteEditor files (*.ssp)");

    currentModel->loadFile(path);
}


void MainWindow::on_actionSave_triggered() {

    if (currentModel->isFilepathEmpty()){
        on_actionSave_As_triggered();
    }
    else{
        save();
    }
}


void MainWindow::on_actionSave_As_triggered() {

    currentModel->setFilePath(QFileDialog::getSaveFileName(this, "Save your Sprite", "Sprite", "SpriteEditor files (*.ssp)"));

    save();
}


void MainWindow::on_actionClose_triggered() {
    QApplication::quit();
}

void MainWindow::on_pushButton_ColorPicker_clicked() {
    colorDialog.show();
}

void MainWindow::newColorSelected(const QColor& color) {
    QString styleSheet = "background-color: rgb(";
    styleSheet.append(QString::number(color.red()));
    styleSheet.append(",");
    styleSheet.append(QString::number(color.green()));
    styleSheet.append(",");
    styleSheet.append(QString::number(color.blue()));
    styleSheet.append(");");

    ui->pushButton_ColorPicker->setStyleSheet(styleSheet);

    emit colorPicked(color);

}

void MainWindow::redrawCanvas(std::vector<std::vector<QColor>> arr) {

    int setX = ui->canvasButton->width();
    int setY = ui->canvasButton->height();

    QImage image = QImage(setX, setY, QImage::QImage::Format_RGBA8888);

    //draw pixels
    for (unsigned long int row = 0; row < arr.size(); row++)
       {
           for (unsigned long int column = 0; column < arr[row].size(); column++)
           {
               drawPixel(row, column, arr[row][column], image);
           }
       }

    if(displayGrid)
    {
        //columns
        for(int i = getSquareRatioOfGrid(); i < setX; i = i + getSquareRatioOfGrid()) {
            for(int j = 0; j < setY; j++) {
                image.setPixel(i, j, qRgba(0, 0, 0, 255));
            }
        }

        //rows
        for(int k = 0; k < setX; k++) {
            for(int l = getSquareRatioOfGrid(); l < setY; l = l + getSquareRatioOfGrid()) {
                image.setPixel(k, l, qRgba(0, 0, 0, 255));
            }
        }
    }

    ui->canvasButton->setIcon(QPixmap::fromImage(image));
    ui->canvasButton->setIconSize(QSize(setX,setY));
}

void MainWindow::drawPixel(int row, int column, QColor color, QImage &image) {
    for(int startX = row * getSquareRatioOfGrid(); startX < (row * getSquareRatioOfGrid()) + getSquareRatioOfGrid(); startX++){
        for(int startY = column * getSquareRatioOfGrid(); startY < (column * getSquareRatioOfGrid()) + getSquareRatioOfGrid(); startY++) {

            image.setPixel(startX, startY, color.rgba());
        }
    }
}

void MainWindow::on_canvasButton_clicked() {

    tuple<int,int> coordinates = calculateCoordinate();

    int xCoordinate = get<0>(coordinates);

    int yCoordinate = get<1>(coordinates);

    emit canvasPaint(xCoordinate, yCoordinate);
}

void MainWindow::on_toolButton_Eraser_toggled(bool checked) {
    if(checked){
        emit eraserSelected();
    }

}

void MainWindow::on_toolButton_Pen_toggled(bool checked) {

    if (checked){
        emit brushSelected();
    }
}

tuple<int,int> MainWindow::calculateCoordinate() {

    QPoint realCoords = ui->canvasButton->mapFromGlobal(ui->pushButton->cursor().pos());

    int interpretedX = realCoords.x() / getSquareRatioOfGrid();
    int interpretedY = realCoords.y() / getSquareRatioOfGrid();

    tuple<int,int> result = tuple(interpretedX, interpretedY);

    return result;
}

bool MainWindow::coordiantesUpdated() {

    int lastEmittedXPosition = get<0>(lastEmittedCoordinates);
    int lastEmittedYPosition = get<1>(lastEmittedCoordinates);

    tuple<int,int> newCoordiantes = calculateCoordinate();
    int newXPosition = get<0>(newCoordiantes);
    int newYPosition = get<1>(newCoordiantes);

    return lastEmittedXPosition != newXPosition || lastEmittedYPosition != newYPosition;
}

void MainWindow::on_toolButtonStamp_Star_toggled(bool checked) {
    if(checked){
        emit setStamp("star");
    }
}

void MainWindow::on_toolButtonStamp_Circle_toggled(bool checked) {
    if(checked)
    {
       emit setStamp("circle");
    }
}

void MainWindow::on_toolButton_Stamp_toggled(bool checked) {
    ui->groupBoxStamps->setVisible(checked);
    emit stampSelected(checked);
}


void MainWindow::closeEvent(QCloseEvent* event){
    event->ignore();
    if (currentModel->isModified()) showUnsavedChangesWarningDialog();
    else QApplication::quit();
}

void MainWindow::showUnsavedChangesWarningDialog(){
    QMessageBox mbox;
    mbox.setText("You are about to lose unsaved changes. Proceed?");
    mbox.setStandardButtons(QMessageBox::Save | QMessageBox::Discard | QMessageBox::Cancel);
    mbox.setDefaultButton(QMessageBox::Save);
    int result = mbox.exec();

    switch (result){
    case QMessageBox::Save:
        on_actionSave_triggered();
        QApplication::quit();
        break;
    case QMessageBox::Discard:
        QApplication::quit();
        break;
    case QMessageBox::Cancel:
        // close the dialog, do nothing
        break;
    }
}

void MainWindow::on_displayGridCheckbox_stateChanged(int state)
{
    displayGrid = state == Qt::Checked;

    emit toggleGrid();
}

int MainWindow::getSquareRatioOfGrid() {
    int squareRatio = ui->canvasButton->width() / currentModel->getGridDimension();

    return squareRatio;
}
