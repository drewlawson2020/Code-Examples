package assign09;

import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.List;

import org.junit.jupiter.api.Test;

/**
 * This is test class for hashtable
 * 
 * @author linglei and Drew lawson
 *
 */
class HashTableTest {
	HashTable<StudentBadHash, Double> testBad = new HashTable<StudentBadHash, Double>();
	HashTable<StudentMediumHash, Double> testMedium = new HashTable<StudentMediumHash, Double>();
	HashTable<StudentGoodHash, Double> testGood = new HashTable<StudentGoodHash, Double>();
	StudentBadHash stu1 = new StudentBadHash(1019999, "Alan", "Turing");
	StudentBadHash stu2 = new StudentBadHash(1004203, "Ada", "Lovelace");
	StudentBadHash stu3 = new StudentBadHash(1010661, "Edsger", "Dijkstra");
	StudentBadHash stu4 = new StudentBadHash(1019941, "Grace", "Hopper");
	StudentBadHash stu5 = new StudentBadHash(1315098, "ling", "lei");
	StudentBadHash stu6 = new StudentBadHash(1316784, "Ming", "fen");
	StudentBadHash stu7 = new StudentBadHash(1316723, "King", "Dof");
	StudentBadHash stu8 = new StudentBadHash(1316721, "Sam", "fod");
	//
	StudentMediumHash st1 = new StudentMediumHash(1019999, "Alan", "Turing");
	StudentMediumHash st2 = new StudentMediumHash(1004203, "Ada", "Lovelace");
	StudentMediumHash st3 = new StudentMediumHash(1010661, "Edsger", "Dijkstra");
	StudentMediumHash st4 = new StudentMediumHash(1019941, "Grace", "Hopper");
	StudentMediumHash st5 = new StudentMediumHash(1315098, "ling", "lei");
	StudentMediumHash st6 = new StudentMediumHash(1316784, "Ming", "fen");
	StudentMediumHash st7 = new StudentMediumHash(1316723, "King", "Dof");
	StudentMediumHash st8 = new StudentMediumHash(1316721, "Sam", "fod");
	StudentMediumHash st9 = new StudentMediumHash(1019999, "Som", "Turing");
	StudentMediumHash st10 = new StudentMediumHash(1002303, "DGw", "Lovelace");
	StudentMediumHash st11 = new StudentMediumHash(1012661, "MBV", "Dijkstra");
	StudentMediumHash st12 = new StudentMediumHash(1769941, "LJH", "Hopper");
	StudentMediumHash st13 = new StudentMediumHash(2315438, "THUNG", "lei");
	StudentMediumHash st14 = new StudentMediumHash(1313484, "daew", "fen");
	StudentMediumHash st15 = new StudentMediumHash(1309723, "drew", "Dof");
	StudentMediumHash st16 = new StudentMediumHash(1316451, "mngj", "fod");

	HashTable<String, Integer> simpleTable = new HashTable<String, Integer>();
	HashTable<Integer, Integer> easyTable = new HashTable<Integer, Integer>();

	@Test
	void testPutOneElement() {
		testBad.put(stu1, 1.2);
		// size++
		assertEquals(1, testBad.size());
		// should be in place 1
		assertTrue(testBad.containsKey(stu1));
		assertTrue(testBad.containsValue(1.2));
		assertEquals(1.2, testBad.get(stu1));
		assertEquals(null, testBad.get(stu2));
		assertFalse(testBad.isEmpty());
	}

	@Test
	void testPutTwoDifferentElement() {
		testBad.put(stu1, 1.2);
		testBad.put(stu2, 2.1);
		// size++
		assertEquals(2, testBad.size());
		// should be in place 1
		assertTrue(testBad.containsKey(stu1));
		assertTrue(testBad.containsValue(1.2));
		assertTrue(testBad.containsKey(stu2));
		assertTrue(testBad.containsValue(2.1));
		//
		assertEquals(1.2, testBad.get(stu1));
		assertEquals(2.1, testBad.get(stu2));
		assertFalse(testBad.isEmpty());
	}

	@Test
	void testPutTwoDuplicatedElement() {
		testBad.put(stu1, 1.2);
		testBad.put(stu1, 2.1);
		// size++
		assertEquals(1, testBad.size());
		// should be in place 1
		assertTrue(testBad.containsKey(stu1));
		assertFalse(testBad.containsValue(1.2));
		assertFalse(testBad.containsKey(stu2));
		assertTrue(testBad.containsValue(2.1));
		//
		assertEquals(2.1, testBad.get(stu1));
		assertFalse(testBad.isEmpty());
	}

