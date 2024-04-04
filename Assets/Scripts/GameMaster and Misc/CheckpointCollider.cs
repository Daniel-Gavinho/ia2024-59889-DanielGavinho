using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour
{
    public int checkpointNumber;
    private GameMasterScript gm;

    public void Start()
    {
        gm = GameMasterScript.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Checkpoint reached, new spawn position: " + transform.position + new Vector3(0, -1, 0));
            gm.currentCheckpoint = checkpointNumber;
        }
    }
}

