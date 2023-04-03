package assign06;
/** 
 * Tests for WebBrowser
 * 
 * @author Drew Lawson
 * @version October 20th, 2021
 *
 */
import static org.junit.Assert.assertTrue;
import static org.junit.jupiter.api.Assertions.assertEquals;
import java.util.Arrays;
import java.util.Iterator;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import java.net.MalformedURLException;
import java.net.URL;
import org.junit.jupiter.api.BeforeEach;

class WebBrowserTest {
	private WebBrowser testBrowser;
	private SinglyLinkedList<URL> listTest;
	private WebBrowser testBrowser2;
	
	@BeforeEach
	void setUp() {
		try {
			listTest = new SinglyLinkedList<URL>();
			
			listTest.insertFirst(new URL("https://www.a.com"));
			listTest.insertFirst(new URL("https://www.b.com"));
			listTest.insertFirst(new URL("https://www.c.com"));
			listTest.insertFirst(new URL("https://www.d.com"));
			listTest.insertFirst(new URL("https://www.e.com"));
			  } catch (MalformedURLException e) {
			   e.printStackTrace();
			  }
		
		
		
		testBrowser = new WebBrowser(listTest);
		testBrowser2 = new WebBrowser();
		}
	
	@Test
	public void testVisitAndBackAndForwards() {
			try {
				testBrowser.visit(new URL("https://www.valve.com"));
				testBrowser.visit(new URL("https://www.sega.com"));
				testBrowser.visit(new URL("https://www.nintendo.com"));
				testBrowser.back();
				testBrowser.back();
				testBrowser.back();
				testBrowser.forward();
				testBrowser.forward();
				SinglyLinkedList<URL> actual = testBrowser.history();
				assertEquals("https://www.sega.com", actual.getFirst().toString());
			} catch (MalformedURLException e) {
				e.printStackTrace();
			}
	}
	@Test
			public void testHistory() {
				try {
				testBrowser.visit(new URL("https://www.f.com"));
					testBrowser.visit(new URL("https://www.g.com"));
					testBrowser.visit(new URL("https://www.h.com"));
					testBrowser.visit(new URL("https://www.i.com"));
					testBrowser.visit(new URL("https://www.j.com"));
					testBrowser.visit(new URL("https://www.k.com"));
					testBrowser.visit(new URL("https://www.l.com"));                                                                         
					testBrowser.back();
					testBrowser.back();
					testBrowser.back();
					testBrowser.back();
					testBrowser.forward();
					testBrowser.forward();
					testBrowser.visit(new URL("https://www.m.com"));
					SinglyLinkedList<URL> actual = testBrowser.history();
					assertEquals("https://www.m.com", actual.getFirst().toString());
					assertEquals("https://www.j.com", actual.get(1).toString());
					assertEquals("https://www.i.com", actual.get(2).toString());
					assertEquals("https://www.h.com", actual.get(3).toString());
					assertEquals("https://www.g.com", actual.get(4).toString());
					assertEquals("https://www.f.com", actual.get(5).toString());
					assertEquals("https://www.e.com", actual.get(6).toString());
					assertEquals("https://www.d.com", actual.get(7).toString());
					assertEquals("https://www.c.com", actual.get(8).toString());
					assertEquals("https://www.b.com", actual.get(9).toString());
					assertEquals("https://www.a.com", actual.get(10).toString());
				} catch (MalformedURLException e) {
					e.printStackTrace();
				}
				
			}
			@Test
				public void testVisit() {
					try {
						testBrowser2.visit(new URL("https://www.valve.com"));
						testBrowser2.visit(new URL("https://www.sega.com"));
						testBrowser2.visit(new URL("https://www.nintendo.com"));
						SinglyLinkedList<URL> actual = testBrowser2.history();
						assertEquals("https://www.nintendo.com", actual.getFirst().toString());
						assertEquals("https://www.sega.com", actual.get(1).toString());
						assertEquals("https://www.valve.com", actual.get(2).toString());
					} catch (MalformedURLException e) {
						e.printStackTrace();
					}
				
	}
			
}

