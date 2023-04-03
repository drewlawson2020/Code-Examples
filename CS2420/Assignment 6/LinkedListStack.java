package assign06;

import java.util.NoSuchElementException;
/** 
 * The stack data structure with SinglyLinkedList as a backing.
 * 
 * @author Drew Lawson
 * @version October 20th, 2021
 *
 * @param <E> - the type of elements contained in the stack
 */
public class LinkedListStack<E> implements Stack<E>{
	
	private SinglyLinkedList<E> stack;
	public LinkedListStack()
	{
		stack = new SinglyLinkedList<E>();
	}
	@Override
	public void clear() {
		stack.clear();
	}

	@Override
	public boolean isEmpty() {
		return stack.isEmpty();
	}

	@Override
	public E peek() throws NoSuchElementException {
		return stack.getFirst();
	}

	@Override
	public E pop() throws NoSuchElementException {
		return stack.deleteFirst();
	}

	@Override
	public void push(E element) {
		stack.insertFirst(element);
		
	}

	@Override
	public int size() {
		return stack.size();
	}

}
