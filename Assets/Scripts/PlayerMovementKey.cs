using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKey : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            player.transform.position += player.transform.forward * Time.deltaTime;
            pauseMouseMov();
        }
        if(Input.GetKey(KeyCode.A)){
            player.transform.position += -player.transform.right * Time.deltaTime;
            pauseMouseMov();
        }
        if(Input.GetKey(KeyCode.S)){
            player.transform.position += -player.transform.forward * Time.deltaTime;
            pauseMouseMov();
        }
        if(Input.GetKey(KeyCode.D)){
            player.transform.position += player.transform.right * Time.deltaTime;
            pauseMouseMov();
        }
    }

    void pauseMouseMov(){
        gameObject.GetComponent<PlayerMovementMouse>().pauseMove();
    }
}