	@Test
	void testPutThreeDuplicatedElement() {
		testBad.put(stu1, 1.2);
		testBad.put(stu1, 2.1);
		testBad.put(stu1, 3.2);
		// size++
		assertEquals(1, testBad.size());
		// should be in place 1
		assertTrue(testBad.containsKey(stu1));
		assertFalse(testBad.containsValue(1.2));
		assertFalse(testBad.containsKey(stu2));
		assertFalse(testBad.containsValue(2.1));
		assertTrue(testBad.containsValue(3.2));
		//
		assertEquals(3.2, testBad.get(stu1));
		assertFalse(testBad.isEmpty());
	}

	@Test
	void testPutThreeDuplicatedElement2() {
		testBad.put(stu2, 1.2);
		testBad.put(stu2, 2.1);
		testBad.put(stu2, 3.2);
		// size++
		assertEquals(1, testBad.size());
		// should be in place 1
		assertTrue(testBad.containsKey(stu2));
		assertFalse(testBad.containsValue(1.2));
		assertFalse(testBad.containsKey(stu1));
		assertFalse(testBad.containsValue(2.1));
		assertTrue(testBad.containsValue(3.2));
		//
		assertEquals(3.2, testBad.get(stu2));
		assertFalse(testBad.isEmpty());
	}

	@Test
	void testPutThreeDifferentElement() {
		testBad.put(stu1, 1.2);
		testBad.put(stu2, 2.1);
		testBad.put(stu3, 3.2);

		// size++
		assertEquals(3, testBad.size());
		// should be in place 1
		assertTrue(testBad.containsKey(stu1));
		assertTrue(testBad.containsValue(1.2));
		assertTrue(testBad.containsKey(stu2));
		assertTrue(testBad.containsValue(2.1));
		assertTrue(testBad.containsKey(stu3));
		assertTrue(testBad.containsValue(3.2));
		//
		assertEquals(1.2, testBad.get(stu1));
		assertEquals(2.1, testBad.get(stu2));
		assertEquals(3.2, testBad.get(stu3));
		//
		assertFalse(testBad.isEmpty());
	}

	@Test
	void testClear() {
		testBad.put(stu1, 1.2);
		// size++
		assertEquals(1, testBad.size());
		testBad.clear();
		assertEquals(0, testBad.size());
		// should be in place 1
		assertTrue(testBad.isEmpty());
		// put another
		testBad.put(stu2, 2.1);
		assertEquals(1, testBad.size());
		testBad.clear();
		assertEquals(0, testBad.size());
		// put others
		testBad.put(stu1, 1.2);
		assertEquals(1, testBad.size());
		testBad.put(stu2, 2.1);
		assertEquals(2, testBad.size());
		testBad.put(stu3, 3.2);
		testBad.put(stu4, 4.3);
		testBad.put(stu5, 5.4);
		testBad.put(stu6, 6.5);
		assertEquals(6, testBad.size());
	}

	@Test
	void testRemoveOneElement() {
		testBad.put(stu1, 1.2);
		// size++
		assertEquals(1, testBad.size());
		assertEquals(1.2, testBad.remove(stu1));
		//
		assertEquals(0, testBad.size());
		assertFalse(testBad.containsValue(1.2));
		assertEquals(null, testBad.get(stu1));
		assertEquals(null, testBad.get(stu2));
		assertTrue(testBad.isEmpty());
	}

	@Test
	void testContainsKeyOneElement() {
		testBad.put(stu2, 2.1);
		assertEquals(1, testBad.size());
		assertTrue(testBad.containsValue(2.1));
		assertTrue(testBad.containsKey(stu2));
		assertFalse(testBad.containsValue(1.2));
		assertFalse(testBad.containsKey(stu1));
	}

	@Test
	void testContainsKeyTwoElement() {
		testBad.put(stu1, 1.2);
		testBad.put(stu2, 2.1);
		assertEquals(2, testBad.size());
		assertTrue(testBad.containsValue(2.1));
		assertTrue(testBad.containsKey(stu2));
		assertTrue(testBad.containsValue(1.2));
		assertTrue(testBad.containsKey(stu1));
	}

