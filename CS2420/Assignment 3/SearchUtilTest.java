package assign03;

import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.Comparator;

import org.junit.jupiter.api.Test;

/**
 * This class is used for testing SearchUtilTest
 * 
 * @author Ling Lei and Drew Lawson
 *
 */
class SearchUtilTest {
	ArrayList<String> a1 = new ArrayList<String>();

	ArrayCollection<Integer> a2 = new ArrayCollection<Integer>();

	ArrayCollection<String> a3 = new ArrayCollection<String>();

	class GenericComparator<T extends Comparable<? super T>> implements Comparator<T> {
		@Override
		public int compare(T o1, T o2) {
			return (o1.compareTo(o2));
		}
	}

	@Test
	void testStringArrayList() {
		a1.add("a");
		a1.add("b");
		a1.add("c");
		a1.add("e");
		a1.add("f");
		GenericComparator<String> cmp = new GenericComparator<String>();
		assertTrue(SearchUtil.binarySearch(a1, "a", cmp));
		assertTrue(SearchUtil.binarySearch(a1, "b", cmp));
		assertTrue(SearchUtil.binarySearch(a1, "c", cmp));
		assertTrue(SearchUtil.binarySearch(a1, "e", cmp));
		assertTrue(SearchUtil.binarySearch(a1, "f", cmp));
		assertFalse(SearchUtil.binarySearch(a1, "oiugfvbnjkuyg", cmp));
	}

	@Test
	void testIntegerArrayCollection() {
		a2.add(3);
		a2.add(1);
		a2.add(4);
		a2.add(2);
		a2.add(6);
		GenericComparator<Integer> cmp = new GenericComparator<Integer>();
		ArrayList<Integer> a3 = a2.toSortedList(cmp);
		assertTrue(SearchUtil.binarySearch(a3, 1, cmp));
		assertTrue(SearchUtil.binarySearch(a3, 3, cmp));
		assertTrue(SearchUtil.binarySearch(a3, 2, cmp));
		assertTrue(SearchUtil.binarySearch(a3, 4, cmp));
		assertTrue(SearchUtil.binarySearch(a3, 6, cmp));
		assertFalse(SearchUtil.binarySearch(a3, 43, cmp));
	}

	@Test
	void testStringArrayCollection() {
		a3.add("5");
		a3.add("3");
		a3.add("4");
		a3.add("2");
		a3.add("6");
		GenericComparator<String> cmp = new GenericComparator<String>();
		ArrayList<String> a4 = a3.toSortedList(cmp);
		assertTrue(SearchUtil.binarySearch(a3.toSortedList(cmp), "2", cmp));
		assertTrue(SearchUtil.binarySearch(a3.toSortedList(cmp), "3", cmp));
		assertTrue(SearchUtil.binarySearch(a3.toSortedList(cmp), "4", cmp));
		assertTrue(SearchUtil.binarySearch(a3.toSortedList(cmp), "5", cmp));
		assertTrue(SearchUtil.binarySearch(a3.toSortedList(cmp), "6", cmp));
		assertFalse(SearchUtil.binarySearch(a3.toSortedList(cmp), "65", cmp));
	}

}
