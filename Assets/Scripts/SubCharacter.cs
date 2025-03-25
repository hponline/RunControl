using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SubCharacter : MonoBehaviour
{
    GameObject mainCharacterTarget;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCharacterTarget = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().agentTargetPoint; // Main karakteri takip eder.
    }
    
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        agent.SetDestination(mainCharacterTarget.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            GameManager.currentSpawnCount--;
            Debug.Log($"Agent sayýsý: {GameManager.currentSpawnCount}");
        }
    }
}
