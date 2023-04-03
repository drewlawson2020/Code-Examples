package assign10;

import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

import org.junit.jupiter.api.Test;

/**
 * This is test for FindKLargest
 * 
 * @author linglei and Drew lawson
 *
 */
class FindKLargestTest {
	public class IntegerComparator implements Comparator<Integer> {
		@Override
		public int compare(Integer o1, Integer o2) {
			return o1.compareTo(o2);
		}
	}

	public class IntegerInverseComparator implements Comparator<Integer> {
		@Override
		public int compare(Integer o1, Integer o2) {
			if (o1.compareTo(o2) > 0)
				return -1;
			if (o1.compareTo(o2) == 0)
				return 0;
			else
				return 1;
		}
	}

	IntegerComparator cmp = new IntegerComparator();
	IntegerInverseComparator cmpInverse = new IntegerInverseComparator();
	List<Integer> list = new ArrayList<Integer>();
	List<Integer> expected = new ArrayList<Integer>();

	@Test
	void testOneElement() {
		list.add(1);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1));
	}

	@Test
	void testAddTwoElementFindOne() {
		list.add(1);
		list.add(2);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1));
	}

	@Test
	void testAddTwoElementFindTwo() {
		list.add(1);
		list.add(2);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 2));
	}

	@Test
	void testAddThreeElementFindOne() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1));
	}

	@Test
	void testAddThreeElementFindTwo() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 2));
	}

	@Test
	void testAddThreeElementFindThree() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 3));
	}

	@Test
	void testAddFourElementFindOne() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1));
	}

	@Test
	void testAddFourElementFindTwo() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 2));
	}

	@Test
	void testAddFourElementFindThree() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 3));
	}

	@Test
	void testAddFourElementFindFour() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 4));
	}

	@Test
	void testMiddle() {

		for (int i = 0; i < 1000; i++) {
			list.add(i);
		}

		for (int i = 999; i >= 0; i--) {
			expected.add(i);
		}

		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1000));
	}

	@Test
	void testLarge() {

		for (int i = 0; i < 100000; i++) {
			list.add(i);
		}

		for (int i = 99999; i >= 0; i--) {
			expected.add(i);
		}

		assertEquals(expected, FindKLargest.findKLargestHeap(list, 100000));
	}

	@Test
	void testOneElementWithCmp() {
		list.add(1);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1, cmp));
	}

	@Test
	void testAddTwoElementFindOneWithCmp() {
		list.add(1);
		list.add(2);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1, cmp));
	}

	@Test
	void testAddTwoElementFindTwoWithCmp() {
		list.add(1);
		list.add(2);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 2, cmp));
	}

	@Test
	void testAddThreeElementFindOneWithCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1, cmp));
	}

	@Test
	void testAddThreeElementFindTwoWithCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 2, cmp));
	}

	@Test
	void testAddThreeElementFindThreeWithCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 3, cmp));
	}

	@Test
	void testAddFourElementFindOneWithCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1, cmp));
	}

	@Test
	void testAddFourElementFindTwoWithCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 2, cmp));
	}

	@Test
	void testAddFourElementFindThreeWithCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 3, cmp));
	}

	@Test
	void testAddFourElementFindFourWithCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestHeap(list, 4, cmp));
	}

	@Test
	void testMiddleWithCmp() {

		for (int i = 0; i < 1000; i++) {
			list.add(i);
		}

		for (int i = 999; i >= 0; i--) {
			expected.add(i);
		}

		assertEquals(expected, FindKLargest.findKLargestHeap(list, 1000, cmp));
	}

	@Test
	void testLargeWithCmp() {

		for (int i = 0; i < 100000; i++) {
			list.add(i);
		}

		for (int i = 99999; i >= 0; i--) {
			expected.add(i);
		}

		assertEquals(expected, FindKLargest.findKLargestHeap(list, 100000, cmp));
	}

	@Test
	void testOneElementUsingJavaSort() {
		list.add(1);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 1));
	}

	@Test
	void testAddTwoElementFindOneUsingJavaSort() {
		list.add(1);
		list.add(2);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 1));
	}

	@Test
	void testAddTwoElementFindTwoUsingJavaSort() {
		list.add(1);
		list.add(2);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 2));
	}

	@Test
	void testAddThreeElementFindOneUsingJavaSort() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 1));
	}

	@Test
	void testAddThreeElementFindTwoUsingJavaSort() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 2));
	}

	@Test
	void testAddThreeElementFindThreeUsingJavaSort() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 3));
	}

	@Test
	void testAddFourElementFindOneUsingJavaSort() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 1));
	}

	@Test
	void testAddFourElementFindTwoUsingJavaSort() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 2));
	}

	@Test
	void testAddFourElementFindThreeUsingJavaSort() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 3));
	}

	@Test
	void testAddFourElementFindFourUsingJavaSort() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 4));
	}

	@Test
	void testMiddleUsingJavaSort() {

		for (int i = 0; i < 1000; i++) {
			list.add(i);
		}

		for (int i = 999; i >= 0; i--) {
			expected.add(i);
		}

		assertEquals(expected, FindKLargest.findKLargestSort(list, 1000));
	}

	@Test
	void testLargeUsingJavaSort() {

		for (int i = 0; i < 100000; i++) {
			list.add(i);
		}

		for (int i = 99999; i >= 0; i--) {
			expected.add(i);
		}

		assertEquals(expected, FindKLargest.findKLargestSort(list, 100000));
	}

	@Test
	void testOneElementUsingJavaSortCmp() {
		list.add(1);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 1, cmpInverse));
	}

	@Test
	void testAddTwoElementFindOneUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 1, cmpInverse));
	}

	@Test
	void testAddTwoElementFindTwoUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 2, cmpInverse));
	}

	@Test
	void testAddThreeElementFindOneUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 1, cmpInverse));
	}

	@Test
	void testAddThreeElementFindTwoUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 2, cmpInverse));
	}

	@Test
	void testAddThreeElementFindThreeUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		expected.add(3);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 3, cmpInverse));
	}

	@Test
	void testAddFourElementFindOneUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 1, cmpInverse));
	}

	@Test
	void testAddFourElementFindTwoUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 2, cmpInverse));
	}

	@Test
	void testAddFourElementFindThreeUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		expected.add(2);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 3, cmpInverse));
	}

	@Test
	void testAddFourElementFindFourUsingJavaSortCmp() {
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(100);
		expected.add(100);
		expected.add(3);
		expected.add(2);
		expected.add(1);
		assertEquals(expected, FindKLargest.findKLargestSort(list, 4, cmpInverse));
	}

	@Test
	void testMiddleUsingJavaSortCmp() {

		for (int i = 0; i < 1000; i++) {
			list.add(i);
		}

		for (int i = 999; i >= 0; i--) {
			expected.add(i);
		}

		assertEquals(expected, FindKLargest.findKLargestSort(list, 1000, cmpInverse));
	}

	@Test
	void testLargeUsingJavaSortCmp() {

		for (int i = 0; i < 100000; i++) {
			list.add(i);
		}

		for (int i = 99999; i >= 0; i--) {
			expected.add(i);
		}

		assertEquals(expected, FindKLargest.findKLargestSort(list, 100000, cmpInverse));
	}

}
