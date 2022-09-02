using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseFollow : MonoBehaviour
{
    [SerializeField] Camera cam;  
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform player;
    [SerializeField] float slerpFrac;
    public float maxRadius;
    public float minRadius;
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            maxRadius=8;
            minRadius=2;
        }else if(Input.GetMouseButtonUp(1)){
            maxRadius=3;
            minRadius=6;
        }
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
        {
            Vector3 mousePos = raycastHit.point;
 
            Vector3 difference = mousePos - player.position;
            float magnitude = difference.magnitude;

            if(Input.GetKey(KeyCode.LeftControl) && !(Input.GetMouseButton(1))){
                magnitude=0;
            }

            if (magnitude > maxRadius) {
                difference = difference * (maxRadius / magnitude);
                slerpFrac = 0.1f;
            }

            if (magnitude < minRadius) {
                difference = Vector3.zero;
                slerpFrac = 0.075f;
            }

            GetComponent<Rigidbody>().position = Vector3.Lerp(transform.position,new Vector3(player.position.x + difference.x,player.position.y,player.position.z + difference.z),slerpFrac);      
        }
    }
}