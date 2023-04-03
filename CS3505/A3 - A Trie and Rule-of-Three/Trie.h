// Drew Lawson
// CS 3505
// Professor Johnson
// September 21st, 2022
// This is the Trie.h file, which declares all the necessary methods and variables for
// Trie.cpp

// File guards
#ifndef Trie_H
#define Trie_H
using namespace std;

#include <iostream>
#include <vector>
#include "Node.h"

// Declare class.
class Trie
{
    // Declare all public members to be used by the class.
public:
    Node* rootNode;
    Trie();
    Trie(const Trie&);
    ~Trie();
    Trie& operator=(Trie);
    void addAWord(string);
    bool isAWord(string);
    std::vector<string> allWordsBeginningWithPrefix(string);
};
#endif
