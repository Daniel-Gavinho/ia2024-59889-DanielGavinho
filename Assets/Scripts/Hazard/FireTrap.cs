using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerStats>().TakeDamage(10);
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<SpiderScript>().Die();
        }
    }
}
