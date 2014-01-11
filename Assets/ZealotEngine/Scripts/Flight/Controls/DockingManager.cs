using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DockingManager : MonoBehaviour{
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
    List<Collider2D> collidingObjects = new List<Collider2D>();
    public GameObject dockUI;

    //TextStrings, temporary until localisation system;
    string dockUIString = "Press (P) to Dock";
    string undockUIString = "Press (P) to Undock";
    string dockingUIString = "Docking...";
    string undockingUIString = "Undocking...";

    public TextMesh uiPlanetInfo;
    public TextMesh uiPlanetTitle;

    public List<GameObject> VisitedPlanets;

    //Temporary OnGUI for docking debugging;
    void OnGUI(){
        Vector2 boxsize = new Vector2(24.0F, 10.0F);
        if(inDockingRange == false){
            dockingToolTipOff();
            setPlanetTitleOffAndPlanetInfoOff();
            return;
        }
        if(docked){
            dockingToolTipOn();
            setPlanetTitleOnAndPlanetInfoOn();
            return;
        }
        if(!docked && inDockingRange){
            if(planetInDockingRange){
                dockingToolTipOn();
                planetTitleOnWithNoPlanetInfo();
                checkIfVisited();
            }
        }
    }

    // Use this for initialization
    void Start(){
        shipTransform = this.gameObject.transform;
        uiPlanetInfo = GameObject.Find("UIPlanetInfoText").GetComponent<TextMesh>();
        uiPlanetTitle = GameObject.Find("UIPlanetText").GetComponent<TextMesh>();
        setPlanetInfoTextToUnknown();
        setPlanetTitleToUnknown();
        setPlanetTitleOffAndPlanetInfoOff();
    }

    void dockingToolTipOn(){
        if(dockUI.activeSelf == false){
            dockUI.SetActive(true);
        }
    }

    void dockingToolTipOff(){
        if(dockUI.activeSelf == true)
            dockUI.SetActive(false);
    }

    void planetTitleOnWithNoPlanetInfo(){
        if(uiPlanetInfo.gameObject.activeSelf == true)
            uiPlanetInfo.gameObject.SetActive(false);
        if(uiPlanetTitle.gameObject.activeSelf == false)
            uiPlanetTitle.gameObject.SetActive(true);
    }

    void setPlanetTitleOnAndPlanetInfoOn(){
        if(uiPlanetInfo.gameObject.activeSelf == false)
            uiPlanetInfo.gameObject.SetActive(true);
        if(uiPlanetTitle.gameObject.activeSelf == false)
            uiPlanetTitle.gameObject.SetActive(true);
    }

    void setPlanetTitleOffAndPlanetInfoOff(){
        if(uiPlanetInfo.gameObject.activeSelf == true)
            uiPlanetInfo.gameObject.SetActive(false);
        if(uiPlanetTitle.gameObject.activeSelf == true)
            uiPlanetTitle.gameObject.SetActive(false);
    }

    void checkIfVisited(){
        if(VisitedPlanets.Contains(planetInDockingRange.gameObject)){
            uiPlanetTitle.text = planetInDockingRange.gameObject.name;
            return;
        } else{
            setPlanetTitleToUnknown();
            return;
        }
    }

    void setPlanetTitleToUnknown(){
        uiPlanetTitle.text = "?????????";
    }

    void setPlanetInfoTextToUnknown(){
        uiPlanetInfo.text = "????";
    }

    void OnTriggerEnter2D(Collider2D triggerCollider){
        if(triggerCollider == null)
            return;
        collidingObjects.Add(triggerCollider);
    }

    void OnTriggerExit2D(Collider2D triggerCollider){
        if(planetInDockingRange == null && !inDockingRange && docking)
            return;
        if(planetInDockingRange == null && !inDockingRange && !docking)
            return;
        if(Vector2.Distance(planetInDockingRange.transform.position, transform.position) > radius && !docking){
            planetInDockingRange = null;
            inDockingRange = false;
        }
        collidingObjects.Remove(triggerCollider);
    }

    // Update is called once per frame
    void Update(){
        PlayerShipControl helm = GameObject.FindWithTag("Player").GetComponent<PlayerShipControl>();
        dockButton = Input.GetButton("Dock");
        if(!inDockingRange && collidingObjects.Count > 0){
            foreach (Collider2D collider in collidingObjects){
                //We have at least one in docking range.
                if(collider.tag == "planetTag"){
                    planetInDockingRange = collider.transform;
                    inDockingRange = true;
                } 
            }
        }

        if(inDockingRange){
            if(!docking && !undocking && !docked && undockTimer < 0){
                dockUI.guiText.text = dockUIString;
            }
            if(dockButton){
                if(!docked && !docking && !undocking){
                    docking = true;
                    undocking = false;
                    targetPosition = planetInDockingRange.transform.position;
                        
                } 
            }
            if(docking){
                dockUI.guiText.text = dockingUIString;
                if(Vector3.Distance(transform.position, targetPosition) > 0.3F){
                    helm.brake = false;
                    helm.rigidbody2D.isKinematic = true;
                    helm.transform.position = (Vector2.Lerp(transform.position, targetPosition, dockingSpeed * Time.deltaTime));
                    helm.transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, dockingSpeed * Time.deltaTime);
                    helm.SetPlayerControl(false);
                } else{
                    docked = true;
                    if(VisitedPlanets.Contains(planetInDockingRange.gameObject) == false){
                        VisitedPlanets.Add(planetInDockingRange.gameObject);
                    }
                    uiPlanetTitle.text = planetInDockingRange.name;
                    uiPlanetInfo.text = planetInDockingRange.GetComponent<Planet>().getDescription();
                    helm.rigidbody2D.isKinematic = true;
                    helm.SetPlayerControl(false);
                    helm.brake = true;
                    docking = false;
                }
            }
            if(docked && !docking && !undocking){
                //Docked
                dockUI.guiText.text = undockUIString;
                if(dockButton == true && undockTimer < 0){
                    undockTimer = 5F;
                    undocking = true;
                    docking = false;
                    docked = false;

                    helm.brake = false;
                    helm.rigidbody2D.isKinematic = false;
                    helm.SetPlayerControl(true);
                    return;
                } else{
                    if(undockTimer > 0)
                        undockTimer -= Time.deltaTime;
                    helm.SetPlayerControl(false);
                }
                helm.SetPlayerControl(false);
            }
            if(undocking && undockTimer > 0){
                dockUI.guiText.text = undockingUIString;
                if(undockTimer > 0)
                    undockTimer -= Time.deltaTime;
            } else if(!docked && !docking && undocking){
                undocking = false;
                helm.brake = false;
                helm.rigidbody2D.isKinematic = false;
                helm.SetPlayerControl(true);
            }
        }
    }
}
