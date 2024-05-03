using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{

    public string tagTarget = "Player";

    public List<Collider2D> detectedObjects = new List<Collider2D>();
    public Collider2D col;

    //object enter range
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player"))
        {
            detectedObjects.Add(collider);

        }
    }

    //object leave range
    private void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            detectedObjects.Remove(collider);          
        }
    }
}
