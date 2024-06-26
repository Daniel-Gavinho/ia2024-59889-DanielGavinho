using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSequenceTrigger : MonoBehaviour
{
    public GameObject Lava;
    public MovingPlatform[] platforms;

    private Coroutine lavaCoroutine;
    private Vector3 lavaStartPos;

    private void Start()
    {
        lavaStartPos = Lava.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(lavaCoroutine != null) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("End sequence triggered");
            Music.Instance.PlaySound(1, 100f);
            lavaCoroutine = StartCoroutine(LavaRising());
        }
    }

    private IEnumerator LavaRising()
    {
        while (Lava.transform.position.y < 4)
        {
            Lava.transform.position += Vector3.up * Time.deltaTime;
            yield return null;
        }
    }

    public void StopSequence()
    {
        if(lavaCoroutine != null)
            StopCoroutine(lavaCoroutine);
    }

    public void ResetSequence()
    {
        if(lavaCoroutine != null)
            StopCoroutine(lavaCoroutine);

        lavaCoroutine = null;

        foreach (var platform in platforms)
        {
            platform.ResetPlatform();
        }
        
        Lava.transform.position = lavaStartPos;
    }
}
