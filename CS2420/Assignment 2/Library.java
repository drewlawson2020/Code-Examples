package assign02;

import java.io.File;
import java.io.FileNotFoundException;
import java.text.ParseException;
import java.util.ArrayList;
import java.util.GregorianCalendar;
import java.util.Scanner;

/**
 * This class represents a library, which is a collection of library books.
 * 
 * @author Erin Parker and Ling Lei, Drew Lawson
 * @version September 2, 2020
 */
public class Library {

	private ArrayList<LibraryBook> library;

	/**
	 * Creates an empty library.
	 */
	public Library() {
		library = new ArrayList<LibraryBook>();
	}

	/**
	 * Adds to the library the book, specified by the given ISBN, author, and title.
	 * Assumes there is no possibility of duplicate library books.
	 * 
	 * @param isbn   - ISBN of the book to be added
	 * @param author - author of the book to be added
	 * @param title  - title of the book to be added
	 */
	public void add(long isbn, String author, String title) {
		library.add(new LibraryBook(isbn, author, title));
	}

	/**
	 * Add the list of library books to the library. Assumes there is no possibility
	 * of duplicate library books.
	 * 
	 * @param list - list of library books to be added
	 */
	public void addAll(ArrayList<LibraryBook> list) {
		library.addAll(list);
	}

	/**
	 * Adds the the library the books specified by the input file. Assumes the input
	 * files specifies one book per line with ISBN, author, and title separated by
	 * tabs.
	 * 
	 * If file does not exist or format is violated, prints an error message and
	 * does not change the library.
	 * 
	 * @param filename
	 */
	public void addAll(String filename) {
		ArrayList<LibraryBook> toBeAdded = new ArrayList<LibraryBook>();

		try {
			Scanner fileIn = new Scanner(new File(filename));
			int lineNum = 1;

			while (fileIn.hasNextLine()) {
				String line = fileIn.nextLine();

				Scanner lineIn = new Scanner(line);
				lineIn.useDelimiter("\\t");

				if (!lineIn.hasNextLong()) {
					lineIn.close();
					throw new ParseException("ISBN", lineNum);
				}
				long isbn = lineIn.nextLong();

				if (!lineIn.hasNext()) {
					lineIn.close();
					throw new ParseException("Author", lineNum);
				}
				String author = lineIn.next();

				if (!lineIn.hasNext()) {
					lineIn.close();
					throw new ParseException("Title", lineNum);
				}
				String title = lineIn.next();

				toBeAdded.add(new LibraryBook(isbn, author, title));

				lineNum++;
				lineIn.close();
			}
			fileIn.close();
		} catch (FileNotFoundException e) {
			System.err.println(e.getMessage() + " Nothing added to the library.");
			return;
		} catch (ParseException e) {
			System.err.println(e.getLocalizedMessage() + " formatted incorrectly at line " + e.getErrorOffset()
					+ ". Nothing added to the library.");
			return;
		}

		library.addAll(toBeAdded);
	}

	/**
	 * Returns the holder of the library book with the specified ISBN.
	 * 
	 * If no book with the specified ISBN is in the library, returns null.
	 * 
	 * @param isbn - ISBN of the book to be looked up
	 * @return String of holder's representation
	 */
	public String lookup(long isbn) {
		for (LibraryBook book : library) {
			// check ISBN of each libraryBook
			if (book.getIsbn() == isbn)
				return book.getHolder();
		}
		return null;
	}

	/**
	 * Returns the list of library books checked out to the specified holder.
	 * 
	 * If the specified holder has no books checked out, returns an empty list.
	 * 
	 * @param holder - holder whose checked out books are returned
	 * @return holderBook - which is ArrayList of library books
	 */
	public ArrayList<LibraryBook> lookup(String holder) {
		ArrayList<LibraryBook> holderBook = new ArrayList<LibraryBook>();
		for (LibraryBook book : library) {
			// if holder equals null, skip current loop get into the next loop
			if (book.getHolder() == null)
				continue;
			if (book.getHolder().equals(holder))
				holderBook.add(book);
		}
		return holderBook;
	}

	/**
	 * Sets the holder and due date of the library book with the specified ISBN.
	 * 
	 * If no book with the specified ISBN is in the library, returns false.
	 * 
	 * If the book with the specified ISBN is already checked out, returns false.
	 * 
	 * Otherwise, returns true.
	 * 
	 * @param isbn   - ISBN of the library book to be checked out
	 * @param holder - new holder of the library book
	 * @param month  - month of the new due date of the library book
	 * @param day    - day of the new due date of the library book
	 * @param year   - year of the new due date of the library book
	 * 
	 */
	public boolean checkout(long isbn, String holder, int month, int day, int year) {
		LibraryBook findBook = null;
		// loop over all books in library, get target book
		for (LibraryBook book : library) {
			if (book.getIsbn() == isbn)
				findBook = book;
		}

		if (findBook == null)
			return false;
		if (findBook.getCheckStatus() == true)
			return false;
		else {
			// set the holder and due date of the library book
			findBook.setCheckOut(holder, new GregorianCalendar(year, month, day));
			return true;
		}

	}

	/**
	 * Unsets the holder and due date of the library book.
	 * 
	 * If no book with the specified ISBN is in the library, returns false.
	 * 
	 * If the book with the specified ISBN is already checked in, returns false.
	 * 
	 * Otherwise, returns true.
	 * 
	 * @param isbn - ISBN of the library book to be checked in
	 */
	public boolean checkin(long isbn) {
		LibraryBook findBook = null;

		for (LibraryBook book : library) {
			if (book.getIsbn() == isbn)
				findBook = book;
		}

		if (findBook == null)
			return false;
		if (findBook.getCheckStatus() == false)
			return false;
		else {
			findBook.setCheckIn();
			return true;
		}
	}

	/**
	 * Unsets the holder and due date for all library books checked out be the
	 * specified holder.
	 * 
	 * If no books with the specified holder are in the library, returns false;
	 * 
	 * Otherwise, returns true.
	 * 
	 * @param holder - holder of the library books to be checked in
	 */
	public boolean checkin(String holder) {
		LibraryBook findBook = null;
		for (LibraryBook book : library) {
			if (book.getHolder() == holder)
				findBook = book;
		}

		if (findBook == null)
			return false;

		else {
			findBook.setCheckIn();
			return true;
		}
	}
}