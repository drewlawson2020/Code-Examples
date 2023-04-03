// Drew Lawson
// CS 3505
// Professor Johnson
// September 21st, 2022
// This is the Node.cpp file, which contains all the methods necessary for 
// helping the trie class with its own node class, each node storing an implied
// letter and a word flag.

using namespace std;

#include "Node.h"

// This is the main constructor for the node class. The array is declared and init in the header file, and the flag is set here.
Node::Node()
{
    isAWordFlag = false;
}

// Sets the word flag to true.
void Node::setWordFlag()
{
    isAWordFlag = true;
}

// Returns the word flag. 
bool Node::getWordFlag()
{
    return isAWordFlag;
}

// This returns the int index for the arrays.
int Node::alphabetIndex(char letter)
{ 
    return letter - 97;
}

// This method doesn't actually add a character, but uses the letter given and uses it as an index to create a new node at.
Node* Node::addAChar(char letter){

    // Call alphabetIndex to convert.
    int index = alphabetIndex(letter);

    // Generates a new node and returns it.
    alphabetArray[index] = new Node;
    return alphabetArray[index];
}

// This method checks to see if a letter exists in the alphabet array.
Node* Node::hasAChar(char letter)
{
    // Call alphabetIndex to convert.
    int index = alphabetIndex(letter);
  
    // Returns the pointer of index.
    return alphabetArray[index];
}



// This method loops through every single node until it reaches the bottom most node, and then from bottom to top,
// deletes every single node.
void Node::nodeDestructor()
{
    // Iterate through the array.
    for(int i = 0; i < 26; i++)
    {
        // Null check at index.
        if(alphabetArray[i] != nullptr)
        {
            // Recursion until the bottom is reached
            alphabetArray[i]->nodeDestructor();

            // Deletion of index pointer.
            delete alphabetArray[i];
        }
    }
}

// This method will copy (this) node into a brand new node that has no link to the original.
void Node::copyANode(Node* nodeToCopy)
{
    // Iterate through the entire pointer array.
    for(int i = 0; i < 26; i++)
    {
        // If the location is not equal to null, copy node data.
        if(alphabetArray[i] != nullptr)
        {
            // Create a new node for that location.
            nodeToCopy->alphabetArray[i] = new Node;

            // Copy the boolean word flag.
            nodeToCopy->alphabetArray[i]->isAWordFlag = alphabetArray[i]->isAWordFlag;

            // Call this method again to ensure that all node pointers update.
            alphabetArray[i]->copyANode(nodeToCopy->alphabetArray[i]);
        }
    }
}

// This method will search through all of the array to find an expected prefix. If a match is found, it will
// Add it to the vector list and return all the suggestions. If given a blank prefix, instead, return all words.
vector<string> Node::retrieveAllPrefixWords(string prefix, string word, vector<string>* wordList)
{
    if(prefix == "")
    {
        for(int i = 0; i < 26; i++)
        {
            // Null check at index
            if(alphabetArray[i] != nullptr)
            {
                // Using the index of i and a 97 offset, generate the next character (So if i was 0, it would start at char 'a')
                char suggestChar = i + 97;
                // Create a new word from word and the obtained char.
                string newWord = word + suggestChar;

                // Check if the word flag is true
                if(alphabetArray[i]->getWordFlag())
                {
                    // Add the word to the vector.
                    wordList->push_back(newWord);
                }
                // Recursion with the newWord.
                alphabetArray[i]->retrieveAllPrefixWords(prefix, newWord, wordList);
            }
        }
    }
    // If the prefix is not empty, retrieve all words.
    else
    {
        // Use character as index from prefix letter.
        int index = alphabetIndex(prefix[0]);
        // Search deeper if not null.
        if(alphabetArray[index] != nullptr)
        {
            // Move the string prefix over by one and recurse.
            alphabetArray[index]->retrieveAllPrefixWords(prefix.substr(1), word, wordList);
        }
    }
    return *wordList;
}
