package assign03;

import static org.junit.Assert.assertArrayEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.Iterator;

import org.junit.jupiter.api.Test;

/**
 * This class is testing ArrayCollection
 * 
 * @author Ling Lei and Drew Lawson
 *
 */
class ArrayCollectionTest {
	// ArrayCollection for type String
	ArrayCollection<String> collection1 = new ArrayCollection<String>();

	class Stringcomparator implements Comparator<String> {
		@Override
		public int compare(String o1, String o2) {
			return o1.compareTo(o2);
		}
	}

	// ArrayCollection for type int
	ArrayCollection<Integer> collection2 = new ArrayCollection<Integer>();

	ArrayCollection<Integer> collection3 = new ArrayCollection<Integer>();

	class OrderByIntValue implements Comparator<Integer> {

		public int compare(Integer lhs, Integer rhs) {
			return lhs.compareTo(rhs);
		}

	}

	@Test
	void testStringAddMethod() {
		assertTrue(collection1.add("1"));
		assertFalse(collection1.add("1"));
		assertTrue(collection1.add("3"));
		assertTrue(collection1.add("4"));
		assertTrue(collection1.add("5"));
		assertFalse(collection1.add("3"));
		assertFalse(collection1.add("5"));
		assertFalse(collection1.add("1"));
	}

	@Test
	void testStringAddAllMethod() {
		collection1.add("1");
		collection1.add("2");
		ArrayCollection<String> collection2 = new ArrayCollection<String>();
		collection2.add("1");
		assertFalse(collection1.addAll(collection2));
		collection2.clear();
		collection2.add("A");
		collection2.add("B");
		collection2.add("C");
		collection2.add("D");
		assertTrue(collection1.addAll(collection2));
		assertFalse(collection1.addAll(collection2));
		// test empty ArrayCollecton
		ArrayCollection<String> collection3 = new ArrayCollection<String>();
		ArrayCollection<String> collection4 = new ArrayCollection<String>();
		assertFalse(collection3.addAll(collection4));
		assertFalse(collection1.addAll(collection4));
	}

	@Test
	void testStringClear() {
		collection1.add("1");
		collection1.add("2");
		collection1.clear();
		assertEquals(0, collection1.size());
		collection1.add("A");
		collection1.add("B");
		collection1.clear();
		assertEquals(0, collection1.size());
	}

	@Test
	void testStringContains() {
		collection1.add("1");
		collection1.add("2");
		assertFalse(collection1.contains("2323"));
		assertTrue(collection1.contains("1"));
		collection1.add("87");
		assertTrue(collection1.contains("87"));
		assertTrue(collection1.contains("2"));
		assertFalse(collection1.contains("334"));
		collection1.clear();
		assertFalse(collection1.contains("2"));
	}

	@Test
	void testStringContainsAll() {
		collection1.add("1");
		collection1.add("2");
		ArrayCollection<String> collection2 = new ArrayCollection<String>();
		collection2.add("1");
		collection2.add("2");
		assertTrue(collection1.containsAll(collection2));
		// empty set
		collection2.clear();
		assertTrue(collection1.containsAll(collection2));
		collection1.clear();
		assertTrue(collection1.containsAll(collection2));
	}

	@Test
	void testStringIsEmpty() {
		collection1.add("1");
		collection1.add("2");
		assertFalse(collection1.isEmpty());
		collection1.clear();
		assertTrue(collection1.isEmpty());
		collection1.add("1");
		collection1.add("2");
		assertFalse(collection1.isEmpty());
	}

	@Test
	void testStringIndexOf() {
		collection1.add("1");
		collection1.add("2");
		assertEquals(0, collection1.indexOf("1"));
		assertEquals(1, collection1.indexOf("2"));
		collection1.clear();
		assertEquals(null, collection1.indexOf("2"));
		assertEquals(null, collection1.indexOf("1"));
	}

	@Test
	void testRemove() {
		collection1.add("1");
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		assertTrue(collection1.remove("3"));
		assertTrue(collection1.remove("4"));
		assertTrue(collection1.remove("1"));
		Object[] expected = new Object[collection1.size()];
		expected[0] = "2";
		assertArrayEquals(expected, collection1.toArray());
		// empty ones
		collection1.add("1");
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		collection1.clear();
		Object[] expected1 = new Object[collection1.size()];
		assertArrayEquals(expected1, collection1.toArray());
	}

