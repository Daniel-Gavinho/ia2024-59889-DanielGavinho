using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{

    public float LavaSpeed = 0.001f;
    public bool Lethal = false;

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
        float offsetX = Mathf.Cos(LavaSpeed * Time.time) * Mathf.Sin(LavaSpeed * Time.time) * randomX;
        float offsetY = (Mathf.Sin(LavaSpeed * Time.time) * Mathf.Cos(LavaSpeed * Time.time) + Mathf.Sin(LavaSpeed * Time.time) / 2)  * randomY;
        
        rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            if(Lethal)
            {
                gm.Die();
            } 
            else
            {
                gm.Player.GetComponent<PlayerStats>().TakeDamage(1);
                if(gm.Player.GetComponent<PlayerStats>().Health > 0)
                    gm.Player.GetComponent<PlayerMovement>().ToLastSafePosition();
                else gm.Die();
            }
        }
    }

}
