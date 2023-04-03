/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief Declarations and include statements for gamemodel.h
 *
 * This acts as the main game model.
 */

#ifndef GAMEMODEL_H
#define GAMEMODEL_H

#include <QObject>
#include <string>
#include <vector>
#include <tuple>
#include "logicgatesmodel.h"

using std::vector;

class GameModel : public QObject
{
    Q_OBJECT
public:
    /**
     * @brief GameModel Default
     */
    GameModel();
    /**
     * Cleans up memory.
     */
    ~GameModel();

    /**
     * @brief StartNewGame - Starts a new game.
     */
    void StartNewGame();

    /**
     * @brief SetChapter
     * @param chapter - Set Chapter, up to 6 chapters.
     */
    void SetChapter(int chapter);

    /**
     * @brief NextLevel - Advances the game to the next level.
     */
    void NextLevel();

    /**
     * @brief ValidateLevel - Validates the level. On completion, emits LevelComplete signal.
     */
    void ValidateLevel();

    /**
     * @brief RestartRound - Reemits ResetBoard signal. Refires off SetUpLevel and AllowGates
     */
    void RestartRound();

    /**
     * @brief GetChapter - Gets the current chapter.
     * @return
     */
    int GetChapter();

signals:
    /**
     * @brief SetUpLevel - Setups a level, emits the Positions on the scene.
     * @param x
     * @param y
     * @param gateType
     */
    void SetUpLevel(int x, int y, QString gateType);

    /**
     * @brief AllowGates - Gates to allow to be used this round.
     */
    void AllowGates(vector<QString>);

    /**
     * @brief LevelComplete - When the level is completed.
     */
    void LevelComplete();

    /**
     * @brief ResetBoard - When board is reset.
     */
    void ResetBoard();

public slots:
    /**
     * @brief GateCreated - WHen a gate is added. GateType is the type.
     * @param gateType
     */
    void GateCreated(QString gateType);
    /**
     * @brief GateConnected - Slot to connect two gates together by ID, and IO - ID.
     * @param firstGateId
     * @param firstIOID
     * @param secondGateId
     * @param secondIOID
     */
    void GateConnected(int firstGateId, int firstIOID, int secondGateId, int secondIOID);

private:
    int _currentChapter;
    vector<LogicGate*> _gates;
    void CleanUpBoard();
};

#endif // GAMEMODEL_H
