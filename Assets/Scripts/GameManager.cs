using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arithmetich;

public class GameManager : MonoBehaviour
{
    public GameObject agentTargetPoint;

    public static int currentSpawnCount = 1;

    public List<GameObject> agentObjectPool;
    public List<GameObject> spawnNpcParticles;
    public List<GameObject> DeadNpcParticles;
    public List<GameObject> agentBloodEffect;



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
    public void DeadNpcParticleEffect(Vector3 position, bool Balyoz = false)
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

        if (Balyoz)
        {
            Vector3 offset = new (position.x, 0.005f, position.z);
            foreach (var item in agentBloodEffect)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = offset;
                    break;
                }
            }
        }
    }   

    public void ShowCurrentSpawnCount()
    {
        Debug.Log($"Agent sayýsý: {GameManager.currentSpawnCount}");
    }


}
