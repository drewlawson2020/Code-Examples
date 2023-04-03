#ifndef MAINWINDOW_H
#define MAINWINDOW_H
#include <QMainWindow>
#include <QColor>
#include "spritemakermodel.h"
#include <QColorDialog>
#include <QTimer>
#include <tuple>

using std::tuple;
using std::get;

/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Lawson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Implementation of MainWindow (VIEW).
 *
 * Code Reviewer: Drew Lawson
 */
QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

/**
 * @brief The MainWindow class contains the main form in which the application is run.
 */
class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    /**
     * @brief MainWindow - Creates a new main window which saves a reference to a SpriteMakerModel.
     * @param model - The model to save a reference to.
     * @param parent
     */
    MainWindow(SpriteMakerModel& model, QWidget *parent = nullptr);
    /**
     * Destroys the MainWindow and frees allocated resources.
     */
    ~MainWindow();

private:
    Ui::MainWindow *ui;

    // instance vars

    /**
     * @brief currentModel - A reference to the backing model.
     */
    SpriteMakerModel *currentModel;

    /**
     * @brief colorDialog - A color dialog that prompts the user for colors and passes them to the model.
     */
    QColorDialog colorDialog;

    /**
     * @brief timer - The timer used for the animation playback window.
     */
    QTimer *timer = new QTimer(this);

    /**
     * @brief displayGrid - Determines whether or not the grid should be displayed.
     */
    bool displayGrid = true;

    /**
     * @brief _lastEmittedCoordinates - The last coordinates send to the model.
     */
    tuple<int,int> lastEmittedCoordinates;


    // functions

    /**
     * @brief save - Saves the image to a file.
     */
    void save();

    /**
     * @brief Draws the pixels and grid.
     * @param row
     * @param column
     * @param arr
     * @param image
     */
    void drawPixel(int row, int column, QColor arr, QImage &image);

    /**
     * @brief Getter for square ratio of grid
     */
    int getSquareRatioOfGrid();

    /**
     * @brief Draws a popup window warning the user against closing before saving.
     */
    void showUnsavedChangesWarningDialog();

    /**
     * @brief The event on closing the window.
     */
    void closeEvent(QCloseEvent*);

    /**
     * @brief eventFilter - Ties into the MouseEvent. Delegates to the correct slot for all other events.
     * @param obj
     * @param event
     * @return
     */
    bool eventFilter(QObject *obj, QEvent *event);

    /**
     * @brief calculateCoordinate
     * @return  Returns a Tuple<x,y> of the coordinates of the mouse in the sprite canvas.
     */
    tuple<int,int> calculateCoordinate();

    /**
     * @brief Checks if the current coordinates are different from what was last updated.
     * @return
     */
    bool coordiantesUpdated();



public slots:
    /**
     * @brief Takes in the 2D animation data from the model, and displays it on the animation preview window.
     */
     void receiveAnimationCycle(std::vector<std::vector<QColor>>);
     /**
      * @brief Takes in the frame data as a QColor and is used to redraw the UI to udpate it.
      */
     void redrawCanvas(std::vector<std::vector<QColor>> arr);

signals:
     /**
      * @brief Calls the next frame data.
      */
     void nextAnimationCycle();
     /**
      * @brief Paints on the canvas
      */
     void canvasPaint(int x, int y);
     /**
      * @brief Tracks x/y coodinates
      */
     void hoverCanvas(int x, int y);
     /**
      * @brief Picks the color to be drawn onto the canvas.
      */
     void colorPicked(const QColor&);
     /**
      * @brief Toggles when the eraser is selected
      */
     void eraserSelected();
     /**
      * @brief Toggles when the stamp is selected
      */
     void stampSelected(bool);
     /**
      * @brief Toggles when the stamp is selected
      */
     void setStamp(std::string);
     /**
      * @brief Toggles when the brush is selected
      */
     void brushSelected();
     /**
      * @brief Toggles when the grid is drawn
      */
     void toggleGrid();

private slots:
     /**
      * @brief Handler for when new file is made
      */
     void on_actionNew_triggered();
     /**
      * @brief Handler for when a new file is opened
      */
     void on_actionOpen_triggered();
     /**
      * @brief Handler for when a file is saved
      */
     void on_actionSave_triggered();
     /**
      * @brief Handler for when a file is saved as
      */
     void on_actionSave_As_triggered();
     /**
      * @brief Handler for when the window is closed.
      */
     void on_actionClose_triggered();
     /**
      * @brief Handler for when the canvas is clicked
      */
     void on_canvasButton_clicked();
     /**
      * @brief Handler for when the ColorPicker is clicked
      */
     void on_pushButton_ColorPicker_clicked();
     /**
      * @brief Handler for when the pen is toggled
      */
     void on_toolButton_Pen_toggled(bool checked);
     /**
      * @brief Handler for when the stamp is toggled
      */
     void on_toolButton_Stamp_toggled(bool checked);
     /**
      * @brief Handler for when the eraser is toggled
      */
     void on_toolButton_Eraser_toggled(bool checked);

     // custom private slots
     void newColorSelected(const QColor&);
     /**
      * @brief Enables or disables the animation playback.
      */
     void toggleAnimation();
     /**
      * @brief Provides the logic for stopping the playback timer and scaling the playback speed.
      */
     void adjustTimerSlider();
     /**
      * @brief Displaying checkbox
      */
     void on_displayGridCheckbox_stateChanged(int state);
     /**
      * @brief Stamp toggled for star
      */
     void on_toolButtonStamp_Star_toggled(bool checked);
     /**
      * @brief Stampe toggled for Circle
      */
     void on_toolButtonStamp_Circle_toggled(bool checked);
};
#endif // MAINWINDOW_H
