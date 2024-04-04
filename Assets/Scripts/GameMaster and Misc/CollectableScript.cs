using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public bool isGold = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(isGold)
            {
                GameMasterScript.Instance.CollectGold();
            }
            else
            {
                GameMasterScript.Instance.Collect();
            }
            Destroy(gameObject);
        }
    }    

}
