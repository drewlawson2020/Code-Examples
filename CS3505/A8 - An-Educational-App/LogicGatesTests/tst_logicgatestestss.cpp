#include "logicgatesmodel.h"
#include <QtTest>

// add necessary includes here

class LogicGatesTestss : public QObject
{
   Q_OBJECT

public:
   LogicGatesTestss();
   ~LogicGatesTestss();

private slots:
   void test_can_make_gates();
   void test_power_toggle();
   void test_one_and_gate_powered();
   void test_one_and_gate_pinA_powered();
   void test_one_and_gate_pinB_powered();
   void test_one_and_gate_unpowered();
   void test_one_and_gate_cleared_readded();
   void test_one_or_gate_powered();
   void test_one_or_gate_pinA_powered();
   void test_one_or_gate_pinB_powered();
   void test_one_or_gate_unpowered();
   void test_one_or_gate_cleared_readded();
   void test_one_not_gate_powered();
   void test_one_not_gate_unpowered();
   void test_one_not_gate_cleared_readded();
   void test_outPower();
   void test_two_outPower();
   void test_one_nand_gate_powered();
   void test_one_nand_gate_pinA_powered();
   void test_one_nand_gate_pinB_powered();
   void test_one_nand_gate_unpowered();
   void test_one_nand_gate_cleared_readded();
   void test_one_nor_gate_powered();
   void test_one_nor_gate_pinA_powered();
   void test_one_nor_gate_pinB_powered();
   void test_one_nor_gate_unpowered();
   void test_one_nor_gate_cleared_readded();
   void test_one_xor_gate_powered();
   void test_one_xor_gate_pinA_powered();
   void test_one_xor_gate_pinB_powered();
   void test_one_xor_gate_unpowered();
   void test_one_xor_gate_cleared_readded();
   void test_one_xnor_gate_powered();
   void test_one_xnor_gate_pinA_powered();
   void test_one_xnor_gate_pinB_powered();
   void test_one_xnor_gate_unpowered();
   void test_one_xnor_gate_cleared_readded();
   void test_base_gates_together();
   void test_all_gates_together();
   void test_incomplete_gates();
   void test_complete_gate();
};

LogicGatesTestss::LogicGatesTestss()
{

}

LogicGatesTestss::~LogicGatesTestss()
{

}

//Testing if gate can be made without crashing
void LogicGatesTestss::test_can_make_gates()
{
   AndGate andGate1;
   OrGate orGate1;
   NotGate notGate1;
   InputPower power1(0);
   OutPower outPower1(0);
}

//testing if toggle output works, should start out at 0, for off, toggle to 1, on
//currently fails.
void LogicGatesTestss::test_power_toggle()
{
   InputPower power1(0);
   QCOMPARE(power1.performGateLogic(), 0);
   power1.toggleOutput();
   QCOMPARE(power1.performGateLogic(), 1);
}

////sets up an and gate, and two powers, toggle powers to on, sets the pins equal to the two powers
////which should set the output to on,
void LogicGatesTestss::test_one_and_gate_powered()
{
   AndGate andGate1;
   InputPower  power1(0);
   InputPower  power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   andGate1.setPinA(&power1);
   andGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(andGate1.performGateLogic(), 1);
}

////just one input powered should not power gate
void LogicGatesTestss::test_one_and_gate_pinA_powered()
{
   AndGate andGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   power2.toggleOutput();
   andGate1.setPinA(&power1);
   andGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(andGate1.performGateLogic(), 0);
}

////just one input powered but b not a. should not power gate
void LogicGatesTestss::test_one_and_gate_pinB_powered()
{
   AndGate andGate2;
   InputPower power3(0);
   InputPower power4(0);
   power4.toggleOutput();
   andGate2.setPinA(&power3);
   andGate2.setPinB(&power4);
   QCOMPARE(power3.performGateLogic(), 0);
   QCOMPARE(power4.performGateLogic(), 1);
   QCOMPARE(andGate2.performGateLogic(), 0);
}

