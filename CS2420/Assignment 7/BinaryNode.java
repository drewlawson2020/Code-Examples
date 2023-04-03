package assign07;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.Queue;

/**
 * Represents a (generic) node in a binary tree.
 * 
 * @author Drew Lawson 
 * @version March 25th, 2021
 */
public class BinaryNode<T extends Comparable<? super T>> {

	private T element;

	private BinaryNode<T> parent;

	private BinaryNode<T> leftChild;

	private BinaryNode<T> rightChild;

	/**
	 * Creates a new BinaryNode object.
	 * 
	 * @param element    - data element to store at this node
	 * @param leftChild  - this node's left child
	 * @param rightChild - this node's right child
	 */
	public BinaryNode(T element, BinaryNode<T> left, BinaryNode<T> right) {
		this.element = element;
		this.leftChild = left;
		this.rightChild = right;
		this.parent = null;

	}

	/**
	 * Creates a new BinaryNode object.
	 * 
	 * @param element - data element to store at this node
	 */
	public BinaryNode(T element) {
		this(element, null, null);
	}

	/**
	 * Getter method for the data element contained in this node.
	 * 
	 * @return the data element
	 */
	public T element() {
		return element;
	}

	public T getFirst() {
		BinaryNode<T> start = this;
		while (start.leftChild != null) {
			start = start.leftChild;
		}
		return start.element;
	}

	public T getLast() {
		BinaryNode<T> start = this;
		while (start.rightChild != null) {
			start = start.rightChild;
		}
		return start.element;
	}
	/**
 	 * Adds a new node to the array from an item element. Returns true if it was able to be added, false if not.
 	 * 
	 * @return True or False
	 */
	public boolean addItem(BinaryNode<T> currentValue, BinaryNode<T> item) {
		
		int cmpVal = item.element.compareTo(currentValue.element());
		if (cmpVal < 0) {
			if (currentValue.leftChild() == null) {
				currentValue.leftChild = item;
				item.parent = currentValue;
				return true;
			} else {
				return addItem(currentValue.leftChild, item);
			}
		} else if (cmpVal > 0) {
			if (currentValue.rightChild() == null) {
				currentValue.rightChild = item;
				item.parent = currentValue;
				return true;
			} else {
				return addItem(currentValue.rightChild, item);
			}
		}
		return false;
	}
	/**
 	 * A recursive remover that cycles through the binary tree until it finds the item.
 	 * @param current - The original binary tree
 	 * @param item - The element to be removed
	 */
	public void recursiveRemover(BinaryNode<T> current, T item) {
		int comparison = item.compareTo(current.element());
		if (comparison < 0) {
			recursiveRemover(current.leftChild(), item);
		} else if (comparison > 0) {
			recursiveRemover(current.rightChild(), item);
		} else {
			switchNodeCases(current);
		}
	}
	/**
 	 * Checks the current binary tree for which case it should use when shifting/deleting nodes
 	 * @param current - The original binary tree
	 */
	public void switchNodeCases(BinaryNode<T> current) {
			if (current.leftChild() == null && current.rightChild() == null) {
				int leftOrRight = leftOrRight(current);
				if (leftOrRight == 0) {
					current.parent.leftChild = null;
					current = null;
				}
				if (leftOrRight == 1) {
					current.parent.rightChild = null;
					current = null;
				}
			} else if (current.leftChild() == null && current.rightChild() != null) {
				int leftOrRight = leftOrRight(current);
				if (leftOrRight == 0) {
					BinaryNode<T> newValue = current.rightChild;
					current.parent.leftChild = newValue;
					current = null;
				}
				if (leftOrRight == 1) {
					BinaryNode<T> newValue = current.rightChild;
					current.parent.rightChild = newValue;
					current = null;
				}
			} else if (current.rightChild == null && current.leftChild != null) {
				int leftOrRight = leftOrRight(current);
				if (leftOrRight == 0) {
					BinaryNode<T> newValue = current.leftChild;
					current.parent.leftChild = newValue;
					current = null;
				}
				if (leftOrRight == 1) {
					BinaryNode<T> newValue = current.leftChild;
					current.parent.rightChild = newValue;
					current = null;
				}
			} else {
				BinaryNode<T> smallest = current.rightChild();
				BinaryNode<T> returnValue = findMinimum(smallest);
				T minimum = returnValue.element;
				recursiveRemover(returnValue, minimum);
				current.setElement(minimum);
			}
	}
	/**
 	 * A helper method for switchNodeCases to help it determine if it should
 	 * go left or right in the tree.
 	 * @param current - The original binary tree
	 */
	public int leftOrRight(BinaryNode<T> current) {
		if (current.parent == null)
		{
			return 0;
		}
		BinaryNode<T> parent = current.parent;
		if (parent.leftChild == current) {
			return 0;
		} else {
			return 1;
		}
	}
	/**
 	 * Finds the lowest value on the tree from a given starting point
 	 * @param start - The starting node
	 */
	public BinaryNode<T> findMinimum(BinaryNode<T> start) {
		BinaryNode<T> current = start;
		while (current.leftChild != null) {
			current = current.leftChild;
		}
		return current;
	}
	/**
 	 * Given an input from a binary node, it returns the parent node's element.
 	 * @param input - The inputed node
	 */
	public T getParent(BinaryNode<T> input) {
		return input.parent.element;
	}
	/**
 	 * Finds an element in the tree given an item. Returns false if there is no result or if it is null, 
 	 * and returns true if found.
 	 * @param elem - The element to be found.
	 */
	public boolean contains(T elem) {
		if (this.element == null) {
			return false;
		}
		int compare = elem.compareTo(this.element);
		if (compare == 0)
			return true;
		else if (compare > 0) {
			if (rightChild() != null)
				return rightChild().contains(elem);
			else
				return false;
		} else {
			if (leftChild() != null)
				return leftChild().contains(elem);
			else
				return false;
		}
	}

