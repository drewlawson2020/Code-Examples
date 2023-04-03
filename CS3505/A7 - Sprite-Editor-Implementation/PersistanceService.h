#ifndef PERSISTANCESERVICE_H
#define PERSISTANCESERVICE_H

#include "AnimationFrame.h"
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>

using std::vector;

/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Larson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Implementation of PersistanceService that handles serializing and deserializing json sprite files.
 *
 *
 * Code Reviewer: Austin Fashimpaur
 */
class PersistanceService
{
public:

    /**
     * @brief SpriteSaver::LoadFile
     *
     * See https://doc.qt.io/qt-6/qtcore-serialization-savegame-example.html for examples.
     *
     * @param fileName
     * @return
     */
    vector<AnimationFrame> loadFile(QString fileName);

    /**
     * @brief saveFile - Saves the model in a format that can be loaded.
     * @param fileName - Appends .ssp extension automatically.
     * @param animationFrames - The model.
     * @param width - Max width of Model.
     * @param height - Max Height of model.
     */
    void saveFile(QString fileName, vector<AnimationFrame> animationFrames, unsigned int width, unsigned int height);


private:

   /**
    * @brief SpriteSaver::SaveModel Private method that takes the FileName and Model, saves it to a json file.
    * @param fileName - Anyfile name. Do not include .json as the extension.
    * @param model - The QJsonObject model to save.
    */
    void saveModel(QString fileName, QJsonObject model);

    /**
     * @brief saveFrames
     * @param frames
     * @param width
     * @param height
     * @return
     */
    QJsonObject saveFrames(vector<AnimationFrame> frames, unsigned int width, unsigned int height);
};

#endif // PERSISTANCESERVICE_H

