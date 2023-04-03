package assign10;

import java.util.Comparator;
import java.util.List;
import java.util.NoSuchElementException;

/**
 * This is BinaryMaxHeap that implements the PriorityQueue interface
 * 
 * @author linglei adn Drew Lawson
 *
 * @param <E>
 */
public class BinaryMaxHeap<E> implements PriorityQueue<E> {
	// basic array
	private Object array[];
	private int size;
	Comparator<? super E> comparator;

	/**
	 * If this constructor is used to create an empty binary heap, it is assumed
	 * 
	 * that the elements are ordered using their natural ordering (i.e., E
	 * 
	 * implements Comparable<? super E>).
	 */
	@SuppressWarnings("unchecked")
	public BinaryMaxHeap() {
		size = 0;
		array = (E[]) new Object[10];
		comparator = null;
	}

	/**
	 * If this constructor is used to create an empty binary heap, it is assumed
	 * 
	 * that the elements are ordered using the provided Comparator object.
	 * 
	 * @param cmp
	 */
	@SuppressWarnings("unchecked")
	public BinaryMaxHeap(Comparator<? super E> cmp) {
		size = 0;
		array = (E[]) new Object[10];
		comparator = cmp;
	}

	/**
	 * If this constructor is used, then the binary heap is constructed from the
	 * given list.
	 * 
	 * Also, it is assumed that the elements are ordered using their natural
	 * ordering
	 * 
	 * (i.e., E implements Comparable<? super E>). RECALL: Using the buildHeap
	 * operation,
	 * 
	 * we can construct a binary heap from these N items in O(N) time,
	 * 
	 * which is more efficient than adding them to the binary heap one at a time.
	 * 
	 * This constructor must use such an operation.
	 * 
	 * @param list
	 */
	@SuppressWarnings("unchecked")
	public BinaryMaxHeap(List<? extends E> list) {
		array = new Object[list.size() + 1];
		size = 0;
		int i = 0;
		for (E item : list) {
			array[i] = item;
			i++;
			size++;
		}
		comparator = null;
		buildHeap((E[]) array, size);
	}

	/**
	 * This is helper method for building a heap
	 * 
	 * @param arr
	 * @param n
	 */
	void buildHeap(E arr[], int n) {

		int startIdx = (n / 2) - 1; // Index of last non-leaf node
		for (int i = startIdx; i >= 0; i--) {
			heapify(arr, n, i);
		}
	}

	/**
	 * This is helper method for building a heap
	 * 
	 * @param array
	 * @param size
	 * @param i
	 */
	void heapify(E array[], int size, int i) {

		int curr = i;
		int left = leftChildIndex(i);
		int right = rightChildIndex(i);

		if (left < size && innerCompare(array[left], (array[curr])) > 0) {
			curr = left;
		}

		if (right < size && innerCompare(array[right], (array[curr])) > 0) {
			curr = right;
		}

		if (curr != i) {
			E temp = array[i];
			array[i] = array[curr];
			array[curr] = temp;

			heapify(array, size, curr);
		}
	}

	/**
	 * If this constructor is used, then the binary heap is constructed from the
	 * given list
	 * 
	 * (see RECALL note above).
	 * 
	 * Also, it is assumed that the elements are ordered using the provided
	 * Comparator object.
	 * 
	 * @param list
	 * @param cmp
	 */
	@SuppressWarnings("unchecked")
	public BinaryMaxHeap(List<? extends E> list, Comparator<? super E> cmp) {
		array = new Object[list.size() + 1];
		size = 0;
		comparator = cmp;
		int i = 0;
		for (E item : list) {
			array[i] = item;
			i++;
			size++;
		}
		comparator = cmp;
		buildHeap((E[]) array, size);
	}

	/**
	 * Adds the given item to this priority queue. O(1) in the average case, O(log
	 * N) in the worst case
	 * 
	 * @param item
	 */
	@SuppressWarnings("unchecked")
	@Override
	public void add(E item) {
		if (size == array.length) {
			E[] newArry = (E[]) new Object[size * 2];
			for (int i = 0; i < size; i++) {
				newArry[i] = (E) array[i];
			}
			array = newArry;
		}

		// add to the array last place
		array[size] = item;
		size++;
		percolateUp(size - 1);
	}

