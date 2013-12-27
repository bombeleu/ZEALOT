using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZealotEngine_Gen : MonoBehaviour {
	

	//Magic Constants
	public int planetMaxPop = 30;
	public int shipsMaxPop = 50;

	//Prefabs
	public GameObject PlanetPrefab;



	//Lists
	public List<GameObject> PlanetsInScene;
	public List<GameObject> ShipsInScene;
	
	public List<GameObject> Planets_CoreWorlds;

	//Variables
	public int planets;
	
	// Use this for initialization
	void Start () {
	//CoreWorlds();
	//GeneratePlanets();
	}
	
	void CoreWorlds()
	{
		//Instantiate(PlanetPrefab,new Vector3(-6.5f,11.7f,0),Quaternion.identity);
	}

	void GeneratePlanets()
	{
		//For Loop generating planets
		for(planets=0;planets < planetMaxPop; planets++)
		{
			float x = Random.Range(-1000,1000);
			float y = Random.Range(-1000,1000);
			GameObject Planet = (Instantiate(PlanetPrefab,new Vector3(x,y,0),Quaternion.identity)as GameObject);
			PlanetsInScene.Add(Planet);
			Planet.transform.parent = GameObject.FindWithTag("planetContainer").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
