package assign05;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Random;

/**
 * @author Ling Lei and Drew Lawson: Uses Mergesort, Insertion Sort, and
 *         Quicksort all as methods of sorting different sizes of arrays, along
 *         with methods that automatically generate lists of arrays to be sorted
 *         using said methods.
 */
public class ArrayListSorter {
	private static int threshold = 10;
	public static int pivotChoice = 1;

	/**
	 * This is a driver method for mergeSort
	 * 
	 * comparing with natural order
	 * 
	 * @param <T>
	 * @param arg
	 */
	public static <T extends Comparable<? super T>> void mergesort(ArrayList<T> arg) {
		// make sure temp is big enough
		ArrayList<T> temp = new ArrayList<T>();
		// make sure the ArrayList has enough size
		for (int i = 0; i < arg.size(); i++) {
			temp.add(null);
		}
		mergesort(arg, temp, 0, arg.size() - 1);
	}

	/**
	 * This is private helper recursive merge sort method,
	 * 
	 * it separates ArrayList into halves until the length hits threshold
	 * 
	 * then uses insertion sort to sort the small array
	 * 
	 * finally merge two arrays together
	 * 
	 * @param <T>
	 * @param arg
	 * @param temp
	 * @param start
	 * @param end
	 */
	private static <T extends Comparable<? super T>> void mergesort(ArrayList<T> arg, ArrayList<T> temp, int start,
			int end) {

		// basic case: hit threshold
		if (end - start <= threshold) {
			insertionSort(arg, start, end);
			return;
		}

		int mid = start + (end - start) / 2;
		mergesort(arg, temp, start, mid);
		mergesort(arg, temp, mid + 1, end);
		merge(arg, temp, start, mid, end);

	}

	/**
	 * This is a helper merge method, which merge two arrays together
	 * 
	 * utilizing temp array
	 * 
	 * @param <T>
	 * @param arr
	 * @param temp
	 * @param start
	 * @param mid
	 * @param end
	 */
	private static <T extends Comparable<? super T>> void merge(ArrayList<T> arr, ArrayList<T> temp, int start, int mid,
			int end) {

		int i = start;
		int j = mid + 1;
		int mergePos = start;

		while (i <= mid && j <= end) {
			if (arr.get(i).compareTo(arr.get(j)) <= 0)
				temp.set(mergePos++, arr.get(i++));
			else
				temp.set(mergePos++, arr.get(j++));
		}

		// copy anything left over from larger half to temp
		while (i <= mid) {
			temp.set(mergePos++, arr.get(i++));
		}
		while (j <= end) {
			temp.set(mergePos++, arr.get(j++));
		}

		// copy temp (from start to end) back into arr (from start to end)
		for (int index = start; index <= end; index++) {
			arr.set(index, temp.get(index));
		}
	}

	/**
	 * This insertion sort supports user without comparator input
	 * 
	 * which chooses Comparable by default
	 * 
	 * @param <T>
	 * @param arg
	 * @param start
	 * @param end
	 */
	public static <T extends Comparable<? super T>> void insertionSort(ArrayList<T> arg, int start, int end) {
		for (int i = start; i <= end; i++) {
			for (int j = i - 1; j >= start; j--) {
				if (arg.get(j).compareTo(arg.get(j + 1)) > 0) {
					T temp = arg.get(j);
					arg.set(j, arg.get(j + 1));
					arg.set(j + 1, temp);
				}
			}
		}
	}

	/**
	 * This method overloads the one compares with natural ordering
	 * 
	 * if users want to define their own compare method,
	 * 
	 * pass in customized comparator
	 * 
	 * @param <T>
	 * @param arg
	 * @param cmp
	 */
	public static <T> void mergesort(ArrayList<T> arg, Comparator<? super T> cmp) {
		// make sure temp is big enough
		ArrayList<T> temp = new ArrayList<T>();
		// make sure the ArrayList has enough size
		for (int i = 0; i < arg.size(); i++) {
			temp.add(null);
		}
		mergesort(arg, temp, 0, arg.size() - 1, cmp);
	}

	/**
	 * This recursive helper method overloads the one compares with natural ordering
	 * 
	 * it separates ArrayList into halves until the length hits threshold
	 * 
	 * then uses insertion sort to sort the small array
	 * 
	 * finally merge two arrays together
	 * 
	 * @param <T>
	 * @param arg
	 * @param temp
	 * @param start
	 * @param end
	 * @param cmp
	 */
	private static <T> void mergesort(ArrayList<T> arg, ArrayList<T> temp, int start, int end,
			Comparator<? super T> cmp) {

		// basic case: hit threshold
		if (end - start <= threshold) {
			insertionSort(arg, start, end, cmp);
			return;
		}

		int mid = start + (end - start) / 2;
		mergesort(arg, temp, start, mid, cmp);
		mergesort(arg, temp, mid + 1, end, cmp);
		merge(arg, temp, start, mid, end, cmp);
	}

