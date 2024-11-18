using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurniturePlacementManager : MonoBehaviour
{
    public GameObject spawnableFurniture;
    
    public ARSession sessionManager;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    void Update()
    {
        if(Input.touchCount>0){
            if(Input.GetTouch(0).phase == TouchPhase.Began){
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);

                if(collision && isButtonPressed() == false){ // if button is pressed so the prefab not spawn
                    GameObject cloneObject = Instantiate(spawnableFurniture);
                    cloneObject.transform.position = raycastHits[0].pose.position;
                    cloneObject.transform.rotation = raycastHits[0].pose.rotation;
                }

                // this below logic for our ar look more realstic
                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false); //plane detection stop
                }
                planeManager.enabled = false; // plane manager componant disable
            }
        }        
    }

    public bool isButtonPressed(){
        if(EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null){
            return false;
        }
        else{
            return true;
        }
    }

    public void SwitchFurniture(GameObject furnitureObject){
        spawnableFurniture = furnitureObject;
    }
    
}
