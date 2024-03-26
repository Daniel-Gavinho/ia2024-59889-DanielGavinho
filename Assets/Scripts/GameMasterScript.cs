using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
    public static GameMasterScript Instance { get; private set; }
    public GameObject Player;
    public Vector3 checkpoint;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Debug.Log("Player has died");
        Player.transform.position = checkpoint;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
