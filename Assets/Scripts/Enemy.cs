using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject playerPos;
    bool isAttack;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();    
    }

    public void AnimationTrigger()
    {
        GetComponent<Animator>().SetBool("IsAttack", true);
        isAttack = true;
    }

    private void LateUpdate()
    {
        if (isAttack)
        {
            agent.SetDestination(playerPos.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            Vector3 particlePosition = new(transform.position.x, 0.23f, transform.position.z);

            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DeadNpcParticleEffect(particlePosition, false, true);
            GameManager.currentSpawnCount--;            
        }
    }
}
