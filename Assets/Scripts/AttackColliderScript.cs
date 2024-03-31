using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderScript : MonoBehaviour
{
    private bool canAttack = false;

    public bool CanAttack {get { return canAttack; }}

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is in attack range");
            canAttack = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            canAttack = false;
        }
    }
}
