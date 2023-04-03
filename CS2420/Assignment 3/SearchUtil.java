package assign03;

import java.util.ArrayList;
import java.util.Comparator;

/**
 * This class has binary search methods on ArrayList with customized type
 * 
 * @author Daniel Kopta and Ling Lei, Drew Lawson A utility class for searching,
 *         containing just one method (see below).
 *
 */
public class SearchUtil {

	/**
	 * This method does binary search on a ArrayList
	 * 
	 * @param <T>  The type of elements contained in the list
	 * @param list An ArrayList to search through. This ArrayList is assumed to be
	 *             sorted according to the supplied comparator. This method has
	 *             undefined behavior if the list is not sorted.
	 * @param item The item to search for
	 * @param cmp  Comparator for comparing Ts or a super class of T
	 * @return true if the item exists in the list, false otherwise
	 */
	public static <T> boolean binarySearch(ArrayList<T> list, T item, Comparator<? super T> cmp) {
		int left = 0;
		int right = list.size() - 1;

		while (left <= right) {
			int mid = (left + right) / 2;
			if (cmp.compare(list.get(mid), item) == 0)
				return true;
			else if (cmp.compare(list.get(mid), item) < 0) {
				left = mid + 1;
			} else
				right = mid - 1;

		}
		return false;
	}
}