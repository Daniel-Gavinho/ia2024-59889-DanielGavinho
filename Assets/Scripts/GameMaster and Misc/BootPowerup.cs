using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootPowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DashingScript dash = other.GetComponent<DashingScript>();
            if (dash != null)
            {
                dash.hasDash = true;
            }
            Destroy(this.gameObject);
        }
    }
}
