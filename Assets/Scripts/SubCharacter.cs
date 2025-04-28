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

    private void LateUpdate()
    {
        agent.SetDestination(mainCharacterTarget.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 particlePosition = new (transform.position.x, 0.23f, transform.position.z);

            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DeadNpcParticleEffect(particlePosition);
            GameManager.currentSpawnCount--;
            Debug.Log($"Agent sayýsý: {GameManager.currentSpawnCount}");
        }

        if (other.CompareTag("Balyoz"))
        {
            Vector3 particlePosition = new(transform.position.x, 0.23f, transform.position.z);

            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DeadNpcParticleEffect(particlePosition, true);
            GameManager.currentSpawnCount--;
            Debug.Log($"Agent sayýsý: {GameManager.currentSpawnCount}");
        }
    }
}
