using UnityEngine;
using System.Collections;

public class ZealotEngine_Gen : MonoBehaviour {
	

	//Magic Constants
	public int planetMaxPop;
	public int shipsMaxPop;

	//Prefabs
	public GameObject PlanetPrefab;



	//Arrays
	public GameObject[] PlanetsArray;
	public GameObject[] ShipsArray;

	public GameObject[] Planets_CoreWorlds;


	
	// Use this for initialization
	void Start () {
	CoreWorlds();
	GeneratePlanets();
	}
	
	void CoreWorlds()
	{
		//Instantiate(PlanetPrefab,new Vector3(-6.5f,11.7f,0),Quaternion.identity);
	}

	void GeneratePlanets()
	{
		//For Loop generating planets

		//Instantiate(PlanetPrefab,new Vector3(x,y,0),Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
