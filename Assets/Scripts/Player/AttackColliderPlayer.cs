using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy");
            Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<SpiderScript>().TakeDamage();
        }
    }
}
