using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
   [SerializeField] public NavMeshAgent enemy;
    [SerializeField] private GameObject player;
   
    void Start()
    {
        
    }

    private void Awake()
    {
       player = GameObject.Find("Player");
            enemy = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
       enemy.SetDestination(player.transform.position);//moves to play positon with assistance of the nav mesh
    }
}
