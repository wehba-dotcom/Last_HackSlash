using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{

 // public Transform Player;
    public Slider healthBar;
    NavMeshAgent agent;
    Animator anim;
    public int HP = 4;
    public int damageAmount= 3;
    bool canAttack = true;
    float attackCoolDown = 2f;

    public float attackRaduis = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.value= HP;
        anim.SetFloat("Speed", agent.velocity.magnitude);
        float distance = Vector3.Distance(transform.position, LevelManager.instance.player.position);
        if (distance < attackRaduis)
        {
            agent.SetDestination(LevelManager.instance.player.position);
           
            if (distance <= agent.stoppingDistance)
            {
                if(canAttack)
                {
                    StartCoroutine(cooldown());
                    anim.SetTrigger("Attack");
                    LevelManager.instance.player.GetComponent<PlayerScript>().takeDamage();
                }
            }

        }
    }
    IEnumerator cooldown()

    {
        canAttack = false;
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

 IEnumerator waitTime()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    public void takeDamage(int DamageAmount)
    {
        HP -= DamageAmount;
        if(HP <= 0)
        {
           anim.SetTrigger("die"); 
            StartCoroutine(waitTime());
        } 
        else
        {
           anim.SetTrigger("damage");
        }
    }


    private void OnTriggerEnter(Collider other )
    {
        if (other.CompareTag("Player"))
        {
           takeDamage(damageAmount);
          
        }
    }
}
