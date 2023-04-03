package assign05;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Random;

import assign03.ArrayCollection;
import assign03.IntegerComparator;
import assign03.SearchUtil;

//IMPORTANT NOTE:
//FOR TIMING PURPOSES, IN PERMUTED LIST YOU MUST SET IT UP WITH A SEED. THIS CAN BE ACCOMPLISHED VIA
//
//public static ArrayList<Integer> generatePermuted(int size) {
//	Random random = new Random(<insert seed int value here>);
//	ArrayList<Integer> arry = generateAscending(size);
//	Collections.shuffle(arry, random);
//	return arry;
//}
//
//THIS ALLOWS THE RAND TO BE FIXED EVERY RUN, SO TIMING IS MORE CONSISTENT AND FAIR FOR COMPARING THRESHOLDS.
//MAKE SURE TO CHANGE THE THRESHOLD AND/OR PIVOT METHOD EACH TIME YOU RUN IT, AND TO HAVE THE MERGESORT THRESHOLD 
//BE 0 FOR ONE OF THE TESTS.
//COMMENT OUT TESTS AS NEEDED!!!!!

/**
 * This is timer class
 * 
 * @author linglei and Drew lawson
 *
 */
public class ArrayListSorterTimer {
	private static Random rand;

	public static void main(String[] args) {
		rand = new Random();
		rand.setSeed(System.currentTimeMillis());

		// MergeSort
		// for each size
		for (int n = 1000; n <= 50000; n += 1000) {
			int timesToLoop = 2000;
			// generate a size n random Array
			ArrayList<Integer> arry = ArrayListSorter.generatePermuted(n);
			ArrayList<Integer> arryCopy = new ArrayList<Integer>(arry);
			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				// While it does conflict with the time a bit, it allows for modification of the
				// array without having it
				// pre-sorted after the first run.
				arryCopy = new ArrayList<Integer>(arry);
				ArrayListSorter.mergesort(arryCopy);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				arryCopy = new ArrayList<Integer>(arry);
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);

		}

		for (int n = 1000; n <= 40000; n += 1000) {
			int timesToLoop = 100;
			// generate a size n random Array
			ArrayList<Integer> arry = ArrayListSorter.generateAscending(n);
			ArrayList<Integer> arryCopy = new ArrayList<Integer>(arry);
			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				// While it does conflict with the time a bit, it allows for modification of the
				// array without having it
				// pre-sorted after the first run.
				arryCopy = new ArrayList<Integer>(arry);
				ArrayListSorter.mergesort(arryCopy);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				arryCopy = new ArrayList<Integer>(arry);
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);

		}

		for (int n = 1000; n <= 40000; n += 1000) {
			int timesToLoop = 100;
			// generate a size n random Array
			ArrayList<Integer> arry = ArrayListSorter.generateDescending(n);
			ArrayList<Integer> arryCopy = new ArrayList<Integer>(arry);
			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				// While it does conflict with the time a bit, it allows for modification of the
				// array without having it
				// pre-sorted after the first run.
				arryCopy = new ArrayList<Integer>(arry);
				ArrayListSorter.mergesort(arryCopy);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				arryCopy = new ArrayList<Integer>(arry);
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);

		}

		// Quicksort
		// for each size
		for (int n = 1000; n <= 20000; n += 1000) {
			int timesToLoop = 3000;
			// generate a size n random Array
			ArrayListSorter.pivotChoice = 2;
			ArrayList<Integer> arry = ArrayListSorter.generatePermuted(n);
			ArrayList<Integer> arryCopy = new ArrayList<Integer>(arry);
			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				// While it does conflict with the time a bit, it allows for modification of the
				// array without having it
				// pre-sorted after the first run.
				arryCopy = new ArrayList<Integer>(arry);
				ArrayListSorter.quicksort(arryCopy);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				arryCopy = new ArrayList<Integer>(arry);
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);
		}

		// Ascending order with quick sort
		for (int n = 1000; n <= 20000; n += 1000) {
			int timesToLoop = 50;
			// generate a size n random Array
			ArrayListSorter.pivotChoice = 2;
			ArrayList<Integer> arry = ArrayListSorter.generateAscending(n);
			ArrayList<Integer> arryCopy = new ArrayList<Integer>(arry);
			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				// While it does conflict with the time a bit, it allows for modification of the
				// array without having it
				// pre-sorted after the first run.
				arryCopy = new ArrayList<Integer>(arry);
				ArrayListSorter.quicksort(arryCopy);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				arryCopy = new ArrayList<Integer>(arry);
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);
		}

		// descending order with quick sort
		for (int n = 1000; n <= 20000; n += 1000) {
			int timesToLoop = 50;
			// generate a size n random Array
			ArrayListSorter.pivotChoice = 2;
			ArrayList<Integer> arry = ArrayListSorter.generateDescending(n);
			ArrayList<Integer> arryCopy = new ArrayList<Integer>(arry);
			// set up all time values
			long startTime, midpointTime, stopTime;

			// First, spin computing stuff until one second has gone by.
			// This allows this thread to stabilize.
			startTime = System.nanoTime();
			while (System.nanoTime() - startTime < 1000000000) { // empty block
			}

			startTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				// While it does conflict with the time a bit, it allows for modification of the
				// array without having it
				// pre-sorted after the first run.
				arryCopy = new ArrayList<Integer>(arry);
				ArrayListSorter.quicksort(arryCopy);
			}

			midpointTime = System.nanoTime();

			for (int i = 0; i < timesToLoop; i++) {
				arryCopy = new ArrayList<Integer>(arry);
			}

			stopTime = System.nanoTime();

			// Compute the time, subtract the cost of running the loop
			// from the cost of running the loop and doing the lookups.
			// Average it over the number of runs.
			double averageTime = ((midpointTime - startTime) - (stopTime - midpointTime)) / (double) timesToLoop;

			System.out.println(n + "\t" + averageTime);
		}

	}

}
