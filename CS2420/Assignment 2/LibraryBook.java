package assign02;

import java.util.GregorianCalendar;

/**
 * This is LibraryBook class which extend Book class, specifying book behaviors
 * in library
 * 
 * @author Ling Lei and Drew Lawson
 * @version September 8, 2020
 */
public class LibraryBook extends Book {
	// unique variables in class LibraryBook
	private String placeHolder;

	private GregorianCalendar dueDate;

	private boolean checkOut;

	/**
	 * This is constructor for LibraryBook
	 * 
	 * @param isbn
	 * @param author
	 * @param title
	 */
	public LibraryBook(long isbn, String author, String title) {
		super(isbn, author, title);
		// by default setup placeHolder, dueDate, and checkOut
		placeHolder = null;
		dueDate = null;
		checkOut = false;
	}

	/**
	 * Accessor method for the placeHolder field
	 * 
	 * @return the placeHolder
	 */
	public String getHolder() {
		return placeHolder;
	}

	/**
	 * Accessor method for the due date
	 * 
	 * @return the due date
	 */
	public GregorianCalendar getDueDate() {
		return dueDate;
	}

	/**
	 * This method changes placeHolder and dueDate when checkIn happens
	 * 
	 * @return void
	 */
	public void setCheckIn() {
		placeHolder = null;
		dueDate = null;
		checkOut = false;
	}

	/**
	 * This method changes placeHolder and dueDate when checkOut happens
	 * 
	 * @param Holder
	 * @param due
	 * @return void
	 */
	public void setCheckOut(String holder, GregorianCalendar due) {
		placeHolder = holder;
		dueDate = due;
		checkOut = true;
	}

	/**
	 * Accessor method for the checkOut status
	 * 
	 * @return checkOut
	 */
	public boolean getCheckStatus() {
		return checkOut;
	}

	/**
	 * This method overrides toString method in Book class, adding check status.
	 * 
	 * @return String
	 */
	@Override
	public String toString() {
		return this.getIsbn() + ", " + this.getAuthor() + ", \"" + this.getTitle() + "\"" + placeHolder + dueDate;
	}

}