/* Drew Lawson and Deveren Schultz
CS 3505
Professor Johnson
September 21st - October 4th, 2022
This is the Trie class file responsible for organizing a Trie data structure with nodes.
*/

using namespace std;
#include "Trie.h"

/* Main constructor for Trie. Initialized in header. */
Trie::Trie() {}

/* This method adds new words to the Binary Trie.
@param addWord  String to add to the trie structure by one letter per node's map*/
void Trie::addAWord(std::string addedWord)
{
    if (addedWord.length() != 0)
    {
        /* This was referenced and adapted from: https://stackoverflow.com/questions/17863079/get-index-of-element-in-c-map
        As well as https://cplusplus.com/reference/map/map/operator%5B%5D/
        This will basically recursively call the method addAWord, subtracting one from the string each time.
        Once the end is reached, the stacks will return back to the start, and add the letter at each stack,
        Which is what allows it to add the character to each level of the node. */
        alphabetMap[addedWord[0]].addAWord(addedWord.substr(1));
    }
    // Once end of word is reached, set the flag.
    else
    {
        isAWordFlag = true;
    }
}

/* This method checks to see if a word is in the Trie.
@param  wordToCheck  String to search for
@return bool         True if successful, false if not*/
bool Trie::isAWord(string wordToCheck)
{ // Check if string isn't empty.
    if (wordToCheck.length() != 0)
    {
        // Calls wordSearch helper method and returns flag.
        return wordSearch(wordToCheck).isAWordFlag;
    }
    // Returns false if word was too short.
    else
    {
        return false;
    }
}

/* This helper method recursively searches the Trie.
@param  wordToCheck  String to search for
@return Trie         Returns the data structure after modification*/
Trie Trie::wordSearch(string wordToCheck)
{
    // Check if the word length is zero. Done to end recursion when necessary.
    if (wordToCheck.length() == 0)
    {
        // Returns a starting point for where the word(s) are at.
        return *this;
    }
    // Otherwise, checks to see if the letter at the string's char index of zero is in the alphabetMap.
    else if (alphabetMap.count(wordToCheck[0]))
    {
        // Similar to above, uses recursion with subtraction of string to keep checking to see if there is a path found, going deeper into the Trie.
        return alphabetMap[wordToCheck[0]].wordSearch(wordToCheck.substr(1));
    }
    // If both checks fail, return an empty Trie.
    return Trie();
}

/* This method checks to see if there is a given set of prefixed words. If there is
a blank string, return all words. Otherwise, return all words that have the given prefix.
@param  prefix          String to search with
@return vector<string>  The list of words found*/
vector<string> Trie::allWordsBeginningWithPrefix(string prefix)
{
    // Declare vector
    vector<string> wordVector;
    // Empty string check
    if (prefix == "")
    {
        // Get all words.
        getAllWords(wordVector, prefix);
    }
    else
    {
        // Get all words using the prefix. wordSearch is used to get a starting point.
        wordSearch(prefix).getAllWords(wordVector, prefix);
    }
    // Default return statement
    return wordVector;
}

/* This helper method recursively retrieves all words suggested with the prefix as a base.
@param wordVector  Vector of Strings that make a list to search for
@param prefix      Strign to search with*/
void Trie::getAllWords(vector<string> &wordVector, string prefix)
{
    // Recursion check if the word is a word.
    if (isAWordFlag)
    {
        // If so, adds prefix to vector.
        wordVector.push_back(prefix);
    }
    // Alphabet loop
    for (int i = 0; i < 26; i++)
    {
        // Generate a character index with the offset 97
        char charIndex = (i + 97);
        if (alphabetMap.count(charIndex))
        {
            // Recursively loops through getAllWords and adds together prefix + charIndex for a suggested character.
            alphabetMap[charIndex].getAllWords(wordVector, prefix + charIndex);
        }
    }
}