using UnityEngine;
using System.Collections;

public class ThrusterAnimationControl : MonoBehaviour {
	
	//public bool thrustGlowOn;
	
	public bool On = false;
	public float thrustAmount = 0;
	public GameObject AttachedLight;
		
	// Use this for initialization
	void Start () {
	
	}

	public void SetThrustAnimsOn()
	{
		On = true;	
		//this.particleSystem.startSpeed = 1;
		AttachedLight.active = true;
	}
	public void SetThrustAnimsOff()
	{
		this.particleSystem.startSpeed = 0;
		AttachedLight.active = false;
		On = false;
	}
	
	public void SetThrustAnimsAmout(float t)
	{
		this.particleSystem.startSpeed = t;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(On == false)
		{
			this.particleSystem.startSpeed = 0;
		}
	
	}
}
