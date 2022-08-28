using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    
    public GameObject nearestItem;
    public Color lineColor;
    public float nearestDistance;
    List<GameObject> items = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && nearestItem!=null){
            nearestItem.GetComponent<ItemScript>().StartCollect();
            items.Remove(nearestItem);
        }

        nearestDistance = 999;
        if(items.Count > 0){
            foreach (GameObject item in items){
                float dist = Vector3.Distance(transform.position, item.transform.position);
                if(dist < nearestDistance){
                    nearestDistance = dist;
                    nearestItem = item;
                }
            }
            Debug.DrawLine(transform.position, nearestItem.transform.position, lineColor);
        }else{
            nearestItem = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ItemScript>() != null && other.gameObject.GetComponent<ItemScript>().collecting!=true){
            Debug.Log(other.gameObject+" in range");
            items.Add(other.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.GetComponent<ItemScript>() != null && other.gameObject.GetComponent<ItemScript>().collecting!=true){
            Debug.Log(other.gameObject+" out of range");
            items.Remove(other.gameObject);
        }
    }

    public void ItemCollect(int itemID){
        // Calling Item Effect Script (GameManager?)
        switch (itemID)
        {
           case 1:
           Debug.Log("Collected debug 1");
           break; 

           case 2:
           Debug.Log("Collected debug 2");
           break; 

           default:
           Debug.Log("Invalid itemID");
           break;
        }
    }
}
