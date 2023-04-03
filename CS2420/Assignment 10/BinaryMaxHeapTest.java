package assign10;

import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

import org.junit.jupiter.api.Test;

/**
 * This is test for BinaryMaxHeap
 * 
 * @author linglei and Drew lawson
 *
 */
class BinaryMaxHeapTest {
	public class IntegerComparator implements Comparator<Integer> {
		@Override
		public int compare(Integer o1, Integer o2) {
			return o1.compareTo(o2);
		}
	}

	IntegerComparator cmp = new IntegerComparator();
	BinaryMaxHeap<Integer> heap = new BinaryMaxHeap<Integer>();
	BinaryMaxHeap<Integer> heapCmp = new BinaryMaxHeap<Integer>(cmp);

	List<Integer> list = new ArrayList<Integer>();
	List<Integer> expected = new ArrayList<Integer>();

	@Test
	void testConstructor1() {
		assertEquals(0, heap.size());
	}

	@Test
	void testAddOneWithOutCmp() {
		assertEquals(0, heap.size());
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
	}

	@Test
	void testAddTwoWithOutCmp() {
		assertEquals(0, heap.size());
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		heap.add(2);
		assertEquals(2, heap.size());
		assertEquals(2, heap.peek());
	}

	@Test
	void testAddTwoDuplicatesWithOutCmp() {
		assertEquals(0, heap.size());
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		heap.add(1);
		assertEquals(2, heap.size());
		assertEquals(1, heap.peek());
	}

	@Test
	void testAddThreeWithOutCmp() {
		assertEquals(0, heap.size());
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		heap.add(2);
		assertEquals(2, heap.size());
		assertEquals(2, heap.peek());
		heap.add(3);
		assertEquals(3, heap.size());
		assertEquals(3, heap.peek());
	}

	@Test
	void testAddThreeDuplicatesWithOutCmp() {
		assertEquals(0, heap.size());
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		heap.add(1);
		assertEquals(2, heap.size());
		assertEquals(1, heap.peek());
		heap.add(1);
		assertEquals(3, heap.size());
		assertEquals(1, heap.peek());
	}

	@Test
	void testAddFourWithOutCmp() {
		assertEquals(0, heap.size());
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		heap.add(2);
		assertEquals(2, heap.size());
		assertEquals(2, heap.peek());
		heap.add(4);
		assertEquals(3, heap.size());
		assertEquals(4, heap.peek());
		heap.add(9);
		assertEquals(4, heap.size());
		assertEquals(9, heap.peek());
	}

	@Test
	void testAddFourDuplicatesWithOutCmp() {
		assertEquals(0, heap.size());
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		heap.add(1);
		assertEquals(2, heap.size());
		assertEquals(1, heap.peek());
		heap.add(1);
		assertEquals(3, heap.size());
		assertEquals(1, heap.peek());
		heap.add(1);
		assertEquals(4, heap.size());
		assertEquals(1, heap.peek());
	}

	@Test
	void testAddSize_largeWithOutCmp() {
		assertEquals(0, heap.size());
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		heap.add(2);
		assertEquals(2, heap.size());
		assertEquals(2, heap.peek());
		heap.add(3);
		assertEquals(3, heap.size());
		assertEquals(3, heap.peek());
		heap.add(4);
		assertEquals(4, heap.size());
		assertEquals(4, heap.peek());
		heap.add(5);
		assertEquals(5, heap.size());
		assertEquals(5, heap.peek());
		heap.add(6);
		assertEquals(6, heap.size());
		assertEquals(6, heap.peek());
		heap.add(7);
		assertEquals(7, heap.size());
		assertEquals(7, heap.peek());
		heap.add(8);
		assertEquals(8, heap.size());
		assertEquals(8, heap.peek());
		heap.add(9);
		assertEquals(9, heap.size());
		assertEquals(9, heap.peek());
		heap.add(10);
		assertEquals(10, heap.size());
		assertEquals(10, heap.peek());
		heap.add(11);
		assertEquals(11, heap.size());
		assertEquals(11, heap.peek());
	}

	@Test
	void testAddLargeWithOutCmp() {
		assertEquals(0, heap.size());
		for (int i = 0; i < 1000; i++) {
			heap.add(i);
			assertEquals(i + 1, heap.size());
			assertEquals(i, heap.peek());
		}

	}

