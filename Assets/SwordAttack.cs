using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{   

    Vector2 rightAttackOffset;
    Collider2D swordCollider;

    private void Start(){
        rightAttackOffset = transform.position;
    }



    public void AttackRight(){
        print("attack right");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft(){
        print ("attack left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack(){
        swordCollider.enabled = false;
    }
}
