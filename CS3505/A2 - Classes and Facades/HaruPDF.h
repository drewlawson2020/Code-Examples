#ifndef HARUPDF_DEF
#define HARUPDF_DEF
//Guard

#include "hpdf.h"
#include <stdlib.h>
#include <stdio.h>
#include <iostream>
#include <string>
#include <string.h>
#include <math.h>

class HaruPDF 
{

private:
HPDF_Doc doc;
HPDF_Page page;
HPDF_Font font;

char filename[256];
char buffer[2];

void pageSetup();

public:

HaruPDF(char nameOfFile[256]);

void writeToPDF(char letter, double angle, double x, double y);

void savePDFDocument();

};

#endif