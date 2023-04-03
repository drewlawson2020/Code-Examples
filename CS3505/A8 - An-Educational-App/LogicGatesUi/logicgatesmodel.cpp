/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief This is the implementation of the BinaryGate class.
 */

#include "logicgatesmodel.h"

/**
 * @brief LogicGate::performGateLogic
 * @return
 */
bool LogicGate::performGateLogic() {
    throw "Not Implemented";
}
bool LogicGate::checkIfPathIsComplete() {
    throw "Not Implemented";
};



/**
 * @brief BinaryGate::BinaryGate
 */
BinaryGate::BinaryGate() : LogicGate()
{
    pinA = nullptr;
    pinB = nullptr;
}

void BinaryGate::setPinA(LogicGate* source) {
    pinA = source;
}
void BinaryGate::setPinB(LogicGate* source) {
    pinB = source;
}
void BinaryGate::clearPinA() {
    pinA = nullptr;
}
void BinaryGate::clearPinB() {
    pinB = nullptr;
}
LogicGate* BinaryGate::getPinA()
{
    return this->pinA;
}
LogicGate* BinaryGate::getPinB()
{
    return this->pinB;
}
bool BinaryGate::checkIfPathIsComplete()
{
    bool lResult = false;
    bool rResult = false;

    if (this->getPinA() != nullptr)
    {
        lResult = getPinA()->checkIfPathIsComplete();
    }
    if (this->getPinB() != nullptr)
    {
        rResult = getPinB()->checkIfPathIsComplete();
    }
    return lResult && rResult;
}


/**
 * @brief UnaryGate::UnaryGate
 */
UnaryGate::UnaryGate() : LogicGate() {
    pinA = nullptr;
}
LogicGate* UnaryGate::getPinA() {
    return this->pinA;
}
void UnaryGate::setPinA(LogicGate* source) {
    pinA = source;
}
void UnaryGate::clearPinA() {
    pinA = nullptr;
}
bool UnaryGate::checkIfPathIsComplete()
{
    if (this->pinA != nullptr)
    {
        return this->getPinA()->checkIfPathIsComplete();
    }
    return false;
}


/**
 * @brief InputPower::InputPower
 */
InputPower::InputPower(bool state) : UnaryGate()
{
    logicalOutput = state;
}
void InputPower::toggleOutput()
{
    logicalOutput = !logicalOutput;
}

bool InputPower::performGateLogic() {
    return logicalOutput;
}
bool InputPower::checkIfPathIsComplete()
{
    return true;
}



/**
 * @brief OutPower::performGateLogic
 * @return
 */

OutPower::OutPower(bool state) : UnaryGate() {
    _state = state;
}

bool OutPower::GetExpectedState() {
    return _state;
}

bool OutPower::performGateLogic() {
    if (getPinA() == nullptr)
    {
        return false;
    }
    else {
        bool a = getPinA()->performGateLogic();
        return a;
    }
}

/**
 * @brief AndGate::AndGate
 */
bool AndGate::performGateLogic()
{
    if (getPinA() == nullptr || getPinB() == nullptr)
    {
        return false;
    }
    else
    {
        bool a = getPinA()->performGateLogic();
        bool b = getPinB()->performGateLogic();
        return a && b;
    }
}


/**
 * @brief OrGate
 */
bool OrGate::performGateLogic() {
    if (getPinA() == nullptr || getPinB() == nullptr)
    {
        return false;
    }
    else
    {
        bool a = getPinA()->performGateLogic();
        bool b = getPinB()->performGateLogic();
        return a || b;
    }
}

/**
 * @brief NandGate::NandGate
 */
bool NandGate::performGateLogic() {
    if (getPinA() == nullptr || getPinB() == nullptr)
    {
        return false;
    }
    else
    {
        bool a = getPinA()->performGateLogic();
        bool b = getPinB()->performGateLogic();
        return !(a && b);
    }
}


/**
 * @brief NorGate::NorGate
 */
bool NorGate::performGateLogic() {
    if (getPinA() == nullptr || getPinB() == nullptr)
    {
        return false;
    }
    else
    {
        bool a = getPinA()->performGateLogic();
        bool b = getPinB()->performGateLogic();
        return !(a || b);
    }
}


/**
 * @brief XorGate::XorGate
 */
bool XorGate::performGateLogic() {
    if (getPinA() == nullptr || getPinB() == nullptr)
    {
        return false;
    }
    else
    {
        bool a = getPinA()->performGateLogic();
        bool b = getPinB()->performGateLogic();
        return a != b;
    }
}

/**
 * @brief XnorGate::XnorGate
 */
bool XnorGate::performGateLogic() {
    if (getPinA() == nullptr || getPinB() == nullptr)
    {
        return false;
    }
    else
    {
        bool a = getPinA()->performGateLogic();
        bool b = getPinB()->performGateLogic();
        return a == b;
    }
}

/**
 * @brief NotGate::NotGate
 */
bool NotGate::performGateLogic() {
    if (getPinA() == nullptr)
    {
        return false;
    }
    else
    {
        return !getPinA()->performGateLogic();
    }
}
