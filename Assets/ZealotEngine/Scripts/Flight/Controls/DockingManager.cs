using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DockingManager : MonoBehaviour {
    bool docked;
    bool docking;
    bool undocking;
    float undockTimer = 5F;
    bool dockButton;
    float radius = 5.1F;
    GameObject currentDockedLocation;
    GameObject lastDockedLocation;
    Transform shipTransform;
    public float dockingSpeed = 0.01f;
    Vector3 targetPosition;
    Vector3 currentPosition;
    bool inDockingRange;
    Transform planetInDockingRange; 
    List<Collider2D> collidingObjects = new List<Collider2D> ();
    public GameObject dockUI;

    //TextStrings, temporary until localisation system;
    string dockUIString = "Press (P) to Dock";
    string undockUIString = "Press (P) to Undock";
    string dockingUIString = "Docking...";
    string undockingUIString = "Undocking...";

    //Temporary OnGUI for docking debugging;
    void OnGUI () {
        Vector2 boxsize = new Vector2 (24.0F, 10.0F);
        if (inDockingRange == false) {
            if (dockUI.activeSelf == true)
                dockUI.SetActive (false);
            return;
        }
        if (dockUI.activeSelf == false) {
            dockUI.SetActive (true);
        }
    }

    // Use this for initialization
    void Start () {
        shipTransform = this.gameObject.transform;
    }

    void OnTriggerEnter2D (Collider2D triggerCollider) {
        if (triggerCollider == null)
            return;
        collidingObjects.Add (triggerCollider);
    }
    void OnTriggerExit2D (Collider2D triggerCollider) {
        collidingObjects.Remove (triggerCollider);
    }

    // Update is called once per frame
    void Update () {
        if (collidingObjects.Count <= 0) {
            collidingObjects.Clear ();
            inDockingRange = false;
            return;
        }
        foreach (Collider2D collider in collidingObjects) {
            //We have at least one in docking range.
            if (collider.tag == "planetTag") {
                planetInDockingRange = collider.transform;
                inDockingRange = true;
            } 
        }
        PlayerShipControl helm = GameObject.FindWithTag ("Player").GetComponent<PlayerShipControl> ();
        dockButton = Input.GetButton ("Dock");
        if (inDockingRange) {
            if (docking == false && undocking == false && docked == false && undockTimer < 0) {
                dockUI.guiText.text = dockUIString;
            }
            if (dockButton) {
                if (docked == false && docking == false && undocking == false) {
                    docking = true;
                    undocking = false;
                    targetPosition = planetInDockingRange.transform.position;
                        
                } 
            }
            if (docking) {
                dockUI.guiText.text = dockingUIString;
                if (Vector3.Distance (transform.position, targetPosition) > 0.5F) {
                    helm.brake = false;
                    helm.rigidbody2D.isKinematic = true;
                    helm.transform.position = (Vector2.MoveTowards (transform.position, targetPosition, dockingSpeed * Time.deltaTime));
                    helm.transform.localRotation = Quaternion.Lerp (transform.rotation, Quaternion.identity, dockingSpeed * Time.deltaTime);
                    helm.SetPlayerControl (false);
                } else {
                    docked = true;
                    helm.rigidbody2D.isKinematic = true;
                    helm.SetPlayerControl (false);
                    helm.brake = true;
                    docking = false;
                }
            }
            if (docked && docking == false && undocking == false) {
                //Docked
                dockUI.guiText.text = undockUIString;
                if (dockButton == true && undockTimer < 0) {
                    undockTimer = 5F;
                    undocking = true;
                    docking = false;
                    docked = false;

                    helm.brake = false;
                    helm.rigidbody2D.isKinematic = false;
                    helm.SetPlayerControl (true);
                    return;
                } else {
                    if (undockTimer > 0)
                        undockTimer -= Time.deltaTime;
                    helm.SetPlayerControl (false);
                }
                helm.SetPlayerControl (false);
            }
            if (undocking && undockTimer > 0) {
                dockUI.guiText.text = undockingUIString;
                if (undockTimer > 0)
                    undockTimer -= Time.deltaTime;
            } else if (docked == false && docking == false && undocking == true) {
                undocking = false;
                helm.brake = false;
                helm.rigidbody2D.isKinematic = false;
                helm.SetPlayerControl (true);
            }
        }
    }
}