////not powered, should not power gate
void LogicGatesTestss::test_one_and_gate_unpowered()
{
   AndGate andGate1;
   InputPower power1(0);
   InputPower power2(0);
   andGate1.setPinA(&power1);
   andGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(andGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_and_gate_cleared_readded()
{
   AndGate andGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   andGate1.setPinA(&power1);
   andGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(andGate1.performGateLogic(), 1);

   andGate1.clearPinA();
   QCOMPARE(andGate1.performGateLogic(), 0);
   andGate1.clearPinB();
   QCOMPARE(andGate1.performGateLogic(), 0);
   InputPower power3(0);
   InputPower power4(0);
   andGate1.setPinA(&power3);
   andGate1.setPinB(&power4);
   QCOMPARE(andGate1.performGateLogic(), 0);
   power3.toggleOutput();
   power4.toggleOutput();
   QCOMPARE(andGate1.performGateLogic(), 1);
}


////sets up an or gate, and two powers, toggle powers to on, sets the pins equal to the two powers
////which should set the output to on,
void LogicGatesTestss::test_one_or_gate_powered()
{
   OrGate         orGate1;
   InputPower     power1(0);
   InputPower     power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   orGate1.setPinA(&power1);
   orGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(orGate1.performGateLogic(), 1);
}

////just one input powered should power gate
void LogicGatesTestss::test_one_or_gate_pinA_powered()
{
   OrGate        orGate1;
   InputPower     power1(0);
   InputPower     power2(0);
   power1.toggleOutput();
   orGate1.setPinA(&power1);
   orGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(orGate1.performGateLogic(), 1);
}

////just one input powered but b not a. should power gate
void LogicGatesTestss::test_one_or_gate_pinB_powered()
{
   OrGate        orGate1;
   InputPower     power1(0);
   InputPower     power2(0);
   power2.toggleOutput();
   orGate1.setPinA(&power1);
   orGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(orGate1.performGateLogic(), 1);
}

////not powered, should not power gate
void LogicGatesTestss::test_one_or_gate_unpowered()
{
   OrGate    orGate1;
   InputPower power1(0);
   InputPower power2(0);
   orGate1.setPinA(&power1);
   orGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(orGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_or_gate_cleared_readded()
{
   OrGate    orGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   orGate1.setPinA(&power1);
   orGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(orGate1.performGateLogic(), 1);

   orGate1.clearPinA();
   QCOMPARE(orGate1.performGateLogic(), 0);
   orGate1.clearPinB();
   QCOMPARE(orGate1.performGateLogic(), 0);
   InputPower power3(0);
   InputPower power4(0);
   orGate1.setPinA(&power3);
   orGate1.setPinB(&power4);
   QCOMPARE(orGate1.performGateLogic(), 0);
   power4.toggleOutput();
   QCOMPARE(orGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_not_gate_powered()
{
   NotGate  notGate1;
   InputPower power1(0);
   power1.toggleOutput();
   notGate1.setPinA(&power1);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(notGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_not_gate_unpowered()
{
   NotGate  notGate1;
   InputPower power1(0);
   notGate1.setPinA(&power1);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(notGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_not_gate_cleared_readded()
{
   NotGate    notGate1;
   InputPower power1(0);
   notGate1.setPinA(&power1);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(notGate1.performGateLogic(), 1);

   notGate1.clearPinA();
   QCOMPARE(notGate1.performGateLogic(), 0);
   InputPower power2(0);
   notGate1.setPinA(&power2);
   QCOMPARE(notGate1.performGateLogic(), 1);
   power2.toggleOutput();
   QCOMPARE(notGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_outPower()
{
   InputPower power1(0);
   OutPower   outPower1(0);
   power1.toggleOutput();
   QCOMPARE(outPower1.performGateLogic(), 0);
   outPower1.setPinA(&power1);
   QCOMPARE(outPower1.performGateLogic(), 1);
}

void LogicGatesTestss::test_two_outPower()
{
   InputPower power1(0);
   InputPower power2(0);
   OutPower   outPower1(0);
   OutPower   outPower2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   QCOMPARE(outPower1.performGateLogic(), 0);
   outPower1.setPinA(&power1);
   QCOMPARE(outPower1.performGateLogic(), 1);
   QCOMPARE(outPower2.performGateLogic(), 0);
   outPower2.setPinA(&power2);
   QCOMPARE(outPower2.performGateLogic(), 1);
   QCOMPARE(outPower1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_nand_gate_powered()
{
   NandGate nandGate1;
   InputPower  power1(0);
   InputPower  power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   nandGate1.setPinA(&power1);
   nandGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(nandGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_nand_gate_pinA_powered()
{
   NandGate nandGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   power2.toggleOutput();
   nandGate1.setPinA(&power1);
   nandGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(nandGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_nand_gate_pinB_powered()
{
   NandGate nandGate2;
   InputPower power3(0);
   InputPower power4(0);
   power4.toggleOutput();
   nandGate2.setPinA(&power3);
   nandGate2.setPinB(&power4);
   QCOMPARE(power3.performGateLogic(), 0);
   QCOMPARE(power4.performGateLogic(), 1);
   QCOMPARE(nandGate2.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_nand_gate_unpowered()
{
   NandGate nandGate1;
   InputPower power1(0);
   InputPower power2(0);
   nandGate1.setPinA(&power1);
   nandGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(nandGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_nand_gate_cleared_readded()
{
   NandGate nandGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   nandGate1.setPinA(&power1);
   nandGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(nandGate1.performGateLogic(), 0);

   nandGate1.clearPinA();
   QCOMPARE(nandGate1.performGateLogic(), 0);
   nandGate1.clearPinB();
   QCOMPARE(nandGate1.performGateLogic(), 0);
   InputPower power3(0);
   InputPower power4(0);
   nandGate1.setPinA(&power3);
   nandGate1.setPinB(&power4);
   QCOMPARE(nandGate1.performGateLogic(), 1);
   power3.toggleOutput();
   power4.toggleOutput();
   QCOMPARE(nandGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_nor_gate_powered()
{
   NorGate norGate1;
   InputPower  power1(0);
   InputPower  power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   norGate1.setPinA(&power1);
   norGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(norGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_nor_gate_pinA_powered()
{
   NorGate norGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   power2.toggleOutput();
   norGate1.setPinA(&power1);
   norGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(norGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_nor_gate_pinB_powered()
{
   NorGate norGate2;
   InputPower power3(0);
   InputPower power4(0);
   power4.toggleOutput();
   norGate2.setPinA(&power3);
   norGate2.setPinB(&power4);
   QCOMPARE(power3.performGateLogic(), 0);
   QCOMPARE(power4.performGateLogic(), 1);
   QCOMPARE(norGate2.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_nor_gate_unpowered()
{
   NorGate norGate1;
   InputPower power1(0);
   InputPower power2(0);
   norGate1.setPinA(&power1);
   norGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(norGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_nor_gate_cleared_readded()
{
   NorGate norGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   norGate1.setPinA(&power1);
   norGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(norGate1.performGateLogic(), 0);

   norGate1.clearPinA();
   QCOMPARE(norGate1.performGateLogic(), 0);
   norGate1.clearPinB();
   QCOMPARE(norGate1.performGateLogic(), 0);
   InputPower power3(0);
   InputPower power4(0);
   norGate1.setPinA(&power3);
   norGate1.setPinB(&power4);
   QCOMPARE(norGate1.performGateLogic(), 1);
   power3.toggleOutput();
   QCOMPARE(norGate1.performGateLogic(), 0);
   power4.toggleOutput();
}

void LogicGatesTestss::test_one_xor_gate_powered()
{
   XorGate xorGate1;
   InputPower  power1(0);
   InputPower  power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   xorGate1.setPinA(&power1);
   xorGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(xorGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_xor_gate_pinA_powered()
{
   XorGate xorGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   power2.toggleOutput();
   xorGate1.setPinA(&power1);
   xorGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(xorGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_xor_gate_pinB_powered()
{
   XorGate xorGate2;
   InputPower power3(0);
   InputPower power4(0);
   power4.toggleOutput();
   xorGate2.setPinA(&power3);
   xorGate2.setPinB(&power4);
   QCOMPARE(power3.performGateLogic(), 0);
   QCOMPARE(power4.performGateLogic(), 1);
   QCOMPARE(xorGate2.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_xor_gate_unpowered()
{
   XorGate xorGate1;
   InputPower power1(0);
   InputPower power2(0);
   xorGate1.setPinA(&power1);
   xorGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(xorGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_xor_gate_cleared_readded()
{
   XorGate xorGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   xorGate1.setPinA(&power1);
   xorGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(xorGate1.performGateLogic(), 0);

   xorGate1.clearPinA();
   QCOMPARE(xorGate1.performGateLogic(), 0);
   xorGate1.clearPinB();
   QCOMPARE(xorGate1.performGateLogic(), 0);
   InputPower power3(0);
   InputPower power4(0);
   xorGate1.setPinA(&power3);
   xorGate1.setPinB(&power4);
   power3.toggleOutput();
   QCOMPARE(xorGate1.performGateLogic(), 1);
   power4.toggleOutput();
   QCOMPARE(xorGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_xnor_gate_powered()
{
   XnorGate xnorGate1;
   InputPower  power1(0);
   InputPower  power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   xnorGate1.setPinA(&power1);
   xnorGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(xnorGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_xnor_gate_pinA_powered()
{
   XnorGate xnorGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   power2.toggleOutput();
   xnorGate1.setPinA(&power1);
   xnorGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(xnorGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_xnor_gate_pinB_powered()
{
   XnorGate xnorGate2;
   InputPower power3(0);
   InputPower power4(0);
   power4.toggleOutput();
   xnorGate2.setPinA(&power3);
   xnorGate2.setPinB(&power4);
   QCOMPARE(power3.performGateLogic(), 0);
   QCOMPARE(power4.performGateLogic(), 1);
   QCOMPARE(xnorGate2.performGateLogic(), 0);
}

void LogicGatesTestss::test_one_xnor_gate_unpowered()
{
   XnorGate xnorGate1;
   InputPower power1(0);
   InputPower power2(0);
   xnorGate1.setPinA(&power1);
   xnorGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 0);
   QCOMPARE(power2.performGateLogic(), 0);
   QCOMPARE(xnorGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_one_xnor_gate_cleared_readded()
{
   XnorGate xnorGate1;
   InputPower power1(0);
   InputPower power2(0);
   power1.toggleOutput();
   power2.toggleOutput();
   xnorGate1.setPinA(&power1);
   xnorGate1.setPinB(&power2);
   QCOMPARE(power1.performGateLogic(), 1);
   QCOMPARE(power2.performGateLogic(), 1);
   QCOMPARE(xnorGate1.performGateLogic(), 1);

   xnorGate1.clearPinA();
   QCOMPARE(xnorGate1.performGateLogic(), 0);
   xnorGate1.clearPinB();
   QCOMPARE(xnorGate1.performGateLogic(), 0);
   InputPower power3(0);
   InputPower power4(0);
   xnorGate1.setPinA(&power3);
   xnorGate1.setPinB(&power4);
   QCOMPARE(xnorGate1.performGateLogic(), 1);
   power4.toggleOutput();
   QCOMPARE(xnorGate1.performGateLogic(), 0);
}

void LogicGatesTestss::test_base_gates_together()
{
   InputPower power1(0);
   InputPower power2(0);
   AndGate    andGate1;
   power1.toggleOutput();
   power2.toggleOutput();
   andGate1.setPinA(&power1);
   andGate1.setPinB(&power2);
   QCOMPARE(andGate1.performGateLogic(), 1);

   InputPower power3(0);
   InputPower power4(0);
   AndGate    andGate2;
   power3.toggleOutput();
   andGate2.setPinA(&power3);
   andGate2.setPinB(&power4);
   QCOMPARE(andGate2.performGateLogic(), 0);

   OrGate     orGate1;
   orGate1.setPinA(&andGate1);
   orGate1.setPinB(&andGate2);
   QCOMPARE(orGate1.performGateLogic(), 1);

   NotGate    notGate1;
   notGate1.setPinA(&orGate1);
   QCOMPARE(notGate1.performGateLogic(), 0);

   OrGate     orGate2;
   InputPower power5(0);
   orGate2.setPinA(&notGate1);
   orGate2.setPinB(&power5);
   QCOMPARE(orGate2.performGateLogic(), 0);

   InputPower power6(0);
   AndGate    andGate3;
   andGate3.setPinA(&power6);
   andGate3.setPinB(&orGate2);
   QCOMPARE(andGate3.performGateLogic(), 0);

   NotGate    notGate2;
   notGate2.setPinA(&andGate3);
   QCOMPARE(notGate2.performGateLogic(), 1);

   OutPower   outPower1(0);
   outPower1.setPinA(&notGate2);
   QCOMPARE(outPower1.performGateLogic(), 1);

   orGate2.clearPinA();
   QCOMPARE(outPower1.performGateLogic(), 1);
   QCOMPARE(orGate2.performGateLogic(), 0);
   orGate2.setPinA(&notGate1);
   QCOMPARE(outPower1.performGateLogic(), 1);
   QCOMPARE(orGate2.performGateLogic(), 0);

   orGate1.clearPinA();
   QCOMPARE(outPower1.performGateLogic(), 1);
   QCOMPARE(orGate1.performGateLogic(), 0);
   orGate1.setPinA(&andGate1);
   QCOMPARE(outPower1.performGateLogic(), 1);
   QCOMPARE(orGate1.performGateLogic(), 1);
}

void LogicGatesTestss::test_all_gates_together()
{
   InputPower power1(0);
   InputPower power2(0);
   XorGate    xorGate1;
   power1.toggleOutput();
   xorGate1.setPinA(&power1);
   xorGate1.setPinB(&power2);
   QCOMPARE(xorGate1.performGateLogic(), 1);

   InputPower power3(0);
   InputPower power4(0);
   XnorGate    xnorGate1;
   xnorGate1.setPinA(&power3);
   xnorGate1.setPinB(&power4);
   QCOMPARE(xnorGate1.performGateLogic(), 1);

   InputPower power5(0);
   InputPower power6(0);
   OrGate    orGate1;
   orGate1.setPinA(&power5);
   orGate1.setPinB(&power6);
   power6.toggleOutput();
   QCOMPARE(orGate1.performGateLogic(), 1);

   InputPower power7(0);
   NotGate    notGate1;
   notGate1.setPinA(&power7);
   power7.toggleOutput();
   QCOMPARE(notGate1.performGateLogic(), 0);

   AndGate    andGate1;
   andGate1.setPinA(&xnorGate1);
   andGate1.setPinB(&xorGate1);
   QCOMPARE(andGate1.performGateLogic(), 1);

   NandGate    nandGate1;
   nandGate1.setPinA(&orGate1);
   nandGate1.setPinB(&notGate1);
   QCOMPARE(nandGate1.performGateLogic(), 1);

   NorGate    norGate1;
   norGate1.setPinA(&andGate1);
   norGate1.setPinB(&nandGate1);
   QCOMPARE(norGate1.performGateLogic(), 0);

   NotGate    notGate2;
   notGate2.setPinA(&norGate1);
   QCOMPARE(notGate2.performGateLogic(), 1);

   OutPower   outPower1(0);
   outPower1.setPinA(&notGate2);
   QCOMPARE(outPower1.performGateLogic(), 1);

   nandGate1.clearPinB();
   QCOMPARE(outPower1.performGateLogic(), 1);
   QCOMPARE(nandGate1.performGateLogic(), 0);

   InputPower power8(0);
   nandGate1.setPinB(&power8);
   QCOMPARE(outPower1.performGateLogic(), 1);
   QCOMPARE(nandGate1.performGateLogic(), 1);
   QCOMPARE(norGate1.performGateLogic(), 0);

   notGate2.clearPinA();
   outPower1.clearPinA();

   NorGate norGate2;
   InputPower power9(0);
   norGate2.setPinA(&norGate1);
   norGate2.setPinB(&power9);
   QCOMPARE(norGate2.performGateLogic(), 1);
   outPower1.setPinA(&norGate2);
   QCOMPARE(outPower1.performGateLogic(), 1);
}

void LogicGatesTestss::test_incomplete_gates()
{
    InputPower power1(1);
    AndGate AndGate1;
    OutPower output1(0);

    output1.setPinA(&AndGate1);
    AndGate1.setPinA(&power1);

    bool actual = output1.checkIfPathIsComplete();
    QCOMPARE(actual, false);
}

void LogicGatesTestss::test_complete_gate()
{
    InputPower power1(1);
    InputPower power2(1);
    AndGate AndGate1;
    NotGate NotGate1;
    OutPower output1(0);

    output1.setPinA(&NotGate1);
    NotGate1.setPinA(&AndGate1);
    AndGate1.setPinA(&power1);
    AndGate1.setPinB(&power2);


    bool actual = output1.checkIfPathIsComplete();
    QCOMPARE(actual, true);
}

QTEST_APPLESS_MAIN(LogicGatesTestss)

#include "tst_logicgatestestss.moc"

