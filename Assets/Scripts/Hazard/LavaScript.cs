using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{

    public float lavaSpeed = 0.001f;

    private GameMasterScript gm;
    private Renderer rend;
    private float randomX;
    private float randomY;

    void Start()
    {
        rend = GetComponent<Renderer>();
        gm = GameMasterScript.Instance;
        randomX = Random.Range(0.1f, 0.5f);
        randomY = Random.Range(0.1f, 0.5f);
    }

    void Update()
    {
        float offsetX = Mathf.Cos(lavaSpeed * Time.time) * Mathf.Sin(lavaSpeed * Time.time) * randomX;
        float offsetY = (Mathf.Sin(lavaSpeed * Time.time) * Mathf.Cos(lavaSpeed * Time.time) + Mathf.Sin(lavaSpeed * Time.time) / 2)  * randomY;
        
        rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
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
