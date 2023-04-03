
package assign02;

import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

/**
 * This class contains tests for LibraryGeneric.
 * 
 * @author Erin Parker and Ling Lei, Drew lawson
 * @version September 2, 2020
 */
public class LibraryGenericTester {

	private LibraryGeneric<String> nameLib; // library that uses names to identify patrons (holders)
	private LibraryGeneric<PhoneNumber> phoneLib; // library that uses phone numbers to identify patrons
	private LibraryGeneric<Integer> numberLib; // library that uses integer to identify patrons
	private LibraryGeneric<StudentID> IDLib; // library that uses StudentID to identify patrons
	private LibraryGeneric<Integer> emptyLib;

	@BeforeEach
	void setUp() throws Exception {
		nameLib = new LibraryGeneric<String>();
		nameLib.add(9780374292799L, "Thomas L. Friedman", "The World is Flat");
		nameLib.add(9780330351690L, "Jon Krakauer", "Into the Wild");
		nameLib.add(9780446580342L, "David Baldacci", "Simple Genius");

		phoneLib = new LibraryGeneric<PhoneNumber>();
		phoneLib.add(9780374292799L, "Thomas L. Friedman", "The World is Flat");
		phoneLib.add(9780330351690L, "Jon Krakauer", "Into the Wild");
		phoneLib.add(9780446580342L, "David Baldacci", "Simple Genius");

		numberLib = new LibraryGeneric<Integer>();
		numberLib.add(9780374292799L, "Thomas L. Friedman", "The World is Flat");
		numberLib.add(9780330351690L, "Jon Krakauer", "Into the Wild");
		numberLib.add(9780446580342L, "David Baldacci", "Simple Genius");

		IDLib = new LibraryGeneric<StudentID>();
		IDLib.add(9780374292799L, "Thomas L. Friedman", "The World is Flat");
		IDLib.add(9780330351690L, "Jon Krakauer", "Into the Wild");
		IDLib.add(9780446580342L, "David Baldacci", "Simple Genius");

		emptyLib = new LibraryGeneric<Integer>();
	}

	@Test
	public void testNameLibCheckout() {
		String patron = "Jane Doe";
		assertTrue(nameLib.checkout(9780330351690L, patron, 1, 1, 2008));
		assertTrue(nameLib.checkout(9780374292799L, patron, 1, 1, 2008));
	}

	@Test
	public void testNameLibLookup() {
		String patron = "Jane Doe";
		nameLib.checkout(9780330351690L, patron, 1, 1, 2008);
		nameLib.checkout(9780374292799L, patron, 1, 1, 2008);
		ArrayList<LibraryBookGeneric<String>> booksCheckedOut = nameLib.lookup(patron);

		assertNotNull(booksCheckedOut);
		assertEquals(2, booksCheckedOut.size());
		assertTrue(booksCheckedOut.contains(new Book(9780330351690L, "Jon Krakauer", "Into the Wild")));
		assertTrue(booksCheckedOut.contains(new Book(9780374292799L, "Thomas L. Friedman", "The World is Flat")));
		assertEquals(patron, booksCheckedOut.get(0).getHolder());
		assertEquals(patron, booksCheckedOut.get(1).getHolder());
	}

	@Test
	public void testNameLibCheckin() {
		String patron = "Jane Doe";
		nameLib.checkout(9780330351690L, patron, 1, 1, 2008);
		nameLib.checkout(9780374292799L, patron, 1, 1, 2008);
		assertTrue(nameLib.checkin(patron));
	}

	@Test
	public void testNameLibGetInventory() {
		ArrayList<LibraryBookGeneric<String>> expected = new ArrayList<LibraryBookGeneric<String>>();
		expected.add(new LibraryBookGeneric<String>(9780330351690L, "Jon Krakauer", "Into the Wild"));
		expected.add(new LibraryBookGeneric<String>(9780374292799L, "Thomas L. Friedman", "The World is Flat"));
		expected.add(new LibraryBookGeneric<String>(9780446580342L, "David Baldacci", "Simple Genius"));
		assertEquals(expected, nameLib.getInventoryList());
	}

	@Test
	public void testNameLibGetOverDue() {
		String patron = "someone";
		nameLib.checkout(9780330351690L, patron, 1, 1, 2008);
		nameLib.checkout(9780374292799L, patron, 1, 1, 2008);
		ArrayList<LibraryBookGeneric<String>> expected = new ArrayList<LibraryBookGeneric<String>>();
		expected.add(new LibraryBookGeneric<String>(9780374292799L, "Thomas L. Friedman", "The World is Flat"));
		expected.add(new LibraryBookGeneric<String>(9780330351690L, "Jon Krakauer", "Into the Wild"));
		assertEquals(expected, nameLib.getOverdueList(1, 1, 2009));
	}

