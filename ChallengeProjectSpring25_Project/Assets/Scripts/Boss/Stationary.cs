using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Stationary : MonoBehaviour
{
    [SerializeField] NavMeshAgent enemy;
    [SerializeField] public GameObject[] minions;
    void Start()
    {
        
    }

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if(minions != null)
        {
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            if(minions == null)
            {
                enemy.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }
}
