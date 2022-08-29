using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMouse : MonoBehaviour
{
    GameObject player;
    float speedMulti = 1;
    Vector3 destination;
    bool moving;
    public Rigidbody rb;
    Vector3 movement;

    //int layerMask = 1 << 6;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    
    void Update()
    {
        /* To be removed???
        if(Input.GetMouseButton(1)){
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 1000, layerMask)) {
                Debug.Log ("Hit: "+ hit.transform.name);
                destination = new Vector3(hit.point.x,0,hit.point.z);
                moving =true;
            }
        }
        */
        
        player.transform.rotation=Quaternion.identity;
        if(Input.GetKey(KeyCode.W)){
            pauseMove();
        }
        if(Input.GetKey(KeyCode.A)){
            pauseMove();
        }
        if(Input.GetKey(KeyCode.S)){
            pauseMove();
        }
        if(Input.GetKey(KeyCode.D)){
            pauseMove();
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        movement = new Vector3(x, 0, z);
        //player.transform.Translate(movement * speed * Time.deltaTime);

        
        if(moving)moveStep();
        
    }

    void FixedUpdate()
    {
        rb.position += movement * speedMulti * 0.06f;
    }

    public void SetSpeed(float newSpeed){
        speedMulti = newSpeed;
        if(speedMulti<0)speedMulti=0;
        Debug.Log("New speed: "+speedMulti);
    }
    
    public void ChangeSpeed(float newSpeed){
        speedMulti += newSpeed;
        if(speedMulti<0)speedMulti=0;
        Debug.Log("Speed changed: "+newSpeed);
        Debug.Log("New speed: "+speedMulti);
    }

    public void ResetSpeed(){
        speedMulti = 1;
        Debug.Log("Speed reset");
    }

    public float GetSpeed(){
        Debug.Log("Returning speed: "+speedMulti);
        return speedMulti;
    }

    void moveStep()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, destination, Time.deltaTime * speedMulti);
    }

    public void pauseMove()
    {
        //Debug.Log("Pausingmove");
        moving=false;
    }
}
