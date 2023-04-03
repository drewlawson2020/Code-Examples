package assign06;

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
/** 
 * Tests for LinkedListStack
 * 
 * @author Drew Lawson
 * @version October 20th, 2021
 *
 */
class LinkedListStackTest {
		private LinkedListStack<Integer> test;
	
	@BeforeEach
	void setUp()
	{
		test = new LinkedListStack<Integer>();
	}
	@Test
	void testPop()
	{
		test.push(1);
		test.push(5);
		test.push(8);
		int actual = test.pop();
		assertEquals(8, actual);
	}
	@Test
	void testPop2()
	{
		test.push(1);
		test.push(5);
		test.push(8);
		int actual = test.pop();
		assertEquals(8, actual);
		actual = test.pop();
		assertEquals(5, actual);
	}
	@Test
	void testPush()
	{
		test.push(3);
		test.push(2);
		test.push(1);
		test.push(0);
		int actual;
		for (int i = 0; i < test.size(); i++)
		{
			actual = test.pop();
			assertEquals(i, actual);
		}
	}
	@Test
	void TestIfEmpty()
	{
		assertTrue(test.isEmpty());
		test.push(3);
		test.pop();
		assertTrue(test.isEmpty());
		test.push(3);
		test.push(3);
		test.push(3);
		test.pop();
		test.pop();
		test.pop();
		assertTrue(test.isEmpty());
		test.push(3);
		test.clear();
		assertTrue(test.isEmpty());
	}
	@Test
	void TestPeek()
	{
		test.push(1);
		int actual = test.peek();
		assertEquals(1, actual);
	}
	@Test
	void testThrowsPeek()
	{
		Assertions.assertThrows(NoSuchElementException.class, () -> {test.peek();});
	}
	@Test
	void testThrowsPop()
	{
		Assertions.assertThrows(NoSuchElementException.class, () -> {test.pop();});
	}
	
	
	
	
	
	
}
