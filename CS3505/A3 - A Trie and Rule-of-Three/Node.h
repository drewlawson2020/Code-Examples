// Drew Lawson
// CS 3505
// Professor Johnson
// September 21st, 2022
// This is the Node.h file for declaring all the necessary header file info for Node.cpp.

// File guards
#ifndef Node_H
#define Node_H

using namespace std;

#include <iostream>
#include <vector>
#include <string>


// Declare class.
class Node
{

    // Declare array and word flag.
private:
    Node *alphabetArray[26] = { nullptr };
    bool isAWordFlag;
    // Declare all public members to be used by the class.
public:
    Node();
    void nodeDestructor();
    int alphabetIndex(char);
    void setWordFlag();
    bool getWordFlag();
    Node* addAChar(char);
    Node* hasAChar(char); 
    void copyANode(Node*);
    vector<string> retrieveAllPrefixWords(string, string, vector<string>*);
};
#endif
