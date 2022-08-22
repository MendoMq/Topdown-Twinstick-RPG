using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationMouse : MonoBehaviour
{
    GameObject player;
    Vector3 destination;
    int layerMask = 1 << 7;
    GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        head = player.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit, 1000, layerMask)) {
            destination = hit.point;
        }
        RotStep();
        
    }

    void RotStep()
    {
        head.transform.LookAt(destination);
    }
}
