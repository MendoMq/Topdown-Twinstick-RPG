using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFreeCam : MonoBehaviour
{
    Vector3 Angles;
    public Vector2 sensitivity=new Vector2(1,1);

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.position += transform.forward * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position += -transform.right * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S)){
            transform.position += -transform.forward * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D)){
            transform.position += transform.right * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Space)){
            transform.position += transform.up * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.C)){
            transform.position += -transform.up * Time.deltaTime;
        }

        float rotationY = Input.GetAxis("Mouse Y")*sensitivity.x;
        float rotationX = Input.GetAxis("Mouse X")*sensitivity.y;

        if(rotationY>0){
            Angles = new Vector3(Mathf.MoveTowards(Angles.x, -80, rotationY), Angles.y + rotationX, 0);
        }else{
            Angles = new Vector3(Mathf.MoveTowards(Angles.x, 80, -rotationY), Angles.y + rotationX, 0);
            transform.localEulerAngles = Angles;
        }

        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.Escape))Cursor.lockState = CursorLockMode.None;
    }
}

