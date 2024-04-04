using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePowerupScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerActions player = other.GetComponent<PlayerActions>();
            player.hasGrapple = true;
            Destroy(gameObject);
        }
    }
}
