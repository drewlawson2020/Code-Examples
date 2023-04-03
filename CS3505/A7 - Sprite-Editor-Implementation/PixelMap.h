#ifndef PixelMap_H
#define PixelMap_H

#include <vector>
#include <QColor>

using std::vector;


/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Larson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Implementation of PixelMap that handles 2d array of the pixelmap.
 *
 * Code Reviewer: Jonathan Draney
 */
class PixelMap {
    private:
        /**
         * @brief Store the Hex value of the color.
         */
        vector<vector<QColor>> _colorMap;

        /**
         * @brief Width - or the the number of columns.
         *
         */
        unsigned long _width;

        /**
         * @brief Height - or the the number of rows.
         *
         */
        unsigned long _height;

    public:

        /**
         * @brief Construct a new Pixel Map object
         *
         * Defines the dimensions of this pixelmap.
         *
         * @param width
         * @param height
         */
        PixelMap(unsigned long width, unsigned long height, QColor defaultValue);

        /**
         * @brief Creates a map
         *
         * @param map - Creates a PixelMap using a 2-dimensional vector of the map.
         */
        PixelMap(PixelMap const &pixelMap);

        /**
         * @brief Set the Value object
         *
         * @param x
         * @param y
         * @param value
         */
        void setValue(unsigned long x, unsigned long y, QColor value);


        /**
         * @brief Get the Map object
         *
         * @return std::vector<std::vector<std::string>>
         */
        vector<vector<QColor>>& getMap();

};

#endif
