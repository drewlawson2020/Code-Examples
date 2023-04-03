package assign06;

import java.util.Iterator;
import java.util.NoSuchElementException;
/** 
 * SinglyLinkedList implementing methods from the provided List code.
 * 
 * @author Drew Lawson
 * @version October 20th, 2021
 * 
 * @param <E> - the type of elements contained in the stack
 */
public class SinglyLinkedList<E> implements List<E>{
	
	private Node head;
	private int size;
	
	public SinglyLinkedList()
	{
		
		this.head = null;
		this.size = 0;
	}
	
	private class Node {
		private E data;
		private Node next;
		Node(E data, Node next)
		{
			this.data = data;
			this.next = next;
		}
	}
	
	
	
	
	@Override
	public void insertFirst(E element) {
		Node temp = new Node(element, head);
			head = temp;
			size++;
	}

	@Override
	public void insert(int index, E element) throws IndexOutOfBoundsException {
		if (index < 0 || index > size)
			throw new IndexOutOfBoundsException();
		if (index == 0)
		{
			insertFirst(element);
		}
		else
		{
			
			Node temp = new Node(element, null);

			Node currNode = head;
			while (--index > 0)
			{
				currNode = currNode.next;
			}
			
			temp.next = currNode.next;
			currNode.next = temp;
			size++;
		}
		
	}

	@Override
	public E getFirst() throws NoSuchElementException {
		if (head == null || head.data == null)
			throw new NoSuchElementException();
		return head.data;
	}

	@Override
	public E get(int index) throws IndexOutOfBoundsException {
		if (index < 0 || index > size)
			throw new IndexOutOfBoundsException();
		Node currNode = head;
		int i = 0;
		while(i < index)
		{
			currNode = currNode.next;
			i++;
		}
		return currNode.data;
	}

	@Override
	public E deleteFirst() throws NoSuchElementException {
		if (head == null || head.data == null)
			throw new NoSuchElementException();
		Node temp = head;
		head = head.next;
		size--;
		return temp.data;
	}

	@Override
	public E delete(int index) throws IndexOutOfBoundsException {
		if (index < 0 || index > size)
			throw new IndexOutOfBoundsException();
		Node prevNode = null;
		Node currNode = head;
		int i = 0;
		
		while(i < index)
		{
			prevNode = currNode;
			currNode = currNode.next;
			i++;
		}
		if (prevNode == null)
		{
			head = head.next;
		}
		else
		{
			prevNode.next = currNode.next;
		}
		size--;
		return currNode.data;
				
	}

	@Override
	public int indexOf(E element) {
		Node temp = head;
		for (int i = 0; i < size; i++)
		{
			if (temp.data.equals(element))
			{
				return i;
			}
			if (temp.next != null)
			{
				temp = temp.next;
			}
		}
		return -1;
	}

	@Override
	public int size() {
		return size;
	}

	@Override
	public boolean isEmpty() {
		return size == 0;
	}

	@Override
	public void clear() {
		head.data = null;
		head.next = null;
		size = 0;		
	}

	@Override
	public Object[] toArray() {
		Object[] array = new Object[size];
		Node temp = head;
		int i = 0;
		while(i < size)
		{
			array[i] = temp.data;
			temp = temp.next;
			i++;
		}
		return array;
	}

	@Override
	public Iterator<E> iterator() {
		return new Iterator<E>() {
		Node first = head;
		Node position;
		Node previous;
		boolean nextCalled = false;
		
		@Override
		public boolean hasNext() {
			if (position == null)
				return first != null;
			else
				return position.next != null;
		}
		
		@Override
		public E next() {
			if (!hasNext())
				throw new NoSuchElementException();
			previous = position;
			
			if (position == null)
				position = first;
			else
				position = position.next;
			
			nextCalled = true;
			return position.data;
			}
		
		@Override
		public void remove()
			{
				if (previous == position || !nextCalled)
					throw new IllegalStateException();
				if (position == first)
				{
					deleteFirst();
				}
				else
				{
					previous.next = position.next;
					size--;
				}
				nextCalled = false;
			}
		
		};
	}
}
