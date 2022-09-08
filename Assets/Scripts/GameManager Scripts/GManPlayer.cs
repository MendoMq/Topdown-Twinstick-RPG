using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManPlayer : MonoBehaviour
{
    GameObject player;
    PlayerMovementMouse pmm;
    GunObjectScript gos;

    bool speedActive=false;
    public float speedIncrease = 0.5f;
    public float speedTimeLength = 2;
    float speedTimeRemaining;

    CapsuleCollider crouchCap;
    bool crouching;
    bool sprinting;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pmm = player.GetComponent<PlayerMovementMouse>();
        gos = player.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<GunObjectScript>();
        crouchCap = player.GetComponent<CapsuleCollider>();
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

        if(Input.GetKeyDown(KeyCode.C) && !sprinting){
            Crouch();
        }else if(Input.GetKeyUp(KeyCode.C) && !sprinting){
            Uncrouch();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && !crouching){
            Sprint();
        }else if(Input.GetKeyUp(KeyCode.LeftShift) && !crouching){
            Unsprint();
        }

        if(speedActive && speedTimeRemaining<Time.time){
            pmm.ChangeSpeed(-speedIncrease);
            speedActive=false;
            Debug.Log("Speed worn off");
        }
    }

    public void ItemCollect(int itemID){
        switch (itemID)
        {
            // RELOAD
            case 1:
            Debug.Log("Reload Weapon Item");
            gos.Reload(gos.GetWepID());
            break;

            // SPEED
            case 2:
            Debug.Log("Temp Increased Speed Item");
            SpeedItem();
            break;

            // ERROR
            default:
            Debug.Log("Incorrect item handling");
            break;
        }
    }

    void Crouch(){
        crouching=true;
        pmm.ChangeSpeed(-0.5f);
        crouchCap.center = new Vector3(0,0.25f,0);
        crouchCap.height = 0.5f;
        player.transform.GetChild(0).position = player.transform.position + new Vector3(0,0.625f,0);
        player.transform.GetChild(1).position = player.transform.position;
    }

    void Uncrouch(){
        crouching=false;
        pmm.ChangeSpeed(0.5f);
        crouchCap.center = new Vector3(0,0.5f,0);
        crouchCap.height = 1f;
        player.transform.GetChild(0).position = player.transform.position + new Vector3(0,1.125f,0);
        player.transform.GetChild(1).position = player.transform.position + new Vector3(0,0.5f,0);
    }

    void Sprint(){
        sprinting=true;
        pmm.ChangeSpeed(0.5f);
        gos.gManInterupt(true);
    }

    void Unsprint(){
        sprinting=false;
        pmm.ChangeSpeed(-0.5f);
        gos.gManInterupt(false);
    }

    void SpeedItem(){
        // Potentially just for debug, Unknown if to be implemented as is (MAY NEED REVISION)
        speedActive=true;
        pmm.ChangeSpeed(speedIncrease);
        speedTimeRemaining = Time.time + speedTimeLength;
    }

    // Time based Effects + Toggleables

    // Player Health, Speed, Stats and Skills
    // Item Effect Handling
    // Gun Type, Ammo, Charged, RateOfFire and Automatic

    // Maybe a different script?
    // Menus, Inventory, Player status, etc
}
