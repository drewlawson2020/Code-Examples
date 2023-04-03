package assign08;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Queue;

/**
 * 
 * @author Daniel Kopta and Ling Lei, Drew Lawson. This Graph class acts as a
 *         starting point for your maze path finder. Add to this class as
 *         needed.
 */
public class Graph {

	// The graph itself is just a 2D array of nodes
	private Node[][] nodes;

	// The node to start the path finding from
	private Node start;
	private ArrayList<Node> goals = new ArrayList<Node>();

	// The size of the maze
	private int width;
	private int height;

	// keep track of path length
	private int pathLength;

	// count steps for BFS and DFS
	int count2 = 0;
	int count1 = 0;

	/**
	 * Constructs a maze graph from the given text file.
	 * 
	 * @param filename - the file containing the maze
	 * @throws Exception
	 */
	public Graph(String filename) throws Exception {
		BufferedReader input;
		input = new BufferedReader(new FileReader(filename));

		if (!input.ready()) {
			input.close();
			throw new FileNotFoundException();
		}

		// read the maze size from the file
		String[] dimensions = input.readLine().split(" ");
		height = Integer.parseInt(dimensions[0]);
		width = Integer.parseInt(dimensions[1]);
		// instantiate and populate the nodes
		nodes = new Node[height][width];
		for (int i = 0; i < height; i++) {
			String row = input.readLine().trim();

			for (int j = 0; j < row.length(); j++)
				switch (row.charAt(j)) {
				case 'X':
					nodes[i][j] = new Node(i, j);
					nodes[i][j].isWall = true;
					break;
				case ' ':
					nodes[i][j] = new Node(i, j);
					break;
				case 'S':
					nodes[i][j] = new Node(i, j);
					nodes[i][j].isStart = true;
					start = nodes[i][j];
					break;
				case 'G':
					nodes[i][j] = new Node(i, j);
					nodes[i][j].isGoal = true;
					goals.add(nodes[i][j]);
					break;
				default:
					throw new IllegalArgumentException("maze contains unknown character: \'" + row.charAt(j) + "\'");
				}
		}
		input.close();
	}

	/**
	 * Outputs this graph to the specified file. Use this method after you have
	 * found a path to one of the goals. Before using this method, for the nodes on
	 * the path, you will need to set their isOnPath value to true.
	 * 
	 * @param filename - the file to write to
	 */
	public void printGraph(String filename) {
		try {
			PrintWriter output = new PrintWriter(new FileWriter(filename));
			output.println(height + " " + width);
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					output.print(nodes[i][j]);
				}
				output.println();
			}
			output.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * Traverse the graph with BFS (shortest path to closest goal) A side-effect of
	 * this method should be that the nodes on the path have had their isOnPath
	 * member set to true. BFS
	 * 
	 * @return - the length of the path
	 */
	public int CalculateShortestPath() {
		Myqueue<Node> queue = new Myqueue<Node>();
		queue.add(start);

		while (!queue.isEmpty()) {

			Node current = queue.poll();
			count1++;

			ArrayList<Node> neighbors = new ArrayList<Node>();
			neighbors.add(nodes[current.x][current.y - 1]);
			neighbors.add(nodes[current.x][current.y + 1]);
			neighbors.add(nodes[current.x - 1][current.y]);
			neighbors.add(nodes[current.x + 1][current.y]);

			for (Node next : neighbors) {

				if (next.isWall)
					continue;

				if (!next.isVisited) {
					next.isVisited = true;
					next.cameFrom = current;
					queue.add(next);
				}
			}
		}

		System.out.println("count for BFS is: " + count1);

		int shortestOverallPathLength = 0;

		Node tempGoal = goals.get(0);
		// handle isOnpath
		for (Node goal : goals) {
			int currentPathLength = 0;
			if (goal.isVisited) {
				Node extra = goal;
				while (!extra.equals(start)) {
					extra = extra.cameFrom;
					currentPathLength++;
				}

			}
			if (shortestOverallPathLength == 0) {
				shortestOverallPathLength = currentPathLength;
			} else if (currentPathLength < shortestOverallPathLength) {
				shortestOverallPathLength = currentPathLength;
				tempGoal = goal;
			}
		}
		if (tempGoal.isVisited && tempGoal != null) {
			Node extra = tempGoal;
			while (!extra.equals(start)) {
				extra.isOnPath = true;
				extra = extra.cameFrom;
				pathLength++;
			}

		}
		return pathLength;

	}

