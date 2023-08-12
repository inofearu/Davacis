using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiGoTo : MonoBehaviour
{
    [SerializeField] Transform goal;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
