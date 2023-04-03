package assign04;

import static org.junit.jupiter.api.Assertions.*;

import java.util.Arrays;
import java.util.Comparator;

import org.junit.jupiter.api.Test;

/**
 * This is test class
 * 
 * @author linglei and Drew Lawson
 *
 */
class AnagramCheckerTest {
	String[] arg1;
	Integer[] arg2;

	public class stringComparator implements Comparator<String> {

		@Override
		public int compare(String o1, String o2) {
			return o1.compareTo(o2);

		}

	}

	public class IntegerComparator implements Comparator<Integer> {

		@Override
		public int compare(Integer o1, Integer o2) {
			return o1.compareTo(o2);
		}
	}

	public class StudentsComparator implements Comparator<Students> {
		@Override
		public int compare(Students o1, Students o2) {
			// TODO Auto-generated method stub
			int i = o1.getID();
			int j = o2.getID();
			if (i > j)
				return 1;
			else if (i == j)
				return 0;
			else
				return -1;
		}
	}

	// test String sort
	@Test
	void testOrderedStringSort() {
		String o1 = "12345";
		assertEquals("12345", AnagramChecker.sort(o1));
	}

	@Test
	void testOrderedDuplicateStringSort() {
		String o1 = "12333345";
		assertEquals("12333345", AnagramChecker.sort(o1));
	}

	@Test
	void testEmptyStringSort() {
		String o1 = "";
		assertEquals("", AnagramChecker.sort(o1));
	}

	@Test
	void testUnorderedStringSort() {
		String o1 = "9876";
		assertEquals("6789", AnagramChecker.sort(o1));
	}

	@Test
	void testUnorderedDuplicatedStringSort() {
		String o1 = "9534272251748";
		assertEquals("1222344557789", AnagramChecker.sort(o1));
	}

	@Test
	void testDuplicatedStringSort() {
		String o1 = "9999";
		assertEquals("9999", AnagramChecker.sort(o1));
	}

	// test insertionSort
	@Test
	void testinsertionSortInt() {
		arg1 = new String[] { "9", "7", "1", "2" };
		stringComparator cmp = new stringComparator();
		AnagramChecker.insertionSort(arg1, cmp);
		assertEquals("9", arg1[3]);
		assertEquals("7", arg1[2]);
		assertEquals("2", arg1[1]);
		assertEquals("1", arg1[0]);

		// test all same String nums
		arg1 = new String[] { "1", "1", "1", "1" };
		AnagramChecker.insertionSort(arg1, cmp);
		assertEquals("1", arg1[3]);
		assertEquals("1", arg1[2]);
		assertEquals("1", arg1[1]);
		assertEquals("1", arg1[0]);

		// test all zeros
		arg1 = new String[] { "0", "0", "0", "0" };
		AnagramChecker.insertionSort(arg1, cmp);
		assertEquals("0", arg1[0]);
		assertEquals("0", arg1[1]);
		assertEquals("0", arg1[2]);
		assertEquals("0", arg1[3]);

		// test all negatives string nums
		arg1 = new String[] { "-4", "-3", "-2", "-1" };
		AnagramChecker.insertionSort(arg1, cmp);
		assertEquals("-1", arg1[0]);
		assertEquals("-2", arg1[1]);
		assertEquals("-3", arg1[2]);
		assertEquals("-4", arg1[3]);

		// test String letters
		arg1 = new String[] { "abs", "abc", "bs", "mn", "zh" };
		AnagramChecker.insertionSort(arg1, cmp);
		assertEquals("abc", arg1[0]);
		assertEquals("abs", arg1[1]);
		assertEquals("bs", arg1[2]);
		assertEquals("mn", arg1[3]);
		assertEquals("zh", arg1[4]);

		// test Duplicate letters
		arg1 = new String[] { "a", "a", "a", "c", "z", "r" };
		AnagramChecker.insertionSort(arg1, cmp);
		assertEquals("a", arg1[0]);
		assertEquals("a", arg1[1]);
		assertEquals("a", arg1[2]);
		assertEquals("c", arg1[3]);
		assertEquals("r", arg1[4]);
		assertEquals("z", arg1[5]);
	}

