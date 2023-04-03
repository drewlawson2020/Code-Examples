package assign09;

/**
 * This class represents a single entry in a map, i.e., a key-value pair.
 * 
 * @author Erin Parker and ling lei, Drew lawson
 * @version March 24, 2021
 *
 * @param <K> - placeholder for key type
 * @param <V> - placeholder for values type
 */
public class MapEntry<K, V> {

	private K key;
	private V value;
	private boolean isDeleted;

	/**
	 * Creates a new MapEntry with the specified key and value.
	 * 
	 * @param key
	 * @param value
	 */
	public MapEntry(K key, V value) {
		this.key = key;
		this.value = value;
		isDeleted = false;
	}

	/**
	 * @return the key of this MapEntry
	 */
	public K getKey() {
		return this.key;
	}

	/**
	 * @return the value of this MapEntry
	 */
	public V getValue() {
		return this.value;
	}

	/**
	 * Resets the value of this MapEntry.
	 * 
	 * @param value
	 */
	public void setValue(V value) {
		this.value = value;
	}

	public void setDelete() {
		this.isDeleted = true;
	}

	public void unsetDelete() {
		this.isDeleted = false;
	}

	public boolean getDeleted() {
		return isDeleted;
	}

	/**
	 * Overrides Objects's equals method to leverage the equals methods of the key
	 * and value members.
	 */
	public boolean equals(Object other) {
		if (!(other instanceof MapEntry<?, ?>))
			return false;

		MapEntry<?, ?> rhs = (MapEntry<?, ?>) other;

		return key.equals(rhs.key) && value.equals(rhs.value);
	}
}
