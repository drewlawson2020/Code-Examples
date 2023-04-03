```
Author:    Peter Bruns
Partner:   None
Date:      28-Jan-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: brunsp10
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/spreadsheet-brunsp10/tree/main
Commit #:  fd9afa71ffc75812fe27045e2cb10f3eb44b7102
Project:   Dependency Graph
Copyright: CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
```
# Project description
This project contains methods to implement a dependency graph for future use in a spreadsheet application. There are methods to add and remove relationships one at a time, replace dependency relations with using collections of new dependents or dependees, access the set of dependents or dependees for an item, and return the number of dependency relationships in the graph. The graph is implemented using two dictionaries containing a string as a key and a HashSet as its value. The first dictionary (dependents) contains nodes as keys and all nodes dependent on the key in the mapped value set. The other dictionary (dependees) has all nodes upon which the key node is dependent as its value set.

# Assignment time estimate and actual time
I estimated that it would take about 9 hours to complete the assignment. It actually took about 7 hours. This is broken up
into 1.5 hours for reading the instructions and intitial setup including picking data structures, 1.5 hours to write the code and get the provided tests to pass, 3 hours to add more tests and debug, and 1 hour to finish comments and headers.

# Comments to Evaluators:
All of the files for this solution are in the 'main' branch of the repository on my GitHub. I tried to move everything into the master branch but have not been able to get that to work. I tried to keep comments to a minimum, but I still think I might have over commented as a whole.

# Assignment specific topics
This assignment was part of an introduction to IEnumerable objects and unit tests in Visual Studio. It also provided an opportunity to pick the algorithm to solve a problem with a given API.

# Consulted peers
I did consult any classmates about this assignment. I am recovering from a non-Covid related illness and have not been on campus for the week.

# References
Along with the class page and Piazza, I consulted the following sources:
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.remove?view=net-6.0#system-collections-generic-hashset-1-remove(-0)
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.getenumerator?view=net-6.0#system-collections-generic-hashset-1-getenumerator
https://docs.microsoft.com/en-us/dotnet/api/system.tuple-2?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/using-indexers
https://www.dotnetperls.com/keynotfoundexception