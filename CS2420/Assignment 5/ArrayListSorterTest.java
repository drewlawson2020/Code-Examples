package assign05;

import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.Comparator;

import org.junit.jupiter.api.Test;

/**
 * This is test class for ArrayListSorter
 * 
 * @author linglei and Drew lawson
 *
 */
class ArrayListSorterTest {

	public class IntegerCmp implements Comparator<Integer> {
		@Override
		public int compare(Integer o1, Integer o2) {
			return o1.compareTo(o2);
		}
	}

	IntegerCmp Intcmp = new IntegerCmp();

	public class StringCmp implements Comparator<String> {
		@Override
		public int compare(String o1, String o2) {
			return o1.compareTo(o2);
		}
	}

	@Test
	void testGenerateAscendingSmallSize() {
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(4);
		assertEquals(4, arry.size());
		for (int i = 0; i < 4; i++) {
			assertEquals(i + 1, arry.get(i));
			if (i >= 1)
				assertTrue(arry.get(i) > arry.get(i - 1));
		}
	}

	@Test
	void testGenerateAscendingBigSize() {
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(2000);
		assertEquals(2000, arry.size());
		for (int i = 0; i < 2000; i++) {
			assertEquals(i + 1, arry.get(i));
			if (i >= 1)
				assertTrue(arry.get(i) > arry.get(i - 1));
		}
	}

	@Test
	void testGenerateAscendingOneTerm() {
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(1);
		assertEquals(1, arry.size());
		assertEquals(1, arry.get(0));
	}

	@Test
	void testGenerateAscendingNull() {
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(0);
		assertEquals(0, arry.size());
	}

	@Test
	void testGenerateDescendingNull() {
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(0);
		assertEquals(0, arry.size());
	}

	@Test
	void testGenerateDescendingOne() {
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(1);
		assertEquals(1, arry.size());
		assertEquals(1, arry.get(0));
	}

	@Test
	void testGenerateDescendingTwo() {
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(2);
		assertEquals(2, arry.size());
		assertEquals(2, arry.get(0));
		assertEquals(1, arry.get(1));
	}

	@Test
	void testGenerateDescendingMiddleSize() {
		int size = 10;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		assertEquals(size, arry.size());
		for (int i = 0; i < size; i++) {
			assertEquals(size--, arry.get(i));
		}
	}

	@Test
	void testGenerateDescendingBigSize() {
		int size = 20000;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		assertEquals(size, arry.size());
		for (int i = 0; i < 20000; i++) {
			assertEquals(size--, arry.get(i));
		}
	}

	@Test
	void testGeneratePermutedNullSize() {
		int size = 0;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		assertEquals(size, arry.size());
	}

	@Test
	void testGeneratePermutedOneSize() {
		int size = 1;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		assertEquals(size, arry.size());
		assertEquals(1, arry.get(0));
	}

	@Test
	void testGeneratePermutedtwoSize() {
		int size = 2;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		assertEquals(size, arry.size());
		assertTrue(arry.get(0) <= size);
		assertTrue(arry.get(1) <= size);
	}

	@Test
	void testCmpInsertionSortOrderedArray() {
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(10);
		IntegerCmp cmp = new IntegerCmp();
		ArrayListSorter.insertionSort(arry, 0, 9, cmp);
		int i = 1;
		for (int num : arry) {
			assertEquals(i++, num);
		}
		assertEquals(10, arry.size());
	}

	@Test
	void testCmpInsertionSortSmallUnorderedArray() {
		ArrayList<Integer> arry = new ArrayList<Integer>();
		arry.add(10);
		arry.add(6);
		arry.add(19);
		arry.add(-2);
		arry.add(0);
		arry.add(5);
		IntegerCmp cmp = new IntegerCmp();
		ArrayListSorter.insertionSort(arry, 0, 5, cmp);
		assertEquals(-2, arry.get(0));
		assertEquals(0, arry.get(1));
		assertEquals(5, arry.get(2));
		assertEquals(6, arry.get(3));
		assertEquals(10, arry.get(4));
		assertEquals(19, arry.get(5));
		assertEquals(6, arry.size());
	}

	@Test
	void testComparableInsertionSortOrderedArray() {
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(10);
		ArrayListSorter.insertionSort(arry, 0, 9);
		int i = 1;
		for (int num : arry) {
			assertEquals(i++, num);
		}
		assertEquals(10, arry.size());
	}

