// Drew Lawson
// CS 3505
// Professor Johnson
// September 21st, 2022
// This is TrieTest.cpp. It simply tests to see if the input functions correctly using a defined
// dictionary file and query file.
using namespace std;

#include <string>
#include <fstream>
#include "Trie.h"
#include "Node.h"

int main(int argc, const char * argv[])
{

    if(argc != 3)
    {
        cout << "Error: No input. Type in a dictionary file and a query file name." << endl;
        return 0;
    }
    string dictionaryFile = argv[1];
    string queryFile = argv[2];
    Trie* trieTree = new Trie();
    // Generate the file reader and query
    ifstream wordDictionary;
    ifstream wordQuery;
    // Open dictionary. 
    wordDictionary.open(dictionaryFile);
    // Read in each line.
    // Create an empty string.
    string input = "";
    while(getline(wordDictionary, input))
    {
        // Add the read line to the dictionary.
        trieTree->addAWord(input);
    }
    // Reset input.
    input = "";
    wordQuery.open(queryFile);
    // Read in each line from the text.
        while(getline(wordQuery, input))
        {
            // If word was found, return it.
            if(trieTree->isAWord(input))
            {
                cout << input + " is found." << endl;
            }
            else
            {
                // Search for word using prefix.
                vector<string> wordsFromDict = trieTree->allWordsBeginningWithPrefix(input);
                // If nothing was found
                if(wordsFromDict.size() == 0)
                {
                      cout << input << " no alternatives found" << endl;
                }
                // Print out suggestions otherwise.
                else
                {
                cout << input << " is not found, did you mean: " << endl;
                for(string & word : wordsFromDict)
                    {
                        cout << word << endl;
                    } 
                }
            }
        }
    // Delete the tree.
    delete trieTree;
    return 0;
}
