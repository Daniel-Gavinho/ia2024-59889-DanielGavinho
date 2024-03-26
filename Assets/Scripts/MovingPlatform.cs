using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform InitialPosition;
    public Transform FinalPosition;
    public float timeToTake = 2;
    public float timeToWait = 2;
    private bool isMovingForward = true;
    private Vector3 velocity;

    public Vector3 Velocity { get => velocity; }

    private void Start()
    {
        velocity = Vector3.zero;
        StartCoroutine(MovePlatform(InitialPosition.position, FinalPosition.position, timeToTake));
    }

    // private void testKey()
    // {
    //     if (Input.GetKeyDown(KeyCode.K))
    //     {
    //         timeToTake -= 0.1f;
    //         Debug.Log("Time to take: " + timeToTake);
    //     }

    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         timeToTake += 0.1f;
    //         Debug.Log("Time to take: " + timeToTake);
    //     }
    // }

    private IEnumerator MovePlatform(Vector3 initialPosition, Vector3 finalPosition, float timeToTake)
    {
        velocity = (finalPosition - initialPosition) / timeToTake;
        float journey = 0.0f;

        while (journey <= timeToTake)
        {
            journey += Time.deltaTime;
            float percent = Mathf.Clamp01(journey / timeToTake);

            transform.position = Vector3.Lerp(initialPosition, finalPosition, percent);
            yield return null;
        }

        StartCoroutine(changeDirection());
    }

    private IEnumerator changeDirection()
    {
        velocity = Vector3.zero;
        yield return new WaitForSeconds(timeToWait);

        if (isMovingForward)
        {
            StartCoroutine(MovePlatform(FinalPosition.position, InitialPosition.position, timeToTake));
            isMovingForward = false;
        }
        else
        {
            StartCoroutine(MovePlatform(InitialPosition.position, FinalPosition.position, timeToTake));
            isMovingForward = true;
        }
    }

}