	/**
	 * This is a helper merge method, overloads the one compares with natural
	 * ordering
	 * 
	 * which merge two arrays together utilizing temp array
	 * 
	 * @param <T>
	 * @param arr
	 * @param temp
	 * @param start
	 * @param mid
	 * @param end
	 * @param cmp
	 */
	public static <T> void merge(ArrayList<T> arr, ArrayList<T> temp, int start, int mid, int end,
			Comparator<? super T> cmp) {

		int i = start;
		int j = mid + 1;
		int mergePos = start;

		while (i <= mid && j <= end) {
			if (cmp.compare(arr.get(i), arr.get(j)) <= 0)
				temp.set(mergePos++, arr.get(i++));
			else
				temp.set(mergePos++, arr.get(j++));
		}

		// copy anything left over from larger half to temp
		while (i <= mid) {
			temp.set(mergePos++, arr.get(i++));
		}
		while (j <= end) {
			temp.set(mergePos++, arr.get(j++));
		}

		// copy temp (from start to end) back into arr (from start to end)
		for (int index = start; index <= end; index++) {
			arr.set(index, temp.get(index));
		}

	}

	/**
	 * This insertion sort supports user with comparator input
	 * 
	 * which uses the customized comparator provided by users
	 * 
	 * @param <T>
	 * @param arg
	 * @param start
	 * @param end
	 * @param cmp
	 */
	public static <T> void insertionSort(ArrayList<T> arg, int start, int end, Comparator<? super T> cmp) {
		for (int i = start; i <= end; i++) {
			for (int j = i - 1; j >= start; j--) {
				if (cmp.compare(arg.get(j), arg.get(j + 1)) > 0) {
					T temp = arg.get(j);
					arg.set(j, arg.get(j + 1));
					arg.set(j + 1, temp);
				}
			}
		}
	}

	/**
	 * This method generates and returns an ArrayList of
	 * 
	 * integers 1 to size in ascending order.
	 * 
	 * @param size
	 * @return ArrayList
	 */
	public static ArrayList<Integer> generateAscending(int size) {
		ArrayList<Integer> arry = new ArrayList<Integer>();
		for (int index = 0; index < size; index++) {
			arry.add(index + 1);
		}
		return arry;
	}

	/**
	 * This method generates and returns an ArrayList of
	 * 
	 * integers 1 to size in permuted order (i,e., randomly ordered)
	 * 
	 * @param size
	 * @return ArrayList
	 */
	public static ArrayList<Integer> generatePermuted(int size) {
		ArrayList<Integer> arry = generateAscending(size);
		Collections.shuffle(arry);
		return arry;
	}

	/**
	 * This method generates and returns an ArrayList
	 * 
	 * of integers 1 to size in descending order.
	 * 
	 * @param size
	 * @return ArrayList
	 */
	public static ArrayList<Integer> generateDescending(int size) {
		ArrayList<Integer> arry = new ArrayList<Integer>();
		for (int i = size; i > 0; i--) {
			arry.add(i);
		}
		return arry;
	}

	/**
	 * This is helper method which choose pivot
	 * 
	 * @param <T>
	 * @param arry
	 * @param pivot
	 * @return a T type pivot
	 */
	public static <T> int pivotChoose(ArrayList<T> arry, int start, int end) {
		// the first of the array
		if (pivotChoice == 1)
			return start;
		else if (pivotChoice == 2)
			return (start + end) / 2;
		// by default, return random one
		return new Random().nextInt(end - start + 1) + start;
	}

	/**
	 * This is driver method for quickSort
	 * 
	 * @param <T>
	 * @param arry
	 */
	public static <T extends Comparable<? super T>> void quicksort(ArrayList<T> arry) {
		// call helper recursive method
		quicksort(arry, 0, arry.size() - 1);
	}

	private static <T extends Comparable<? super T>> void quicksort(ArrayList<T> arry, int start, int end) {
		if (end <= start)
			return;
		if (end - start <= threshold) {
			insertionSort(arry, start, end);
			return;
		}
		// Choose Pivot

		int pivotIndex = pivotChoose(arry, start, end);

		T pivot = arry.get(pivotIndex);

		swap(arry, pivotIndex, start);

		int L = start;
		int R = end;
		while (L <= R) {
			while (L < R && arry.get(L).compareTo(pivot) <= 0) {
				L++;
			}
			while (R >= L && arry.get(R).compareTo(pivot) > 0) {
				R--;
			}

			if (L >= R)
				break;
			swap(arry, L, R);
		}

		swap(arry, start, R);
		quicksort(arry, start, R - 1);
		quicksort(arry, R + 1, end);
	}

	/**
	 * This is a helper method which helps swap two items in an array
	 * 
	 * @param <T>
	 * @param arry
	 * @param left
	 * @param right
	 */
	public static <T> void swap(ArrayList<T> arry, int left, int right) {
		T temp = arry.get(left);
		arry.set(left, arry.get(right));
		arry.set(right, temp);
	}

}