	/**
	 * Traverse the graph with DFS (any path to any goal) A side-effect of this
	 * method should be that the nodes on the path have had their isOnPath member
	 * set to true. DFS
	 * 
	 * @return - the length of the path
	 */
	public int CalculateAPath() {
		pathLength = 0;
		DFS(start);
		System.out.println("count for DFS is: " + count2);
		return pathLength;
	}

	/**
	 * This is the helper method for DFS
	 * 
	 * @param current
	 */
	public void DFS(Node current) {
		// mark it as visited
		current.isVisited = true;

		// base case
		if (current.isGoal == true) {
			return;
		}

		ArrayList<Node> neighbors = new ArrayList<Node>();
		neighbors.add(nodes[current.x][current.y + 1]);
		neighbors.add(nodes[current.x + 1][current.y]);
		neighbors.add(nodes[current.x][current.y - 1]);
		neighbors.add(nodes[current.x - 1][current.y]);

		for (Node next : neighbors) {
			// if we hit the wall
			if (next.isWall == true) {
				continue;
			}
			// if the node has not been visited
			if (!next.isVisited) {
				next.cameFrom = current;
				count2++;
				DFS(next);
			}
		}

		// handle isOnpath
		if (goals.get(0).isVisited) {
			Node extra = goals.get(0);
			while (!extra.equals(start)) {
				extra.isOnPath = true;
				extra = extra.cameFrom;
				pathLength++;
			}
			start.isOnPath = true;
			return;
		}
	}

	/**
	 * @author Daniel Kopta A node class to assist in the implementation of the
	 *         graph. You will need to add additional functionality to this class.
	 */
	public static class Node {
		// The node's position in the maze
		private int x, y;

		// The type of the node
		private boolean isStart;
		private boolean isGoal;
		private boolean isOnPath;
		private boolean isWall;

		private boolean isVisited;
		// get access to its neighbors
		private Node cameFrom;

		public Node(int _x, int _y) {
			isStart = false;
			isGoal = false;
			isOnPath = false;
			x = _x;
			y = _y;
			// extra to keep track of
			cameFrom = null;
			isVisited = false;
		}

		@Override
		public String toString() {
			if (isWall)
				return "X";
			if (isStart)
				return "S";
			if (isGoal)
				return "G";
			if (isOnPath)
				return ".";
			return " ";
		}

	}

	/**
	 * @author linglei and Drew Lawson
	 * 
	 *         this class is implementation for queue.
	 *
	 * @param <E>
	 */
	public class Myqueue<E> implements Queue<E> {
		private int size;

		Queue<E> myQueue = new LinkedList<>();

		public Myqueue() {

		}

		@Override
		public int size() {
			return size;
		}

		@Override
		public boolean isEmpty() {
			return myQueue.isEmpty();
		}

		@Override
		public boolean contains(Object o) {
			return myQueue.contains(o);
		}

		@Override
		public Iterator<E> iterator() {
			// TODO Auto-generated method stub
			return null;
		}

		@Override
		public Object[] toArray() {
			return myQueue.toArray();
		}

		@Override
		public <T> T[] toArray(T[] a) {
			return myQueue.toArray(a);
		}

		@Override
		public boolean remove(Object o) {
			return myQueue.remove(o);
		}

		@Override
		public boolean containsAll(Collection<?> c) {
			return myQueue.containsAll(c);
		}

		@Override
		public boolean addAll(Collection<? extends E> c) {
			return myQueue.addAll(c);
		}

		@Override
		public boolean removeAll(Collection<?> c) {
			return myQueue.removeAll(c);
		}

		@Override
		public boolean retainAll(Collection<?> c) {
			return myQueue.retainAll(c);
		}

		@Override
		public void clear() {
			myQueue.clear();
		}

		@Override
		public boolean add(E e) {
			return myQueue.add(e);
		}

		@Override
		public boolean offer(E e) {
			return myQueue.offer(e);
		}

		@Override
		public E remove() {
			return myQueue.remove();
		}

		@Override
		public E poll() {
			return myQueue.poll();
		}

		@Override
		public E element() {
			return myQueue.element();
		}

		@Override
		public E peek() {
			return myQueue.peek();
		}

	}

}
