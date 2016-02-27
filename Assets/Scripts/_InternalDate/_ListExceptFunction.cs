using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

/// <summary>
/// _ double list except function.
/// </summary>
public class _ListExceptFunction : MonoBehaviour {

	List<double> numbers1 = new List<double>{ 2.0d, 2.1d, 2.2d, 2.3d, 2.4d, 2.5d };
	List<double> numbers2 = new List<double>{ 2.2d };

	List<Person> persons1 = new List<Person>(){
		new Person(){id = 5, name = "Robin"},
		new Person(){id = 2, name = "Eva"},
		new Person(){id = 4, name = "Whatever"},
		new Person(){id = 0, name = "Robinn Wielloms"},
		new Person(){id = 8, name = "Somebody Else"}
	};

	List<Person> persons2 = new List<Person>(){
		new Person(){id = 8, name = "Somebody Else"},
		new Person(){id = 2, name = "Eva"},
		new Person(){id = 6, name = "Somebody"}
	};
	// Use this for initialization
	void Start () {
//		IEnumerable<double> onlyInFirstSet = numbers1..Except(numbers2);
		
//		foreach (double number in numbers1.FindAll( delegate(double x){return numbers2.Contains(x);}))
//		foreach (double number in numbers1.FindAll( x => !numbers2.Contains(x)))
//			Debug.Log(number);
		foreach(Person p in persons1)
			Debug.Log (p.id + ", " + p.name);
		Debug.Log ("Sort....");
		persons1.Sort();
		foreach(Person p in persons1)
			Debug.Log (p.id + ", " + p.name);

		foreach (Person p in persons1.FindAll( p => persons2.Contains(p)))
					Debug.Log(p.name);
//			Console.WriteLine(number);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class Person : IEquatable<Person>, IComparable<Person>{
	public int id{set; get;}
	public string name{set; get;}

	public Person(){}

	public override bool Equals(object obj){
		if(obj == null) return false;
		Person p = obj as Person;
		if(p == null) return false;
		return Equals (p);

	}

	public bool Equals(Person other){
		return this != null & other != null & id == other.id & name == other.name;
	}

	public override int GetHashCode()
	{
		
		// Get the hash code for the Name field if it is not null. 
		int hashID = id.GetHashCode();
		
		// Get the hash code for the Code field. 
		int hashNameCode = name == null ? 0 : name.GetHashCode();
		
		// Calculate the hash code for the product. 
		return hashID ^ hashNameCode;
	}

	public int CompareTo(Person other){
		if(other == null) return 1;
		return this.id.CompareTo (other.id);
	}

}