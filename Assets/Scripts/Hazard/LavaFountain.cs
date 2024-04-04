using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFountain : MonoBehaviour
{
    private GameMasterScript gm;

    void Start() {
        gm = GameMasterScript.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.Die();
        }
    }
}
