using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {
	
	public GameObject orbitTarget;
	public Vector3 targetposition;
	public Vector3 orbitDirection;
	public float orbitSpeed = 50f;
	public float rotateSpeed;
	public float minOrb;
	public float maxOrb;
	public float minRot;
	public float maxRot;
	private bool shouldExist = true;
	
	
	// Use this for initialization
	void Start () {
	
		//orbitSpeed = Random.Range(minOrb,maxOrb);
		//rotateSpeed = Random.Range(minRot,maxRot);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround(
			orbitTarget.transform.position,orbitDirection,orbitSpeed * Time.deltaTime
			);
	//	targetposition = new Vector3(orbitTarget.transform.position,Vector3.up,orbitSpeed * Time.deltaTime);
	//	Debug.Log (targetposition);
	}
	public void SetTarget(GameObject t)
	{
		orbitTarget = t;
	}
}