	@Test
	void testRemoveAll() {
		collection1.add("1");
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		ArrayCollection<String> collection2 = new ArrayCollection<String>();
		collection2.add("1");
		collection2.add("2");
		collection2.add("3");
		collection2.add("4");
		assertTrue(collection1.removeAll(collection2));
		Object[] expected = new Object[collection1.size()];
		assertArrayEquals(expected, collection1.toArray());
	}

	@Test
	void testRetainAll() {
		collection1.add("1");
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		ArrayCollection<String> collection2 = new ArrayCollection<String>();
		collection2.add("1");
		collection2.add("2");
		collection2.add("3");
		collection2.add("4");
		assertFalse(collection1.retainAll(collection2));
		collection1.add("7");
		collection1.add("9");
		assertTrue(collection1.retainAll(collection2));
		collection2.clear();
		assertTrue(collection1.retainAll(collection2));
		Object[] expected = new Object[collection1.size()];
		assertArrayEquals(expected, collection1.toArray());
		// clear and test empty collection
		collection1.clear();
		collection2.clear();
		assertFalse(collection1.retainAll(collection2));
		collection2.add("3");
		collection2.add("4");
		assertFalse(collection1.retainAll(collection2));
	}

	@Test
	void testDuplicateAdd() {
		collection1.add("1");
		assertFalse(collection1.add("1"));
	}

	@Test
	void testSize() {
		collection1.add("1");
		assertEquals(1, collection1.size());
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		assertEquals(4, collection1.size());
		ArrayCollection<String> collection2 = new ArrayCollection<String>();
		collection2.add("1");
		collection2.add("2");
		collection2.add("3");
		collection2.add("4");
		collection1.retainAll(collection2);
		assertEquals(4, collection1.size());
		// clear
		collection2.clear();
		collection1.retainAll(collection2);
		assertEquals(0, collection1.size());
		// clear
		// test size track and addAll method
		collection1.clear();
		collection2.clear();
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		collection2.addAll(collection1);
		assertEquals(3, collection2.size());
		// clear
		// test size track and removeAll method
		collection1.clear();
		collection2.clear();
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		collection2.removeAll(collection1);
		assertEquals(0, collection2.size());
		collection2.add("1");
		collection2.add("6");
		collection2.add("9");
		collection2.removeAll(collection1);
		assertEquals(3, collection2.size());
		// clear
		// test size track and retainAll method
		collection1.clear();
		collection2.clear();
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		collection2.add("1");
		collection2.add("6");
		collection2.add("9");
		collection1.retainAll(collection2);
		assertEquals(0, collection1.size());
	}

	@Test
	void testToArray() {
		collection1.add("2");
		collection1.add("3");
		collection1.add("4");
		collection1.toArray();
		Object[] expected = new Object[collection1.size()];
		expected[0] = "2";
		expected[1] = "3";
		expected[2] = "4";
		assertArrayEquals(expected, collection1.toArray());
	}

	@Test
	void testSortedList() {
		collection1.add("87");
		collection1.add("3");
		collection1.add("1");
		// create an comparator
		Stringcomparator cmp = new Stringcomparator();
		Object[] expected = new Object[collection1.length()];
		expected[0] = "1";
		expected[1] = "3";
		expected[2] = "87";
		assertEquals(expected[0], collection1.toSortedList(cmp).get(0));
		assertEquals(expected[1], collection1.toSortedList(cmp).get(1));
		assertEquals(expected[2], collection1.toSortedList(cmp).get(2));
		// clear
		collection1.clear();
		collection2.clear();
		collection1.add("z");
		collection1.add("y");
		collection1.add("a");
		// create an comparator
		Object[] expected1 = new Object[collection1.length()];
		expected1[0] = "a";
		expected1[1] = "y";
		expected1[2] = "z";
		assertEquals(expected1[0], collection1.toSortedList(cmp).get(0));
		assertEquals(expected1[1], collection1.toSortedList(cmp).get(1));
		assertEquals(expected1[2], collection1.toSortedList(cmp).get(2));
		// clear
		collection1.clear();
		collection2.clear();
		collection1.add("aba");
		collection1.add("aa");
		collection1.add("bde");
		// create an comparator
		Object[] expected2 = new Object[collection1.length()];
		expected2[0] = "aa";
		expected2[1] = "aba";
		expected2[2] = "bde";
		assertEquals(expected2[0], collection1.toSortedList(cmp).get(0));
		assertEquals(expected2[1], collection1.toSortedList(cmp).get(1));
		assertEquals(expected2[2], collection1.toSortedList(cmp).get(2));
	}

