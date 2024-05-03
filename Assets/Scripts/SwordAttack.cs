using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordAttack : MonoBehaviour
{   
    public Collider2D swordCollider;
    public float knockbackForce = 500f;
    public float damage = 1;
    private Vector2 rightAttackOffset;
    private void Start(){
        rightAttackOffset = transform.position;

    }



    public void AttackRight(){
        //print("attack right");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft(){
        //print ("attack left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack(){
        swordCollider.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Enemy"){
            // deal damage to enemy
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.Health -= damage;            
        }


    }
}
