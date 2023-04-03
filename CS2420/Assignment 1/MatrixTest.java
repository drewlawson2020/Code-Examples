package assignment1;
import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.Test;

/**
 * 
 * @author CS 2420 staff and Drew Lawson
 * @version August 18, 2021
 *
 */
class MatrixTest {

	/* ******equals tests****** */
	@Test
	void testModerateMatricesEqual() {
		Matrix m1 = new Matrix(new int[][] {
			{1, 2, 3},
			{4, 5, 6}
		});

		Matrix m2 = new Matrix(new int[][] {
			{1, 2, 3},
			{4, 5, 6}
		});

		assertTrue(m1.equals(m2));
	}
	
	@Test
	void testSimilarMatricesEqual() {
		Matrix m1 = new Matrix(new int[][] {
			{1, 2, 3},
			{6, 2, 4}
		});

		Matrix m2 = new Matrix(new int[][] {
			{1, 2, 3},
			{4, 5, 6}
		});

		assertFalse(m1.equals(m2));
	}
	@Test
	void testDifferentSizeMatricesEqual() {
		Matrix m1 = new Matrix(new int[][] {
			{1, 2, 3},
			{6, 2, 4}
		});

		Matrix m2 = new Matrix(new int[][] {
			{1, 2},
			{4, 6}
		});

		assertFalse(m1.equals(m2));
	}
	@Test
	void testTallMatricesEqual() {
		Matrix m1 = new Matrix(new int[][] {
			{1},
			{6},
			{3}
		});

		Matrix m2 = new Matrix(new int[][] {
			{1},
			{6},
			{3}
		});
		assertTrue(m1.equals(m2));
	}
	@Test
	void testLongMatricesEqual() {
		Matrix m1 = new Matrix(new int[][] {
			{1, 2, 3}
		});

		Matrix m2 = new Matrix(new int[][] {
			{1, 2, 3}
		});
		assertTrue(m1.equals(m2));
	}
	/* ******end equals tests****** */


	/* ******toString tests****** */
	@Test
	void testModerateToString() {
		Matrix m = new Matrix(new int[][] {
			{1, 2},
			{3, 4}
		});

		assertEquals("1 2\n3 4\n", m.toString());
	}
	
	@Test
	void testLargerToString() {
		Matrix m = new Matrix(new int[][] {
			{4, 5, 4},
			{3, 2, 5},
			{1, 1, 6}
			});

		assertEquals("4 5 4\n3 2 5\n1 1 6\n", m.toString());
	}
	@Test
	void testTinyToString() {
		Matrix m = new Matrix(new int[][] {
			{0}});

		assertEquals("0\n", m.toString());
	}
	
	/* ******end toString tests****** */



	/* ******times tests****** */
	@Test
	void testCompatibleTimes() {
		Matrix m1 = new Matrix(new int[][]
				{{1, 2, 3},
				{2, 5, 6}});

		Matrix m2 = new Matrix(new int[][]
				{{4, 5},
				{3, 2},
				{1, 1}});

		// the known correct result of multiplying m1 by m2
		int[][] expected = new int[][]
				{{13, 12},
				{29, 26}};
		
		// the result produced by the times method
		Matrix mulResult = m1.times(m2);
		int[][] resultArray = mulResult.getData();
		
		
		// compare the raw arrays
		assertEquals(expected.length, resultArray.length);
		for(int i = 0; i < expected.length; i++)
			assertArrayEquals(expected[i], resultArray[i]);
	}
	
	@Test
	void testLargerCompatibleTimes() {
		Matrix m1 = new Matrix(new int[][]
				{{1, 5, 4},
				{7, 9, 2},
				{10,4,2}});

		Matrix m2 = new Matrix(new int[][]
				{{2, 4, 1, 5, 5},
				{6, 3, 2, 5, 1},
				{7, 6, 9, 8, 4}});

		// the known correct result of multiplying m1 by m2
		int[][] expected = new int[][]
				{{60, 43, 47, 62, 26},
				{82, 67, 43, 96, 52},
				{58, 64, 36, 86, 62}};
		
		// the result produced by the times method
		Matrix mulResult = m1.times(m2);
		int[][] resultArray = mulResult.getData();
		
		
		// compare the raw arrays
		assertEquals(expected.length, resultArray.length);
		for(int i = 0; i < expected.length; i++)
			assertArrayEquals(expected[i], resultArray[i]);
	}

