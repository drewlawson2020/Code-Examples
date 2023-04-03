package assign04;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.Random;

import assign03.ArrayCollection;
import assign03.IntegerComparator;
import assign03.SearchUtil;

/**This is time tester code
 * @author linglei and Drew Lawson
 *
 */
public class AnagramTimer1 {
	private static Random rand;

	public static void main(String[] args) {
		rand = new Random();
		rand.setSeed(System.currentTimeMillis());

		// AreAnagrams
		// for each size
		for (int n = 100; n <= 2000; n += 100) {
			int timesToLoop = 100;
			// generate a size n random String
			String rand1 = randString("", n), rand2 = randString("", n);
			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				AnagramChecker.areAnagrams(rand1, rand2);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);
		}

		// getLargestAnagramGroup

		for (int n = 100; n <= 2000; n += 100) {
			int timesToLoop = 100;
			// generate a size n random ArrayList

			// Create a array with random strings
			String[] testArray = randStringArray(n);

			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				AnagramChecker.getLargestAnagramGroup(testArray);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);
		}

		// sort method
		for (int n = 100; n <= 8000; n += 100) {
			int timesToLoop = 1000;
			// generate a size n random ArrayList

			// Create a array with random strings
			String[] testArray = randStringArray(n);
			StringComparator cmp = new StringComparator();

			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				Arrays.sort(testArray, cmp);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);
		}
	}

	public static String randString(String arg, int n) {
		String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < n; i++) {
			int index = rand.nextInt(alphabet.length());
			char randomChar = alphabet.charAt(index);
			sb.append(randomChar);
		}
		arg = sb.toString();
		return arg;
	}

	public static String[] randStringArray(int n) {
		String[] arg = new String[n];
		for (int i = 0; i < n; i++) {
			arg[i] = randString("", rand.nextInt(15));
		}
		return arg;
	}

	public static class StringComparator implements Comparator<String> {

		@Override
		public int compare(String o1, String o2) {
			return o1.compareTo(o2);
		}

	}

}
