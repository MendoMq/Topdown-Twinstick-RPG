using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManPlayer : MonoBehaviour
{
    PlayerMovementMouse pmm;
    GunObjectScript gos;
    
    // Start is called before the first frame update
    void Start()
    {
        pmm = GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>();
        gos = GameObject.FindWithTag("Player").transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<GunObjectScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            pmm.SetSpeed(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            pmm.ResetSpeed();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            pmm.ChangeSpeed(0.5f);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            pmm.ChangeSpeed(-0.5f);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            pmm.GetSpeed();
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            gos.SetWeaponID(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha7)){
            gos.SetWeaponID(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)){
            gos.SetWeaponID(2);
        }
    }
    // Time based Effects? Toggleable?

    // Player Health, Speed, Stats and Skills
    // Item Effect Handling
    // Gun Type, Ammo, Charged, RateOfFire and Automatic

    // Maybe a different script?
    // Menus, Inventory, Player status, etc
}
