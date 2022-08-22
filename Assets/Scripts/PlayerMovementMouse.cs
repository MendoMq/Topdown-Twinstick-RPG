using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMouse : MonoBehaviour
{
    GameObject player;
    public float speed;
    Vector3 destination;
    bool moving;
    public float minDistance;
    public Vector3 inputVector;
    public Rigidbody rb;
    Vector3 movement;

    int layerMask = 1 << 6;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 1000, layerMask)) {
                Debug.Log ("Hit: "+ hit.transform.name);
                destination = new Vector3(hit.point.x,0,hit.point.z);
                moving =true;
            }
        }
        
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
        rb.position += movement * speed * 0.06f;
    }

    void moveStep()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, destination, Time.deltaTime * speed);
    }

    public void pauseMove()
    {
        //Debug.Log("Pausingmove");
        moving=false;
    }
}
