using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 1f;
    public float timeToBeScared = 3f;
    public bool Awake = false;
    public AttackColliderScript attackTrigger;
    public GameObject Orientation;
    private GameMasterScript gm;
    private Animator anim;
    private float timeToDie = 1.2f;
    private bool attacking = false;
    private bool scared = false;
    private float attackTime = 2.6f * 2.1f;
    private Coroutine attackCoroutine;

    public void StartChase()
    {
        Awake = true;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameMasterScript.Instance;
    }

    void Update()
    {
        if(!Awake) return;

        if (!attacking && !scared)
        {
            if (attackTrigger.CanAttack)
            {
                attackCoroutine = StartCoroutine(Attack(gm.Player.GetComponent<PlayerStats>()));
            }
            else
            {
                Move();
            }
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage();
        }
    }

    private void Move()
    {
        anim.SetFloat("Speed", 1f);
        Vector3 playerPosition = gm.Player.transform.position;
        Vector3 moveDirection = (playerPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.x, transform.position.y, playerPosition.z), speed * Time.deltaTime);
    }


    private IEnumerator Attack(PlayerStats player)
    {
        attacking = true;
        anim.SetFloat("Speed", 0f);
        anim.SetTrigger("Attacking");
        StartCoroutine(AttackCooldown());
        yield return new WaitForSeconds(0.8f);
        if (attackTrigger.CanAttack)
            player.TakeDamage(damage);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

    public void TakeDamage()
    {
        if(scared) return;
        scared = true; 
        attacking = false;
        if(attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        StartCoroutine(BackAway());
    }

    private IEnumerator BackAway()
    {
        anim.SetTrigger("DamageTaken");
        Vector3 moveDirection = -Orientation.transform.forward;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        Debug.Log("Move direction: " + moveDirection);
        float timer = 0f;
        while (timer < timeToBeScared)
        {
            timer += Time.deltaTime;
            transform.position += moveDirection * speed * Time.deltaTime;

            RaycastHit hit;
            Debug.DrawRay(transform.position, moveDirection * 1f, Color.red);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), moveDirection, out hit, 1f))
            {
                Debug.Log("Found Wall");
                break;
            }

            yield return null;
        }
        anim.SetTrigger("NotScared");
        yield return new WaitForSeconds(2.5f);
        scared = false;
    }


    public void Die()
    {
        anim.SetTrigger("Die");
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }
}
