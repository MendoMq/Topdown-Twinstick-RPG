using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManPlayerMovement : MonoBehaviour
{
    PlayerMovementMouse pmm;
    
    // Start is called before the first frame update
    void Start()
    {
        pmm = GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>();
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
    }
}