	@Test
	void testAddVeryLargeWithOutCmp() {
		assertEquals(0, heap.size());
		for (int i = 0; i < 100000; i++) {
			heap.add(i);
			assertEquals(i + 1, heap.size());
			assertEquals(i, heap.peek());
		}
	}

	@Test
	void testClear() {
		heap.add(1);
		heap.add(2);
		assertEquals(2, heap.size());
		assertEquals(2, heap.peek());
		heap.clear();
		assertEquals(0, heap.size());
		assertTrue(heap.isEmpty());
		heap.add(2);
		heap.add(2);
		assertEquals(2, heap.size());
		assertEquals(2, heap.peek());
		heap.clear();
		assertEquals(0, heap.size());
		assertTrue(heap.isEmpty());
	}

	@Test
	void testExtractOneWithOutCmp() {
		heap.add(1);
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		//
		assertEquals(1, heap.extractMax());
		assertEquals(0, heap.size());
	}

	@Test
	void testExtractTwoWithOutCmp() {
		heap.add(1);
		heap.add(10);
		assertEquals(2, heap.size());
		assertEquals(10, heap.peek());
		assertEquals(10, heap.extractMax());
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		assertEquals(1, heap.extractMax());
		assertEquals(0, heap.size());
	}

	@Test
	void testExtractThreeWithOutCmp() {
		heap.add(1);
		heap.add(10);
		heap.add(100);
		assertEquals(3, heap.size());
		assertEquals(100, heap.peek());
		assertEquals(100, heap.extractMax());
		assertEquals(2, heap.size());
		assertEquals(10, heap.peek());
		assertEquals(10, heap.extractMax());
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		assertEquals(1, heap.extractMax());
	}

	@Test
	void testExtractFourWithOutCmp() {
		heap.add(1);
		heap.add(10);
		heap.add(100);
		heap.add(1000);
		assertEquals(4, heap.size());
		assertEquals(1000, heap.peek());
		assertEquals(1000, heap.extractMax());
		assertEquals(3, heap.size());
		assertEquals(100, heap.peek());
		assertEquals(100, heap.extractMax());
		assertEquals(2, heap.size());
		assertEquals(10, heap.peek());
		assertEquals(10, heap.extractMax());
		assertEquals(1, heap.size());
		assertEquals(1, heap.peek());
		assertEquals(1, heap.extractMax());
		assertEquals(0, heap.size());
	}

	@Test
	void testExtractLargeWithOutCmp() {
		assertEquals(0, heap.size());
		for (int i = 0; i < 100; i++) {
			heap.add(i);
			assertEquals(i + 1, heap.size());
			assertEquals(i, heap.peek());
		}
		for (int i = 99; i >= 0; i--) {
			assertEquals(i + 1, heap.size());
			assertEquals(i, heap.peek());
			assertEquals(i, heap.extractMax());
		}
	}

	@Test
	void testExtractVeryLargeWithOutCmp() {
		assertEquals(0, heap.size());
		for (int i = 0; i < 10000; i++) {
			heap.add(i);
			assertEquals(i + 1, heap.size());
			assertEquals(i, heap.peek());
		}
		for (int i = 9999; i >= 0; i--) {
			assertEquals(i + 1, heap.size());
			assertEquals(i, heap.peek());
			assertEquals(i, heap.extractMax());
		}
	}

	@Test
	void testConstructor1WithCmp() {
		assertEquals(0, heapCmp.size());
	}

	@Test
	void testAddOneWithCmp() {
		assertEquals(0, heapCmp.size());
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
	}

	@Test
	void testAddTwoWithCmp() {
		assertEquals(0, heapCmp.size());
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(2);
		assertEquals(2, heapCmp.size());
		assertEquals(2, heapCmp.peek());
	}

	@Test
	void testAddTwoDuplicatesWithCmp() {
		assertEquals(0, heapCmp.size());
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(1);
		assertEquals(2, heapCmp.size());
		assertEquals(1, heapCmp.peek());
	}

