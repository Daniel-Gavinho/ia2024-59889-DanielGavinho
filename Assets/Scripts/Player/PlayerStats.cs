using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int Health = 3;
    private GameMasterScript gm;

    private void Start()
    {
        gm = GameMasterScript.Instance;
    }

    public void TakeDamage(int damage) {
        Health -= damage;
    }

    public void ResetHealth() {
        Health = 3;
    }
}
