using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{

    private GameMasterScript gm;

    private void Start()
    {
        gm = GameMasterScript.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            gm.Die();
        }
    }

}
