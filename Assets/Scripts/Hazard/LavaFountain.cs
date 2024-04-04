using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFountain : MonoBehaviour
{
    private GameMasterScript gm;
    private Animator anim;

    void Start() {
        gm = GameMasterScript.Instance;
        anim = GetComponent<Animator>();
        StartCoroutine(LavaFountainCoroutine());
    }

    IEnumerator LavaFountainCoroutine() {
            yield return new WaitForSeconds(Random.Range(0f, 3f));
            anim.Play("Move", 0, 0.5f);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.Die();
        }
    }
}
