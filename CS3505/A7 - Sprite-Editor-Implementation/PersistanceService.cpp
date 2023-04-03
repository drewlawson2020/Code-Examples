#include "PersistanceService.h"
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>
#include <QFile>
#include <QString>
#include <QByteArray>
#include <QColor>

/**
 * Assignment07 - Qt SpriteMaker
 * Cs3505 - University of Utah, 2022-2023
 *
 * Team members: Austin Fashimpaur, Tyler Debruin, Drew Larson, Jonathan Draney, Alex Fischer
 * Team:  ;DROP TABLE * --;
 *
 * Documentation of class implementation is in the associated header file.
 */
vector<AnimationFrame> PersistanceService::loadFile(QString fileName) {

    QFile file{fileName};

    file.open(QIODevice::ReadOnly);

    QByteArray saveData = file.readAll();

    QJsonDocument loadDoc(QJsonDocument::fromJson(saveData));

    int animationFrames = loadDoc["numberOfFrames"].toInt();
    int height = loadDoc["height"].toInt();
    int width = loadDoc["width"].toInt();

    QJsonObject frames = loadDoc["frames"].toObject();

    std::vector<AnimationFrame> result;

    for(int frameId = 0; frameId < animationFrames; frameId++)
    {
        QString frameIdentifier = QString("frame") + QString::number( frameId );

        QJsonArray columns = frames[frameIdentifier].toArray();

        AnimationFrame animationFrame;
        int layerId = animationFrame.addLayer(width, height);

        PixelMap& pixelMap = animationFrame.getLayer(layerId);

        for(int y = 0; y < height; y++)
        {
            QJsonArray row = columns[y].toArray();

            for(int x = 0; x < width; x++)
            {
                QJsonArray colorValues = row[x].toArray();

                int red = colorValues[0].toInt();
                int green = colorValues[1].toInt();
                int blue = colorValues[2].toInt();
                int alpha = colorValues[3].toInt();

                QColor color(red, green, blue, alpha);

                pixelMap.setValue(x,y, color);
            }
        }

        result.push_back(animationFrame);
    }

    return result;
}


void PersistanceService::saveFile(QString fileName, std::vector<AnimationFrame> animationFrames, unsigned int width, unsigned int height) {

    QJsonObject savedModel;
    savedModel["numberOfFrames"] = (int) animationFrames.size();
    savedModel["height"] = (int) height;
    savedModel["width"] = (int) width;
    savedModel["frames"] = saveFrames(animationFrames, width, height);

    saveModel(fileName, savedModel);
}


QJsonObject PersistanceService::saveFrames(vector<AnimationFrame> frames, unsigned int width, unsigned int height) {

    QJsonObject framesDictionary;

    for(unsigned long frameId = 0; frameId < frames.size(); frameId++)
    {
        QString frameIdentifier = QString("frame") + QString::number( frameId );

        PixelMap mergedLayers = frames[frameId].getMergedLayers();

        vector<vector<QColor>> pixelMap = mergedLayers.getMap();

        QJsonArray matrix;

        for(unsigned int y = 0; y < height; y++)
        {
            QJsonArray row;

            for(unsigned int x = 0; x < width; x++)
            {
                QJsonArray column;

                QColor colorValue = pixelMap[x][y];

                column.append(colorValue.red());
                column.append(colorValue.green());
                column.append(colorValue.blue());
                column.append(colorValue.alpha());

                row.append(column);
            }

            matrix.append(row);
        }

        framesDictionary[frameIdentifier] = matrix;
    }

    return framesDictionary;
}

void PersistanceService::saveModel(QString fileName, QJsonObject model) {

    QJsonDocument jsonDocument{model};

    QByteArray json = jsonDocument.toJson(QJsonDocument::Compact);

    QFile file{fileName};

    file.open(QIODevice::WriteOnly);

    file.write(json);
}


