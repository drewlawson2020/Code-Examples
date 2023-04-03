package assign04;

public class Students {
	public String studentName;
	public int ID;

	public Students(String name, int num) {
		studentName = name;
		ID = num; 
	}
	
	public String getname() {
		return studentName;
	}
	
	public int getID() {
		return ID;
	}
}
