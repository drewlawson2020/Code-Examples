package assign04;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Scanner;

/**
 * This class has methods to check if the two are anagrams
 * 
 * @author linglei and Drew Lawson
 *
 */
public class AnagramChecker {

	/**
	 * This method returns the lexicographically-sorted version of the input string.
	 * 
	 * The sorting must be accomplished using an insertion sort.
	 * 
	 * @param o1
	 * @return String
	 */
	public static String sort(String o1) {
		// split everything in str
		String[] splited = o1.split("");
		for (int i = 0; i < o1.length(); i++) {
			for (int j = i - 1; j >= 0; j--) {
				if (splited[j + 1].compareTo(splited[j]) >= 0)
					break;
				else {
					String temp = splited[j + 1];
					splited[j + 1] = splited[j];
					splited[j] = temp;
				}
			}
		}
		String resultFinal = "";
		for (String each : splited) {
			resultFinal += each;
		}
		return resultFinal;
	}

	/**
	 * This generic method sorts the input array using an insertion sort
	 * 
	 * and the input Comparator object.
	 * 
	 * @param <T>
	 * @param arg
	 * @param cmp
	 */
	public static <T> void insertionSort(T[] arg, Comparator<? super T> cmp) {

		for (int i = 0; i < arg.length; i++) {
			for (int j = i - 1; j >= 0; j--) {
				if (cmp.compare(arg[j + 1], arg[j]) >= 0)
					break;
				else {
					T temp = arg[j + 1];
					arg[j + 1] = arg[j];
					arg[j] = temp;
				}
			}
		}
	}

	/**
	 * This comparator compare if the two are anagrams
	 * 
	 * ignore upper and lower case
	 * 
	 * @author linglei and Drew Lawson
	 *
	 */
	private static class StringComparator implements Comparator<String> {
		@Override
		public int compare(String arg1, String arg2) {
			return sort(arg1.toLowerCase()).compareTo(sort(arg2.toLowerCase()));
		}
	}

	/**
	 * This method returns true if the two input strings are anagrams of each other
	 * 
	 * otherwise returns false.
	 * 
	 * @param arg1
	 * @param arg2
	 * @return boolean
	 */
	public static boolean areAnagrams(String arg1, String arg2) {
		return sort(arg1.toLowerCase()).equals(sort(arg2.toLowerCase()));
	}

	/**
	 * This method returns the largest group of anagrams in the input array of
	 * words,
	 * 
	 * in no particular order. It returns an empty array if there are no anagrams in
	 * 
	 * the input array.
	 * 
	 * @param arg
	 * @return
	 */
	public static String[] getLargestAnagramGroup(String[] arg) {
		String[] result = new String[] {};
		StringComparator cmp = new StringComparator();
		// sort by specific comparator
		insertionSort(arg, cmp);

		// find the biggest group
		int i = 0, track = 1, maxIndex = 0, max = 1;
		while (i < arg.length - 1) {
			if (areAnagrams(arg[i], arg[i + 1]) == true) {
				track++;
			} else {
				// keep updating max group information
				if (max < track) {
					max = track;
					maxIndex = i;
				}
				track = 1;
			}
			i++;
		}
		if (max > 1) {
			result = new String[max];
			for (int j = 0; j < max; j++) {
				result[j] = arg[maxIndex - j];
			}
		}
		// else return
		return result;
	}

	/**
	 * This method behaves the same as the previous method,
	 * 
	 * but reads the list of words from the input filename.
	 * 
	 * It is assumed that the file contains one word per line.
	 * 
	 * If the file does not exist or is empty,
	 * 
	 * the method returns an empty array because there are no anagrams.
	 * 
	 * @param filename
	 * @return
	 */
	public static String[] getLargestAnagramGroup(String filename) {
		String[] convertedArray = new String[] {};
		int i = 0;
		ArrayList<String> fileString = new ArrayList<String>();
		try (Scanner readFile = new Scanner(new File(filename))) {
			// initialize fileString
			while (readFile.hasNextLine()) {
				fileString.add(readFile.nextLine());
				i++;
			}
			readFile.close();
		} catch (FileNotFoundException e) {
			return convertedArray;
		}

		// sort and get largest AnagramGroup
		convertedArray = new String[i];
		int j = 0;
		for (String line : fileString) {
			convertedArray[j] = line;
			j++;
		}
		String[] result = getLargestAnagramGroup(convertedArray);
		return result;
	}

}
