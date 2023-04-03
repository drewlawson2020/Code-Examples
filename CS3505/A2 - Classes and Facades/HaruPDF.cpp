#include "HaruPDF.h"
// This is the main constructor, which takes in a filename.
HaruPDF::HaruPDF(char name[256])
{
	strcpy (filename, name);
	strcat (filename, ".pdf");

	pageSetup();
}

// pageSetup sets up the page, with a document, page, size, font, etc.
void HaruPDF::pageSetup()
  {
  //Create document
	doc = HPDF_New (NULL, NULL);
  //Give it a page.
	this->page = HPDF_AddPage (doc);
  //Set size of paper.
	HPDF_Page_SetSize (page, HPDF_PAGE_SIZE_A5, HPDF_PAGE_PORTRAIT);
  
	HPDF_Page_SetTextLeading (page, 20);
	HPDF_Page_SetGrayStroke (page, 0);
  //Start the ability to write to the document.
  HPDF_Page_BeginText (page);
  //Set font.
  font = HPDF_GetFont (doc, "Courier-Bold", NULL);
  HPDF_Page_SetFontAndSize (page, font, 30);
  //Make buffer[1] = 0 for Haru requirements.
	buffer[1] = 0;
  }

  // writeToPDF allows you to write to the actual PDF file, taking in a letter, radius, and xy values.
  void HaruPDF::writeToPDF(char letter, double radius, double x, double y)
  {
    //Set letter to buffer 0;
    buffer[0] = letter;
    //Write text using all the values to the page.
    HPDF_Page_SetTextMatrix(page, cos(radius), sin(radius), -sin(radius), cos(radius), x, y);
    //Display the text on the document.
    HPDF_Page_ShowText (page, buffer);
  }
  //savePDFDocument allows you to save the document as a proper PDF file.
  void HaruPDF::savePDFDocument()
  {
    //End the writing.
    HPDF_Page_EndText(page);
    //Save the file to disk.
    HPDF_SaveToFile(doc, filename);
    //Frees up the document from memory.
    HPDF_Free(doc);
  }