	@Test
	public void testIncompatibleMultiplication() {	
		Matrix m1 = new Matrix(new int[][] {
			{1, 1, 1},
			{1, 1, 1}
		});

		Matrix m2 = new Matrix(new int[][] {
			{2, 2},
			{2, 2}
		});
		assertThrows(IllegalArgumentException.class, () -> { m1.times(m2); });  
	}
	
	/* ******end times tests****** */
	

	/* ******plus tests****** */
	@Test
	public void testIncompatiblePlus() {	
		Matrix m1 = new Matrix(new int[][] {
			{1, 1, 1},
			{1, 1, 1}
		});

		Matrix m2 = new Matrix(new int[][] {
			{2, 2},
			{2, 2}
		});
		assertThrows(IllegalArgumentException.class, () -> { m1.plus(m2); });  
	}

	@Test
	public void testCompatiblePlus() {
		int[][] expected = new int[][]
				{{2, 2, 2},
				{2, 2, 2}};
		Matrix m1 = new Matrix(new int[][] {
			{1, 1, 1},
			{1, 1, 1}
		});

		Matrix m2 = new Matrix(new int[][] {
			{1, 1, 1},
			{1, 1, 1}
		});
		Matrix addResult = m1.plus(m2);
		int[][] resultArray = addResult.getData();
			
		// compare the raw arrays
		assertEquals(expected.length, resultArray.length);
		for(int i = 0; i < expected.length; i++)
			assertArrayEquals(expected[i], resultArray[i]); 
	}
	
	@Test
	public void testCompatiblePlusTwo() {
		int[][] expected = new int[][]
				{{0, 0, 0},
				{0, 0, 0}};
		Matrix m1 = new Matrix(new int[][] {
			{1, 1, 1},
			{1, 1, 1}
		});

		Matrix m2 = new Matrix(new int[][] {
			{-1, -1, -1},
			{-1, -1, -1}
		});
		Matrix addResult = m1.plus(m2);
		int[][] resultArray = addResult.getData();
			
		// compare the raw arrays
		assertEquals(expected.length, resultArray.length);
		for(int i = 0; i < expected.length; i++)
			assertArrayEquals(expected[i], resultArray[i]); 
	}
	
	/* ******end plus tests****** */
	
	
	
	/* ******transpose tests****** */
	@Test
	public void testSmallTranspose() {
		Matrix m1 = new Matrix(new int[][] {
			{1, 1, 1},
			{1, 1, 1}
		});
		
		int expected[][] = {
				{1, 1},
				{1, 1},
				{1, 1}
		};
		
		Matrix t = m1.transpose();
		int resultArray[][] = t.getData();
		
		assertEquals(expected.length, resultArray.length);
		for(int i = 0; i < expected.length; i++)
			assertArrayEquals(expected[i], resultArray[i]);
	}
	
	@Test
	public void testLargeTranspose() {
		Matrix m1 = new Matrix(new int[][] {
			{1, 1, 1},
			{1, 1, 1},
			{1, 1, 1},
			{1, 1, 1},
			{1, 1, 1},
			{1, 1, 1},
		});
		
		int expected[][] = {
				{1, 1, 1, 1, 1, 1},
				{1, 1, 1, 1, 1, 1},
				{1, 1, 1, 1, 1, 1}
		};
		
		Matrix t = m1.transpose();
		int resultArray[][] = t.getData();
		
		assertEquals(expected.length, resultArray.length);
		for(int i = 0; i < expected.length; i++)
			assertArrayEquals(expected[i], resultArray[i]);
	}
	
	@Test
	public void testVariousIntsTranspose() {
		Matrix m1 = new Matrix(new int[][] {
			{2, 1, 1},
			{1, 3, 1},
			{1, 1, 4},
			{1, 5, 1},
			{6, 1, 1},
			{1, 7, 1},
		});
		
		int expected[][] = {
				{2, 1, 1, 1, 6, 1},
				{1, 3, 1, 5, 1, 7},
				{1, 1, 4, 1, 1, 1}
		};
		
		Matrix t = m1.transpose();
		int resultArray[][] = t.getData();
		
		assertEquals(expected.length, resultArray.length);
		for(int i = 0; i < expected.length; i++)
			assertArrayEquals(expected[i], resultArray[i]);
	}
	/* ******end transpose tests****** */


}
