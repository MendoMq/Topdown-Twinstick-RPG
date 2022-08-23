using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    GameObject player;
    GameObject head;
    public float t = 0.0025f;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        head = player.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.eulerAngles = new Vector3(
            transform.eulerAngles.x+360,
            Mathf.Lerp(transform.eulerAngles.y,head.transform.eulerAngles.y,t)+360,
            transform.eulerAngles.z+360
        );*/
        
        target = Vector3.Slerp(target,head.transform.forward,t);

        Quaternion rotate = Quaternion.LookRotation(target, Vector3.up);
        transform.rotation = rotate;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
    }
}
