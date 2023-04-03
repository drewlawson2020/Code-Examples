package assign03;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Comparator;
import java.util.Iterator;
import java.util.NoSuchElementException;

/**
 * @author Daniel Kopta, Ling Lei, Drew Lawson: Implements the Collection
 *         interface using an array as storage. The array must grow as needed.
 *         An ArrayCollection can not contain duplicates. All methods should be
 *         implemented as defined by the Java API, unless otherwise specified.
 * 
 *         It is your job to fill in the missing implementations and to comment
 *         this class. Every method should have comments (Javadoc-style
 *         prefered). The comments should at least indicate what the method
 *         does, what the arguments mean, and what the returned value is. It
 *         should specify any special cases that the method handles. Any code
 *         that is not self-explanatory should be commented.
 *
 * @param <T> - generic type placeholder
 */
public class ArrayCollection<T> implements Collection<T> {

	private T data[]; // Storage for the items in the collection
	private int size; // Keep track of how many items this collection holds

	// There is no clean way to convert between T and Object, so we suppress the
	// warning.
	@SuppressWarnings("unchecked")
	public ArrayCollection() {
		size = 0;
		// We can't instantiate an array of unknown type T, so we must create an Object
		// array, and typecast
		data = (T[]) new Object[10]; // Start with an initial capacity of 10
	}

	/**
	 * This is a helper method specific to ArrayCollection Doubles the size of the
	 * data storage array, retaining its current contents.
	 * 
	 * @author Ling Lei and Drew Lawson
	 */
	@SuppressWarnings("unchecked")
	private void grow() {
		// Doubles size
		T newData[] = (T[]) new Object[data.length * 2];
		// pass values
		for (int i = 0; i < data.length; i++)
			newData[i] = data[i];
		data = newData;
	}

	/**
	 * This method add new T into ArrayCollection return true when it adds return
	 * false when collection already contains pass in value
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @param arg0
	 * @return boolean
	 */
	public boolean add(T arg0) {
		// check if arg0 contains in array
		if (contains(arg0))
			return false;

		else {
			// create a new array then pass in new value
			if (size == data.length - 1)
				grow();
			// set value
			data[size] = arg0;
			// increase size
			size++;
			return true;
		}
	}

	/**
	 * This method iterate all entries in collection and add
	 * 
	 * only the items that don't exist in this collection
	 * 
	 * return true if all added
	 * 
	 * otherwise return false
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @param a collection of Objects
	 * @return boolean
	 */
	@SuppressWarnings("unchecked")
	public boolean addAll(Collection<? extends T> arg0) {
		boolean itemAdded = false;
		// Loop through each item in collection
		for (Object each : arg0) {
			if (add((T) each) == true)
				itemAdded = true;
		}
		return itemAdded;
	}

	/**
	 * This method clears all items from the collection
	 * 
	 * @author Ling Lei and Drew Lawson
	 */
	@SuppressWarnings("unchecked")
	public void clear() {
		// create a new collection
		data = (T[]) new Object[data.length];
		size = 0;
	}

	/**
	 * This method checks if parameter has existed in collection
	 * 
	 * return true when it does
	 * 
	 * return false if it is not
	 * 
	 * throws a cast error when casting errors happen
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @param arg0 - goal want to find
	 * @return boolean
	 */
	public boolean contains(Object arg0) throws ClassCastException {
		try {
			for (T each : data) {
				if (arg0.equals(each))
					return true;
				else
					continue;
			}
			return false;
		} catch (ClassCastException e) {
			throw new ClassCastException("Errors in casting class");
		}
	}

	/**
	 * This method check if the ArrayCollection contains all items in the input
	 * collection
	 * 
	 * return true if contains
	 * 
	 * return false otherwise
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @param arg0
	 * @return boolean
	 */
	public boolean containsAll(Collection<?> arg0) {
		// why do we use object here
		for (Object each : arg0) {
			if (!contains(each))
				return false;
		}
		return true;
	}

	/**
	 * This method checks if the collection is empty
	 * 
	 * return true if it is empty
	 * 
	 * return false it it is not empty
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @return boolean
	 */
	public boolean isEmpty() {
		return size == 0;
	}

	/**
	 * This method creates a iterator
	 * 
	 * @return a iterator
	 */
	public Iterator<T> iterator() {
		ArrayCollectionIterator ite = new ArrayCollectionIterator();
		return ite;
	}

	/**
	 * This method helps find the indeOf of input
	 * 
	 * return null if there is no corresponding index
	 * 
	 * @param a@author Ling Lei and Drew Lawsonrg0
	 * @return index
	 */
	@SuppressWarnings("unchecked")
	public Integer indexOf(Object arg0) {
		for (int i = 0; i < data.length; i++) {
			if (data[i] == (T) arg0)
				return i;
		}
		return null;
	}

