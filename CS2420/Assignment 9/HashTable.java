package assign09;

import java.util.ArrayList;
import java.util.List;
import java.math.*;

/**
 * This interface represents a map of keys to values. It cannot contain
 * duplicate keys, and each key can map to at most one value.
 * 
 * @author Ling lei, Drew Lawson
 * @version April 08, 2021
 *
 * @param <K> - placeholder for key type
 * @param <V> - placeholder for value type
 */
public class HashTable<K, V> implements Map<K, V> {
	private ArrayList<MapEntry<K, V>> table;
	public int countValue;
	private int collisions;
	public int capacity;

	/**
	 * @return the (current) capacity of this hash table
	 */
	public int capacity() {
		return this.capacity;
	}

	/**
	 * Creates a new hash table with a default capacity.
	 */
	public HashTable() {
		capacity = 13;
		table = new ArrayList<MapEntry<K, V>>();
		for (int i = 0; i < capacity; i++)
			table.add(null);
		countValue = 0;
		collisions = 0;
	}

	/**
	 * @return the number of items stored in this hash table
	 */
	public int size() {
		return countValue;
	}

	/**
	 * @return the number of collisions incurred (so far)
	 */
	public int collisions() {
		return this.collisions;
	}

	/**
	 * Resets the number-of-collisions statistic to 0.
	 */
	public void resetCollisions() {
		collisions = 0;
	}

	/**
	 * Removes all mappings from this map.
	 * 
	 * O(table length)
	 */
	@Override
	public void clear() {
		for (int i = 0; i < this.capacity; i++) {
			table.set(i, null);
		}
		countValue = 0;
	}

	/**
	 * Determines whether this map contains the specified key.
	 * 
	 * O(1)
	 * 
	 * @param key
	 * @return true if this map contains the key, false otherwise
	 */
	@Override
	public boolean containsKey(K key) {
		int temp = this.collisions;
		boolean itemFound;
		itemFound = table.get(find(key)) != null;
		this.collisions = temp;
		return itemFound;
	}

	/**
	 * Helper method that searches hash table for item starting from desired index.
	 * In the event of a collision, quadratic probing is used to resolve.
	 * 
	 * @param item
	 * @return the index where the search terminates
	 */
	private int find(K key) {
		int hash = key.hashCode();
		if (hash == Integer.MIN_VALUE) {
			hash++;
		}
		// changed size to capacity
		int currentPos = Math.abs(hash) % capacity();
		int originalPos = currentPos;
		int count = 0;

		while (table.get(currentPos) != null) {

			if (key.equals(table.get(currentPos).getKey()) && table.get(currentPos).getDeleted() == false)
				break;

			collisions++;
			count++;
			currentPos = originalPos + (int) Math.pow(count, 2);

			currentPos = Math.abs(currentPos) % capacity();
		}
		return currentPos;
	}

	/**
	 * Determines whether this map contains the specified value.
	 * 
	 * O(table length)
	 * 
	 * @param value
	 * @return true if this map contains one or more keys to the specified value,
	 *         false otherwise
	 */
	@Override
	public boolean containsValue(V value) {
		for (int i = 0; i < this.capacity; i++) {
			if (table.get(i) != null) {
				if (value.equals(table.get(i).getValue())) {
					return true;
				}
			}
		}
		return false;
	}

	/**
	 * Returns a List view of the mappings contained in this map, where the ordering
	 * of mapping in the list is insignificant.
	 * 
	 * O(table length)
	 * 
	 * @return a List object containing all mapping (i.e., entries) in this map
	 */
	@Override
	public List<MapEntry<K, V>> entries() {
		List<MapEntry<K, V>> list = new ArrayList<MapEntry<K, V>>();

		for (int i = 0; i < this.capacity; i++) {
			if (table.get(i) != null) {
				list.add(table.get(i));
			}
		}

		return list;
	}

