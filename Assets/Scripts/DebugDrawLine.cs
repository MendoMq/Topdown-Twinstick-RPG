using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawLine : MonoBehaviour
{
    public Color color;
    public float length;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position,transform.position + transform.forward * length,color);
    }
}