	@Test
	public void testNameLibCheckTitlesSorted() {
		LibraryBookGeneric<String> testBook1 = new LibraryBookGeneric<String>(9780330351690L, "Jon Krakauer",
				"Into the Wild");
		LibraryBookGeneric<String> testBook2 = new LibraryBookGeneric<String>(9780446580342L, "David Baldacci",
				"Simple Genius");
		LibraryBookGeneric<String> testBook3 = new LibraryBookGeneric<String>(9780374292799L, "Thomas L. Friedman",
				"The World is Flat");
		ArrayList<LibraryBookGeneric<String>> expected = new ArrayList<LibraryBookGeneric<String>>();
		expected.add(0, testBook1);
		expected.add(1, testBook2);
		expected.add(2, testBook3);

		ArrayList<LibraryBookGeneric<String>> actual = nameLib.getOrderedByTitle();

		for (int i = 0; i < 3; i++) {
			assertEquals(expected.get(i).getTitle(), actual.get(i).getTitle());
		}
	}

	@Test
	public void testPhoneLibCheckout() {
		PhoneNumber patron = new PhoneNumber("801.555.1234");
		assertTrue(phoneLib.checkout(9780330351690L, patron, 1, 1, 2008));
		assertTrue(phoneLib.checkout(9780374292799L, patron, 1, 1, 2008));
	}

	@Test
	public void testPhoneLibLookup() {
		PhoneNumber patron = new PhoneNumber("801.555.1234");
		phoneLib.checkout(9780330351690L, patron, 1, 1, 2008);
		phoneLib.checkout(9780374292799L, patron, 1, 1, 2008);
		ArrayList<LibraryBookGeneric<PhoneNumber>> booksCheckedOut = phoneLib.lookup(patron);

		assertNotNull(booksCheckedOut);
		assertEquals(2, booksCheckedOut.size());
		assertTrue(booksCheckedOut.contains(new Book(9780330351690L, "Jon Krakauer", "Into the Wild")));
		assertTrue(booksCheckedOut.contains(new Book(9780374292799L, "Thomas L. Friedman", "The World is Flat")));
		assertEquals(patron, booksCheckedOut.get(0).getHolder());
		assertEquals(patron, booksCheckedOut.get(1).getHolder());
	}

	@Test
	public void testPhoneLibCheckin() {
		PhoneNumber patron = new PhoneNumber("801.555.1234");
		phoneLib.checkout(9780330351690L, patron, 1, 1, 2008);
		phoneLib.checkout(9780374292799L, patron, 1, 1, 2008);
		assertTrue(phoneLib.checkin(patron));
	}

	// check Integer placeholder
	@Test
	public void testnumberLibCheckout() {
		Integer patron = 1;
		assertTrue(numberLib.checkout(9780330351690L, patron, 1, 1, 2008));
		assertTrue(numberLib.checkout(9780374292799L, patron, 1, 1, 2008));
	}

	@Test
	public void testnumberLibLookup() {
		Integer patron = 2;
		numberLib.checkout(9780330351690L, patron, 1, 1, 2008);
		numberLib.checkout(9780374292799L, patron, 1, 1, 2008);
		ArrayList<LibraryBookGeneric<Integer>> booksCheckedOut = numberLib.lookup(patron);

		assertNotNull(booksCheckedOut);
		assertEquals(2, booksCheckedOut.size());
		assertTrue(booksCheckedOut.contains(new Book(9780330351690L, "Jon Krakauer", "Into the Wild")));
		assertTrue(booksCheckedOut.contains(new Book(9780374292799L, "Thomas L. Friedman", "The World is Flat")));
		assertEquals(patron, booksCheckedOut.get(0).getHolder());
		assertEquals(patron, booksCheckedOut.get(1).getHolder());
	}

	@Test
	public void testnumberLibCheckin() {
		Integer patron = 3;
		numberLib.checkout(9780330351690L, patron, 1, 1, 2008);
		numberLib.checkout(9780374292799L, patron, 1, 1, 2008);
		assertTrue(numberLib.checkin(patron));
	}

	@Test
	public void testnumberLibCheckDueDatesSorted() {
		String person = "Guy";

		LibraryBookGeneric<String> testBook1 = new LibraryBookGeneric<String>(9780446580342L, "David Baldacci",
				"Simple Genius");
		LibraryBookGeneric<String> testBook2 = new LibraryBookGeneric<String>(9780330351690L, "Jon Krakauer",
				"Into the Wild");
		ArrayList<LibraryBookGeneric<String>> expected = new ArrayList<LibraryBookGeneric<String>>();
		expected.add(0, testBook1);
		expected.add(1, testBook2);

		nameLib.checkout(9780330351690L, person, 1, 1, 2008);
		nameLib.checkout(9780374292799L, person, 3, 3, 2010);
		nameLib.checkout(9780446580342L, person, 4, 4, 2007);
		ArrayList<LibraryBookGeneric<String>> actual = nameLib.getOverdueList(2, 2, 2009);

		assertEquals(expected.get(0).getIsbn(), actual.get(0).getIsbn());
		assertEquals(expected.get(1).getIsbn(), actual.get(1).getIsbn());
	}

