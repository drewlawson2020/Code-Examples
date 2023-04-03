package assignment1;

/**
 * This class represents a 2D matrix and simple operations on them.
 * 
 * @author Daniel Kopta and Drew Lawson
 * @version September 02, 2021
 *
 */

public class Matrix {
	
	// the dimensions of the matrix
	private int numRows;
	private int numColumns;
	
	// the internal storage for the matrix elements 
	private int data[][];
	
	/**
	 * DO NOT MODIFY THIS METHOD.
	 * Constructor for a new Matrix. Automatically determines dimensions.
	 * This is implemented for you.
	 * @param d - the raw 2D array containing the initial values for the Matrix.
	 */
	public Matrix(int d[][])
	{
		// d.length is the number of 1D arrays in the 2D array
		numRows = d.length; 
		if(numRows == 0)
			numColumns = 0;
		else
			numColumns = d[0].length; // d[0] is the first 1D array
		
		// create a new matrix to hold the data
		data = new int[numRows][numColumns]; 
		
		// copy the data over
		for(int i=0; i < numRows; i++) 
			for(int j=0; j < numColumns; j++)
				data[i][j] = d[i][j];
	}
	
	

	/**
	 * Determines whether this Matrix is equal to another object.
	 * @param o - the other object to compare to, which may not be a Matrix
	 */
	@Override // instruct the compiler that we intend for this method to override the superclass' (Object) version
	public boolean equals(Object o)
	{
		// make sure the Object we're comparing to is a Matrix
		if(!(o instanceof Matrix)) 
			return false;
		
		// if the above was not true, we know it's safe to treat 'o' as a Matrix
		Matrix m = (Matrix)o;
		// Loops through array and checks if it's all equal.
		if (this.numColumns == m.numColumns && this.numRows == m.numRows)
		{
		for (int i = 0; i < this.numRows; i++)
			for (int j = 0; j < this.numColumns; j++)
				if (this.data[i][j] != m.data[i][j])
					return false;
		
		return true;
		}
			
		return false; 
	}
	
	/**
	 * Returns a String representation of this Matrix. 
	 */
	@Override // instruct the compiler that we intend for this method to override the superclass' (Object) version
	public String toString()	
	{
		//Loops through array and generates a string representation
		String array = "";
		for (int i = 0; i < this.numRows; i++)
		{
			for (int j = 0; j < this.numColumns; j++)
			{
				array += this.data[i][j];
				//Only adds a space between ints if it's not at the end of the row.
				if (j != this.numColumns - 1)
				{
					array += " ";
				}
			}
			array += "\n";
		} 
		return array; 
	}
	
	/**
	 * Returns a new Matrix that is the result of multiplying this Matrix as the left hand side
	 * by the input matrix as the right hand side. Loops through the array, multiplying by each row/column.
	 * 
	 * @param m - the Matrix on the right hand side of the multiplication
	 * @return - the result of the multiplication
	 * @throws IllegalArgumentException - if the input Matrix does not have compatible dimensions
	 * for multiplication
	 */
	public Matrix times(Matrix m) throws IllegalArgumentException
	{
		//Checks if columns of this are equal to m's rows. If now, throws an exception.
		if (this.numColumns == m.numRows)
		{
		//Loops through array and multiplies each row/column up from each respective Matrix, then returns the product result Matrix.
		Matrix product = new Matrix(new int[this.numRows][m.numColumns]);
		for (int i = 0; i < this.numRows; i++)
			for (int j = 0; j < m.numColumns; j++)
				for (int k = 0; k < this.numColumns; k++)
					product.data[i][j] += this.data[i][k] * m.data[k][j];
		
		return product;
		}
		else
		{
			throw new IllegalArgumentException("Columns in first Matrix and rows in second Matrix are not equal.");
		}
		
	}
	
	/**
	 * Returns a new Matrix that is the result of adding this Matrix as the left hand side
	 * by the input matrix as the right hand side. Checks if the columns and rows of each
	 * Matrix are equal, then using a loop through, it adds each array together.
	 * 
	 * @param m - the Matrix on the right hand side of the addition
	 * @return - the result of the addition
	 * @throws IllegalArgumentException - if the input Matrix does not have compatible dimensions
	 * for addition
	 */
	public Matrix plus(Matrix m) throws IllegalArgumentException
	{
		if (this.numColumns == m.numColumns && this.numRows == m.numRows)
		{
		for (int i = 0; i < this.numRows; i++)
			for (int j = 0; j < m.numColumns; j++)
				this.data[i][j] += m.data[i][j];
		return this;
		}
		else
		{
			throw new IllegalArgumentException("Columns/rows in first Matrix and/or columns/rows in second Matrix are not equal.");
		}
	}
	
	
	/**
	 * Produces the transpose of this matrix by reversing the x and y coordinates with a new matrix and transferring
	 * the data over to correspond with it being transposed.
	 * @return - the transpose
	 */
	public Matrix transpose()
	{
		Matrix transposed = new Matrix(new int[this.numColumns][this.numRows]);
		for (int i = 0; i < this.numRows; i++)
			for (int j = 0; j < this.numColumns; j++)
				transposed.data[j][i] = this.data[i][j];
							
		return transposed;
	}
	
	
	/**
	 * DO NOT MODIFY THIS METHOD.
	 * Produces a copy of the internal 2D array representing this matrix.
	 * This method is for grading and testing purposes.
	 * @return - the copy of the data
	 */
	public int[][] getData()
	{
		int[][] retVal = new int[data.length][];
		for(int i = 0; i < data.length; i++)
			retVal[i] = data[i].clone();
		return retVal;
	}
}
