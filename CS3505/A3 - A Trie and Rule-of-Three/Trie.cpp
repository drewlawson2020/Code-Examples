// Drew Lawson
// CS 3505
// Professor Johnson
// September 21st, 2022
// This is the Trie class file. It has a destructor, operator= and a copy constructor to make independent tries, and all
// the necessary methods to store words in a trie.
using namespace std;

#include "Trie.h"

// Main constructor for Trie. Creates the root.
Trie::Trie()
{
    rootNode = new Node;
}

// The Destructor for the Trie class. Calls the nodeDestructor method to delete every single node and then the root one.
Trie::~Trie()
{
    rootNode->nodeDestructor();
    delete rootNode;
}

// The copy constructor. Creates a new and independent Trie based on an existing one.
Trie::Trie(const Trie& trieToCopy)
{
    // Make new root
    rootNode = new Node;
    // Copy root node from inputted one to current one.
    trieToCopy.rootNode->copyANode(this->rootNode);
}

// Does the same as the copy constructor, but now it will override the "=" functionality, and thereby make it more intuitive.
Trie& Trie::operator=(Trie trieToCopyTo){
    // Make new root
    rootNode = new Node;

    // Copy root node from inputted one to current one.
    trieToCopyTo.rootNode->copyANode(this->rootNode);

    // Return the copied node.
    return *this;
}

// This method adds new words to the Binary Trie.
void Trie::addAWord(std::string word){

    // Create a pointer current node from root.
    Node* currNode = rootNode;

    // Loop through the input word's chars.
    for(unsigned int i = 0; i < word.length(); i++){

        // Saves a last node in case the end is reached.
        Node* lastNode = currNode;

        // If the word length is equal to the index.
        if(i == word.length() - 1){

            // Check if next node is not null.
            if((currNode = currNode->hasAChar(word[i])) != nullptr)
            {
                // If not null, set word flag.
                currNode->setWordFlag();
                break;
            }
            // If the node was null, set curr to last, and add the char there and set flag.
            else
            {
                currNode = lastNode->addAChar(word[i]);
                currNode->setWordFlag();
                break;
            }

        }

        // If node is all together null, just add a new node with the word.
        if((currNode = currNode->hasAChar(word[i])) == nullptr)
        {
            currNode = lastNode->addAChar(word[i]);
        }
    }
}

// Checks to see if a word is in the Trie.
bool Trie::isAWord(string word)
{

    // Set curr to root.
    Node* currNode = rootNode;

    // Loop through the input word's chars.
    for(unsigned int i = 0; i < word.length(); i++){

        // If the word length is equal to the index.
        if(i == word.length() - 1)
        {
            // If node is not null, returns the word flag.
            if((currNode = currNode->hasAChar(word[i])) != nullptr)
            {
                return currNode->getWordFlag();
            }
            return false;
        }
        // Check if node isn't null.
        else
        {
            if((currNode = currNode->hasAChar(word[i])) == nullptr){

                return false;
            }
        }
    }
    return false;
}

// Uses retrieveAllPrefixWords to get the words with the inputted prefix as a suggestion.
vector<string> Trie::allWordsBeginningWithPrefix(string prefix)
{
    // Checks to see if word already exists. If not, cycles through all words.
    vector<string> allPrefixWords;
    if (isAWord(prefix))
    {
        allPrefixWords.push_back(prefix);
    }
    else
    {
        rootNode->retrieveAllPrefixWords(prefix, prefix, &allPrefixWords);
    }
    return allPrefixWords;
}
