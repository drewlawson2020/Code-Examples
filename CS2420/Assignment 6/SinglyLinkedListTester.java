package assign06;
/** 
 * Tests for SinglyLinkedList
 * 
 * @author Drew Lawson
 * @version October 20th, 2021
 *
 */
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNull;
import static org.junit.Assert.assertThrows;
import static org.junit.Assert.assertTrue;
import static org.junit.jupiter.api.Assertions.assertThrows;

import java.util.Arrays;
import java.util.Iterator;
import java.util.NoSuchElementException;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

class SinglyLinkedListTester {
		private SinglyLinkedList<Integer> test;
	
	@BeforeEach
	void setUp()
	{
		test = new SinglyLinkedList<Integer>();
	}
	@Test
	void testFirstInsertion()
	{
		test.insertFirst(1);
		String expected = "[1]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void testFirstInsertion2()
	{
		test.insertFirst(1);
		test.insertFirst(2);
		test.insertFirst(3);
		String expected = "[3, 2, 1]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	
	@Test
	void testInsertion()
	{

		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		String expected = "[1, 2, 3]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	
	@Test
	void testInsertion2()
	{
		test.insert(0, 2);
		test.insert(0, 1);
		test.insert(2, 3);
		String expected = "[1, 2, 3]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void testInsertion3()
	{
		test.insert(0, 34);
		test.insert(1, 53);
		test.insert(2, 104);
		test.insert(3, 436);
		test.insert(4, 621);
		String expected = "[34, 53, 104, 436, 621]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void testGetFirst()
	{
		test.insert(0, 66);
		test.insert(1, 34);
		test.insert(2, 63);
		test.insert(3, 345);
		test.insert(4, 243);
		int expected = 66;
		int actual = test.getFirst();
		assertEquals(expected, actual);
	}
	@Test
	void testGetAtIndex()
	{
		test.insert(0, 63214);
		test.insert(1, 634);
		test.insert(2, 56);
		test.insert(3, 346);
		test.insert(4, 763);
		int expected = 56;
		int actual = test.get(2);
		assertEquals(expected, actual);
	}
	@Test
	void testGetAtIndex2()
	{
		test.insert(0, 63214);
		test.insert(1, 634);
		test.insert(2, 56);
		test.insert(3, 346);
		test.insert(4, 763);
		int expected = 56;
		int actual = test.get(2);
		assertEquals(expected, actual);
	}
	@Test
	void testDeleteFirst()
	{
		test.insertFirst(63214);
		int actual1 = test.deleteFirst();
		assertEquals(63214, actual1);
		assertEquals(0, test.size());
		Assertions.assertThrows(NoSuchElementException.class, () -> {test.getFirst();});
		Assertions.assertThrows(NullPointerException.class, () -> {test.get(0);});
	}
	@Test
	void testDeleteFirst2()
	{
		test.insertFirst(23457);
		int actual = test.deleteFirst();
		assertEquals(23457, actual);
		assertEquals(0, test.size());
		Assertions.assertThrows(NoSuchElementException.class, () -> {test.getFirst();});
		Assertions.assertThrows(NullPointerException.class, () -> {test.get(0);});
	}
	@Test
	void testDeleteFirst3()
	{
		test.insert(0, 5342);
		test.insert(1, 6733);
		test.insert(2, 56);
		test.insert(3, 346);
		test.insert(4, 763);
		int actual1 = test.deleteFirst();
		assertEquals(5342, actual1);
		assertEquals(4, test.size());
		String expected = "[6733, 56, 346, 763]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void testDeleteAtIndex()
	{
		test.insert(0, 45);
		test.insert(1, 4667);
		test.insert(2, 574);
		test.insert(3, 4643);
		test.insert(4, 5456);
		int actual1 = test.delete(3);
		assertEquals(4643, actual1);
		assertEquals(4, test.size());
		String expected = "[45, 4667, 574, 5456]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void testDeleteAtIndex2()
	{
		test.insert(0, 45);
		test.insert(1, 4667);
		test.insert(2, 574);
		test.insert(3, 4643);
		test.insert(4, 5456);
		int actual1 = test.delete(1);
		int actual2 = test.delete(1);
		assertEquals(4667, actual1);
		assertEquals(574, actual2);
		assertEquals(3, test.size());
		String expected = "[45, 4643, 5456]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void testDeleteAtIndex3()
	{
		test.insert(0, 22);
		test.insert(1, 11);
		test.insert(2, 55);
		test.insert(3, 66);
		test.insert(4, 577);
		int actual1 = test.delete(4);
		assertEquals(577, actual1);
		assertEquals(4, test.size());
		String expected = "[22, 11, 55, 66]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void testDeleteAndInsertAtIndex()
	{
		test.insert(0, 22);
		test.insert(1, 11);
		test.insert(2, 55);
		test.insert(3, 66);
		test.insert(4, 577);
		int actual1 = test.delete(4);
		assertEquals(577, actual1);
		assertEquals(4, test.size());
		test.insert(4, 583);
		assertEquals(5, test.size());
		String expected = "[22, 11, 55, 66, 583]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void testClear()
	{
		test.insert(0, 22);
		test.insert(1, 11);
		test.insert(2, 55);
		test.insert(3, 66);
		test.insert(4, 577);
		test.insert(4, 634);
		test.clear();
		assertNull(test.get(0));
		assertTrue(test.isEmpty());
	}
	@Test
	void testIndexOf()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		test.insert(3, 4);
		test.insert(4, 5);
		test.insert(5, 6);
		int actual = test.indexOf(4);
		assertEquals(3, actual);
	}
	@Test
	void testIndexOf2()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		test.insert(3, 4);
		test.insert(4, 5);
		test.insert(5, 6);
		test.delete(0);
		int actual = test.indexOf(2);
		assertEquals(0, actual);
	}
	@Test
	void testGetException()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Assertions.assertThrows(IndexOutOfBoundsException.class, () -> {test.get(-1);});
		Assertions.assertThrows(IndexOutOfBoundsException.class, () -> {test.get(4);});
	}
	@Test
	void testInsertException()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Assertions.assertThrows(IndexOutOfBoundsException.class, () -> {test.insert(-1, 0);});
		Assertions.assertThrows(IndexOutOfBoundsException.class, () -> {test.insert(4, 0);});
	}
	@Test
	void testIndexOfNotFound()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		int actual = test.indexOf(4);
		assertEquals(-1, actual);
	}
	@Test
	void testDeleteFirstException()
	{
		Assertions.assertThrows(NoSuchElementException.class, () -> {test.deleteFirst();});
	}
	@Test
	void testDeleteException()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Assertions.assertThrows(IndexOutOfBoundsException.class, () -> {test.delete(-1);});
		Assertions.assertThrows(IndexOutOfBoundsException.class, () -> {test.delete(4);});
	}
	@Test
	void IteratorNextTest()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Iterator<Integer> Iterator = test.iterator();
		int actual = Iterator.next();
		assertEquals(1, actual);
		actual = Iterator.next();
		assertEquals(2, actual);
		actual = Iterator.next();
		assertEquals(3, actual);
	}
	@Test
	void IteratorHasNextTest()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Iterator<Integer> Iterator = test.iterator();
		int actual = Iterator.next();
		assertEquals(1, actual);
		assertTrue(Iterator.hasNext());
		actual = Iterator.next();
		assertEquals(2, actual);
		assertTrue(Iterator.hasNext());
		actual = Iterator.next();
		assertEquals(3, actual);
		assertFalse(Iterator.hasNext());
	}
	@Test
	void IteratorRemoveTest()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Iterator<Integer> Iterator = test.iterator();
		Iterator.next();
		Iterator.remove();
		String expected = "[2, 3]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void IteratorRemoveTest2()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Iterator<Integer> Iterator = test.iterator();
		Iterator.next();
		Iterator.next();
		Iterator.remove();
		String expected = "[1, 3]";
		Object[] arr = test.toArray();
		assertEquals(expected, Arrays.toString(arr));
	}
	@Test
	void HasNextExceptionTest()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Iterator<Integer> Iterator = test.iterator();
		Iterator.next();
		Iterator.next();
		Iterator.next();
		Assertions.assertThrows(NoSuchElementException.class, () -> {Iterator.next();});
	}
	@Test
	void RemoveExceptionTest()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Iterator<Integer> Iterator = test.iterator();
		Iterator.next();
		Iterator.remove();
		Assertions.assertThrows(IllegalStateException.class, () -> {Iterator.remove();});
	}
	@Test
	void RemoveExceptionTest2()
	{
		test.insert(0, 1);
		test.insert(1, 2);
		test.insert(2, 3);
		Iterator<Integer> Iterator = test.iterator();
		Iterator.next();
		Iterator.next();
		Iterator.next();
		Iterator.remove();
		Assertions.assertThrows(IllegalStateException.class, () -> {Iterator.remove();});
	}
	
	
	
	
}
