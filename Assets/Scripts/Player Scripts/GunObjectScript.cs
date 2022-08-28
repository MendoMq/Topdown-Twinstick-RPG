using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunObjectScript : MonoBehaviour
{
    
    int layerMask = ~((1 << 3)|(1 << 7)|(1 << 8)|(1 << 10));
    public float forceMulti=1;
    public Color color; 
    RaycastHit camHit;
    RaycastHit hit;
    GameObject cam;
    Transform startingTransform;
    GameObject hitEffect;
    public GameObject hitEffectPrefab;

    void Start() {
        cam = GameObject.FindWithTag("MainCamera");
        startingTransform = transform;
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            // Ammo Calc?
            // Realistic Weapon Handling?
            shootSingle();
        }
        //Reloading?

        Debug.DrawLine(transform.position, hit.point, color);
    }

    public void shootSingle(){
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        //Camera to Worldspace (CamRay)
        if (Physics.Raycast (camRay, out camHit, 1000, layerMask)) {
            Vector3 realDir= camHit.point - transform.position;
            Ray realRay = new Ray(transform.position,realDir);
            //CamRay to GunRay
            if(Physics.Raycast (realRay, out hit, 1000, layerMask)){
                // Physics Hit
                if(hit.transform.gameObject.GetComponent<Rigidbody>()!=null){
                    Vector3 dir = Vector3.Normalize(hit.transform.position-transform.position);
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(dir * forceMulti, ForceMode.Impulse);
                }
                // Effect Instancing
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("StaticEnv") || hit.transform.gameObject.layer == LayerMask.NameToLayer("GroundPlane")){
                    Debug.Log(hit.normal);
                    Quaternion quaternion = Quaternion.Euler(-90 + hit.normal.x*-90 + hit.normal.z*-90,-90+hit.normal.y*90 + hit.normal.z*90,0);
                    hitEffect = Instantiate(hitEffectPrefab, hit.point, quaternion); 
                }
                // Damage Calc?

                // Stat Tracking?

                // Animations?
            }
        }
    }
}
