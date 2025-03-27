using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arithmetich;

public class GameManager : MonoBehaviour
{

    //public GameObject agentSpawnPoint;
    public GameObject agentTargetPoint;

    public static int currentSpawnCount = 1;

    public List<GameObject> agentObjectPool;
    public List<GameObject> spawnNpcParticles;
    public List<GameObject> DeadNpcParticles;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    foreach (var agent in agentObjectPool)
        //    {
        //        if (!agent.activeInHierarchy)
        //        {
        //            agent.transform.position = agentSpawnPoint.transform.position;
        //            agent.SetActive(true);
        //            currentSpawnCount++;
        //            break;
        //        }
        //    }
        //}
    }


    // NPC SPAWN
    public void AgentSpawnManager(string stringData, int intData, Transform position)
    {
        switch (stringData)
        {
            // Toplama
            case "Toplama":
                ArithmeticOperation.Toplama(intData, agentObjectPool, position, spawnNpcParticles);
                break;

            // Çýkarma
            case "Cikarma":
                ArithmeticOperation.Cikarma(intData, agentObjectPool, DeadNpcParticles);
                break;

            // Çarpma
            case "Carpma":
                ArithmeticOperation.Carpma(intData, agentObjectPool, position, spawnNpcParticles);
                break;         

            // Bölme
            case "Bolme":
                ArithmeticOperation.Bolme(intData, agentObjectPool, DeadNpcParticles);
                break;
        }
        ShowCurrentSpawnCount();
    }

    // Npc Death ParticleEffect
    public void DeadNpcParticleEffect(Vector3 position)
    {
        foreach (var deadParticle in DeadNpcParticles)
        {
            if (!deadParticle.activeInHierarchy)
            {                
                deadParticle.SetActive(true);
                deadParticle.transform.position = position;
                deadParticle.GetComponent<ParticleSystem>().Play();
                currentSpawnCount--;
                break;
            }
        }
    }   

    public void ShowCurrentSpawnCount()
    {
        Debug.Log($"Agent sayýsý: {GameManager.currentSpawnCount}");
    }


}
