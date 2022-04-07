using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Enemyontroller : MonoBehaviour
{

    public Transform Player;

    NavMeshAgent agent;

    public float attackRaduis = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        if (distance < attackRaduis)
        {
            agent.SetDestination(Player.position);
        }
    }



    private void OnTriggerEnter(Collider other )
    {
        if (other.CompareTag("Player"))
        {
           
            Destroy(gameObject);
        }
    }
}
