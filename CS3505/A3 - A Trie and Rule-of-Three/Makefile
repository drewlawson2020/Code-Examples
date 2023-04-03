CC = g++
CFLAGS = -g -Wall -std=c++11 -fsanitize=undefined -fsanitize=address
OBJECTS =  Trie.o Node.o

TrieTest: Trie.o Node.o TrieTest.cpp
	$(CC) $(CFLAGS) TrieTest.cpp $(OBJECTS) -o TrieTest

Node.o: Node.h Node.cpp
	$(CC) $(CFLAGS) -c -I. Node.cpp


Trie.o: Trie.h Trie.cpp
	$(CC) $(CFLAGS) -c -I. Trie.cpp

clean:
	rm *.o
	rm TrieTest