	@Test
	void testContainsKeyTwoElementRemoveOnehenAnother() {
		testBad.put(stu1, 1.2);
		testBad.put(stu2, 2.1);
		assertEquals(testBad.get(stu1), 1.2);
		assertEquals(1.2, testBad.remove(stu1));
		assertTrue(testBad.containsValue(2.1));
		assertTrue(testBad.containsKey(stu2));
		assertEquals(testBad.get(stu2), 2.1);
		//
		assertFalse(testBad.containsValue(1.2));
		assertFalse(testBad.containsKey(stu1));
		//
		assertEquals(2.1, testBad.remove(stu2));
		assertFalse(testBad.containsValue(2.1));
		assertFalse(testBad.containsKey(stu2));
	}

	@Test
	void testAddAfterRemoveOneElement() {
		testBad.put(stu1, 1.2);
		assertEquals(1.2, testBad.remove(stu1));
		// it should be null after remove value
		assertEquals(testBad.get(stu2), null);
	}

	@Test
	void testAddAfterRemoveTwoElement() {
		testBad.put(stu1, 1.2);
		testBad.put(stu2, 2.1);
		assertEquals(1.2, testBad.remove(stu1));
		testBad.put(stu1, 3.4);
		assertTrue(testBad.containsValue(3.4));
		assertTrue(testBad.containsKey(stu2));
		assertEquals(testBad.get(stu1), 3.4);
		assertEquals(testBad.get(stu2), 2.1);
		//
		assertEquals(2.1, testBad.remove(stu2));
		assertEquals(testBad.get(stu1), 3.4);
		assertEquals(testBad.get(stu2), null);
		//
		assertEquals(3.4, testBad.remove(stu1));
		assertEquals(testBad.get(stu2), null);
		assertEquals(testBad.get(stu1), null);
		testBad.put(stu1, 5.2);
		testBad.put(stu2, 5.2);
		assertEquals(testBad.get(stu1), 5.2);
		assertEquals(testBad.get(stu2), 5.2);
	}

	@Test
	void testPutHitThreshold() {
		testBad.put(stu1, 1.2);
		testBad.put(stu2, 2.1);
		testBad.put(stu3, 3.2);
		testBad.put(stu4, 3.2);
		testBad.put(stu5, 3.2);
		testBad.put(stu6, 3.2);
		// size++
		assertEquals(6, testBad.size());
		// add one more
		testBad.put(stu7, 4.9);
		assertEquals(testBad.capacity(), 29);
		assertEquals(testBad.size(), 7);
		testBad.put(stu8, 3.2);
		assertEquals(8, testBad.size());
		assertEquals(testBad.capacity(), 29);
	}

	@Test
	void testPutOneElementMedium() {
		testMedium.put(st1, 1.2);
		assertEquals(testBad.capacity(), 13);
		assertEquals(3, st1.hashCode() % testMedium.capacity);
		// size++
		assertEquals(1, testMedium.size());
		// should be in place 1
		assertTrue(testMedium.containsKey(st1));
		assertTrue(testMedium.containsValue(1.2));
		assertEquals(1.2, testMedium.get(st1));
		assertEquals(null, testMedium.get(st2));
		assertFalse(testMedium.isEmpty());
	}

	@Test
	void testPutTwoDuplicatedElementMedium() {
		testMedium.put(st1, 1.2);
		int temp = st1.hashCode();
		assertEquals(testBad.capacity(), 13);
		assertEquals(3, st1.hashCode() % testMedium.capacity);
		testMedium.put(st1, 2.1);
		assertEquals(temp, st1.hashCode());
		assertEquals(1, testMedium.size());
		// should be in place 1
		assertTrue(testMedium.containsKey(st1));
		assertTrue(testMedium.containsValue(2.1));
		assertEquals(2.1, testMedium.get(st1));
		assertEquals(null, testMedium.get(st2));
		assertFalse(testMedium.isEmpty());
	}

	@Test
	void testPutTwoElementThenRemoveMedium() {
		testMedium.put(st1, 1.2);
		testMedium.put(st2, 2.1);
		assertEquals(testMedium.capacity(), 13);
		assertEquals(3, st1.hashCode() % testMedium.capacity);
		assertEquals(null, testMedium.remove(st3));
		assertEquals(1.2, testMedium.remove(st1));
		assertEquals(2.1, testMedium.remove(st2));
		//
		assertEquals(0, testMedium.size());
		assertFalse(testMedium.containsKey(st1));
		assertEquals(null, testMedium.get(st1));

		assertFalse(testMedium.containsKey(st2));
		assertEquals(null, testMedium.get(st2));

		assertFalse(testMedium.containsValue(2.1));
		assertFalse(testMedium.containsValue(1.2));
		assertTrue(testMedium.isEmpty());

		assertEquals(null, testMedium.put(st1, 1.3));
		assertEquals(1.3, testMedium.get(st1));
	}

