package assign06;

import java.net.MalformedURLException;
import java.net.URL;
import java.util.Iterator;
import java.util.NoSuchElementException;

public class WebBrowser {
	private LinkedListStack<URL> back;
	private LinkedListStack<URL> forward;
	private URL current;
	//Default Constructor
	public WebBrowser()
	{
		back = new LinkedListStack<URL>();
		forward = new LinkedListStack<URL>();
	}
	/**
	 * Creates a new webbrowser based on the history that it's given from a linkedlist
	 * 
	 * @param iterator - used to find and pass the next element in the LinkedList.
	 * @param temp - a temporary LinkedListStack for storing and sorting the history
	 * 
	 * @return SinglyLinkedList<URL> his - The generated history list
	 */
	public WebBrowser(SinglyLinkedList<URL> history)
	{
		Iterator<URL> iterator = history.iterator();
		
		if (iterator.hasNext())
		{
		current = iterator.next();
		}
		
		back = new LinkedListStack<URL>();
		forward = new LinkedListStack<URL>();
		LinkedListStack<URL> temp = new LinkedListStack<URL>();
		
		while (iterator.hasNext())
		{
			temp.push(iterator.next());
		}
		
		while (!temp.isEmpty())
		{
			back.push(temp.pop());
		}
	}
	//Sets current to new URL object webpage
	public void visit(URL webpage)
	{
		forward = new LinkedListStack<URL>();
		
		if (current != null)
			back.push(current);
		
		current = webpage;
	}
	/**
	 * Goes back in the webbrowser's history.
	 * 
	 * @return the new webpage from the back list.
	 */
	public URL back() throws NoSuchElementException
	{
		if (back.peek() != null)
		{
		forward.push(current);
		current = back.peek();
		back.pop();
		return current;
		}
		else
		{
			throw new NoSuchElementException();
		}
	}
	/**
	 * Goes forward in the webbrowser's history.
	 * 
	 * @return the new webpage from the forwards list.
	 */
	public URL forward() throws NoSuchElementException
	{
		if (forward.peek() != null)
		{
		back.push(current);
		current = forward.peek();
		forward.pop();
		return current;
		}
		else
		{
			throw new NoSuchElementException();
		}
		
	}
	/**
	 * Takes the pages stored in the back list and current url, and combines them into an array and returns it.
	 * 
	 * @param temp - a temporary LinkedListStack for storing and sorting the history
	 * 
	 * @return SinglyLinkedList<URL> his - The generated history list
	 */
	public SinglyLinkedList<URL> history()
	{
		LinkedListStack<URL> temp = new LinkedListStack<URL>();
		while (!back.isEmpty())
		{
			temp.push(back.pop());
		}
		SinglyLinkedList<URL> his = new SinglyLinkedList<URL>();
		while (!temp.isEmpty())
		{
			his.insertFirst(temp.peek());
			back.push(temp.pop());
		}
		his.insertFirst(current);
		return his;
	}
}

