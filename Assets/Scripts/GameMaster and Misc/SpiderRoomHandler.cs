using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRoomHandler : MonoBehaviour
{

    [Header("Spiders")]
    public GameObject Spider;
    public Transform[] SpawnPoints;

    [Header("Doors")]
    public GameObject EntranceDoor;
    public Vector3 EntranceDoorOpenPosition;
    public Vector3 EntranceDoorClosedPosition;
    public GameObject ExitDoor;
    public Vector3 ExitDoorOpenPosition;
    public Vector3 ExitDoorClosedPosition;
    public float TimeToClose;
    private bool fighting = false;

    [Header("Variables")]
    public GameObject[] Spiders;

    public void Reset()
    {
        fighting = false;
        foreach(GameObject spider in Spiders)
        {
            Destroy(spider);
        }
        Start();
    }

    private void Start()
    {
        EntranceDoor.transform.localPosition = EntranceDoorOpenPosition;
        ExitDoor.transform.localPosition = ExitDoorClosedPosition;
        Spiders = new GameObject[SpawnPoints.Length];
        for(int i = 0; i < SpawnPoints.Length; i++)
        {
            Spiders[i] = Instantiate(Spider, SpawnPoints[i].position, SpawnPoints[i].rotation);
            Spiders[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    void Update() {
        if(!fighting) return;

        foreach(GameObject spider in Spiders)
        {
            if(spider != null) return;
        }

        StartCoroutine(MoveDoor(ExitDoor, ExitDoorClosedPosition, ExitDoorOpenPosition));
        StartCoroutine(MoveDoor(EntranceDoor, EntranceDoorClosedPosition, EntranceDoorOpenPosition));
        Destroy(this.gameObject, TimeToClose);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(fighting) return;
        Debug.Log("Spider Room Triggered");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the room");
            StartCoroutine(MoveDoor(EntranceDoor, EntranceDoorOpenPosition, EntranceDoorClosedPosition));
            fighting = true;
            foreach(GameObject spider in Spiders)
            {
                spider.GetComponent<SpiderScript>().StartChase();
            }
        }
    }

    private IEnumerator MoveDoor(GameObject door, Vector3 startPosition, Vector3 endPosition) 
    {
        float elapsedTime = 0;
        while (elapsedTime < TimeToClose)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / TimeToClose;
            door.transform.localPosition = Vector3.Lerp(startPosition, endPosition, percentage);
            yield return null;
        }
    }

}
