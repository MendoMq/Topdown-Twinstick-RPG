using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBodyRotate : MonoBehaviour
{
    public GameObject head;
    Vector3 target;
    public float t = 0.005f;

    // Update is called once per frame
    void Update()
    {
        target = Vector3.Slerp(target,head.transform.forward,t);

        Quaternion rotate = Quaternion.LookRotation(target, Vector3.up);
        transform.rotation = rotate;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
    }
}