	@Test
	public void testnumberLibCheckTitlesSorted() {
		LibraryBookGeneric<String> testBook1 = new LibraryBookGeneric<String>(9780330351690L, "Jon Krakauer",
				"Into the Wild");
		LibraryBookGeneric<String> testBook2 = new LibraryBookGeneric<String>(9780446580342L, "David Baldacci",
				"Simple Genius");
		LibraryBookGeneric<String> testBook3 = new LibraryBookGeneric<String>(9780374292799L, "Thomas L. Friedman",
				"The World is Flat");
		ArrayList<LibraryBookGeneric<String>> expected = new ArrayList<LibraryBookGeneric<String>>();
		expected.add(0, testBook1);
		expected.add(1, testBook2);
		expected.add(2, testBook3);

		ArrayList<LibraryBookGeneric<String>> actual = nameLib.getOrderedByTitle();

		for (int i = 0; i < 3; i++) {
			assertEquals(expected.get(i).getTitle(), actual.get(i).getTitle());
		}
	}

	// check StudentID placeholder
	@Test
	public void testIDLibLoopUpIsbn() {
		IDLib.checkout(9780330351690L, new StudentID("09876"), 12, 26, 2000);
		StudentID expected = new StudentID("09876");
		assertEquals(expected.toString(), IDLib.lookup(9780330351690L).toString());
	}

	@Test
	public void testIDLibLoopUpID() {
		StudentID id = new StudentID("03876457638980");
		IDLib.checkout(9780330351690L, id, 12, 26, 2000);
		ArrayList<LibraryBookGeneric<StudentID>> expected = new ArrayList<LibraryBookGeneric<StudentID>>();
		expected.add(new LibraryBookGeneric<StudentID>(9780330351690L, "Jon Krakauer", "Into the Wild"));
		assertEquals(expected, IDLib.lookup(id));
	}

	@Test
	public void testIDLibCheckout() {
		StudentID id = new StudentID("09876543");
		assertTrue(IDLib.checkout(9780330351690L, id, 12, 1, 2008));
		assertFalse(IDLib.checkout(23456, id, 3, 1, 1998));
	}

	@Test
	public void testIDLibCheckInISBN() {
		StudentID id = new StudentID("12345678098765432");
		assertFalse(IDLib.checkin(9780330351690L));
		IDLib.checkout(9780330351690L, id, 10, 23, 2003);
		assertTrue(IDLib.checkin(9780330351690L));
	}

	@Test
	public void testIDLibCheckInHolder() {
		StudentID id = new StudentID("1287612");
		assertFalse(IDLib.checkin(id));
		IDLib.checkout(9780330351690L, id, 10, 23, 2003);
		assertTrue(IDLib.checkin(id));
	}

	@Test
	public void testIDLibGetInventory() {
		ArrayList<LibraryBookGeneric<StudentID>> expected = new ArrayList<LibraryBookGeneric<StudentID>>();
		expected.add(new LibraryBookGeneric<StudentID>(9780330351690L, "Jon Krakauer", "Into the Wild"));
		expected.add(new LibraryBookGeneric<StudentID>(9780374292799L, "Thomas L. Friedman", "The World is Flat"));
		expected.add(new LibraryBookGeneric<StudentID>(9780446580342L, "David Baldacci", "Simple Genius"));
		assertEquals(expected, IDLib.getInventoryList());
	}

	@Test
	public void testIDLibGetDueDate() {
		StudentID id = new StudentID("73247");
		ArrayList<LibraryBookGeneric<StudentID>> expected = new ArrayList<LibraryBookGeneric<StudentID>>();
		expected.add(new LibraryBookGeneric<StudentID>(9780330351690L, "Jon Krakauer", "Into the Wild"));
		IDLib.checkout(9780330351690L, id, 12, 12, 1997);
		IDLib.checkout(9780374292799L, id, 12, 12, 2008);
		IDLib.checkout(9780446580342L, id, 12, 12, 2007);
		assertEquals(expected, IDLib.getOverdueList(12, 12, 2000));
	}

	@Test
	public void testIDLibCheckTitlesSorted() {
		LibraryBookGeneric<StudentID> testBook1 = new LibraryBookGeneric<StudentID>(9780330351690L, "Jon Krakauer",
				"Into the Wild");
		LibraryBookGeneric<StudentID> testBook2 = new LibraryBookGeneric<StudentID>(9780446580342L, "David Baldacci",
				"Simple Genius");
		LibraryBookGeneric<StudentID> testBook3 = new LibraryBookGeneric<StudentID>(9780374292799L,
				"Thomas L. Friedman", "The World is Flat");
		ArrayList<LibraryBookGeneric<StudentID>> expected = new ArrayList<LibraryBookGeneric<StudentID>>();
		expected.add(0, testBook1);
		expected.add(1, testBook2);
		expected.add(2, testBook3);

		ArrayList<LibraryBookGeneric<StudentID>> actual = IDLib.getOrderedByTitle();

		for (int i = 0; i < 3; i++) {
			assertEquals(expected.get(i).getTitle(), actual.get(i).getTitle());
		}
	}

	@Test
	public void testEmptyLib() {
		assertEquals(null, emptyLib.lookup(1345l));
		assertFalse(emptyLib.checkout(12345, 234, 1, 2, 2009));
		assertFalse(emptyLib.checkin(12345));
		assertFalse(emptyLib.checkin(1234567));
	}
}
