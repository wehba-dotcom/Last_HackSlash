using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Enemyontroller : MonoBehaviour
{

  //  public Transform Player;

    NavMeshAgent agent;
    Animator anim;

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
                    StartCoroutine(coolDown());
                    anim.SetTrigger("Attack");
                    LevelManager.instance.player.GetComponent<PlayerScript>().takeDamage();
                }
            }

        }
    }
    IEnumerator coolDown()

    {
        canAttack = false;
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }



    private void OnTriggerEnter(Collider other )
    {
        if (other.CompareTag("Player"))
        {
           
            Destroy(gameObject);
        }
    }
}
