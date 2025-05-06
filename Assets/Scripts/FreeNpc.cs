using UnityEngine;
using UnityEngine.AI;

public class FreeNpc : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public SkinnedMeshRenderer skinnedMesh;
    public Material targetMaterial;
    public GameObject player;
    bool isTrigger;


    private void LateUpdate()
    {
        if (isTrigger)
            agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Agent"))
        {
            if (gameObject.CompareTag("FreeNpc"))
            {
                MaterialAndAnimationTrigger();
                isTrigger = true;
            }            
        }

        else if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            GameManager.gameManagerInstance.DeadNpcParticleEffect(NewPosition());
            GameManager.gameManagerInstance.currentSpawnCount--;
        }

        else if (other.CompareTag("Balyoz"))
        {
            gameObject.SetActive(false);
            GameManager.gameManagerInstance.DeadNpcParticleEffect(NewPosition(), true);
            GameManager.gameManagerInstance.currentSpawnCount--;
        }

        else if (other.CompareTag("EnemyAgent"))
        {
            gameObject.SetActive(false);
            GameManager.gameManagerInstance.DeadNpcParticleEffect(NewPosition());
            GameManager.gameManagerInstance.currentSpawnCount--;
        }
    }

    Vector3 NewPosition()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }

    void MaterialAndAnimationTrigger()
    {
        Material[] materials = skinnedMesh.materials;
        materials[0] = targetMaterial;
        skinnedMesh.materials = materials;

        animator.SetBool("IsRun", true);

        GameManager.gameManagerInstance.currentSpawnCount++;
        gameObject.tag = "Agent";
    }
}
