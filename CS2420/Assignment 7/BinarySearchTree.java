package assign07;

import java.util.ArrayList;
import java.util.Collection;
import java.util.NoSuchElementException;
/**
 * Methods to manage and search a binary search tree with nodes.
 * 
 * @author Erin Parker & Drew Lawson
 * @version March 25th, 2021
 */
public class BinarySearchTree<Type extends Comparable<? super Type>> implements SortedSet<Type> {
	private BinaryNode<Type> rootNode;
	private int size;

	public BinarySearchTree() {
		rootNode = new BinaryNode<Type>(null);
		size = 0;
	}

	@Override
	public boolean add(Type item) {
		size++;
		BinaryNode<Type> node = new BinaryNode<Type>(item);
		if (rootNode.element() == null) {
			rootNode.setElement(item);
			return true;
		} else if (!rootNode.contains(item)){
			return rootNode.addItem(rootNode, node);
		}
		else
		{
			return false;
		}
	}

	public Type getRoot() {
		return rootNode.element();
	}

	@Override
	public boolean addAll(Collection<? extends Type> items) {
		boolean returnVal = false;
		for (Type item : items) {
			if (add(item));
			{
				if (returnVal != true)
				{
					returnVal = true;
				}
			}
		}
		return returnVal;
	}

	@Override
	public void clear() {
		BinaryNode<Type> newRoot = new BinaryNode<Type>(null); 
		rootNode = newRoot; 
		size = 0;
	}

	@Override
	public boolean contains(Type item) {
		if (rootNode == null)
			return false;
		return rootNode.contains(item);
	}

	@Override
	public boolean containsAll(Collection<? extends Type> items) {
		boolean finalreturn = false;
		for (Type item : items) {
			finalreturn = contains(item);
			if (!finalreturn) {
				return false;
			}
		}
		return true;
	}

	@Override
	public Type first() throws NoSuchElementException {
		if (rootNode.element() == null) {
			throw new NoSuchElementException();
		}
		return rootNode.getFirst();
	}

	@Override
	public boolean isEmpty() {
		if (size == 0) {
			return true;
		}
		return false;
	}

	@Override
	public Type last() throws NoSuchElementException {
		if( rootNode.element() == null) {
			throw new NoSuchElementException(); 
		}
		Type returnVal = rootNode.getLast();
		return returnVal;
	}

	@Override
	public boolean remove(Type item) {
		if (item == rootNode.element() && rootNode.leftChild() == null && rootNode.rightChild() == null) {
			BinaryNode<Type> replacement = new BinaryNode<Type>(null);
			rootNode = replacement;
			size--;
			return true; 
		} else {
			if(!this.contains(item)) {
				return false; 
			}
			if (rootNode == null) {
				return false;
			} else {
				rootNode.recursiveRemover(rootNode, item);
				size--;
				return true;
			}
		}

	}

	@Override
	public boolean removeAll(Collection<? extends Type> items) {
		boolean check = false;
		for (Type item : items) {
			if (this.remove(item))
			{
				check = true;
			}
		}
			return check; 
	}

	@Override
	public int size() {
		return size;
	}

	@Override
	public ArrayList<Type> toArrayList() {
		return this.DFT();
	}

	public ArrayList<Type> DFT() {
		ArrayList<Type> result = new ArrayList<Type>();
		if (!isEmpty())
			rootNode.inOrderDFT(result);
		return result;
	}

}
