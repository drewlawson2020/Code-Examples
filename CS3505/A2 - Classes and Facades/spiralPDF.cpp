// Drew Lawson 
// CS3505 A2
// Professor Johnson
// This code allows for the use of a library, in which we can create facade files to make it more usable in order to generate spiral text PDFs.
#include "HaruPDF.h"
#include "Spiral.h"
// Main body to create the spiral code.
int main (int argc, char **argv)
{
    //Init the X, Y, angle, and radius values.
    double startingX = 200;
    double startingY = 400;
    double startingAngle = 0;
    double startingRadius = 50;
    //Important to check so that libharu functions correctly
    int numOfArgs = argc;
    
    if (numOfArgs < 2)
    {
        std::cout << "Error: Input text to generate Sprial." << std::endl;
    }
    // Create Spiral
    Spiral spiral (startingX, startingY, startingAngle, startingRadius);
    std::cout << spiral.getLetterAngle() << std::endl;

    HaruPDF doc(argv[0]);
    char letters[256];
    strcpy (letters, argv[1]);
    int letterLength = strlen(letters);
    
    //Main loop
    for (int i = 0; i < letterLength; i++)
    {
        char temp = letters[i];
        spiral.nextLetterCounter();
        doc.writeToPDF(temp, spiral.getLetterAngle(), spiral.getTextX(), spiral.getTextY());
    }
    doc.savePDFDocument();
    return 0;
}