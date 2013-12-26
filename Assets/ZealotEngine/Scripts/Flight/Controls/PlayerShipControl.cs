using UnityEngine;
using System.Collections;

 
// Put this on a rigidbody object and instantly
// have 2D spaceship controls like OverWhelmed Arena
// that you can tweak to your heart's content.
 
[RequireComponent (typeof (Rigidbody))]
public class PlayerShipControl : MonoBehaviour
{
    
	//public GameObject bullet;
	
    public float forwardThrust = 5000F;
    public float backwardThrust = 2500F;
    public float turnSpeed = 8000F;
    public float mass = 5F;
 
    // positional drag
    public float sqrdSpeedThresholdForDrag = 25F;
    public float superDrag = 2F;
    public float fastDrag = 0.99F;
    public float slowDrag = 0.90F;
 
    // angular drag
    public float sqrdAngularSpeedThresholdForDrag = 5F;
    public float superADrag = 32F;
    public float fastADrag = 16F;
    public float slowADrag = 0.1F;
 
    public bool playerControl = true;
	public bool brake = false;
	
	public GameObject[] Thrusters;
	
 
      void SetPlayerControl(bool control)
    {
        playerControl = control;
    }
 
    void Start()
    {
        rigidbody.mass = mass;
		//bullet = GameObject.FindGameObjectWithTag("bullet");
    }
 
    void FixedUpdate()
    {
		transform.position = new Vector3(transform.position.x,transform.position.y,0);
        if (Mathf.Abs(thrust) > 0.01F)
        {
            if (rigidbody.velocity.sqrMagnitude > sqrdSpeedThresholdForDrag)
                rigidbody.drag = fastDrag;
            else
                rigidbody.drag = slowDrag;
        }
        else
            rigidbody.drag = superDrag;
 
        if (Mathf.Abs(turn) > 0.01F)
        {
            if (rigidbody.angularVelocity.sqrMagnitude > sqrdAngularSpeedThresholdForDrag)
                rigidbody.angularDrag = fastADrag;
            else
                rigidbody.angularDrag = slowADrag;
        }
        else
            rigidbody.angularDrag = superADrag;
 
        //transform.position = Vector3.Lerp(transform.position.x,transform.position.y, transform.position.z);
 
       /* Vector3 rotation = transform.rotation.eulerAngles;
        rotation *= Mathf.Deg2Rad;
        rotation.x = 0F;
        rotation.z = 0F;
       // rotation += bankAxis * bank;
        //transform.rotation = Quaternion.EulerAngles(rotation);
         rotation *= Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(rotation);*/
    }
 
    float thrust = 0F;
    float turn = 0F;
 
    void Thrust(float t)
    {
        thrust = Mathf.Clamp(t, -1F, 1F);
    }
 
    void Turn(float t)
    {
        turn = Mathf.Clamp(t, -1F, 1F) * turnSpeed;
    }
 
    //bool thrustGlowOn = false;
 
    void Update ()
    {
        float theThrust = thrust;
		
		
        if (playerControl)
        {
            thrust = Input.GetAxis("Vertical");
            turn = Input.GetAxis("Horizontal") * turnSpeed;
			brake = Input.GetKey(KeyCode.LeftControl);
        }
 
        if (thrust > 0F)
        {
            theThrust *= forwardThrust;
           
             
                Thrusters[0].SendMessage("SetThrustAnimsOn", SendMessageOptions.DontRequireReceiver);
				Thrusters[1].SendMessage("SetThrustAnimsOn", SendMessageOptions.DontRequireReceiver);
				Thrusters[2].SendMessage("SetThrustAnimsOn", SendMessageOptions.DontRequireReceiver);
				
				Thrusters[0].SendMessage("SetThrustAnimsAmout",thrust, SendMessageOptions.DontRequireReceiver);
		}
        else
        {
            theThrust *= backwardThrust;
           
                //Retro thrusters later;
               	Thrusters[0].SendMessage("SetThrustAnimsOff", SendMessageOptions.DontRequireReceiver);
				Thrusters[1].SendMessage("SetThrustAnimsOff", SendMessageOptions.DontRequireReceiver);
				Thrusters[2].SendMessage("SetThrustAnimsOff", SendMessageOptions.DontRequireReceiver);
            
        }
		if(turn < 0.5f && turn > 0.5f)
		{
				Thrusters[1].SendMessage("SetThrustAnimsAmout",0.5f, SendMessageOptions.DontRequireReceiver);
				Thrusters[2].SendMessage("SetThrustAnimsAmout",0.5f, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			float turnClamped = Mathf.Clamp(turn,-1,1);
			if(turnClamped>0)
			{
				Thrusters[1].SendMessage("SetThrustAnimsAmout",turnClamped, SendMessageOptions.DontRequireReceiver);
				Thrusters[2].SendMessage("SetThrustAnimsAmout",0.1f, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				Thrusters[1].SendMessage("SetThrustAnimsAmout",0.1f, SendMessageOptions.DontRequireReceiver);
				Thrusters[2].SendMessage("SetThrustAnimsAmout",-turnClamped, SendMessageOptions.DontRequireReceiver);
			}
		}
		if(brake)
		{
			superDrag = 2.5f;
			Thrusters[3].SendMessage("SetThrustAnimsOn", SendMessageOptions.DontRequireReceiver);
			Thrusters[4].SendMessage("SetThrustAnimsOn", SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			Thrusters[3].SendMessage("SetThrustAnimsOff", SendMessageOptions.DontRequireReceiver);
			Thrusters[4].SendMessage("SetThrustAnimsOff", SendMessageOptions.DontRequireReceiver);
			superDrag = 0;
		}
 
        rigidbody.gameObject.transform.Rotate(new Vector3(0,0,-1f) , turn * Time.deltaTime);
        rigidbody.AddRelativeForce(Vector3.up * theThrust * Time.deltaTime);
    }
}