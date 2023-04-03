#include "spritemakermodel.h"
#include <QColor>
#include <iostream>
#include "PersistanceService.h"

/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Larson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Documentation of class implementation is in the associated header file.
 */

SpriteMakerModel::SpriteMakerModel() :
    currentLayer(0)
  , currentFrame(0)
  , currentPlaybackFrame(0)
  , currentColor(QColor("black")) {

    addFrame();
    modified = false;
}

void SpriteMakerModel::canvasFrameChanged(int frameIndex){
    currentFrame = frameIndex;
}

void SpriteMakerModel::eraserSelected(){
    if (currentColor  != QColor(0, 0, 0, 0)) {
        preEraserColor = currentColor;
    }
    currentColor = QColor(0, 0, 0, 0);
}

void SpriteMakerModel::brushSelected(){
    if (currentColor == QColor(0, 0, 0, 0)){
        currentColor = preEraserColor;
    }
}

bool SpriteMakerModel::isModified() {
    return modified;
}

void SpriteMakerModel::setFilePath(QString path) {
    filepath = path;
}

void SpriteMakerModel::setColor(const QColor& color) {

    if (currentColor == QColor(0, 0, 0, 0)){
        preEraserColor = color;
    }
    else{
        currentColor = color;
    }
}

void SpriteMakerModel::animationCyclePlayback()
{
    PixelMap currFrame = frames[currentPlaybackFrame].getMergedLayers();
    std::vector<std::vector<QColor>> frameMap = currFrame.getMap();

    if (currentPlaybackFrame + 1 < (int)frames.size())
    {
        currentPlaybackFrame++;
    }
    else
    {
        currentPlaybackFrame = 0;
    }
    emit emitAnimationCycle(frameMap);
}

void SpriteMakerModel::canvasClicked(int x, int y) {
    PixelMap& layer = frames[currentFrame].getLayer(currentLayer);

    if(isStampSelected) {
        QColor color;
        if (currentColor == QColor(0, 0, 0, 0)){
            color = preEraserColor;
        }
        else{
            color = currentColor;
        }

        if(strCurrentStamp == "star") {
            //draw star
            layer.setValue(x, y, color);
            layer.setValue(x+1, y, color);
            layer.setValue(x-1, y, color);
            layer.setValue(x, y+1, color);
            layer.setValue(x, y-1, color);
        }
        if(strCurrentStamp == "circle") {
            //draw circle
            layer.setValue(x, y, color);
            layer.setValue(x+1, y, color);
            layer.setValue(x, y+1, color);
            layer.setValue(x+1, y+1, color);
            layer.setValue(x-1, y, color);
            layer.setValue(x-1, y-1, color);
            layer.setValue(x, y-1, color);
            layer.setValue(x+1, y-1, color);
            layer.setValue(x-1, y+1, color);

            layer.setValue(x+2, y, color);
            layer.setValue(x+2, y+1, color);
            layer.setValue(x+2, y-1, color);

            layer.setValue(x,   y+2, color);
            layer.setValue(x+1, y+2, color);

            layer.setValue(x,   y-2, color);
            layer.setValue(x+1, y-2, color);
        }
    } else {
        layer.setValue(x, y, currentColor);
    }

    redrawCanvas();
    modified = true;
}


void SpriteMakerModel::addLayer() {
    frames[currentFrame].addLayer(gridDimension, gridDimension);
    emit updateMaxLayer(frames[currentFrame].getLayersSize());
}


void SpriteMakerModel::deleteLayer() {
    frames[currentFrame].deleteLayer(currentLayer);
    currentLayer = 0;
    emit updateCurrentLayer(currentLayer + 1);
    emit updateMaxLayer(frames[currentFrame].getLayersSize());
    redrawCanvas();

}

void SpriteMakerModel::moveLayerRight() {
    if (currentLayer != frames[currentFrame].getLayersSize() - 1) {
        frames[currentFrame].moveLayer(currentLayer, currentLayer + 1);
        currentLayer += 1;
        emit updateCurrentLayer(currentLayer + 1);
    }
    redrawCanvas();
}

void SpriteMakerModel::moveLayerLeft() {

    if (currentLayer != 0) {
        frames[currentFrame].moveLayer(currentLayer, currentLayer - 1);
        currentLayer -= 1;
        emit updateCurrentLayer(currentLayer + 1);
    }
    redrawCanvas();
}

