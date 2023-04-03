#include "Spiral.h"
    //The main constructor for Spiral. Takes in four doubles, a center x, a center y, and an angle and radius.
    Spiral::Spiral(double center_x, double center_y, double angle, double radius) : 
    center_x(center_x), center_y(center_y), angle(angle), radius(radius)
    {
        // Init values.
        currLetterAngle = angle;
        spiralAngle = -angle - 270;
        x = center_x;
        y = center_y;
        increment = 0;
        //Radius check to ensure it doesn't reach below zero.
        if (radius < 0)
        {
            radius = 50;
        }
    }
    // The all important method that allows for the spiral to be created.
    void Spiral::nextLetterCounter()
    {   
        //Set the current character's angle in the spiral.
        currLetterAngle = (spiralAngle - 90) / 180 * M_PI;

        //Save the temp angle for the XY coordinates.
        tempAngle = spiralAngle / 180 * M_PI;

        increment++;
        //Using the increment and radius, as well as a magical number, we can properly scale the spacing.
        spiralAngle -= 1000 / (increment + radius);
        //Give x and y their new values.
        x = center_x + cos(tempAngle) * (increment + radius);
        y = center_y + sin(tempAngle) * (increment + radius);

    }
        //Spiral operator for operator++.
        Spiral& Spiral::operator++() 
    {   
        this->nextLetterCounter();
        return *this;
    }
        //Spiral operator for ++operator.
        Spiral Spiral::operator++(int i)
    {
        Spiral result(*this);
        ++(*this);         
        return result;  
    }
    //Returns X value.
    double Spiral::getTextX()
    {
        return center_x + cos(tempAngle) * (increment + radius);
    }
    //Returns Y value.
    double Spiral::getTextY()
    {
        return center_y + sin(tempAngle) * (increment + radius);
    }
    //Returns letter angle value.
    double Spiral::getLetterAngle()
    {
        return currLetterAngle;
    }