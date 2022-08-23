using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGunLine : MonoBehaviour
{
    
    int layerMask = ~((1 << 3)|(1 << 7)|(1 << 8)|(1 << 10));
    public float forceMulti=1;
    public Color color; 
    RaycastHit camHit;
    RaycastHit hit;
    GameObject cam;
    Transform startingTransform;
    GameObject hitCubeClone;
    public GameObject hitCubePrefab;

    void Start() {
        cam = GameObject.FindWithTag("MainCamera");
        startingTransform = transform;
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
            //Camera to Worldspace (CamRay)
            if (Physics.Raycast (camRay, out camHit, 1000, layerMask)) {
                Vector3 realDir= camHit.point - transform.position;
                Ray realRay = new Ray(transform.position,realDir);
                //CamRay to GunRay
                if(Physics.Raycast (realRay, out hit, 1000, layerMask)){
                    if(hit.transform.gameObject.GetComponent<Rigidbody>()!=null){
                        Vector3 dir = hit.transform.position-transform.position;
                        hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(dir * forceMulti, ForceMode.Impulse);
                    }
                    hitCubeClone = Instantiate(hitCubePrefab, hit.point, Quaternion.identity); 
                }
            }
        }

        /*if(Input.GetMouseButton(1)){
            Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
            //Camera to Worldspace (CamRay)
            if (Physics.Raycast (camRay, out camHit, 1000, layerMask)) {
                Vector3 realDir= camHit.point - transform.position;
                Ray realRay = new Ray(transform.position,realDir);
                //CamRay to GunRay
                if(Physics.Raycast (realRay, out hit, 1000, layerMask)){
                    hitCubeClone = Instantiate(hitCubePrefab, hit.point, Quaternion.identity); 
                }
            }
        }*/
        Debug.DrawLine(transform.position, hit.point, color);
    }
}
