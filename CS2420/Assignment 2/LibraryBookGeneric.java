package assign02;

import java.util.GregorianCalendar;

/**
 * This class represents libraryBook's holder in generic way
 * 
 * @author Ling Lei and Drew Lawson
 *
 * @param <T>
 */
public class LibraryBookGeneric<T> extends Book {
	// unique variables in class LibraryBook
	private T placeHolder;

	private GregorianCalendar dueDate;

	private boolean checkOut;

	/**
	 * This is constructor for LibraryBook
	 * 
	 * @param isbn
	 * @param author
	 * @param title
	 */
	public LibraryBookGeneric(long isbn, String author, String title) {
		super(isbn, author, title);
		// by default setup placeHolder and dueDate
		placeHolder = null;
		dueDate = null;
		checkOut = false;
	}

	/**
	 * Accessor method for the placeHolder field
	 * 
	 * @return the placeHolder
	 */
	public T getHolder() {
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
	public void setCheckOut(T holder, GregorianCalendar due) {
		placeHolder = holder;
		dueDate = due;
		checkOut = true;
	}

	public boolean getCheckStatus() {
		return checkOut;
	}

	/**
	 * This method overrides toString method in Book class, adding check status.
	 *
	 */
	@Override
	public String toString() {
		return this.getIsbn() + ", " + this.getAuthor() + ", \"" + this.getTitle() + "\"" + placeHolder + dueDate;
	}

}