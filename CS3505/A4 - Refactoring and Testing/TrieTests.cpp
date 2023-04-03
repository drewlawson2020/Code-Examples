/* Drew Lawson and Deveren Schultz
CS 3505
Professor Johnson
September 21st - October 4th, 2022
This is TrieTests.cpp. This has various GoogleTests to test our code's functionality.

Assumes that all input is lower case
*/

using namespace std;
#include <iostream>
#include "gtest/gtest.h"
#include "Trie.h"

TEST(TrieTests, SimpleAddTest_0)
{
    Trie root;
    root.addAWord("james");
    vector<string> actualVector = root.allWordsBeginningWithPrefix("james");
    long unsigned int expectedSize = 1;
    EXPECT_EQ(expectedSize, actualVector.size());
    EXPECT_EQ(actualVector.back(), "james");
}

TEST(TrieTests, SimpleAddTest_1)
{
    Trie root;
    root.addAWord("brianlicious");
    vector<string> actualVector = root.allWordsBeginningWithPrefix("brianlicious");
    long unsigned int expectedSize = 1;
    EXPECT_EQ(expectedSize, actualVector.size());
    EXPECT_EQ(actualVector.back(), "brianlicious");
}

TEST(TrieTests, SimpleAddTest_2)
{
    Trie root;
    root.addAWord("aaaaaaaaaaaaaa");
    vector<string> actualVector = root.allWordsBeginningWithPrefix("aaaaaaaaaaaaaa");
    long unsigned int expectedSize = 1;
    EXPECT_EQ(expectedSize, actualVector.size());
    EXPECT_EQ(actualVector.back(), "aaaaaaaaaaaaaa");
}

TEST(TrieTests, SimpleAddTest_3)
{
    Trie root;
    root.addAWord("aaaaaaaaaaaaaazzzzzzzzzzzzzzzz");
    vector<string> actualVector = root.allWordsBeginningWithPrefix("aaaaaaaaaaaaaazzzzzzzzzzzzzzzz");
    long unsigned int expectedSize = 1;
    EXPECT_EQ(expectedSize, actualVector.size());
    EXPECT_EQ(actualVector.back(), "aaaaaaaaaaaaaazzzzzzzzzzzzzzzz");
}

TEST(TrieTests, SimpleAddTest_4)
{
    Trie root;
    root.addAWord("abcd");
    EXPECT_EQ(root.isAWord("abcd"), 1);
}

TEST(TrieTests, SimpleAddTest_5)
{
    Trie root;
    root.addAWord("hihihihihi");
    EXPECT_EQ(root.isAWord("hihihihihi"), 1);
}

TEST(TrieTests, SimpleAddTest_ShouldFail_0)
{
    Trie root;
    root.addAWord("hihihihihi");
    EXPECT_EQ(root.isAWord("hi"), 0);
}

TEST(TrieTests, SimpleAddTest_ShouldFail_1)
{
    Trie root;
    root.addAWord("abcdefghi");
    EXPECT_EQ(root.isAWord("a"), 0);
}

TEST(TrieTests, SimpleAddTest_ShouldFail_2)
{
    Trie root;
    root.addAWord("abcdefghi");
    EXPECT_EQ(0, root.isAWord("a"));
}

TEST(TrieTests, SimplePrefixTest_0)
{
    Trie root;
    root.addAWord("hat");
    root.addAWord("hats");
    vector<string> s;
    s.push_back("hat");
    s.push_back("hats");

    EXPECT_EQ( s.size(), root.allWordsBeginningWithPrefix("h").size());
}

TEST(TrieTests, SimplePrefixTest_1)
{
    Trie root;
    root.addAWord("hat");
    root.addAWord("hats");
    root.addAWord("hatsa");
    root.addAWord("hatsaq");
    root.addAWord("hatsaqs");

    vector<string> s;
    s.push_back("hat");
    s.push_back("hats");
    s.push_back("hats");
    s.push_back("hatsaq");
    s.push_back("hatsaqd");

    EXPECT_EQ( s.size(), root.allWordsBeginningWithPrefix("h").size());
}

TEST(TrieTests, MultiwordAdd)
{
    Trie root;
    root.addAWord("canto");
    root.addAWord("phone");
    root.addAWord("zebra");
    root.addAWord("ardvark");
    root.addAWord("deck");

    vector<string> expectedVector;
    expectedVector.push_back("zebra");
    expectedVector.push_back("phone");
    expectedVector.push_back("deck");
    expectedVector.push_back("canto");
    expectedVector.push_back("ardvark");

    vector<string> actualVector = root.allWordsBeginningWithPrefix("");
    long unsigned int expectedSize = 5;

    EXPECT_EQ(expectedSize, actualVector.size());

    for (string word : actualVector)
    {
        EXPECT_EQ(expectedVector.back(), word);
        expectedVector.pop_back();
    }
}

TEST(TrieTests, PrefixPresentOnce)
{
    Trie root;
    root.addAWord("at");
    root.addAWord("banned");

    vector<string> expectedVector;
    expectedVector.push_back("at");

    vector<string> actualVector = root.allWordsBeginningWithPrefix("a");
    long unsigned int expectedSize = 1;

    EXPECT_EQ(expectedSize, actualVector.size());

    for (string word : actualVector)
    {
        EXPECT_EQ(expectedVector.back(), word);
        expectedVector.pop_back();
    }
}

TEST(TrieTests, PrefixPresentThrice)
{
    Trie root;
    root.addAWord("well");
    root.addAWord("dance");
    root.addAWord("welcome");
    root.addAWord("wake");

    vector<string> expectedVector;
    expectedVector.push_back("well");
    expectedVector.push_back("welcome");
    expectedVector.push_back("wake");

    vector<string> actualVector = root.allWordsBeginningWithPrefix("w");
    long unsigned int expectedSize = 3;

    EXPECT_EQ(expectedSize, actualVector.size());

    for (string word : actualVector)
    {
        EXPECT_EQ(expectedVector.back(), word);
        expectedVector.pop_back();
    }
}

TEST(TrieTests, PrefixPresentThriceMoreSpecific)
{
    Trie root;
    root.addAWord("well");
    root.addAWord("dance");
    root.addAWord("welcome");
    root.addAWord("wake");

    vector<string> expectedVector;
    expectedVector.push_back("well");
    expectedVector.push_back("welcome");

    vector<string> actualVector = root.allWordsBeginningWithPrefix("we");
    long unsigned int expectedSize = 2;

    EXPECT_EQ(expectedSize, actualVector.size());

    for (string word : actualVector)
    {
        EXPECT_EQ(expectedVector.back(), word);
        expectedVector.pop_back();
    }
}
