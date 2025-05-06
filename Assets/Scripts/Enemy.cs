using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    GameManager gameManager;
    public GameObject playerPos;
    bool isAttack;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
            gameManager.DeadNpcParticleEffect(particlePosition, false, true);
            GameManager.gameManagerInstance.enemyCount--;           
        }
    }
}
