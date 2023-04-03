#include "AnimationFrame.h"
#include "PixelMap.h"

/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Larson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Documentation of class implementation is in the associated header file.
 */
int AnimationFrame::addLayer(int width, int height) {

    if(width > maxWidth) {
        maxWidth = width;
    }

    if(height > maxHeight) {
        maxHeight = height;
    }

    QColor defaultColor(0,0,0,0);

    PixelMap* pixelMap = new PixelMap(width, height, defaultColor);

    int layerId = layers.size();
    
    layers.push_back(pixelMap);

    return layerId;
}

void AnimationFrame::deleteLayer(int layerId) {
    if(layers.size() > 1)
    {
        delete layers[layerId];
        layers.erase(layers.begin() + layerId);
    }
}

PixelMap& AnimationFrame::getLayer(int layerId) {

    PixelMap* layer = layers[layerId];

    PixelMap& result = (*layer);

    return result;
}

int AnimationFrame::getLayersSize() {
    return layers.size();
}

void AnimationFrame::moveLayer(int layerId, int newPositionId) {
    PixelMap* swappedElement = layers[newPositionId];

    layers[newPositionId] = layers[layerId];

    layers[layerId] = swappedElement;
}


PixelMap AnimationFrame::getMergedLayers() {

    QColor defaultColor(0,0,0,0);

    PixelMap result( maxWidth, maxHeight, defaultColor);

    for(unsigned long int i = 0; i < layers.size(); i++) {

        PixelMap* pixelMap = layers[i];

        auto map = (*pixelMap).getMap();

        for(unsigned long int row = 0; row < map.size(); row++) {

            for(unsigned long int column = 0; column < map[row].size(); column++) {

                QColor value = map[row][column];

                if(value.alpha() != 0)
                {
                    result.setValue(row, column, value);
                }
            }
        }
    }

    return result;
}


AnimationFrame::~AnimationFrame() {
    for(unsigned long int i = 0; i < layers.size(); i++) {
        delete layers[i];
    }
}

AnimationFrame::AnimationFrame(const AnimationFrame &other) {

    maxHeight = other.maxHeight;
    maxWidth = other.maxWidth;

    for(unsigned long int i = 0; i < other.layers.size(); i++) {

        PixelMap copiedPixelMap = *other.layers[i];

        PixelMap* copiedLayer = new PixelMap(copiedPixelMap);

        layers.push_back(copiedLayer);
    }
}

AnimationFrame& AnimationFrame::operator=(AnimationFrame other) {
    std::swap(layers, other.layers);

    maxHeight = other.maxHeight;
    maxWidth = other.maxWidth;

    return *this;
}
