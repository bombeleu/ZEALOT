using UnityEngine;
using System.Collections;

public class UIPlanetViewManager : MonoBehaviour{
    float closedY = 0.0F;
    float openY = 1F;
    float curY;
    bool opening;
    bool open;
    bool transitioning;

    // Use this for initialization
    void Start(){
	
    }

    void beginTransition(){
        transitioning = true;
    }

    void endTransition(){
        transitioning = false;
    }
	
    void openAnimation(){
        transform.localScale.Set (1.0F, closedY, 1.0F);
        opening = true;
        beginTransition ();
    }

    
    void closedAnimation(){
        transform.localScale.Set (1.0F, openY, 1.0F);
        opening = false;
        beginTransition ();
    }

    // Update is called once per frame
    void Update(){
        //#DEBUG =====================================
//        bool btnopen = Input.GetKeyDown (KeyCode.I);
//        bool btnclose = Input.GetKeyDown (KeyCode.K);
//        if(btnopen){
//            openAnimation ();
//        }
//        if(btnclose){
//            closedAnimation ();
//        }
//        //============================================
//        curY = gameObject.transform.localScale.y;
//        if(transitioning && opening){
//            if(gameObject.transform.localScale.y < openY){
//                curY += 0.2F * Time.deltaTime;
//                gameObject.transform.localScale.Set (1.0F, curY, 1.0F);
//            }
//            if(gameObject.transform.localScale.y >= openY){
//                gameObject.transform.localScale.Set (1.0F, openY, 1.0F);
//                endTransition ();
//                open = true;
//            }
//        }
//        if(transitioning && !opening){
//            if(gameObject.transform.localScale.y > closedY){
//                curY -= 0.2F * Time.deltaTime;
//                gameObject.transform.localScale.Set (1.0F, curY, 1.0F);
//            }
//            if(gameObject.transform.localScale.y <= closedY){
//                gameObject.transform.localScale.Set (1.0F, closedY, 1.0F);
//                endTransition ();
//                open = false;
//            }
//        }
//        if(open && !transitioning){
//            gameObject.transform.localScale.Set (1.0F, openY, 1.0F);
//        }
//        if(!open && !transitioning){
//            gameObject.transform.localScale.Set (1.0F, closedY, 1.0F);
//        }
    }
}