	@Test
	void testIteratorString() {
		collection1.add("1");
		Iterator<String> ite = collection1.iterator();
		assertTrue(ite.hasNext());
		assertEquals("1", ite.next());
		assertFalse(ite.hasNext());
		// clear
		ArrayCollection<String> collection2 = new ArrayCollection<String>();
		collection2.clear();
		collection2.add("1");
		collection2.add("2");
		collection2.add("3");
		collection2.add("4");
		Iterator<String> ite2 = collection2.iterator();
		assertTrue(ite2.hasNext());
		ite2.next();
		ite2.remove();
		assertTrue(ite2.hasNext());
		ite2.next();
		ite2.remove();
		assertTrue(ite2.hasNext());
		ite2.next();
		ite2.remove();
		assertTrue(ite2.hasNext());
		ite2.next();
		ite2.remove();
		assertFalse(ite2.hasNext());
	}

	@Test
	void testIntegerAdd() {
		assertTrue(collection2.add(1));
		assertTrue(collection2.add(2));
		assertFalse(collection2.add(2));
		assertFalse(collection2.addAll(collection3));
	}

	@Test
	void testIntegerAddGrow() {
		assertTrue(collection2.add(1));
		assertTrue(collection2.add(2));
		collection2.add(3);
		collection2.add(4);
		collection2.add(5);
		collection2.add(6);
		collection2.add(7);
		collection2.add(8);
		collection2.add(9);
		collection2.add(10);
		collection2.add(11);
		collection2.add(12);
		collection2.add(13);
		collection2.add(14);
		collection2.add(15);
		collection2.add(16);
		collection2.add(17);
		assertEquals(17, collection2.size());
	}

	@Test
	void testIntegerAddAll() {
		collection2.add(1);
		collection2.add(2);
		collection2.add(3);
		assertTrue(collection3.addAll(collection2));
	}

	@Test
	void testDuplicateFailToAddAll() {
		collection2.add(1);
		collection2.add(2);
		collection2.add(3);
		collection3.add(1);
		collection3.add(2);
		collection3.add(5);
		assertTrue(collection3.addAll(collection2));
		// clear
		collection2.clear();
		collection2.add(97);
		collection2.add(98);
		collection3.addAll(collection2);
	}

	@Test
	void testContainsAll() {
		collection2.add(1);
		collection2.add(2);
		collection2.add(3);
		collection3.add(1);
		collection3.add(2);
		collection3.add(3);
		assertTrue(collection3.containsAll(collection2));
		// test empty set
		// clear
		collection2.clear();
		collection3.clear();
		collection2.containsAll(collection3);
		// test different order
		collection2.clear();
		collection3.clear();
		collection2.add(1);
		collection2.add(2);
		collection2.add(3);
		collection3.add(3);
		collection3.add(1);
		collection3.add(2);
		assertTrue(collection3.containsAll(collection2));
	}

	@Test
	void testRetention() {
		collection2.add(1);
		collection2.add(2);
		collection2.add(3);
		collection3.add(1);
		assertTrue(collection2.retainAll(collection3));
	}

	@Test
	void testContainsInt() {
		collection2.add(1);
		collection2.add(2);
		assertTrue(collection2.contains(2));
	}

	@Test
	void testContainsAllInt() {
		collection2.add(1);
		collection2.add(2);
		collection2.add(3);
		collection3.add(1);
		collection3.add(2);
		collection3.add(3);
		assertTrue(collection2.containsAll(collection3));
	}

	@Test
	void testSorted() {
		Comparator<Integer> cmp = new OrderByIntValue();
		collection2.add(1);
		collection2.add(4);
		collection2.add(5);
		collection2.add(3);
		ArrayList<Integer> actual = collection2.toSortedList(cmp);
		ArrayList<Integer> expected = new ArrayList<Integer>();
		expected.add(1);
		expected.add(3);
		expected.add(4);
		expected.add(5);
		assertTrue(expected.equals(actual));
	}

	@Test
	void testSortedandBinarySearch() {
		collection2.add(1);
		collection2.add(4);
		collection2.add(5);
		collection2.add(3);
		collection2.add(7);
		Comparator<Integer> cmp = new OrderByIntValue();
		ArrayList<Integer> actual = collection2.toSortedList(cmp);
		ArrayList<Integer> expected = new ArrayList<Integer>();
		expected.add(1);
		expected.add(3);
		expected.add(4);
		expected.add(5);
		expected.add(7);
		assertTrue(expected.equals(actual));
		assertTrue(SearchUtil.binarySearch(actual, 5, cmp));
	}

}
