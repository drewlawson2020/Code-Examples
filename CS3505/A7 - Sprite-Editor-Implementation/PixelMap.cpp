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

PixelMap::PixelMap(PixelMap const &pixelMap) :
    _colorMap{pixelMap._colorMap},
    _width{pixelMap._width},
    _height{pixelMap._height} {}

PixelMap::PixelMap(unsigned long width, unsigned long height, QColor defaultValue) :
    _colorMap{width, vector<QColor>(height, defaultValue)},
    _width{width},
    _height{height} {}

void PixelMap::setValue(unsigned long x, unsigned long y, QColor value) {

    if(x >= _width || y >= _height) {
        return;
    }

    _colorMap[x][y] = value;
}

vector<vector<QColor>>& PixelMap::getMap() {
    return _colorMap;
}