	@Test
	void testPositiveIntegerInsertionsort() {
		arg2 = new Integer[] { 92, 482, 32, 21, 1 };
		IntegerComparator cmp = new IntegerComparator();
		AnagramChecker.insertionSort(arg2, cmp);
		assertEquals(1, arg2[0]);
		assertEquals(21, arg2[1]);
		assertEquals(32, arg2[2]);
		assertEquals(92, arg2[3]);
		assertEquals(482, arg2[4]);
	}

	@Test
	void testZeroPositiveIntegerInsertionsort() {
		arg2 = new Integer[] { 56, 98, 76, 0, 98 };
		IntegerComparator cmp = new IntegerComparator();
		AnagramChecker.insertionSort(arg2, cmp);
		assertEquals(0, arg2[0]);
		assertEquals(56, arg2[1]);
		assertEquals(76, arg2[2]);
		assertEquals(98, arg2[3]);
		assertEquals(98, arg2[4]);
	}

	@Test
	void testNegativeIntegerInsertionsort() {
		arg2 = new Integer[] { -9, -98, -76, 0, -7 };
		IntegerComparator cmp = new IntegerComparator();
		AnagramChecker.insertionSort(arg2, cmp);
		assertEquals(-98, arg2[0]);
		assertEquals(-76, arg2[1]);
		assertEquals(-9, arg2[2]);
		assertEquals(-7, arg2[3]);
		assertEquals(0, arg2[4]);
	}

	@Test
	void testAllZerosIntegerInsertionsort() {
		arg2 = new Integer[] { 0, 0, 0, 0, 0 };
		IntegerComparator cmp = new IntegerComparator();
		AnagramChecker.insertionSort(arg2, cmp);
		assertEquals(0, arg2[0]);
		assertEquals(0, arg2[1]);
		assertEquals(0, arg2[2]);
		assertEquals(0, arg2[3]);
		assertEquals(0, arg2[4]);
	}

	// test areAnagrams
	@Test
	void testAreAnagram() {
		String arg1 = "ming";
		String arg2 = "ingm";
		assertTrue(AnagramChecker.areAnagrams(arg1, arg2));
		arg1 = "ling";
		arg2 = "nilg";
		assertTrue(AnagramChecker.areAnagrams(arg1, arg2));
		arg1 = "";
		arg2 = "";
		assertTrue(AnagramChecker.areAnagrams(arg1, arg2));
		arg1 = "1";
		arg2 = "3";
		assertFalse(AnagramChecker.areAnagrams(arg1, arg2));
		arg1 = "lomh.";
		arg2 = "omhl.";
		assertTrue(AnagramChecker.areAnagrams(arg1, arg2));
		arg1 = "0987654321";
		arg2 = "1234567890";
		assertTrue(AnagramChecker.areAnagrams(arg1, arg2));
		arg1 = "10000";
		arg2 = "nilg";
		assertFalse(AnagramChecker.areAnagrams(arg1, arg2));
		arg1 = "Bob";
		arg2 = "obb";
		assertTrue(AnagramChecker.areAnagrams(arg1, arg2));
		arg1 = "AAb";
		arg2 = "aAB";
		assertTrue(AnagramChecker.areAnagrams(arg1, arg2));
	}

	@Test
	void testgetLargestAnagramGroup() {
		String[] arg1 = { "12", "32", "21", "23", "34", "23" };
		String[] arg2 = AnagramChecker.getLargestAnagramGroup(arg1);
		String[] expected = new String[] { "23", "23", "32" };
		assertTrue(Arrays.equals(expected, arg2));
		arg1 = new String[] { "12", "32", "21", "23", "34", "23", "23", "34", "43", "23" };
		arg2 = AnagramChecker.getLargestAnagramGroup(arg1);
		expected = new String[] { "23", "23", "23", "23", "32" };
		assertTrue(Arrays.equals(expected, arg2));
		arg1 = new String[] { "12", "32", "21", "23", "34", "23", "12", "12", "12", "23" };
		arg2 = AnagramChecker.getLargestAnagramGroup(arg1);
		expected = new String[] { "12", "12", "12", "21", "12" };
		assertTrue(Arrays.equals(expected, arg2));
		// no anagram
		arg1 = new String[] { "133", "234", "436" };
		arg2 = AnagramChecker.getLargestAnagramGroup(arg1);
		expected = new String[] {};
		assertTrue(Arrays.equals(expected, arg2));
	}

