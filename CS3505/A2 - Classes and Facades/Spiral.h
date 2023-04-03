#ifndef SPIRAL_DEF
#define SPIRAL_DEF
//Guard
#include <iostream>
#include <math.h>

class Spiral
{
    private:
    double x;
    double y;
    double center_x;
    double center_y;
    double angle;
    double radius;
    double spiralAngle;
    double currLetterAngle;
    double tempAngle;
    int increment;
    
    public:
    Spiral(double center_x, double center_y, double angle, double radius);
    
    void nextLetterCounter();
    double getTextX();
    double getTextY();
    double getLetterAngle();
    Spiral& operator++();
    Spiral operator++(int);
};

#endif