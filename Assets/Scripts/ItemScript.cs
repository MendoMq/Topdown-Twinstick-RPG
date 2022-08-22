using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public int itemID;
    public float distanceCollect = 0.5f;
    public bool collecting;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(collecting){
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime*10);
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if(dist < distanceCollect){
                CompleteCollect();
            }
        }
    }

    public void StartCollect(){
        collecting=true;
    }

    public void CompleteCollect(){
        if(itemID!=0){
            player.transform.GetChild(3).gameObject.GetComponent<ItemPickup>().ItemCollect(itemID);
        }else{
            Debug.Log("No itemID collected");
        }
        Destroy(gameObject);
    }
}
