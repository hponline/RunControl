using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arithmetich;

public class GameManager : MonoBehaviour
{
    public static int currentSpawnCount = 1;
    public GameObject player;
    public GameObject agentTargetPoint;


    public List<GameObject> agentObjectPool;
    public List<GameObject> spawnNpcParticles;
    public List<GameObject> DeadNpcParticles;
    public List<GameObject> agentBloodEffect;

    [Header("Level Variables")]
    public List<GameObject> enemyAgent;
    public int enemyCount;
    public bool isGameOver;


    private void Start()
    {
        EnemySpawner();
    }

    void FightState()
    {
        if (currentSpawnCount == 1 || enemyCount == 0)
        {
            isGameOver = true;

            foreach (var item in enemyAgent)
            {
                if (item.activeInHierarchy)
                {
                    item.GetComponent<Animator>().SetBool("IsAttack", false);
                }
            }

            foreach (var item in agentObjectPool)
            {
                if (item.activeInHierarchy)
                {
                    item.GetComponent<Animator>().SetBool("IsEndGame", false);
                }
            }

            player.GetComponent<Animator>().SetBool("IsEndGame", false);

            if (currentSpawnCount < enemyCount || currentSpawnCount == enemyCount)
            {
                Debug.Log("Lose");
            }
            else
                Debug.Log("Win");
        }
    }

    public void EnemySpawner()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            enemyAgent[i].SetActive(true);
        }
    }

    public void EnemyTrigger()
    {
        foreach (var item in enemyAgent)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Enemy>().AnimationTrigger();
            }
        }
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
    public void DeadNpcParticleEffect(Vector3 position, bool Balyoz = false, bool state = false)
    {
        foreach (var deadParticle in DeadNpcParticles)
        {
            if (!deadParticle.activeInHierarchy)
            {                
                deadParticle.SetActive(true);
                deadParticle.transform.position = position;
                deadParticle.GetComponent<ParticleSystem>().Play();
                if (!state)
                    currentSpawnCount--;
                else
                    enemyCount--;
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

        if (!isGameOver)
            FightState();

    }   

    public void ShowCurrentSpawnCount()
    {
        Debug.Log($"Agent sayýsý: {GameManager.currentSpawnCount}");
    }


}
