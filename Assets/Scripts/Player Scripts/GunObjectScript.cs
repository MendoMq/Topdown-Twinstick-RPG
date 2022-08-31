using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunObjectScript : MonoBehaviour
{
    
    int layerMask = ~((1 << 3)|(1 << 7)|(1 << 8)|(1 << 10)|(1 << 12));
    public LayerMask ignoreLayers;
    public float forceMulti=1;
    public Color color; 
    RaycastHit camHit;
    RaycastHit hit;
    GameObject cam;
    Transform startingTransform;
    GameObject hitEffect;
    public GameObject hitEffectPrefab;

    public int weaponID;
    public int ammo;

    public bool cocked;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI cockedText;

    public bool automatic;
    float timeToShoot;
    public float extraTimePerShot;

    void Start() {
        cam = GameObject.FindWithTag("MainCamera");
        startingTransform = transform;
        UpdateAmmoText();
        UpdateCockedText();
    }

    
    void Update()
    {   
        // Primary Shot
        if(automatic){ // Automatic
            if(Input.GetMouseButton(0)){
                if(Time.time > timeToShoot){ // Rate of Fire
                    timeToShoot = Time.time + extraTimePerShot;
                    if(ammo>0 && cocked){
                        ammo--;
                        UpdateAmmoText();
                        ShootSingle();
                    }
                    else if(ammo==0 && cocked){
                        ShootSingle();
                        cocked = false;
                        UpdateCockedText();
                    }
                }
            }
        }else{ // Semi-automatic
            if(Input.GetMouseButtonDown(0)){
                if(Time.time > timeToShoot){ // Rate of Fire
                    timeToShoot = Time.time + extraTimePerShot;
                    if(ammo>0 && cocked){
                        ammo--;
                        UpdateAmmoText();
                        ShootSingle();
                    }
                    else if(ammo==0 && cocked){
                        ShootSingle();
                        cocked = false;
                        UpdateCockedText();
                    }
                }
            }
        }

        // Reload
        if(Input.GetKeyDown(KeyCode.R)){
            Reload(weaponID);
            UpdateAmmoText();
        }

        // Cocking / Charging
        if(Input.GetKeyDown(KeyCode.X)){
            if(ammo>0){
                ammo--;
                UpdateAmmoText();
                cocked = true;
                UpdateCockedText();
            }else if(ammo==0 && cocked){
                cocked=false;
                UpdateCockedText();
            }
        }

        
    }

    void UpdateAmmoText(){
        ammoText.text = new string(("Magazine: "+ammo).ToString());
    }

    void UpdateCockedText(){
        cockedText.text = new string(("Charged: "+cocked).ToString());
    }

    void ShootSingle(){
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
                    Quaternion quaternion = Quaternion.Euler(-90 + hit.normal.x*-90 + hit.normal.z*-90,-90+hit.normal.y*90 + hit.normal.z*90,0);
                    hitEffect = Instantiate(hitEffectPrefab, hit.point, quaternion); 
                }
                Debug.DrawLine(transform.position, hit.point, color, 5);
                // Inaccuracy?

                // Damage Calc?

                // Stat Tracking?

                // Animations?
            }
        }
    }

    void Reload(int weaponID){
        switch(weaponID)
        {
            case 0:
            ammo = 8;
            break;
        }
    }

    public void SetWeaponID(int newWepID){
        weaponID = newWepID;
    }

    public void SetAmmo(int newAmmo){
        ammo=newAmmo;
    }
    
    public void SetCocked(bool newCocked){
        cocked=newCocked;
    }

    public void SetRateOfFire(float newROF){
        extraTimePerShot = 60 / newROF;
    }

    public void SetAutomatic(bool newAuto){
        automatic = newAuto;
    }
}
