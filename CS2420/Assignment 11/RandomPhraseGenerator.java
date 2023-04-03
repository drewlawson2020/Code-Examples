package comprehensive;
import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Random;
public class RandomPhraseGenerator {
	/**
	 * Uses the HashMap with a String key and String list array to store non-terminals and terminals that are
	 * used to generate a random sentence
	 * @author Ling Lei and Drew Lawson
	 * @version April 26, 2021
	 */
	static HashMap<String, ArrayList<String>> dictionary = new HashMap<String, ArrayList<String>>();
	public static void main(String[] args) {
		readGFile(args[0]);
		int numberOfPhrases = Integer.parseInt(args[1]);
		for (int i = 0; i < numberOfPhrases; i++)
		{
			//For testing purposes, we swap to the the non-println version for the sake of graphing times more easily.
			System.out.println(randomPhraseCreator());
			//randomPhraseCreator();
		}	
	}
	
	/**
	 *Reads in the grammar file using BufferedReader, and loops calling addToDictionary to add all non-terminals and terminals.
	 *Throws an exception if there is no file, or if there is nothing to read from said file.
	 * 
	 * @param fileName - The name/location of the file, given as a String
	 */
	public static  void readGFile(String fileName){
		try {
			BufferedReader file = new BufferedReader(new FileReader(fileName));
			while(file.ready())
			{
				if (file.readLine().equals("{"))
				{
					addToDictionary(file);
				}
			}  
		}
		catch(FileNotFoundException e) 
		{
			System.out.println("Could not find file.");
			e.printStackTrace();
		} 
		catch(IOException e) 
		{
			System.out.println("Could not find read from file.");
			e.printStackTrace();
		}
	}
	/**
	 *Using the information from the dictionary HashMap along with StringBuilder, removes "<start>" and then
	 *loops constantly to check for an index of "<". If found, then both the index of "<" and ">" are found,
	 *and then are replaced by the keyword contained inside of it that is taken from the HashMap, in which the word
	 *used is randomly chosen.
	 * @return - A newly generated sentence in String.
	 */
	public static String randomPhraseCreator()
	{
		StringBuilder sentence = new StringBuilder();
		sentence.append(randomString(dictionary.get("<start>")));
		while (sentence.indexOf("<") != -1)
		{	
			int firstBracket = sentence.indexOf("<");
			int endBracket = sentence.indexOf(">");
			String bracketedString = (String) sentence.substring(firstBracket, endBracket + 1);
			if(endBracket > firstBracket)
				{
					sentence.replace(firstBracket, endBracket + 1, (String) randomString(dictionary.get(bracketedString)));
				}
		}
		return sentence.toString();
	}
	
	/**
	 *Using the HashMap gained from a key, randomly picks a word
	 *from the arraylist.
	 * @param temp - the HashMap gained from the current key.
	 * @return - A random String word from temp.
	 */
	public static Object randomString(ArrayList<String> temp)
	{
		Random rng = new Random();
		return temp.get(rng.nextInt(temp.size()));
	}		
	/**
	 * Reads in a non-terminal from the file and sets it as a new key along with a blank ArrayList
	 * within the dictionary, and while using that new key, adds to the ArrayList
	 * for every word found until it reaches an end bracket of "}".
	 * @param input - The file reader for the current file.
	 * @return - Nothing: Is called when end of loop occurs.
	 */
	public static void addToDictionary(BufferedReader input)
	{
		try {	
		String nonTerminal = input.readLine();
		dictionary.put(nonTerminal, new ArrayList<String>());
		ArrayList<String> temp = (ArrayList<String>) dictionary.get(nonTerminal);
			while(input.ready())
			{
				String current = input.readLine();
				
				if (current.equals("}"))
				{
					return;
				}
				temp.add(current);
			}
		} 
		catch (IOException e) 
		{
			System.out.println("Could not find read from file.");
			e.printStackTrace();
		}
	}
}


