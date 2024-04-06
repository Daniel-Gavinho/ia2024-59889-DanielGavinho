using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform InitialPosition;
    public Transform FinalPosition;
    public float timeToTake = 2;
    public float timeToWait = 2;
    public bool isActive = true;
    private bool isMovingForward = false;
    private Vector3 velocity;
    private bool changingDirection = true;
    private float timer = 0f;

    public Vector3 Velocity { get => velocity; }

    private void Start()
    {
        velocity = Vector3.zero;
        StartCoroutine(changeDirection());
    }

    void FixedUpdate()
    {
        if(!isActive) return;

        if(!changingDirection)
        {
            if (isMovingForward)
            {
                MovePlatform(InitialPosition.position, FinalPosition.position);
            }
            else
            {
                MovePlatform(FinalPosition.position, InitialPosition.position);
            }
        }
    }

    private void MovePlatform(Vector3 initialPosition, Vector3 finalPosition)
    {
        timer += Time.deltaTime;
        float percent = Mathf.Clamp01(timer / timeToTake);

        transform.position = Vector3.Lerp(initialPosition, finalPosition, percent);

        if(timer >= timeToTake)
        {
            timer = 0f;
            changingDirection = true;
            StartCoroutine(changeDirection());
        }
    }

    private IEnumerator changeDirection()
    {
        velocity = Vector3.zero;
        yield return new WaitForSeconds(timeToWait);

        isMovingForward = !isMovingForward;
        changingDirection = false;
        if(isMovingForward) {
            velocity = (FinalPosition.position - InitialPosition.position) / timeToTake;
        }
        else {
            velocity = (InitialPosition.position - FinalPosition.position) / timeToTake;
        }

        velocity.y = Mathf.Max(velocity.y, 0.0f);

        Debug.Log("Velocity: " + velocity);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            collision.gameObject.GetComponent<PlayerMovement>().MomentumJump();
            collision.gameObject.GetComponent<Rigidbody>().velocity += velocity;
            Debug.Log("Player jump velocity: " + collision.gameObject.GetComponent<Rigidbody>().velocity);
        }
    }

}