	@Test
	void testPutHitThresholdMedium() {
		testMedium.put(st1, 1.2);
		testMedium.put(st2, 2.1);
		testMedium.put(st3, 3.2);
		testMedium.put(st4, 3.9);
		testMedium.put(st5, 7.8);
		testMedium.put(st6, 2.4);

		// size++
		assertEquals(6, testMedium.size());
		// add one more
		testMedium.put(st7, 4.9);
		assertEquals(testMedium.capacity(), 29);
		assertEquals(testMedium.size(), 7);
		testMedium.put(st8, 3.2);
		assertEquals(8, testMedium.size());
		assertEquals(testMedium.capacity(), 29);
	}

	@Test
	void testPutHitThresholdMediumBigger() {
		testMedium.put(st1, 1.2);
		testMedium.put(st2, 2.1);
		testMedium.put(st3, 3.2);
		testMedium.put(st4, 3.9);
		testMedium.put(st5, 7.8);
		testMedium.put(st6, 2.4);
		testMedium.put(st7, 1.2);
		assertEquals(testMedium.capacity(), 29);
		testMedium.put(st8, 2.1);
		testMedium.put(st9, 3.2);
		testMedium.put(st10, 3.9);
		testMedium.put(st11, 7.8);
		testMedium.put(st12, 2.4);
		testMedium.put(st13, 7.8);
		testMedium.put(st14, 2.4);

		assertEquals(14, testMedium.size());

		// add one more
		testMedium.put(st15, 4.9);
		assertEquals(testMedium.capacity(), 59);
		assertEquals(testMedium.size(), 15);
		testMedium.put(st16, 3.2);
		assertEquals(16, testMedium.size());
		assertEquals(testMedium.capacity(), 59);
	}

	@Test
	void testPutOneElementSimpleTable() {
		simpleTable.put("ling", 1);
		// size++
		assertEquals(1, simpleTable.size());
		// should be in place 1
		assertTrue(simpleTable.containsKey("ling"));
		assertTrue(simpleTable.containsValue(1));
		assertEquals(1, simpleTable.get("ling"));
		assertEquals(null, simpleTable.get("ming"));
		assertFalse(simpleTable.isEmpty());
	}

	@Test
	void testPutTwoElementSimpleTable() {
		simpleTable.put("ling", 1);
		simpleTable.put("Drew", 2);
		//
		assertEquals(2, simpleTable.size());
		//
		assertTrue(simpleTable.containsKey("ling"));
		assertEquals(1, simpleTable.get("ling"));
		assertTrue(simpleTable.containsKey("Drew"));
		assertEquals(2, simpleTable.get("Drew"));
		assertTrue(simpleTable.containsValue(1));
		assertTrue(simpleTable.containsValue(2));
		assertEquals(null, simpleTable.get("ming"));
		assertFalse(simpleTable.isEmpty());
	}

	@Test
	void testPutTwoElementDuplacatedSimpleTable() {
		simpleTable.put("ling", 1);
		simpleTable.put("ling", 2);
		//
		assertEquals(1, simpleTable.size());
		//
		assertTrue(simpleTable.containsKey("ling"));
		assertEquals(2, simpleTable.get("ling"));
		assertFalse(simpleTable.containsKey("Drew"));
		//
		assertFalse(simpleTable.containsValue(1));
		assertTrue(simpleTable.containsValue(2));
		assertEquals(null, simpleTable.get("ming"));
		assertFalse(simpleTable.isEmpty());
	}

	@Test
	void testPutTwoElementRemoveSimpleTable() {
		simpleTable.put("ling", 1);
		assertEquals(null, simpleTable.remove("Drew"));
		assertEquals(1, simpleTable.remove("ling"));
		//
		assertEquals(0, simpleTable.size());
		//
		assertFalse(simpleTable.containsKey("ling"));
		assertTrue(simpleTable.isEmpty());
	}

