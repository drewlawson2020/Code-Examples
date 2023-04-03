package assign10;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

/**
 * This class contains generic static methods for finding the k largest items in
 * a list.
 * 
 * @author Erin Parker & Ling Lei & Drew Lawson
 * @version
 */
public class FindKLargest {

	/**
	 * Determines the k largest items in the given list, using a binary max heap and
	 * the natural ordering of the items.
	 * 
	 * @param items - the given list
	 * @param k     - the number of largest items
	 * @return a list of the k largest items, in descending order
	 * @throws IllegalArgumentException if k is negative or larger than the size of
	 *                                  the given list
	 */
	public static <E extends Comparable<? super E>> List<E> findKLargestHeap(List<E> items, int k)
			throws IllegalArgumentException {

		BinaryMaxHeap<E> tree = new BinaryMaxHeap<>(items);
		BinaryMaxHeap<E> temp = tree;
		List<E> list = new ArrayList<E>();

		if (k < 0 || k > items.size() || items.size() <= 0) {
			throw new IllegalArgumentException();
		}

		for (int i = 0; i < k; i++) {
			list.add(temp.extractMax());
		}

		return list;
	}

	/**
	 * Determines the k largest items in the given list, using a binary max heap.
	 * 
	 * @param items - the given list
	 * @param k     - the number of largest items
	 * @param cmp   - the comparator defining how to compare items
	 * @return a list of the k largest items, in descending order
	 * @throws IllegalArgumentException if k is negative or larger than the size of
	 *                                  the given list
	 */
	public static <E> List<E> findKLargestHeap(List<E> items, int k, Comparator<? super E> cmp)
			throws IllegalArgumentException {

		BinaryMaxHeap<E> tree = new BinaryMaxHeap<>(items, cmp);
		BinaryMaxHeap<E> temp = tree;
		List<E> list = new ArrayList<E>();

		if (k < 0 || k > items.size() || items.size() <= 0) {
			throw new IllegalArgumentException();
		}

		for (int i = 0; i < k; i++) {
			list.add(temp.extractMax());
		}

		return list;
	}

	/**
	 * Determines the k largest items in the given list, using Java's sort routine
	 * and the natural ordering of the items.
	 * 
	 * @param items - the given list
	 * @param k     - the number of largest items
	 * @return a list of the k largest items, in descending order
	 * @throws IllegalArgumentException if k is negative or larger than the size of
	 *                                  the given list
	 */
	public static <E extends Comparable<? super E>> List<E> findKLargestSort(List<E> items, int k)
			throws IllegalArgumentException {
		if (k < 0 || k > items.size() || items.size() <= 0) {
			throw new IllegalArgumentException();
		}

		List<E> list = new ArrayList<E>();
		List<E> newList = new ArrayList<E>();

		for (int i = 0; i < items.size(); i++) {
			list.add(items.get(i));
		}

		Collections.sort(list, Collections.reverseOrder());

		for (int i = 0; i < k; i++) {
			newList.add(list.get(i));
		}

		return newList;
	}

	/**
	 * Determines the k largest items in the given list, using Java's sort routine.
	 * 
	 * @param items - the given list
	 * @param k     - the number of largest items
	 * @param cmp   - the comparator defining how to compare items
	 * @return a list of the k largest items, in descending order
	 * @throws IllegalArgumentException if k is negative or larger than the size of
	 *                                  the given list
	 */
	public static <E> List<E> findKLargestSort(List<E> items, int k, Comparator<? super E> cmp)
			throws IllegalArgumentException {
		if (k < 0 || k > items.size() || items.size() <= 0) {
			throw new IllegalArgumentException();
		}

		List<E> list = new ArrayList<E>();
		List<E> newList = new ArrayList<E>();

		for (int i = 0; i < items.size(); i++) {
			list.add(items.get(i));
		}

		Collections.sort(list, cmp);

		for (int i = 0; i < k; i++) {
			newList.add(list.get(i));
		}

		return newList;
	}
}