	/**
	 * Setter method for the element contained in this node.
	 * 
	 * @param element - the data element
	 */
	public void resetElement(T element) {
		this.element = element;
	}
	/**
	 * Setter method for the element contained in the left child
	 * 
	 * @param input - the data element
	 */
	public void setLeftChild(T input) {
		this.leftChild.element = input;
		this.leftChild.parent = this;
	}
	/**
	 * Setter method for the element contained in the right child
	 * 
	 * @param input - the data element
	 */
	public void setRightChild(T input) {
		this.rightChild.element = input;
		this.rightChild.parent = this;
	}
	/**
	 * Setter method for the element contained in a given node.
	 * 
	 * @param input - the data element
	 */
	public void setElement(T input) {
		this.element = input;
	}

	/**
	 * Getter method for the left child of this node.
	 * 
	 * @return the left child
	 */
	public BinaryNode<T> leftChild() {
		return leftChild;
	}

	/**
	 * Getter method for the right child of this node.
	 * 
	 * @return the right child
	 */
	public BinaryNode<T> rightChild() {
		return rightChild;
	}

	/**
	 * Determines the number of nodes in the tree rooted at this node.
	 * @return the number of nodes in the tree
	 */
	public int size() {
		// count this node
		int size = 1;

		// count all of the nodes in the left subtree
		if (leftChild != null)
			size += leftChild.size();

		// count all of the nodes in the right subtree
		if (rightChild != null)
			size += rightChild.size();

		return size;
	}

	/**
	 * Generate a copy of the tree rooted at this node.
	 * 
	 * @return the tree copy
	 */
	public BinaryNode<T> duplicate() {
		BinaryNode<T> copyLeft = null;

		// get copy of left subtree
		if (leftChild != null)
			copyLeft = leftChild.duplicate();

		// get copy of right subtree
		BinaryNode<T> copyRight = null;
		if (rightChild != null)
			copyRight = rightChild.duplicate();

		// combine left and right in a new node w/ element
		return new BinaryNode<T>(this.element, copyLeft, copyRight);
	}

	/**
	 * Print the elements of the tree rooted at this node, using preorder traversal
	 * (element followed by left subtree followed by right subtree).
	 */
	public void printPreorder() {
		// "visit" this node
		System.out.print(element + " ");
		// do a recursive traversal of the subtree on the left
		if (leftChild != null)
			leftChild.printPreorder();
		// do a recursive traversal of the subtree on the right
		if (rightChild != null)
			rightChild.printPreorder();
	}

	/**
	 * Print the elements of the tree rooted at this node, using postorder traversal
	 * (left subtree followed by right subtree followed by element).
	 */
	public void printPostorder() {
		// do a recursive traversal of the subtree on the left
		if (leftChild != null)
			leftChild.printPostorder();
		// do a recursive traversal of the subtree on the right
		if (rightChild != null)
			rightChild.printPostorder();
		// "visit" this node
		System.out.print(element + " ");
	}

	/**
	 * Print the elements of the tree rooted at this node, using inorder traversal
	 * (left subtree followed by element followed by right subtree).
	 */
	public void printInorder() {
		// do a recursive traversal of the subtree on the left
		if (leftChild != null)
			leftChild.printInorder();
		// "visit" this node
		System.out.print(element + " ");
		// do a recursive traversal of the subtree on the right
		if (rightChild != null)
			rightChild.printInorder();
	}

	public void inOrderDFT(ArrayList<T> result) {
		if (this.leftChild() != null)
			this.leftChild().inOrderDFT(result);
		if (!result.contains(this.element)) {
			result.add(this.element);
		}
		if (this.rightChild() != null)
			this.rightChild().inOrderDFT(result);
	}

	/**
	 * Print the elements of the tree rooted at this node, using level-order
	 * traversal (i.e., breadth-first search).
	 */
	public void printLevelorder() {
		Queue<BinaryNode<T>> q = new LinkedList<BinaryNode<T>>();
		q.offer(this);

		while (!q.isEmpty()) {
			BinaryNode<T> x = q.poll();
			System.out.print(x.element + " ");
			if (x.leftChild != null)
				q.offer(x.leftChild);
			if (x.rightChild != null)
				q.offer(x.rightChild);
		}
	}

	/**
	 * Generate the DOT representation of the tree rooted at this node, useful for
	 * visualizing the tree at http://www.webgraphviz.com.
	 * 
	 * @return a string containing all of the edges in the tree, DOT format
	 */
	private String generateDot() {
		String ret = "\tnode" + element + " [label = \"<f0> |<f1> " + element + "|<f2> \"]\n";
		if (leftChild != null)
			ret += "\tnode" + element + ":f0 -> node" + leftChild.element + ":f1\n" + leftChild.generateDot();
		if (rightChild != null)
			ret += "\tnode" + element + ":f2 -> node" + rightChild.element + ":f1\n" + rightChild.generateDot();

		return ret;
	}

	/**
	 * Write a DOT representation of the tree rooted at "root" to file.
	 * 
	 * @param filename - output file name
	 * @param root     - the root of the binary tree
	 */
	public static <T> void generateDotFile(String filename, BinaryNode<?> root) {
		try {
			PrintWriter out = new PrintWriter(filename);
			out.println("digraph Tree {\n\tnode [shape=record]\n");

			if (root == null)
				out.println("");
			else
				out.print(root.generateDot());

			out.println("}");
			out.close();
		} catch (IOException e) {
			System.out.println(e);
		}
	}
}