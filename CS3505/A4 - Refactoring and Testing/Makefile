CC = g++
CFLAGS = -g -Wall -std=c++2a
OBJECTS =  Trie.o
GOOGLETEST ?= ./googletest

CXX ?= g++

TrieTests: Trie.o TrieTests.cpp 
	$(CC) $(CFLAGS) TrieTests.cpp -I$(GOOGLETEST)/googletest/include $(OBJECTS) -o TrieTests -L$(GOOGLETEST)/lib -lgtest -lgtest_main -lpthread 

Trie.o: Trie.h Trie.cpp
	$(CC) $(CFLAGS) -c -I. Trie.cpp

TrieTests.o: TrieTests.cpp 
	$(CC) $(CFLAGS) TrieTests.cpp -I$(GOOGLETEST)/googletest/include $(OBJECTS) -c TrieTests.cpp

clean:
	rm *.o
	rm TrieTests

test : TrieTests
	./TrieTests


