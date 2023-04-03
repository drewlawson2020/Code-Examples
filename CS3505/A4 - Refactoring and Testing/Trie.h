/* Drew Lawson and Deveren Schultz
CS 3505
Professor Johnson
September 21st - October 4th, 2022
This is the Trie.h file, which declares all the necessary methods and variables for Trie.cpp file.
*/

// File guards
#ifndef Trie_H
#define Trie_H

using namespace std;

#include <iostream>
#include <vector>
#include <map>

// Declare class.
class Trie
{
    // Based on Lab 6 students example
    map<char, Trie> alphabetMap; // The main underlying data structure is a map.
    bool isAWordFlag = false;    // The Trie knows when a certain letter is the end of a word by this bool applied to node
    //char intToChar(int);         //

    // Public methods for Trie structure
public:
    Trie();
    void addAWord(string);
    bool isAWord(string);
    Trie wordSearch(string);
    std::vector<string> allWordsBeginningWithPrefix(string);
    void getAllWords(vector<string> &, string);
};
#endif
