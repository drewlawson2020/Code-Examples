package assign02;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertNull;
import static org.junit.Assert.assertTrue;
import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;

/**
 * This class contains tests for Library.
 * 
 * @author Erin Parker and Ling Lei, Drew Lawson
 * @version September 2, 2020
 */
public class LibraryTester {

	private Library emptyLib, smallLib, mediumLib;

	@BeforeEach
	void setUp() throws Exception {
		emptyLib = new Library();

		smallLib = new Library();
		smallLib.add(9780374292799L, "Thomas L. Friedman", "The World is Flat");
		smallLib.add(9780330351690L, "Jon Krakauer", "Into the Wild");
		smallLib.add(9780446580342L, "David Baldacci", "Simple Genius");

		mediumLib = new Library();
		mediumLib.addAll("src/assign02/Mushroom_Publishing.txt");

	}

	// test empty library
	@Test
	public void testEmptyLookupISBN1() {
		assertNull(emptyLib.lookup(978037429279L));
	}

	@Test
	public void testEmptyLookupISBN2() {
		assertNull(emptyLib.lookup(123456));
	}

	@Test
	public void testEmptyLookupHolder1() {
		ArrayList<LibraryBook> booksCheckedOut = emptyLib.lookup("Jane Doe");
		assertNotNull(booksCheckedOut);
		assertEquals(0, booksCheckedOut.size());
	}

	@Test
	public void testEmptyLookupHolder2() {
		ArrayList<LibraryBook> booksCheckedOut = emptyLib.lookup("AB");
		assertNotNull(booksCheckedOut);
		assertEquals(0, booksCheckedOut.size());
	}

	@Test
	public void testEmptyCheckout1() {
		assertFalse(emptyLib.checkout(978037429279L, "Jane Doe", 1, 1, 2008));
	}

	@Test
	public void testEmptyCheckout2() {
		assertFalse(emptyLib.checkout(123456, "AB", 1, 1, 2008));
	}

	@Test
	public void testEmptyCheckinISBN1() {
		assertFalse(emptyLib.checkin(978037429279L));
	}

	@Test
	public void testEmptyCheckinISBN2() {
		assertFalse(emptyLib.checkin(123456));
	}

	@Test
	public void testEmptyCheckinHolder1() {
		assertFalse(emptyLib.checkin("Jane Doe"));
	}

	@Test
	public void testEmptyCheckinHolder2() {
		assertFalse(emptyLib.checkin("Ling"));
	}

	// test small size library
	@Test
	public void testSmallLibraryLookupISBN1() {
		assertNull(smallLib.lookup(9780330351690L));
	}

	@Test
	public void testSmallLibraryLookupISBN2() {
		// check out one book
		smallLib.checkout(9780330351690L, "ling", 1, 1, 2008);
		assertEquals("ling", smallLib.lookup(9780330351690L));
		// then check in
		smallLib.checkin(9780330351690L);
		assertNull(smallLib.lookup(9780330351690L));
	}

	@Test
	public void testSmallLibraryLookupHolder1() {
		smallLib.checkout(9780330351690L, "Jane Doe", 1, 1, 2008);
		ArrayList<LibraryBook> booksCheckedOut = smallLib.lookup("Jane Doe");

		assertNotNull(booksCheckedOut);
		assertEquals(1, booksCheckedOut.size());
		assertEquals(new Book(9780330351690L, "Jon Krakauer", "Into the Wild"), booksCheckedOut.get(0));
		assertEquals("Jane Doe", booksCheckedOut.get(0).getHolder());
	}

	@Test
	public void testSmallLibraryCheckout1() {
		assertTrue(smallLib.checkout(9780330351690L, "Jane Doe", 1, 1, 2008));
	}

	@Test
	public void testSmallLibraryCheckout2() {
		assertTrue(smallLib.checkout(9780446580342L, "Ling", 1, 1, 2008));
	}

	@Test
	public void testSmallLibraryCheckout3() {
		assertFalse(smallLib.checkout(123, "Jane Doe", 1, 1, 2008));
	}

	@Test
	public void testSmallLibraryCheckinISBN1() {
		smallLib.checkout(9780330351690L, "Jane Doe", 1, 1, 2008);
		assertTrue(smallLib.checkin(9780330351690L));
	}

	@Test
	public void testSmallLibraryCheckinISBN2() {
		assertFalse(smallLib.checkin(9780330351690L));
	}

	@Test
	public void testSmallLibraryCheckinHolder1() {
		assertFalse(smallLib.checkin("John Smith"));
	}

	@Test
	public void testSmallLibraryCheckinHolder2() {
		smallLib.checkout(9780330351690L, "ling", 1, 1, 2008);
		assertTrue(smallLib.checkin("ling"));
	}

	// test medium size
	@Test
	public void testMediumLibraryLookupISBN1() {
		assertNull(mediumLib.lookup(1234));
	}

	@Test
	public void testMediumLibraryLookupISBN2() {
		// check out one book
		mediumLib.checkout(9781843190004L, "ling", 1, 1, 2008);
		assertEquals("ling", mediumLib.lookup(9781843190004L));
		// then check in
		mediumLib.checkin(9781843190004L);
		assertNull(mediumLib.lookup(9781843190004L));
	}

	@Test
	public void testMediumLibraryCheckout1() {
		assertTrue(mediumLib.checkout(9781843190011L, "Drew", 1, 1, 2008));
	}

	@Test
	public void testMediumLibraryCheckout3() {
		assertFalse(mediumLib.checkout(123, "Drew", 1, 1, 2008));
	}

	@Test
	public void testMediumLibraryCheckinISBN1() {
		mediumLib.checkout(9781843190028L, "Ling", 1, 1, 2008);
		assertTrue(mediumLib.checkin(9781843190028L));
	}

	@Test
	public void testMediumLibraryCheckinISBN2() {
		assertFalse(mediumLib.checkin(9781843190028L));
	}

	@Test
	public void testMediumLibraryCheckinHolder1() {
		assertFalse(mediumLib.checkin("Drew"));
	}

	@Test
	public void testMediumLibraryCheckinHolder2() {
		mediumLib.checkout(9781843190042L, "ling", 1, 1, 2008);
		assertTrue(mediumLib.checkin("ling"));
	}

	@Test
	public void testMediumLibraryLookupHolder() {
		mediumLib.checkout(9781843190004L, "Drew", 1, 1, 2008);
		ArrayList<LibraryBook> booksCheckedOut = mediumLib.lookup("Drew");

		assertNotNull(booksCheckedOut);
		assertEquals(1, booksCheckedOut.size());
		assertEquals(new Book(9781843190004L, "Moyra Caldecott", "Weapons of the Wolfhound"), booksCheckedOut.get(0));
		assertEquals("Drew", booksCheckedOut.get(0).getHolder());
	}

}