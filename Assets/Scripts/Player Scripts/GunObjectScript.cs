using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunObjectScript : MonoBehaviour
{
    
    int layerMask = ~((1 << 3)|(1 << 7)|(1 << 8)|(1 << 10)|(1 << 12)|(1 << 14));
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

    public bool automatic;
    
    int ammo;
    bool cocked;

    float timeToShoot;
    float extraTimePerShot = 0.1f;

    int bulletsPerShot =1;

    float spreadMulti = 0;
    float spreadPerShot = 0.08f;
    float spreadDecrease = 0.2f;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI cockedText;

    void Start() {
        
        cam = GameObject.FindWithTag("MainCamera");
        startingTransform = transform;
        UpdateAmmoText();
        UpdateCockedText();
    }

    
    void Update()
    {   
        if(spreadMulti > 0){
            spreadMulti -=spreadDecrease * Time.deltaTime;
        }else if(spreadMulti<0){
            spreadMulti=0;
        }

        
        
        // Primary Shot
        if(automatic){ // Automatic
            if(Input.GetMouseButton(0)){
                if(Time.time > timeToShoot){ // Rate of Fire
                    timeToShoot = Time.time + extraTimePerShot;
                    if(ammo>0 && cocked){
                        ammo--;
                        UpdateAmmoText();
                        Shoot();
                    }
                    else if(ammo==0 && cocked){
                        Shoot();
                        cocked = false;
                        UpdateCockedText();
                    }else if(ammo>0 && !cocked){
                        timeToShoot = Time.time + extraTimePerShot*2;
                        ammo--;
                        cocked = true;
                        UpdateAmmoText();
                        UpdateCockedText();
                        Debug.Log("Executed slide release");
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
                        Shoot();
                    }
                    else if(ammo==0 && cocked){
                        Shoot();
                        cocked = false;
                        UpdateCockedText();
                    }else if(ammo>0 && !cocked){
                        timeToShoot = Time.time + extraTimePerShot*2;
                        ammo--;
                        cocked = true;
                        UpdateAmmoText();
                        UpdateCockedText();
                        Debug.Log("Executed slide release");
                    }
                }
            }
        }

        // Reload
        if(Input.GetKeyDown(KeyCode.R)){
            Reload(weaponID);
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

    void Shoot(){
        if(bulletsPerShot==1){
            ShootSingle();
            if(spreadMulti<0.8f-spreadPerShot){
                spreadMulti+=spreadPerShot;
            }else{
                spreadMulti=0.8f;
            }
        }else{
            if(spreadMulti<0.8f-spreadPerShot){
                spreadMulti+=spreadPerShot;
            }else{
                spreadMulti=0.8f;
            }
            for(int i=0;i<bulletsPerShot;i++){
                ShootSingle();
            }
        }

        

        Debug.Log(spreadMulti);
    }

    void ShootSingle(){
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        //Camera to Worldspace (CamRay)
        if (Physics.Raycast (camRay, out camHit, 1000, layerMask)) {
            Vector3 realDir= camHit.point - transform.position;
            // Inaccurate Spread Calculation
            realDir = spreadCalc(realDir);
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
                // Damage Calc?

                // Stat Tracking?

                // Animations?
            }
        }
    }

    Vector3 spreadCalc(Vector3 dir){
        float inaccDist = Vector3.Distance(transform.position, camHit.point);
        dir.x += ((1 - 2 * Random.value) * spreadMulti) * inaccDist;
        dir.y += ((1 - 2 * Random.value) * spreadMulti) * inaccDist + (spreadMulti * 0.5f);
        dir.z += ((1 - 2 * Random.value) * spreadMulti) * inaccDist;
        return dir;
    }

    public void Reload(int weaponID){
        switch(weaponID)
        {
            case 0:
            ammo = 8;
            break;

            case 1:
            ammo = 30;
            break;

            case 2:
            ammo = 5;
            break;
        }
        UpdateAmmoText();
    }

    public void SetWeaponID(int newWepID){
        weaponID = newWepID;
        ammo = 0;
        cocked = false;
        UpdateAmmoText();
        UpdateCockedText();
        switch(weaponID)
        {
            //DEBUG PISTOL
            case 0:
            automatic =false;
            spreadPerShot = 0.08f;
            spreadDecrease = 0.2f;
            bulletsPerShot =1;
            SetRateOfFire(600);
            break;

            //DEBUG AR
            case 1:
            automatic =true;
            spreadPerShot = 0.04f;
            spreadDecrease = 0.25f;
            bulletsPerShot =1;
            SetRateOfFire(1000);
            break;

            //DEBUG SHOTGUN
            case 2:
            automatic =false;
            spreadPerShot = 0.1f;
            spreadDecrease = 0.2f;
            bulletsPerShot =8;
            SetRateOfFire(80);
            break;
        }
    }

    public int GetWepID(){
        return weaponID;
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

    public void SetSpreadPerShot(float newSpread){
        spreadPerShot = newSpread;
    }

    public void SetSpreadDecrease(float newSpreadDec){
        spreadDecrease = newSpreadDec;
    }
}
