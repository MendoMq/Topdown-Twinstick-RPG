using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOffset : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
