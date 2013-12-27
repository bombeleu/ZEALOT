using UnityEngine;
using System.Collections;


public class Planet : MonoBehaviour
{
	public bool CoreWorld = false;	
	public float diameter;	//Diameter of the Planet
	public float dayLength;	//Day length on planet in relation to Earth days
	public float yearLength; //Year length on planet in Earth days
	public float gravity; 	//decimal comparison of gravity compared to Earth gravity.
	public float avgTemp;		//Average surface temperature in °C
	public int moons;		//Number of moons orbiting the planet
	public string name;		//Planet Name
	public float Xpos;		//Galactic X position; 			//NOTE - Change during runtime in orbit?
	public float Ypos;		//Galactic Y position;

	public PoliticalSystemType politics;	//Political System, linked to an enum with the full list.
	public ReputationType rep;	//Reputation amongst planets (affects hostility/size if above certain tech)
	public TechLevel tech;		//Technical evolution level
	
	public GameObject Graphic;	//Planet's Graphical display surface;
	public Material PlanetMat;	//Planet's material
	public Texture2D PlanetTex;	  //planet texture


    private Texture2D[] m_textures = new Texture2D[3];
    public int resolution = 128; 
    
    public float zoom = 1f; 
    public float offset = 0f; 
	
	public string[] planetSyllableList1 = {};
	public string[] planetSyllableList2 = {};
	public string[] planetSyllableList3 = {};
	
	public float randomF;
	
	// Use this for initialization
	void Start ()
	{
		/**
		 * Randomisations
		 * */
		if (CoreWorld == false) {
						setDiameter ();
						setDayLength ();
						setYearLength ();
						setGravity ();
						setAvgTemp ();
						setName ();
						setPolitics ();
				}
			

	}
	void setDiameter()
	{
		diameter = Graphic.transform.localScale.x + Random.Range(-2.796f,2.165f);
		diameter = diameter* 5000;
	}
	void setDayLength()
	{
		dayLength = Random.Range(6f,150f);
	}
	void setYearLength()
	{
		yearLength = Random.Range(35f,10005f);
	}
	void setGravity()
	{
		gravity = Random.Range(0.15f,50f);
	}
	void setAvgTemp()
	{
		avgTemp = Random.Range(-254f,1000.0f);
		/*bool minus;
		int i = Random.Range(0,1);
		if(i == 1)
		{avgTemp = 0-avgTemp;}*/
	}
	void setName(){
		name += planetSyllableList1[Random.Range(0,planetSyllableList1.Length)] 
		+ planetSyllableList2[Random.Range(0,planetSyllableList2.Length)]
		+ planetSyllableList3[Random.Range(0,planetSyllableList3.Length)];
	}
	void setPolitics()
	{
	    int rand = Random.Range(0,15);
		switch(rand)
		{
		case 0:
			politics = PoliticalSystemType.Anarchy;
			break;
		case 1:
			politics = PoliticalSystemType.Capitalist;
			break;
		case 2:
			politics = PoliticalSystemType.Communist;
			break;
		case 3:
			politics = PoliticalSystemType.Confederacy;
			break;
		case 4:
			politics = PoliticalSystemType.Corporate;
			break;
		case 5:
			politics = PoliticalSystemType.Cybernetic;
			break;
		case 6:
			politics = PoliticalSystemType.Democracy;
			break;
		case 7:
			politics = PoliticalSystemType.Dictatorship;
			break;
		case 8:
			politics = PoliticalSystemType.Fascist;
			break;
		case 9:
			politics = PoliticalSystemType.Feudal;
			break;
		case 10:
			politics = PoliticalSystemType.Military;
			break;
		case 11:
			politics = PoliticalSystemType.Monarchy;
			break;
		case 12: 
			politics = PoliticalSystemType.Pacifist;
			break;
		case 13:
			politics = PoliticalSystemType.Satori;
			break;
		case 14:
			politics = PoliticalSystemType.Socialist;
			break;
		case 15:
			politics = PoliticalSystemType.Technocracy;
			break;
		case 16:
			politics = PoliticalSystemType.Theocracy;
			break;
		default: politics = PoliticalSystemType.Anarchy;	
			break;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		Graphic.transform.Rotate(transform.forward,2.36f/dayLength);
		
	    
	}
}

