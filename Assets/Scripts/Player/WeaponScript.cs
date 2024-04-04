using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public bool hasWeapon = false;
    public GameObject Weapon;
    public GameObject Hitbox;

    private bool canAttack = true;
    private Animator anim;

    private void Start()
    {
        anim = Weapon.GetComponent<Animator>();
    }

    public void ObtainWeapon()
    {
        hasWeapon = true;
        Weapon.SetActive(true);
    }

    void Update()
    {
        if (hasWeapon && Input.GetButtonDown("Fire1") && canAttack)
        {
            anim.SetTrigger("Attack");
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        float time = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(time / 2);
        Hitbox.SetActive(true);
        yield return new WaitForSeconds(time / 2);
        Hitbox.SetActive(false);
        canAttack = true;
    }
}