	@Test
	void testAddThreeWithCmp() {
		assertEquals(0, heapCmp.size());
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(2);
		assertEquals(2, heapCmp.size());
		assertEquals(2, heapCmp.peek());
		heapCmp.add(3);
		assertEquals(3, heapCmp.size());
		assertEquals(3, heapCmp.peek());
	}

	@Test
	void testAddThreeDuplicatesWithCmp() {
		assertEquals(0, heapCmp.size());
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(1);
		assertEquals(2, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(1);
		assertEquals(3, heapCmp.size());
		assertEquals(1, heapCmp.peek());
	}

	@Test
	void testAddFourWithCmp() {
		assertEquals(0, heapCmp.size());
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(2);
		assertEquals(2, heapCmp.size());
		assertEquals(2, heapCmp.peek());
		heapCmp.add(4);
		assertEquals(3, heapCmp.size());
		assertEquals(4, heapCmp.peek());
		heapCmp.add(9);
		assertEquals(4, heapCmp.size());
		assertEquals(9, heapCmp.peek());
	}

	@Test
	void testAddFourDuplicatesWithCmp() {
		assertEquals(0, heapCmp.size());
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(1);
		assertEquals(2, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(1);
		assertEquals(3, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(1);
		assertEquals(4, heapCmp.size());
		assertEquals(1, heapCmp.peek());
	}

	@Test
	void testAddSize_largeWithCmp() {
		assertEquals(0, heapCmp.size());
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		heapCmp.add(2);
		assertEquals(2, heapCmp.size());
		assertEquals(2, heapCmp.peek());
		heapCmp.add(3);
		assertEquals(3, heapCmp.size());
		assertEquals(3, heapCmp.peek());
		heapCmp.add(4);
		assertEquals(4, heapCmp.size());
		assertEquals(4, heapCmp.peek());
		heapCmp.add(5);
		assertEquals(5, heapCmp.size());
		assertEquals(5, heapCmp.peek());
		heapCmp.add(6);
		assertEquals(6, heapCmp.size());
		assertEquals(6, heapCmp.peek());
		heapCmp.add(7);
		assertEquals(7, heapCmp.size());
		assertEquals(7, heapCmp.peek());
		heapCmp.add(8);
		assertEquals(8, heapCmp.size());
		assertEquals(8, heapCmp.peek());
		heapCmp.add(9);
		assertEquals(9, heapCmp.size());
		assertEquals(9, heapCmp.peek());
		heapCmp.add(10);
		assertEquals(10, heapCmp.size());
		assertEquals(10, heapCmp.peek());
		heapCmp.add(11);
		assertEquals(11, heapCmp.size());
		assertEquals(11, heapCmp.peek());
	}

	@Test
	void testAddLargeWithCmp() {
		assertEquals(0, heapCmp.size());
		for (int i = 0; i < 1000; i++) {
			heapCmp.add(i);
			assertEquals(i + 1, heapCmp.size());
			assertEquals(i, heapCmp.peek());
		}

	}

	@Test
	void testAddVeryLargeWithCmp() {
		assertEquals(0, heapCmp.size());
		for (int i = 0; i < 100000; i++) {
			heapCmp.add(i);
			assertEquals(i + 1, heapCmp.size());
			assertEquals(i, heapCmp.peek());
		}
	}

	@Test
	void testClearwithCmp() {
		heapCmp.add(1);
		heapCmp.add(2);
		assertEquals(2, heapCmp.size());
		assertEquals(2, heapCmp.peek());
		heapCmp.clear();
		assertEquals(0, heapCmp.size());
		assertTrue(heapCmp.isEmpty());
		heapCmp.add(2);
		heapCmp.add(2);
		assertEquals(2, heapCmp.size());
		assertEquals(2, heapCmp.peek());
		heapCmp.clear();
		assertEquals(0, heapCmp.size());
		assertTrue(heapCmp.isEmpty());
	}

	@Test
	void testExtractOneWithCmp() {
		heapCmp.add(1);
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		//
		assertEquals(1, heapCmp.extractMax());
		assertEquals(0, heapCmp.size());
	}

	@Test
	void testExtractTwoWithCmp() {
		heapCmp.add(1);
		heapCmp.add(10);
		assertEquals(2, heapCmp.size());
		assertEquals(10, heapCmp.peek());
		assertEquals(10, heapCmp.extractMax());
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		assertEquals(1, heapCmp.extractMax());
		assertEquals(0, heapCmp.size());
	}

	@Test
	void testExtractThreeWithCmp() {
		heapCmp.add(1);
		heapCmp.add(10);
		heapCmp.add(100);
		assertEquals(3, heapCmp.size());
		assertEquals(100, heapCmp.peek());
		assertEquals(100, heapCmp.extractMax());
		assertEquals(2, heapCmp.size());
		assertEquals(10, heapCmp.peek());
		assertEquals(10, heapCmp.extractMax());
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		assertEquals(1, heapCmp.extractMax());
	}

	@Test
	void testExtractFourWithCmp() {
		heapCmp.add(1);
		heapCmp.add(10);
		heapCmp.add(100);
		heapCmp.add(1000);
		assertEquals(4, heapCmp.size());
		assertEquals(1000, heapCmp.peek());
		assertEquals(1000, heapCmp.extractMax());
		assertEquals(3, heapCmp.size());
		assertEquals(100, heapCmp.peek());
		assertEquals(100, heapCmp.extractMax());
		assertEquals(2, heapCmp.size());
		assertEquals(10, heapCmp.peek());
		assertEquals(10, heapCmp.extractMax());
		assertEquals(1, heapCmp.size());
		assertEquals(1, heapCmp.peek());
		assertEquals(1, heapCmp.extractMax());
		assertEquals(0, heapCmp.size());
	}

	@Test
	void testExtractLargeWithCmp() {
		assertEquals(0, heapCmp.size());
		for (int i = 0; i < 100; i++) {
			heapCmp.add(i);
			assertEquals(i + 1, heapCmp.size());
			assertEquals(i, heapCmp.peek());
		}
		for (int i = 99; i >= 0; i--) {
			assertEquals(i + 1, heapCmp.size());
			assertEquals(i, heapCmp.peek());
			assertEquals(i, heapCmp.extractMax());
		}
	}

	@Test
	void testExtractVeryLargeWithCmp() {
		assertEquals(0, heapCmp.size());
		for (int i = 0; i < 10000; i++) {
			heapCmp.add(i);
			assertEquals(i + 1, heapCmp.size());
			assertEquals(i, heapCmp.peek());
		}
		for (int i = 9999; i >= 0; i--) {
			assertEquals(i + 1, heapCmp.size());
			assertEquals(i, heapCmp.peek());
			assertEquals(i, heapCmp.extractMax());
		}
	}

	@Test
	void testListConstructor() {
		list.add(1);
		BinaryMaxHeap<Integer> heapList = new BinaryMaxHeap<Integer>(list);
		assertEquals(1, heapList.size());
		assertEquals(1, heapList.peek());
	}

	@Test
	void testListConstructorTwoElements() {
		list.add(1);
		list.add(2);
		BinaryMaxHeap<Integer> heapList = new BinaryMaxHeap<Integer>(list);
		assertEquals(2, heapList.size());
		assertEquals(2, heapList.peek());
	}

	@Test
	void testListConstructorLarge() {
		for (int i = 0; i < 1000; i++) {
			list.add(i);
		}

		BinaryMaxHeap<Integer> heapList = new BinaryMaxHeap<Integer>(list);
		assertEquals(1000, heapList.size());
		assertEquals(999, heapList.peek());
	}

	@Test
	void testListConstructorCmp() {
		list.add(1);
		BinaryMaxHeap<Integer> heapList = new BinaryMaxHeap<Integer>(list, cmp);
		assertEquals(1, heapList.size());
		assertEquals(1, heapList.peek());
	}

	@Test
	void testListConstructorTwoElementsCmp() {
		list.add(1);
		list.add(2);
		BinaryMaxHeap<Integer> heapList = new BinaryMaxHeap<Integer>(list, cmp);
		assertEquals(2, heapList.size());
		assertEquals(2, heapList.peek());
	}

	@Test
	void testListConstructorLargeCMP() {
		for (int i = 0; i < 1000; i++) {
			list.add(i);
		}

		BinaryMaxHeap<Integer> heapList = new BinaryMaxHeap<Integer>(list, cmp);
		assertEquals(1000, heapList.size());
		assertEquals(999, heapList.peek());
	}

}
