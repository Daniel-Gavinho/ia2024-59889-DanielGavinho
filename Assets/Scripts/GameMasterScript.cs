using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
    public static GameMasterScript Instance { get; private set; }
    public GameObject Player;
    public GameObject Camera;
    public List<Transform> checkpoints = new List<Transform>();
    public int currentCheckpoint = 0;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentCheckpoint++;
            Debug.Log("Checkpoint: " + currentCheckpoint);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentCheckpoint--;
            Debug.Log("Checkpoint: " + currentCheckpoint);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died");
        Player.transform.position = checkpoints[currentCheckpoint].position - Vector3.up;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
