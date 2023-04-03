/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the GameModel class.
 */
#include "gamemodel.h"

GameModel::GameModel() : _currentChapter(0) {}

GameModel::~GameModel() {

}

void GameModel::StartNewGame() {
    SetChapter(1);
}

void GameModel::SetChapter(int chapter) {

    CleanUpBoard();

    _currentChapter = chapter;

     if(chapter == 1)
     {
         _gates.push_back(new OutPower(1));
         _gates.push_back(new InputPower(1));
         _gates.push_back(new InputPower(1));

        emit SetUpLevel(400, 225, "OutputOn");
        emit SetUpLevel(25, 150, "InputOn");
        emit SetUpLevel(25, 300, "InputOn");

         emit AllowGates(vector<QString>{ "AND" });
     }
     else if(chapter == 2)
     {
         _gates.push_back(new OutPower(1));
         _gates.push_back(new InputPower(0));
         _gates.push_back(new InputPower(1));

        emit SetUpLevel(400, 225, "OutputOn");
        emit SetUpLevel(25, 150, "InputOff");
        emit SetUpLevel(25, 300, "InputOn");

         emit AllowGates(vector<QString>{ "OR" });
     }
     else if(chapter == 3)
     {
         _gates.push_back(new OutPower(1));
         _gates.push_back(new InputPower(0));

        emit SetUpLevel(400, 225, "OutputOn");
        emit SetUpLevel(25, 150, "InputOff");

         emit AllowGates(vector<QString>{ "NOT" });
     }
     else if(chapter == 4)
     {
         _gates.push_back(new OutPower(1));
         _gates.push_back(new InputPower(0));
         _gates.push_back(new InputPower(1));

        emit SetUpLevel(400, 225, "OutputOn");
        emit SetUpLevel(25, 150, "InputOff");
        emit SetUpLevel(25, 300, "InputOn");

         emit AllowGates(vector<QString>{ "NAND" });
     }

     else if(chapter == 5)
     {
         _gates.push_back(new OutPower(1));
         _gates.push_back(new InputPower(0));
         _gates.push_back(new InputPower(1));

        emit SetUpLevel(400, 225, "OutputOn");
        emit SetUpLevel(25, 150, "InputOff");
        emit SetUpLevel(25, 300, "InputOn");

         emit AllowGates(vector<QString>{ "XOR" });
     }

     else if(chapter == 6)
     {
         _gates.push_back(new OutPower(1));
         _gates.push_back(new InputPower(0));
         _gates.push_back(new InputPower(1));

        emit SetUpLevel(400, 225, "OutputOn");
        emit SetUpLevel(25, 150, "InputOff");
        emit SetUpLevel(25, 300, "InputOn");

         emit AllowGates(vector<QString>{ "AND", "OR", "NOT", "NAND", "XOR" });
     }
}

void GameModel::RestartRound() {
    SetChapter(_currentChapter);
}

void GameModel::CleanUpBoard()
{
    for(int i = 0; i < _gates.size(); i++) {
        delete _gates[i];
    }

    _gates.clear();

    emit ResetBoard();
}

void GameModel::NextLevel() {

    if(_currentChapter < 6)
    {
       _currentChapter++;
    }

    SetChapter(_currentChapter);
}

void GameModel::ValidateLevel() {
        OutPower* outPower = dynamic_cast<OutPower*>(_gates[0]);

        bool expected = outPower->GetExpectedState();
        bool actual = outPower->performGateLogic();

        if(expected == actual && outPower->checkIfPathIsComplete()) {
            emit LevelComplete();
        }
}

void GameModel::GateCreated(QString gateType) {
    LogicGate *newGate = nullptr;

    if(gateType == "AND")
    {
        newGate = new AndGate();
    }
    else if(gateType == "OR")
    {
        newGate = new OrGate();
    }
    else if(gateType == "NOT")
    {
        newGate = new NotGate();
    }
    else if(gateType == "NAND")
    {
        newGate = new NandGate();
    }
    else if(gateType == "XOR")
    {
        newGate = new XorGate();
    }

    if(newGate != nullptr)
    {
        _gates.push_back(newGate);
    }
}

void GameModel::GateConnected(int firstGateId, int firstIOID, int secondGateId, int secondIOID) {

    int childGateId = 0;
    int parentGateId = 0;

    if(firstIOID < 0)
    {
        childGateId = firstGateId;
        parentGateId = secondGateId;
    }
    else
    {
        childGateId = secondGateId;
        parentGateId = firstGateId;
    };

    //This is the Gate that will be linked to the other gate.
    LogicGate* childGate = dynamic_cast<LogicGate*>(_gates[childGateId]);

    BinaryGate* parentBinaryGate = dynamic_cast<BinaryGate*>(_gates[parentGateId]);

    if(parentBinaryGate)
    {
        if(firstIOID == 2 || secondIOID == 2)
        {
            parentBinaryGate->setPinB(childGate);
        }
        else
        {
            parentBinaryGate->setPinA(childGate);
        }
    }
    else
    {
        UnaryGate* unaryGate = dynamic_cast<UnaryGate*>(_gates[parentGateId]);

        unaryGate->setPinA(childGate);
    }

    ValidateLevel();
}

int GameModel::GetChapter() {
    return _currentChapter;
}
