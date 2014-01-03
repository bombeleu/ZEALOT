using UnityEngine;
using System.Collections;

public class ThrusterAnimationControl : MonoBehaviour {
	
	//public bool thrustGlowOn;
	
	public bool On = false;
	public float thrustAmount = 0;
		
	// Use this for initialization
	void Start () {
		this.particleSystem.renderer.sortingLayerName = "Particles";
	}

	public void SetThrustAnimsOn()
	{
		On = true;	
	}
	public void SetThrustAnimsOff()
	{
		this.particleSystem.startSpeed = 0;
		On = false;
	}
	
	public void SetThrustAnimsAmout(float t)
	{
		thrustAmount = t;
	}
	
	// Update is called once per frame
	void Update () {

		this.particleSystem.startSpeed = thrustAmount;

		if(On == false)
		{
			this.particleSystem.startSpeed = 0;
		}
	
	}
}