	/**
	 * Gets the value to which the specified key is mapped.
	 * 
	 * O(1)
	 * 
	 * @param key
	 * @return the value to which the specified key is mapped, or null if this map
	 *         contains no mapping for the key
	 */
	@Override
	public V get(K key) {
		if (containsKey(key) == false)
			return null;
		else {
			MapEntry<K, V> temp = table.get(find(key));
			return temp.getValue();
		}
	}

	/**
	 * Determines whether this map contains any mappings.
	 * 
	 * O(1)
	 * 
	 * @return true if this map contains no mappings, false otherwise
	 */
	@Override
	public boolean isEmpty() {
		if (this.size() == 0) {
			return true;
		} else {
			return false;
		}
	}

	/**
	 * Associates the specified value with the specified key in this map. (I.e., if
	 * the key already exists in this map, resets the value; otherwise adds the
	 * specified key-value pair.)
	 * 
	 * O(1)
	 * 
	 * @param key
	 * @param value
	 * @return the previous value associated with key, or null if there was no
	 *         mapping for key
	 */
	@Override
	public V put(K key, V value) {
		if (containsKey(key) == true) {
			int keyPosition = find(key);
			V val = table.get(keyPosition).getValue();
			table.get(keyPosition).setValue(value);
			return val;
		} else {
			this.countValue++;
			add(key, value);
			return null;
		}
	}

	/**
	 * Helper method to put
	 * 
	 * @param key
	 * @param value
	 */
	private void add(K key, V value) {

		if ((countValue / (double) capacity) < 0.5) {
			int index = find(key);
			table.set(index, new MapEntry<K, V>(key, value));
		}

		else if ((countValue / (double) capacity) >= 0.5) {

			ArrayList<MapEntry<K, V>> temp = new ArrayList<MapEntry<K, V>>();

			// get new capacity with prime number
			this.capacity *= 2;
			BigInteger num = new BigInteger(String.valueOf(capacity));
			num = num.nextProbablePrime();
			String nextPrime = num.toString();
			this.capacity = Integer.valueOf(nextPrime);

			// new capacity null temp table
			for (int i = 0; i < this.capacity; i++) {
				temp.add(null);
			}

			// rehash
			for (MapEntry<K, V> entry : table) {
				if (entry != null) {

					int hashcode = entry.getKey().hashCode();
					if (hashcode == Integer.MIN_VALUE) {
						hashcode++;
					}
					int i = 0;
					int index = Math.abs(hashcode) % this.capacity;
					int originalIndex = index;

					if (temp.get(index) == null) {
						temp.set(index, entry);
						continue;
					}

					while (temp.get(index) != null) {
						collisions++;
						i++;
						index = (originalIndex + (int) Math.pow(i, 2)) % capacity;
						if (temp.get(index) == null || temp.get(index).getDeleted() == true) {
							temp.set(index, entry);
							break;
						}
					}

				}

			}

			// add new one
			MapEntry<K, V> newEntry = new MapEntry<K, V>(key, value);

			int hashcode = newEntry.getKey().hashCode();
			if (hashcode == Integer.MIN_VALUE) {
				hashcode++;
			}
			int i = 0;
			int index = Math.abs(hashcode) % this.capacity;
			int originalIndex = index;

			if (temp.get(index) == null) {
				temp.set(index, newEntry);
			}

			while (temp.get(index) != null) {
				collisions++;
				i++;
				index = (originalIndex + (int) Math.pow(i, 2)) % capacity;
				if (temp.get(index) == null || temp.get(index).getDeleted() == true) {
					temp.set(index, newEntry);
					break;
				}
			}
			this.table = temp;
		}
	}

	/**
	 * Removes the mapping for a key from this map if it is present.
	 * 
	 * O(1)
	 *
	 * @param key
	 * @return the previous value associated with key, or null if there was no
	 *         mapping for key
	 */
	@Override

	public V remove(K key) {
		int keyPosition = find(key);
		MapEntry<K, V> temp = table.get(keyPosition);

		if (temp != null) {
			V val = temp.getValue();
			table.get(keyPosition).setDelete();
			table.get(keyPosition).setValue(null);
			countValue--;
			return val;
		} else {
			return null;
		}
	}
}