	/**
	 * This method remove a specific object
	 * 
	 * return true if remove the specific object
	 * 
	 * return false if the object is not in this ArrayCollection
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @param arg0
	 * @return boolean
	 */
	@SuppressWarnings("unchecked")
	public boolean remove(Object arg0) {
		if (contains(arg0)) {
			int indexTarget = indexOf(arg0);
			// remove
			T newData[] = (T[]) new Object[data.length];

			for (int i = 0; i < indexTarget; i++) {
				newData[i] = data[i];
			}

			for (int i = indexTarget + 1; i < data.length; i++) {
				newData[i - 1] = data[i];
			}
			data = newData;
			size--;
			return true;
		} else
			return false;
	}

	/**
	 * This method removes all same contents with input collection from this
	 * ArrayCollection
	 * 
	 * return true if any item has been removed
	 * 
	 * return false otherwise
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @param arg0
	 * @return itemRemoved
	 */
	public boolean removeAll(Collection<?> arg0) {
		boolean itemRemoved = false;
		for (Object each : arg0) {
			if (remove(each) == true)
				itemRemoved = true;
		}
		return itemRemoved;
	}

	/**
	 * This method retains all same elements in both input collection
	 * 
	 * return true when any item has been removed
	 * 
	 * return false otherwise
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @param arg0
	 * @return boolean
	 */
	public boolean retainAll(Collection<?> arg0) {
		boolean itemRemoved = false;
		Iterator<T> ite = iterator();
		while (ite.hasNext()) {
			if (!arg0.contains(ite.next())) {
				ite.remove();
				itemRemoved = true;
			}
		}
		return itemRemoved;
	}

	/**
	 * This method returns current stored size
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @return size
	 */
	public int size() {
		return size;
	}

	/**
	 * This method returns length of data
	 * 
	 * @return length of data
	 */
	public int length() {
		return data.length;
	}

	/**
	 * This method returns a ArrayList of ArrayCollection
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @return a ArrayList form of ArrayCollection
	 */
	public Object[] toArray() {
		Object[] newData = new Object[this.size()];
		for (int i = 0; i < this.size(); i++) {
			newData[i] = data[i];
		}
		return newData;
	}

	/*
	 * Don't implement this method (unless you want to). It must be here to complete
	 * the Collection interface. We will not test this method.
	 */
	@SuppressWarnings("hiding")
	public <T> T[] toArray(T[] arg0) {
		return null;
	}

	/*

	 */
	/**
	 * Sorting method specific to ArrayCollection - not part of the Collection
	 * interface. Must implement a selection sort (see Assignment 2 for code). Must
	 * not modify this ArrayCollection.
	 * 
	 * @author Ling Lei and Drew Lawson
	 * @param cmp - the comparator that defines item ordering
	 * @return - the sorted list
	 */
	public ArrayList<T> toSortedList(Comparator<? super T> cmp) {
		ArrayList<T> sortedList = new ArrayList<T>();
		for (int i = 0; i < size; i++) {
			sortedList.add(data[i]);
		}

		int i, j, minIndex;

		for (i = 0; i < sortedList.size() - 1; i++) {
			minIndex = i;
			for (j = i + 1; j < sortedList.size(); j++)
				if (cmp.compare(sortedList.get(j), sortedList.get(minIndex)) < 0)
					minIndex = j;

			T temp = sortedList.get(minIndex);
			sortedList.set(minIndex, sortedList.get(i));
			sortedList.set(i, temp);
		}
		return sortedList;
	}

	/**
	 * This is private class which helps get access into ArrayCollection
	 * 
	 * @author Ling Lei and Drew Lawson
	 *
	 */
	private class ArrayCollectionIterator implements Iterator<T> {
		private int pointer;
		private boolean nextCalled;

		public ArrayCollectionIterator() {
			// ArrayCollection contains data as T[] which contains elements
			pointer = 0;
			nextCalled = false;
		}

		/**
		 * This method check if the ArrayCollection has item to iterate
		 * 
		 * return true if it has
		 * 
		 * return false when it has no item to run
		 * 
		 * @author Ling Lei and Drew Lawson
		 * @return boolean
		 */
		public boolean hasNext() {
			return pointer < size;
		}

		/**
		 * This method return next item in ArrayCollection
		 * 
		 * @author Ling Lei and Drew Lawson
		 * @return T value
		 */
		public T next() throws NoSuchElementException {
			if (!hasNext())
				throw new NoSuchElementException("No elements");
			else {
				pointer++;
				nextCalled = true;
				return data[pointer - 1];
			}
		}

		/**
		 * This method removes former value returned by next()
		 * 
		 * @author Ling Lei and Drew Lawson
		 */
		public void remove() {
			if (!nextCalled)
				throw new IllegalStateException("Did not call next()");
			ArrayCollection.this.remove(data[pointer - 1]);
			nextCalled = false;
			// keep track of pointer
			pointer--;
		}
	}
}
