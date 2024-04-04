using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponScript wep = other.GetComponent<WeaponScript>();
            if (wep != null)
            {
                wep.ObtainWeapon();
            }
            Destroy(this.gameObject);
        }
    }
}
