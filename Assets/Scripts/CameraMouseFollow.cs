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
            minRadius=2;
            maxRadius=8;
        }else if(Input.GetMouseButtonUp(1)){
            minRadius=5;
            maxRadius=3;
        }
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
        {
            Vector3 mousePos = raycastHit.point;
 
            Vector3 difference = mousePos - player.position;
            float magnitude = difference.magnitude;
            if (magnitude > maxRadius) {
                difference = difference * (maxRadius / magnitude);
            }

            if (magnitude < minRadius) {
                difference = Vector3.zero;
            }

            GetComponent<Rigidbody>().position = Vector3.Lerp(transform.position,new Vector3(player.position.x + difference.x,player.position.y,player.position.z + difference.z),slerpFrac);      
        }
    }
}