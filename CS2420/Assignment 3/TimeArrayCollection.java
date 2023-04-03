package assign03;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.Random;

/**
 * This class testing time: toSortedList, contains, and SearchUtil.binarySearch
 * methods
 * 
 * @author ling Lei and Drew Lawson
 *
 */
public class TimeArrayCollection {
	private static Random rand;

	public static void main(String[] args) {
		rand = new Random();
		rand.setSeed(System.currentTimeMillis());

		// toSortedList

		for (int n = 1000; n <= 20000; n += 1000) {
			int timesToLoop = 100;
			// generate a size n random ArrayList
			ArrayCollection<Integer> sortedList = generateList(n);

			// create comparator
			IntegerComparator cmp = new IntegerComparator();

			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				sortedList.toSortedList(cmp);
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

		// contains

		for (int n = 1000; n <= 20000; n += 1000) {
			int timesToLoop = 10000;
			// generate a size n random ArrayList
			ArrayCollection<Integer> sortedList = generateList(n);

			// create a random Integer
			Integer target = randomInt();

			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				sortedList.contains(target);
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

		// for SearchUtil.binarySearch

		for (int n = 1000; n <= 20000; n += 1000) {
			int timesToLoop = 10000;
			// comparator
			IntegerComparator cmp = new IntegerComparator();

			// using random method generate an Integer number
			Integer target = randomInt();

			// call generate list method, generate a size n ArrayList
			ArrayList<Integer> sortedList = generateList(n).toSortedList(cmp);

			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				SearchUtil.binarySearch(sortedList, target, cmp);
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

	/**
	 * This method is used for generating random integer number
	 * 
	 * @return an Integer number
	 */
	public static Integer randomInt() {
		return rand.nextInt();
	}

	/**
	 * This method is used for generating a random ArrayCollection
	 * 
	 * @param n
	 * @return an ArrayCollection
	 */
	public static ArrayCollection<Integer> generateList(int n) {
		ArrayCollection<Integer> sortedList = new ArrayCollection<Integer>();
		while (sortedList.size() <= n) {
			sortedList.add(randomInt());
		}
		return sortedList;
	}

}