	@Test
	void testComparableInsertionSortSmallUnorderedArray() {
		ArrayList<Integer> arry = new ArrayList<Integer>();
		arry.add(10);
		arry.add(6);
		arry.add(19);
		arry.add(-2);
		arry.add(0);
		arry.add(5);
		ArrayListSorter.insertionSort(arry, 0, 2);
		assertEquals(6, arry.get(0));
		assertEquals(10, arry.get(1));
		ArrayListSorter.insertionSort(arry, 0, 5);
		assertEquals(-2, arry.get(0));
		assertEquals(0, arry.get(1));
		assertEquals(5, arry.get(2));
		assertEquals(6, arry.get(3));
		assertEquals(10, arry.get(4));
		assertEquals(19, arry.get(5));
		assertEquals(6, arry.size());
	}

	@Test
	void testMergeSortTwoTerm() {
		ArrayList<Integer> arry = new ArrayList<Integer>();
		arry.add(10);
		arry.add(2);
		ArrayListSorter.mergesort(arry);
		assertEquals(2, arry.get(0));
		assertEquals(10, arry.get(1));
	}

	@Test
	void testMergeDortTwoAscending() {
		int size = 2;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortTwoGenerateDescending() {
		int size = 2;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortTwoGeneratePermuted() {
		int size = 2;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortThreeTerm() {
		ArrayList<Integer> arry = new ArrayList<Integer>();
		arry.add(10);
		arry.add(2);
		arry.add(19);
		ArrayListSorter.mergesort(arry);
		assertEquals(2, arry.get(0));
		assertEquals(10, arry.get(1));
		assertEquals(19, arry.get(2));
	}

	@Test
	void testMergeSortThreeGenerateAscending() {
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(3);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < 3; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortThreeGenerateDescending() {
		int size = 3;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortThreeGeneratePermuted() {
		int size = 3;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFourGenerateAscending() {
		int size = 4;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFourGenerateDescending() {
		int size = 4;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFourGeneratePermuted() {
		int size = 4;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFiveGenerateAscending() {
		int size = 5;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFiveGenerateDescending() {
		int size = 5;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFiveGeneratePermuted() {
		int size = 5;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortMiddleSizedGenerateAscending() {
		int size = 100;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortMiddleSizedGenerateDescending() {
		int size = 100;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortMiddleSizedGeneratePermuted() {
		int size = 100;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortLargeSizedGenerateAscending() {
		int size = 10000;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortLargeSizedGenerateDescending() {
		int size = 10000;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortLargeSizedGeneratePermuted() {
		int size = 10000;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	// The following tests are testing overload methods with cmp.
	@Test
	void testMergeSortThreeGenerateAscendingWithCmp() {
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(3);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < 3; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortThreeGenerateDescendingWithCmp() {
		int size = 3;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortThreeGeneratePermutedWithCmp() {
		int size = 3;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFourGenerateAscendingWithCmp() {
		int size = 4;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFourGenerateDescendingWithCmp() {
		int size = 4;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFourGeneratePermutedWithCmp() {
		int size = 4;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFiveGenerateAscendingWithCmp() {
		int size = 5;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFiveGenerateDescendingWithCmp() {
		int size = 5;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortFiveGeneratePermutedWithCmp() {
		int size = 5;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortMiddleSizedGenerateAscendingWithCmp() {
		int size = 100;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortMiddleSizedGenerateDescendingWithCmp() {
		int size = 100;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortMiddleSizedGeneratePermutedWithCmp() {
		int size = 100;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortLargeSizedGenerateAscendingWithCmp() {
		int size = 10000;
		ArrayList<Integer> arry = ArrayListSorter.generateAscending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortLargeSizedGenerateDescendingWithCmp() {
		int size = 10000;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortLargeSizedGeneratePermutedWithCmp() {
		int size = 10000;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry, Intcmp);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testquicksortOneTerm() {
		int size = 1;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.quicksort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testquicksortThreeTermGenerateDescending() {
		int size = 3;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.quicksort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testquicksortThreeTermGeneratePermuted() {
		int size = 200;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.quicksort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testquicksortFourTermGenerateDescending() {
		int size = 4;
		ArrayList<Integer> arry = ArrayListSorter.generateDescending(size);
		ArrayListSorter.quicksort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testquicksortFourTermGeneratePermuted() {
		int size = 4;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.quicksort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testquicksortOneIntPermuted() {
		int size = 1;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.quicksort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testquicksortEmptyPermuted() {
		int size = 0;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.quicksort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void testMergeSortInsertionSortThreshold() {
		int size = 1;
		ArrayList<Integer> arry = ArrayListSorter.generatePermuted(size);
		ArrayListSorter.mergesort(arry);
		for (int i = 0; i < size; i++) {
			assertEquals(i + 1, arry.get(i));
		}
	}

	@Test
	void mergesortDuplicateTest1() {
		ArrayList<Integer> expectedList = new ArrayList<>();
		expectedList.add(1);
		expectedList.add(1);
		expectedList.add(1);
		expectedList.add(1);
		expectedList.add(1);

		ArrayList<Integer> actualList = new ArrayList<>();
		actualList.add(1);
		actualList.add(1);
		actualList.add(1);
		actualList.add(1);
		actualList.add(1);

		ArrayListSorter.mergesort(actualList);

		assertEquals(expectedList, actualList);
	}

	@Test
	void quicksortEmptyTest() {
		ArrayList<Integer> list = new ArrayList<>();
		ArrayList<Integer> actualList1 = new ArrayList<>();
		ArrayList<Integer> actualList2 = new ArrayList<>();
		ArrayList<Integer> actualList3 = new ArrayList<>();
		ArrayListSorter.pivotChoice = 1;
		ArrayListSorter.quicksort(actualList1);
		ArrayListSorter.pivotChoice = 2;
		ArrayListSorter.quicksort(actualList2);
		ArrayListSorter.pivotChoice = 5;
		ArrayListSorter.quicksort(actualList3);
		assertEquals(list, actualList1);
		assertEquals(list, actualList2);
		assertEquals(list, actualList3);
	}

	@Test
	void quicksortOneElementTest() {
		ArrayList<Integer> list = ArrayListSorter.generatePermuted(1);

		ArrayList<Integer> actualList1 = ArrayListSorter.generatePermuted(1);

		ArrayList<Integer> actualList2 = ArrayListSorter.generatePermuted(1);

		ArrayList<Integer> actualList3 = ArrayListSorter.generatePermuted(1);

		ArrayListSorter.pivotChoice = 1;
		ArrayListSorter.quicksort(actualList1);
		ArrayListSorter.pivotChoice = 2;
		ArrayListSorter.quicksort(actualList2);
		ArrayListSorter.pivotChoice = 5;
		ArrayListSorter.quicksort(actualList3);
		assertEquals(list, actualList1);
		assertEquals(list, actualList2);
		assertEquals(list, actualList3);
	}

	@Test
	void quicksortSameElementsTest() {
		ArrayList<Integer> list = new ArrayList<Integer>();
		list.add(1);
		list.add(1);
		list.add(1);
		list.add(1);
		list.add(1);
		ArrayList<Integer> actualList1 = new ArrayList<Integer>();
		actualList1.add(1);
		actualList1.add(1);
		actualList1.add(1);
		actualList1.add(1);
		actualList1.add(1);
		ArrayList<Integer> actualList2 = new ArrayList<Integer>();
		actualList2.add(1);
		actualList2.add(1);
		actualList2.add(1);
		actualList2.add(1);
		actualList2.add(1);
		ArrayList<Integer> actualList3 = new ArrayList<Integer>();
		actualList3.add(1);
		actualList3.add(1);
		actualList3.add(1);
		actualList3.add(1);
		actualList3.add(1);
		ArrayListSorter.pivotChoice = 1;
		ArrayListSorter.quicksort(actualList1);
		ArrayListSorter.pivotChoice = 2;
		ArrayListSorter.quicksort(actualList2);
		ArrayListSorter.pivotChoice = 5;
		ArrayListSorter.quicksort(actualList3);
		assertEquals(list, actualList1);
		assertEquals(list, actualList2);
		assertEquals(list, actualList3);
	}

	@Test
	void quicksortSameElements2Test() {
		ArrayList<Integer> list = new ArrayList<Integer>();
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(4);
		list.add(5);
		ArrayList<Integer> actualList1 = new ArrayList<Integer>();
		actualList1.add(1);
		actualList1.add(5);
		actualList1.add(4);
		actualList1.add(3);
		actualList1.add(2);
		ArrayList<Integer> actualList2 = new ArrayList<Integer>();
		actualList2.add(3);
		actualList2.add(2);
		actualList2.add(4);
		actualList2.add(1);
		actualList2.add(5);
		ArrayList<Integer> actualList3 = new ArrayList<Integer>();
		actualList3.add(5);
		actualList3.add(2);
		actualList3.add(1);
		actualList3.add(3);
		actualList3.add(4);
		ArrayListSorter.pivotChoice = 1;
		ArrayListSorter.quicksort(actualList1);
		ArrayListSorter.pivotChoice = 2;
		ArrayListSorter.quicksort(actualList2);
		ArrayListSorter.pivotChoice = 5;
		ArrayListSorter.quicksort(actualList3);
		assertEquals(list, actualList1);
		assertEquals(list, actualList2);
		assertEquals(list, actualList3);
	}

	@Test
	void quicksortAllZeroes2Test() {
		ArrayList<Integer> list = new ArrayList<Integer>();
		list.add(0);
		list.add(0);
		list.add(0);
		ArrayList<Integer> actualList1 = new ArrayList<Integer>();
		actualList1.add(0);
		actualList1.add(0);
		actualList1.add(0);
		ArrayList<Integer> actualList2 = new ArrayList<Integer>();
		actualList2.add(0);
		actualList2.add(0);
		actualList2.add(0);
		ArrayList<Integer> actualList3 = new ArrayList<Integer>();
		actualList3.add(0);
		actualList3.add(0);
		actualList3.add(0);
		ArrayListSorter.pivotChoice = 1;
		ArrayListSorter.quicksort(actualList1);
		ArrayListSorter.pivotChoice = 2;
		ArrayListSorter.quicksort(actualList2);
		ArrayListSorter.pivotChoice = 5;
		ArrayListSorter.quicksort(actualList3);
		assertEquals(list, actualList1);
		assertEquals(list, actualList2);
		assertEquals(list, actualList3);
	}

	@Test
	void quicksortNegativeTest() {
		ArrayList<Integer> list = new ArrayList<Integer>();
		list.add(-7);
		list.add(-6);
		list.add(-5);
		ArrayList<Integer> actualList1 = new ArrayList<Integer>();
		actualList1.add(-6);
		actualList1.add(-5);
		actualList1.add(-7);
		ArrayList<Integer> actualList2 = new ArrayList<Integer>();
		actualList2.add(-5);
		actualList2.add(-6);
		actualList2.add(-7);
		ArrayList<Integer> actualList3 = new ArrayList<Integer>();
		actualList3.add(-6);
		actualList3.add(-7);
		actualList3.add(-5);
		ArrayListSorter.pivotChoice = 1;
		ArrayListSorter.quicksort(actualList1);
		ArrayListSorter.pivotChoice = 2;
		ArrayListSorter.quicksort(actualList2);
		ArrayListSorter.pivotChoice = 5;
		ArrayListSorter.quicksort(actualList3);
		assertEquals(list, actualList1);
		assertEquals(list, actualList2);
		assertEquals(list, actualList3);
	}

	@Test
	void quicksortPostiveAndNegativeTest() {
		ArrayList<Integer> list = new ArrayList<Integer>();
		list.add(-7);
		list.add(-6);
		list.add(2);
		list.add(3);
		ArrayList<Integer> actualList1 = new ArrayList<Integer>();
		actualList1.add(-6);
		actualList1.add(-7);
		actualList1.add(3);
		actualList1.add(2);
		ArrayList<Integer> actualList2 = new ArrayList<Integer>();
		actualList2.add(2);
		actualList2.add(3);
		actualList2.add(-6);
		actualList2.add(-7);
		ArrayList<Integer> actualList3 = new ArrayList<Integer>();
		actualList3.add(-7);
		actualList3.add(-6);
		actualList3.add(3);
		actualList3.add(2);
		ArrayListSorter.pivotChoice = 1;
		ArrayListSorter.quicksort(actualList1);
		ArrayListSorter.pivotChoice = 2;
		ArrayListSorter.quicksort(actualList2);
		ArrayListSorter.pivotChoice = 5;
		ArrayListSorter.quicksort(actualList3);
		assertEquals(list, actualList1);
		assertEquals(list, actualList2);
		assertEquals(list, actualList3);
	}

	@Test
	void pivot() {
		ArrayList<Integer> list = new ArrayList<Integer>();
		list.add(-7);
		list.add(-6);
		list.add(2);
		list.add(3);
		ArrayListSorter.pivotChoice = 1;
		int num = ArrayListSorter.pivotChoose(list, 0, 4);
		assertEquals(0, num);
		ArrayListSorter.pivotChoice = 2;
		int num1 = ArrayListSorter.pivotChoose(list, 0, 4);
		assertEquals(2, num1);
	}

}
