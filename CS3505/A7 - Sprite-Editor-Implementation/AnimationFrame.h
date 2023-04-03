#ifndef AnimationFrame_H
#define AnimationFrame_H
#include <vector>
#include <string>
#include "PixelMap.h"

using std::vector;

/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Larson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Implementation of AnimationFrame that handles layering of pixelmaps.
 *
 *
 * Code Reviewer: Alex Fischer
 */
class AnimationFrame {
    private:
        /**
         * @brief Store the Layers of an animation frame.
         */
        std::vector<PixelMap*> layers;

        /**
         * @brief Tracks the Maximum Width of any PixelMap tracked by this frame.
         */
        int maxWidth;
        /**
         * @brief Tracks the Maximum Height of any PixelMap tracked by this frame.
         */
        int maxHeight;

    public:
        // /**
        //  * @brief Construct a new Animation Frame object
        //  * 
        //  * Creates the object from an 3D Array of Layers.
        //  * 1st Dimension is the Ordering of the layers.
        //  * 
        //  * 2nd/3rd Dimensions form the X_Y Plane of Colors.
        //  * 
        //  * @param layers 
        //  */
        // AnimationFrame(std::vector<std::vector<std::vector<std::string>>> layers);
        
        // /**
        //  * @brief Construct a new Animation Frame object
        //  * 
        //  */
        AnimationFrame() : maxWidth{0} , maxHeight{0} {};

        /**
         * Deconstructor, Implements Rule of 3 to Manage PixelMap* Array.
         */
        ~AnimationFrame();

        /**
         * Copy Ctor
         */
        AnimationFrame(const AnimationFrame &other);

        /**
         * Copy Assignment
         */
        AnimationFrame& operator=(AnimationFrame other);


        /**
         * @brief addLayer - Adds a Layer of any dimension under this frame.
         * @param width
         * @param height
         * @return
         */
        int addLayer(int width, int height);

        /**
         * @brief Deletes a Layer by ID.
         * @param layerId
         * @return
         */
        void deleteLayer(int layerId);

        /**
         * @brief getLayer - Moves a Layer to another position.
         * @param layerId
         * @return
         */
        void moveLayer(int layerId, int newPositionId);

        /**
         * @brief getLayer - Returns the PixelMap associated with the LayerId.
         * @param layerId
         * @return
         */
        PixelMap& getLayer(int layerId);

        /**
         * @brief getLayersSize - Total Count of layers in this AnimationFrame.
         * @return
         */
        int getLayersSize();

        /**
         * @brief getMergedLayers - Merges all layers into a single Pixel Map, and returns that.
         * @return The merged PixelMap.
         */
        PixelMap getMergedLayers();
};

#endif
