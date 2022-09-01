using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMirror : MonoBehaviour
{
    public GameObject target;
    public GameObject mirror;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + (target.transform.position - mirror.transform.position);
    }
}
