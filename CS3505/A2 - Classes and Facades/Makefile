CXX = g++
CXXFLAGS = -Wall -std=c++11 -g
LIBHARU = './libharu'
ENDTAGS = -lhpdfs
OBJECTS =  Spiral.o HaruPDF.o spiralPDF.o
spiralPDF : HaruPDF.o Spiral.o spiralPDF.o
		$(CXX) $(CXXFLAGS) -L$(LIBHARU)/src -o spiralPDF $(OBJECTS) $(ENDTAGS) 

HaruPDF.o : HaruPDF.cpp HaruPDF.h
		$(CXX) -I$(LIBHARU)/include -c HaruPDF.cpp -o HaruPDF.o

Spiral.o : Spiral.cpp Spiral.h
		$(CXX) -c Spiral.cpp -o Spiral.o

spiralPDF.o : spiralPDF.cpp
		$(CXX) -I$(LIBHARU)/include -c spiralPDF.cpp -o spiralPDF.o

clean:
	rm spiralPDF *.o


test:
	./spiralPDF "This is a long sentence to prove that this program will work as intended, I hope anyhow"