	@Test
	void testgetLargestAnagramGroupFile() {
		String[] actual = AnagramChecker.getLargestAnagramGroup("src/assign04/list1.txt");
		String[] expected = new String[] { "traces", "recast", "Reacts", "crates", "caster", "Caters", "carets" };

		for (int i = 0; i < actual.length; i++) {
			assertEquals(actual[i], expected[i]);
		}
	}

	@Test
	void testgetLargestAnagramGroupFileArraySize() {
		String[] actual = AnagramChecker.getLargestAnagramGroup("src/assign04/small_list.txt");
		assertEquals(actual.length, 3);
	}

	@Test
	void testFileFailToRead() {
		String[] actual = AnagramChecker.getLargestAnagramGroup("");
		assertEquals(actual.length, 0);
	}

	@Test
	void testFileFailToRead2() {
		String[] actual = AnagramChecker.getLargestAnagramGroup("2345678");
		assertEquals(actual.length, 0);
	}

	@Test
	void testSmallFileForAnagram() {
		String[] actual = AnagramChecker.getLargestAnagramGroup("src/assign04/small_list.txt");
		String[] expected = new String[] { "recall", "cellar", "Caller" };
		for (int i = 0; i < actual.length; i++) {
			assertEquals(actual[i], expected[i]);
		}
	}

	// some break down tests

	// test null parameters
	@Test
	void sortStringTest() {
		assertThrows(NullPointerException.class, () -> {
			AnagramChecker.sort(null);
		});
	}

	@Test
	void InsertionSortNull() {
		stringComparator cmp = new stringComparator();
		assertThrows(NullPointerException.class, () -> {
			AnagramChecker.insertionSort(null, cmp);
		});
	}

	@Test
	void areAnagramNull() {
		assertThrows(NullPointerException.class, () -> {
			AnagramChecker.areAnagrams(null, null);
		});
	}
	
	@Test
	void areAnagramNull2() {
		assertThrows(NullPointerException.class, () -> {
			AnagramChecker.areAnagrams(null, "thing");
		});
	}
	
	@Test
	void areAnagramNull3() {
		assertThrows(NullPointerException.class, () -> {
			AnagramChecker.areAnagrams("thing", null);
		});
	}

	@Test
	void getLargestAnagramGroupNull() {
		assertThrows(NullPointerException.class, () -> {
			AnagramChecker.getLargestAnagramGroup(new String[] { null, null, null, null, null });
		});
	}

	@Test
	void getLargestAnagramGroupNullFile() {
		String arg1 = null;
		assertThrows(NullPointerException.class, () -> {
			AnagramChecker.getLargestAnagramGroup(arg1);
		});
	}

	@Test
	void InsertionSortAnotherType() {
		Students[] stu = new Students[2];
		stu[0] = new Students("Ling", 123);
		stu[1] = new Students("Drew", 124);
		StudentsComparator cmp = new StudentsComparator();
		AnagramChecker.insertionSort(stu, cmp);
		assertEquals("Ling", stu[0].getname());
		assertEquals("Drew", stu[1].getname());
		assertEquals(123, stu[0].getID());
		assertEquals(124, stu[1].getID());
		assertEquals(2, stu.length);
		stu[1] = new Students("Ling", 123);
		AnagramChecker.insertionSort(stu, cmp);
		assertEquals("Ling", stu[0].getname());
		assertEquals("Ling", stu[1].getname());
		assertEquals(123, stu[0].getID());
		assertEquals(123, stu[1].getID());
		assertEquals(2, stu.length);
		stu[1] = new Students("Ling", 123);
		stu[0] = new Students("Drew", 124);
		AnagramChecker.insertionSort(stu, cmp);
		assertEquals("Ling", stu[0].getname());
		assertEquals("Drew", stu[1].getname());
		assertEquals(123, stu[0].getID());
		assertEquals(124, stu[1].getID());
		assertEquals(2, stu.length);
	}

}
