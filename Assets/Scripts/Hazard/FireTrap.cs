using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public ParticleSystem fire;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fire.Play();
            other.GetComponent<PlayerStats>().TakeDamage(1);
        }

        if(other.tag == "Enemy")
        {
            fire.Play();
            other.GetComponent<SpiderScript>().Die();
        }
    }
}
