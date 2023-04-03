#ifndef SPRITEMAKERMODEL_H
#define SPRITEMAKERMODEL_H
#include <QColor>
#include <QObject>
#include "AnimationFrame.h"
#include "PersistanceService.h"

using std::vector;

/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Larson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Implementation of SpriteMaker (MODEL)
 *
 * Code Reviewer: Tyler DeBruin
 */

class SpriteMakerModel : public QObject{
    Q_OBJECT

private:
    /**
     * @brief filepath - Default FilePath to save/load.
     */
    QString filepath;
    bool modified;

    /**
     * @brief Defines the Layer set for that Frame.
     */
    int currentLayer;

    /**
     * @brief Defines the current Frame selected on the model
     */
    int currentFrame;
    /**
     * @brief The underlying frames/layers of the model.
     */
    vector<AnimationFrame> frames;

    /**
     * @brief Define behavior of the playback window.
     */

    int currentPlaybackFrame;

    /**
     * @brief - Saves the Colors of the Eraser(Full Transparency) - and the current color.
     */
    QColor currentColor;
    QColor preEraserColor;

    /**
     * @brief gridDimension - Defines the Square dimensions of the grid.
     */
    int gridDimension = 16;


    /**
     * @brief Default stamp
     */
    bool isStampSelected = false;

    std::string strCurrentStamp = "star";

    /*
     * Handles Saving/Loading. - This instance variable is a defined class holding signature methods.
     * A prefaced underscore indicates such.
     */
    PersistanceService _persistanceService;

public:
    SpriteMakerModel();
    /**
     * @brief SpriteMakerModel::addLayer
     * Add layer adds a layer to the currently selected frame. This also updates
     * the max layers.
     */
    void addLayer();
    /**
     * @brief SpriteMakerModel::deleteLayer
     * Deletes a layer, and the space used by the layer.
     * For simplicity, it also sets the current layer equal to
     * the first layer, and updates the layer slider as needed.
     * It does not allow you to delete the last layer of a frame
     */
    void deleteLayer();
    /**
     * @brief SpriteMakerModel::moveLayerRight
     * Swaps a layer to the right on the slider, or up in
     * the hierarchy of layers, in other words the right most layer
     * overrides the left layers as needed. Updates canvas
     * and slider as needed. Does not allow movement off
     * the slider.
     */
    void moveLayerRight();
    /**
     * @brief SpriteMakerModel::moveLayerLeft
     * Swaps a layer to the left on the slider, or down in
     * the hierarchy of layers, in other words the right most layer
     * overrides the left layers as needed. Updates canvas
     * and slider as needed. Does not allow movement off
     * the slider.
     */
    void moveLayerLeft();
    /**
     * @brief SpriteMakerModel::selectLayer
     * @param updatedLayer
     * Takes input from the slider, and uses it to set the current layer.
     */
    void selectLayer(int);
    /**
     * @brief SpriteMakerModel::addFrame
     * Adds a new frame to the list of frames. Every frame
     * must have at least one layer, so it also gives it a new layer.
     * Updates frame slider.
     */
    void addFrame();
    /**
     * @brief SpriteMakerModel::deleteFrame
     * Deletes a layer, but it does not allow you to delete the last layer of a frame
     * For simplicity, it also sets the current layer equal to
     * the first layer, and updates the layer slider as needed.
     */
    void deleteFrame();
    /**
     * @brief SpriteMakerModel::moveFrameRight
     * Swaps a frame right on the slider. The left most
     * Frames are the ones that are show first in the preview.
     * Updates slider accordingly. Does not allow movement off
     * the slider.
     */
    void moveFrameRight();
    /**
     * @brief SpriteMakerModel::moveFrameLeft
     * Swaps a frame left on the slider. The left most
     * Frames are the ones that are show first in the preview.
     * Updates slider accordingly. Does not allow movement off
     * the slider.
     */
    void moveFrameLeft();
    /**
     * @brief SpriteMakerModel::selectFrame
     * @param updatedFrame
     * Takes input from the slider, and uses it to set the
     * current frame. Upon moving to a new frame, it also
     * updates the layer slider to show the layers of the new
     * frame. Also redraws the canvas.
     */
    void selectFrame(int);

    bool isModified();

    void setFilePath(QString);

    bool isFilepathEmpty();

    void reloadCanvas(vector<AnimationFrame> animationFrames);

    /**
     * @brief getGridDimension Returns the square dimension of the grid.
     * @return
     */
    int getGridDimension();

signals:
    void updateCurrentLayer(int);
    void updateMaxLayer(int);
    void updateMaxFrame(int);
    void updateCurrentFrame(int);

public slots:
    /**
     * @brief SpriteMakerModel::animationCyclePlayback
     * Drives the main logic for the animation playback.
     * Calls the merged layers, gets the map of QColor, and then emits the framemap to the view.
     */
    void animationCyclePlayback();

    /**
     * @brief canvasClicked(int x, int y)
     * Updates the model with the given pixel addition or stamp.
     */
    void canvasClicked(int x, int y);

    /**
     * @brief setColor(const QColor&)
     * Drives the main logic for the animation playback.
     * Calls the merged layers, gets the map of QColor, and then emits the framemap to the view.
     */
    void setColor(const QColor&);

    /**
     * @brief canvasFrameChanged(int)
     * updates the current frame to the given index.
     */
    void canvasFrameChanged(int);

    /**
     * @brief eraserSelected()
     * eraser selected logic contained here.
     */
    void eraserSelected();

    /**
     * @brief brushSelected()
     * when brush selected logic here.
     */
    void brushSelected();

    /**
     * @brief redrawCanvas()
     * refreshes the canvas from the current model, call this wherever you change your model.
     */
    void redrawCanvas();

    /**
     * @brief stampSelected(bool)
     * is the stamp tool selected bool set.
     */
    void stampSelected(bool);

    /**
     * @brief setStamp(std::string)
     * sets the stamp through a given string to one of the predefined shapes.
     */
    void setStamp(std::string);

    /**
     * @brief saveFile()
     * saves the current file.
     */
    void saveFile();

    /**
     * @brief loadFile(QString);
     * loads the given file with the QString
     */
    void loadFile(QString);
    void createNew(QString);


signals:
    /**
     * @brief emitAnimationCycle(std::vector<std::vector<QColor>>)
     * supports preview by iterating over frames.
     */
    void emitAnimationCycle(std::vector<std::vector<QColor>>);

    /**
     * @brief canvasDrawn(std::vector<std::vector<QColor>>)
     * Draws the given 2d array.
     */
    void canvasDrawn(std::vector<std::vector<QColor>>);
};

#endif // SPRITEMAKERMODEL_H