	@Test
	void testPutHitThresholdSimpleTableBigger() {
		simpleTable.put("ling", 1);
		simpleTable.put("mixg", 2);
		simpleTable.put("misg", 3);
		simpleTable.put("ming", 4);
		simpleTable.put("msng", 7);
		simpleTable.put("mibg", 2);
		simpleTable.put("minl", 1);
		assertEquals(simpleTable.capacity(), 29);
		simpleTable.put("mmng", 11);
		simpleTable.put("mnng", 2);
		simpleTable.put("xing", 34);
		simpleTable.put("zing", 77);
		simpleTable.put("aing", 20);
		simpleTable.put("qing", 34);
		simpleTable.put("wing", 64);

		assertEquals(14, simpleTable.size());

		// add one more
		simpleTable.put("minds", 4);
		assertEquals(simpleTable.capacity(), 59);
		assertEquals(simpleTable.size(), 15);
		simpleTable.put("xinsa", 23);
		assertEquals(16, simpleTable.size());
		assertEquals(simpleTable.capacity(), 59);
	}

	@Test
	void testPutOneElementEasyTable() {
		easyTable.put(100, 1);
		// size++
		assertEquals(1, easyTable.size());
		// should be in place 1
		assertTrue(easyTable.containsKey(100));
		assertTrue(easyTable.containsValue(1));
		assertEquals(1, easyTable.get(100));
		assertEquals(null, easyTable.get(101));
		assertFalse(easyTable.isEmpty());
	}

	@Test
	void testPutTwoElementEasyTable() {
		easyTable.put(100, 1);
		easyTable.put(101, 2);
		//
		assertEquals(2, easyTable.size());
		//
		assertTrue(easyTable.containsKey(100));
		assertEquals(1, easyTable.get(100));
		assertTrue(easyTable.containsKey(101));
		assertEquals(2, easyTable.get(101));
		assertTrue(easyTable.containsValue(1));
		assertTrue(easyTable.containsValue(2));
		assertEquals(null, easyTable.get(102));
		assertFalse(easyTable.isEmpty());
	}

	@Test
	void testPutTwoElementDuplacatedEasyTable() {
		easyTable.put(100, 1);
		easyTable.put(100, 2);
		//
		assertEquals(1, easyTable.size());
		//
		assertTrue(easyTable.containsKey(100));
		assertEquals(2, easyTable.get(100));
		assertFalse(easyTable.containsKey(101));
		//
		assertFalse(easyTable.containsValue(1));
		assertTrue(easyTable.containsValue(2));
		assertEquals(null, easyTable.get(101));
		assertFalse(easyTable.isEmpty());
	}

	@Test
	void testPutTwoElementRemoveEasyTable() {
		easyTable.put(100, 1);
		assertEquals(null, easyTable.remove(101));
		assertEquals(1, easyTable.remove(100));
		//
		assertEquals(0, easyTable.size());
		//
		assertFalse(easyTable.containsKey(100));
		assertTrue(easyTable.isEmpty());
	}

	@Test
	void testPutHitThresholdEasyTableBigger() {
		easyTable.put(100, 1);
		easyTable.put(101, 2);
		easyTable.put(102, 3);
		easyTable.put(103, 4);
		easyTable.put(104, 5);
		easyTable.put(105, 6);
		easyTable.put(106, 7);
		assertEquals(easyTable.capacity(), 29);
		easyTable.put(107, 8);
		easyTable.put(108, 9);
		easyTable.put(109, 10);
		easyTable.put(110, 11);
		easyTable.put(111, 12);
		easyTable.put(112, 13);
		easyTable.put(113, 14);

		assertEquals(14, easyTable.size());

		// add one more
		easyTable.put(114, 15);
		assertEquals(easyTable.capacity(), 59);
		assertEquals(easyTable.size(), 15);
		easyTable.put(115, 23);
		assertEquals(16, easyTable.size());
		assertEquals(easyTable.capacity(), 59);
	}

	@Test
	void testCleareasy() {
		easyTable.put(100, 1);
		easyTable.put(101, 2);
		easyTable.put(102, 3);
		easyTable.put(103, 4);
		easyTable.put(104, 5);
		easyTable.put(105, 6);
		easyTable.put(106, 7);
		easyTable.clear();
		assertEquals(0, easyTable.size());
		for (int i = 0; i < easyTable.capacity; i++) {
			assertEquals(null, easyTable.get(i));
		}

	}

	@Test
	void testList() {
		List<MapEntry<Integer, Integer>> list = new ArrayList<MapEntry<Integer, Integer>>();
		easyTable.put(100, 1);
		easyTable.put(101, 2);
		easyTable.put(102, 3);
		list.add(new MapEntry<Integer, Integer>(100, 1));
		list.add(new MapEntry<Integer, Integer>(101, 2));
		list.add(new MapEntry<Integer, Integer>(102, 3));
		assertEquals(list, easyTable.entries());
	}

}
