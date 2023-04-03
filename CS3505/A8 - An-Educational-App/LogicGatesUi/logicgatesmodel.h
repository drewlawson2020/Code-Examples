/**
 * @authors Tyler DeBruin, Drew Lawson, Alex Fischer, Jonathan Draney, Austin Fashimpaur
 * Team: Bjarne's Boys
 * Assignment: Logic Gates Educational App
 * Institution and Class: CS3505  University of Utah, 2022-2023
 *
 * This assignment is an educational game designed to assist the user in learning how
 * logical gates function.
 *
 * @brief Declarations and include statements for mainwindow.h
 */
#ifndef LOGICGATESMODEL_H
#define LOGICGATESMODEL_H

#include <iostream>
#include <string>

/**
 * @brief The LogicGate class Base class for logic gates,
 * only part of import is the the perfrom gate logic, and  check if path is complete, but they
 * are mostly unimplemented.
 */
class LogicGate {
public:
    virtual bool performGateLogic();
    virtual bool checkIfPathIsComplete();
};

/**
 * @brief The BinaryGate class Adds additonal functionality for binary gates, allows you to set the pins of
 * the gate, and clear them. It also has an implemented check if path is complete to see if all
 * pins down the chain are set.
 */
class BinaryGate : public LogicGate {
public:
    BinaryGate();
    void setPinA(LogicGate* source);
    void setPinB(LogicGate* source);
    void clearPinA();
    void clearPinB();
    LogicGate* getPinA();
    LogicGate* getPinB();
    bool checkIfPathIsComplete() override;

private:
    LogicGate* pinA;
    LogicGate* pinB;
};

/**
 * @brief The UnaryGate class adds on to logic gate for single input gates.
 * Allows one to set the pins, and sets up check if path is complete for unary gates,
 * but some gates override it still.
 */
class UnaryGate : public LogicGate {
public:
    UnaryGate();
    LogicGate* getPinA();
    void setPinA(LogicGate* source);
    void clearPinA();
    bool checkIfPathIsComplete() override;
private:
    LogicGate* pinA;
};

/**
 * @brief The InputPower class Fully implements the perform gate logic, and check if path is complete,
 * allowing it to be involved in checks for if gates output. It can also toggle the output to change
 * if it outputs a 0 or 1.
 */
class InputPower : public UnaryGate{
public:
    InputPower(bool state);
    void toggleOutput();
    bool performGateLogic() override;
    bool checkIfPathIsComplete() override;
private:
    bool logicalOutput;
};

/**
 * @brief The OutPower class Fully implements performGateLogic, which for this just checks if all the
 * inputs are set down the chain. It cannot toggle its value, but you can set what it should be for success
 * on creation, and then get that value.
 */
class OutPower : public UnaryGate{
public:
    OutPower(bool state);
    bool performGateLogic() override;
    bool GetExpectedState();
private:
    bool _state;
};

/**
 * @brief The AndGate class fully implements perform gate logic, which for and gates,
 * if the pins are set, and both pins are outputing true, this gate outputs true.
 */
class AndGate : public BinaryGate {
public:
    bool performGateLogic() override;
};

/**
 * @brief The OrGate class Gate logic is implemented, if all pins are set, and one
 * or both of the pins are outputing true, this gate outputs true.
 */
class OrGate : public BinaryGate {
public:
    bool performGateLogic() override;
};

/**
 * @brief The NandGate class The logic is implemented for Nand gates so that if
 * both pins exist and are outputting true, it outputs false, otherwise as long
 * as the pins are set it outputs true.
 */
class NandGate : public BinaryGate {
public:
    bool performGateLogic() override;
};

/**
 * @brief The NorGate class If both pins are set, and any pin outputs true, it outputs false,
 * otherwise as long as pins are set it outputs true.
 */
class NorGate : public BinaryGate {
public:
    bool performGateLogic() override;
};

/**
 * @brief The XorGate class If both pins are not set it outputs false. Otherwise as long as only
 * one pin outputs true it outputs true.
 */
class XorGate : public BinaryGate {
public:
    bool performGateLogic() override;
};

/**
 * @brief The XnorGate class if both pins are not set it outputs false. Otherwise if neither pin outputs true
 * then it outputs false.
 */
class XnorGate : public BinaryGate {
public:
    bool performGateLogic() override;
};

/**
 * @brief The NotGate class This gate swaps the output of any input.
 */
class NotGate : public UnaryGate {
public:
    bool performGateLogic() override;  
};

#endif // LOGICGATESMODEL_H