	/**
	 * Returns, but does not remove, the maximum item this priority queue. O(1)
	 * 
	 * @return the maximum item
	 * @throws NoSuchElementException if this priority queue is empty
	 */
	@SuppressWarnings("unchecked")
	@Override
	public E peek() throws NoSuchElementException {
		if (array[0] == null)
			throw new NoSuchElementException("There is no new element");
		return (E) array[0];
	}

	/**
	 * Returns and removes the maximum item this priority queue. O(log N)
	 * 
	 * @return the maximum item
	 * @throws NoSuchElementException if this priority queue is empty
	 */
	@SuppressWarnings("unchecked")
	@Override
	public E extractMax() throws NoSuchElementException {
		if (array[0] == null)
			throw new NoSuchElementException("There is no new element");
		// remove
		E temp = (E) array[0];
		array[0] = array[size - 1];
		array[size - 1] = null;
		size--;
		percolateDown(0);
		return temp;
	}

	/**
	 * This returns current size
	 *
	 */
	@Override
	public int size() {
		return size;
	}

	/**
	 * This returns if the array is empty
	 *
	 */
	@Override
	public boolean isEmpty() {
		return size == 0;
	}

	/**
	 * This clears an array
	 *
	 */
	@Override
	public void clear() {
		Object[] newArray = new Object[size];
		array = newArray;
		size = 0;
	}

	/**
	 * Creates and returns an array of the items in this priority queue, in the same
	 * order they appear in the backing array. O(N)
	 * 
	 * (NOTE: This method is needed for grading purposes. The root item must be
	 * stored at index 0 in the returned array, regardless of whether it is in
	 * stored there in the backing array.)
	 */
	@Override
	public Object[] toArray() {
		// create
		Object[] object = new Object[size];
		for (int i = 0; i < size; i++) {
			object[i] = array[i];
		}
		return object;
	}

	/**
	 * This method returns the left child index
	 * 
	 * @param i
	 * @return
	 */
	private int leftChildIndex(int i) {
		return (2 * i) + 1;
	}

	/**
	 * This method returns the right child index
	 * 
	 * @param i
	 * @return
	 */
	private int rightChildIndex(int i) {
		return (2 * i) + 2;
	}

	/**
	 * This method returns the parent index
	 * 
	 * @param i
	 * @return
	 */
	private int parentIndex(int i) {
		return (i - 1) / 2;
	}

	/**
	 * This compares two objects with or without comparator
	 * 
	 * @param arg1
	 * @param arg2
	 * @return
	 */
	@SuppressWarnings("unchecked")
	private int innerCompare(E arg1, E arg2) {
		if (arg1 == null || arg2 == null) {
			return 0;
		}
		if (comparator == null) {
			return ((Comparable<? super E>) arg1).compareTo(arg2);
		}
		return comparator.compare(arg1, arg2);
	}

	/**
	 * percolateUp
	 * 
	 * @param i
	 */
	@SuppressWarnings("unchecked")
	private void percolateUp(int i) {
		int parent = parentIndex(i);
		if (parent < 0)
			return;

		if (innerCompare((E) array[parent], (E) array[i]) >= 0)
			return;
		else {
			// swap
			E temp = (E) array[parent];
			array[parent] = array[i];
			array[i] = (E) temp;
			// recursive
			percolateUp(parent);
		}

	}

	/**
	 * percolateDown
	 * 
	 * @param i
	 */
	@SuppressWarnings("unchecked")
	private void percolateDown(int i) {
		if (array[i] == null)
			return;
		int maxChild = i;
		if (leftChildIndex(i) < size) {
			if (array[leftChildIndex(i)] != null)
				maxChild = leftChildIndex(i);
		}

		if (rightChildIndex(i) < size) {
			if (array[rightChildIndex(i)] != null) {
				if (innerCompare((E) array[leftChildIndex(i)], (E) array[rightChildIndex(i)]) < 0)
					maxChild = rightChildIndex(i);
			}
		}

		if (innerCompare((E) array[maxChild], (E) array[i]) <= 0)
			return;
		else {
			// swap
			E temp = (E) array[maxChild];
			array[maxChild] = array[i];
			array[i] = (E) temp;
			// recursive
			percolateDown(maxChild);
		}
	}

}
