using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
    public static GameMasterScript Instance { get; private set; }
    public GameObject Player;
    public GameObject SpiderRoomHandler;
    public List<Transform> checkpoints = new List<Transform>();
    public int currentCheckpoint = 0;
    private float timer = 0f;
    private int collectables = 0;
    private int goldCollectables = 0;
    private int deaths = 0;

    public float Timer
    {
        get { return timer; }
    }

    public void Collect()
    {
        collectables++;
        Debug.Log("Collectables: " + collectables);
    }

    public void CollectGold()
    {
        goldCollectables++;
        Debug.Log("Gold Collectables: " + goldCollectables);
    }

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

    void Start()
    {
        StartCoroutine(StartTimer());
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
            Teleport();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died");
        deaths++;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.transform.position = checkpoints[currentCheckpoint].position - Vector3.up;
        HandleSpiderRoom();
    }

    private void Teleport()
    {
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.transform.position = checkpoints[currentCheckpoint].position;
    }

    private void HandleSpiderRoom()
    {
        if (SpiderRoomHandler != null)
        {
            SpiderRoomHandler.GetComponent<SpiderRoomHandler>().Reset();
        }
    }

    private IEnumerator StartTimer()
    {
        while (true)
        {
            yield return null;
            timer += Time.deltaTime;
        }
    }
}