void SpriteMakerModel::selectLayer(int updatedLayer) {
    currentLayer = updatedLayer - 1;
    emit updateCurrentLayer(currentLayer + 1);
    redrawCanvas();
}

void SpriteMakerModel::addFrame() {
    frames.push_back(AnimationFrame());
    frames.back().addLayer(gridDimension, gridDimension);
    emit updateMaxFrame(frames.size());
}

void SpriteMakerModel::deleteFrame() {
    if (frames.size() > 1){
        frames.erase(frames.begin() + currentFrame);
        currentFrame = 0;
        emit updateCurrentFrame(currentFrame + 1);
        currentLayer = 0;
        emit updateCurrentLayer(currentLayer + 1);
        emit updateMaxLayer(frames[currentFrame].getLayersSize());
        emit updateMaxFrame(frames.size());
        redrawCanvas();
    }
}

void SpriteMakerModel::moveFrameRight() {
    if (currentFrame != frames.size() - 1) {
        AnimationFrame current = frames[currentFrame];
        AnimationFrame above = frames[currentFrame + 1];
        frames[currentFrame] = above;
        frames[currentFrame + 1] = current;
        currentFrame += 1;
        emit updateCurrentFrame(currentFrame + 1);

        currentLayer = 0;
        emit updateCurrentLayer(currentLayer + 1);
        emit updateMaxLayer(frames[currentFrame].getLayersSize());
    }
    redrawCanvas();
}

void SpriteMakerModel::moveFrameLeft() {
    if (currentFrame != 0) {
        AnimationFrame current = frames[currentFrame];
        AnimationFrame below = frames[currentFrame - 1];
        frames[currentFrame] = below;
        frames[currentFrame - 1] = current;
        currentFrame -= 1;
        emit updateCurrentFrame(currentFrame + 1);

        currentLayer = 0;
        emit updateCurrentLayer(currentLayer + 1);
        emit updateMaxLayer(frames[currentFrame].getLayersSize());
    }
    redrawCanvas();
}

void SpriteMakerModel::selectFrame(int updatedFrame) {
    currentFrame = updatedFrame - 1;
    currentLayer = 0;
    emit updateCurrentLayer(currentLayer + 1);
    emit updateMaxLayer(frames[currentFrame].getLayersSize());
    redrawCanvas();
}

void SpriteMakerModel::redrawCanvas() {
    PixelMap currentPixelMap = frames[currentFrame].getLayer(currentLayer);

    vector<vector<QColor>> resultMap = currentPixelMap.getMap();

    emit canvasDrawn(resultMap);
}


void SpriteMakerModel::saveFile() {
    _persistanceService.saveFile(filepath, frames, gridDimension, gridDimension);
    modified = false;
}

void SpriteMakerModel::loadFile(QString filepathToOpen) {
    filepath = filepathToOpen;

    vector<AnimationFrame> animationFrames = _persistanceService.loadFile(filepath);

    reloadCanvas(animationFrames);
}

void SpriteMakerModel::createNew(QString pixelDimensionString) {
    int newDimension = pixelDimensionString.toInt();

    AnimationFrame animationFrame;

    animationFrame.addLayer(newDimension, newDimension);

    vector<AnimationFrame> animationFrames;

    animationFrames.push_back(animationFrame);

    reloadCanvas(animationFrames);
}

void SpriteMakerModel::reloadCanvas(vector<AnimationFrame> animationFrames) {
    frames = animationFrames;
    currentLayer = 0;
    currentFrame = 0;
    currentPlaybackFrame = 0;
    modified = false;
    gridDimension = animationFrames[0].getLayer(0).getMap().size();

    emit updateCurrentFrame(1);
    emit updateCurrentLayer(1);
    emit updateMaxLayer(frames[currentFrame].getLayersSize());
    emit updateMaxFrame(frames.size());

    redrawCanvas();
}

int SpriteMakerModel::getGridDimension() {
    return gridDimension;
}

void SpriteMakerModel::stampSelected(bool b) {
    isStampSelected = b;
}

void SpriteMakerModel::setStamp(std::string s) {
    strCurrentStamp = s;
}

bool SpriteMakerModel::isFilepathEmpty(){
    return filepath.isEmpty();
}

