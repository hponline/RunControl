using UnityEngine;
using UnityEngine.AI;

public class SubCharacter : MonoBehaviour
{
    GameManager gameManager;
    NavMeshAgent agent;
    public Transform agentTargetPoint;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void LateUpdate()
    {
        agent.SetDestination(agentTargetPoint.transform.position);
    }

    Vector3 NewPosition()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            gameManager.DeadNpcParticleEffect(NewPosition());
            GameManager.gameManagerInstance.currentSpawnCount--;
        }

        else if (other.CompareTag("Balyoz"))
        {
            gameObject.SetActive(false);
            gameManager.DeadNpcParticleEffect(NewPosition(), true);
            GameManager.gameManagerInstance.currentSpawnCount--;
        }

        else if (other.CompareTag("EnemyAgent"))
        {
            gameObject.SetActive(false);
            gameManager.DeadNpcParticleEffect(NewPosition());
            GameManager.gameManagerInstance.currentSpawnCount--;
        }

        else if (other.CompareTag("FreeNpc"))
        {
            GameManager.gameManagerInstance.agentObjectPool.Add(other.gameObject);            
        }
    }
}
