using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyController : MonoBehaviour
{

 // public Transform Player;

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



    public void takeDamage(int DamageAmount)
    {
        HP -= DamageAmount;
        if(HP <= 0)
        {
           anim.SetTrigger("die"); 
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
