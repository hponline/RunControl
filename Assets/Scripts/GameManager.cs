using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arithmetich;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public int currentSpawnCount = 1;

    public List<GameObject> agentObjectPool;
    public List<GameObject> spawnNpcParticles;
    public List<GameObject> DeadNpcParticles;
    public List<GameObject> agentBloodEffect;

    [Header("Level Variables")]
    public GameObject player;
    public List<GameObject> enemyAgent;
    public int enemyCount;
    public bool isGameOver;
    [Tooltip("Son sahneye geldi mi")] bool isEndGame;

    private void Awake()
    {
        gameManagerInstance = this;
    }

    private void Start()
    {
        EnemySpawner();

    }

    // EndGame'e geldi mi kontrol eder.
    void GameState()
    {
        if (isEndGame)
        {            
            if (currentSpawnCount <= 1 || enemyCount == 0)
            {                
                isGameOver = true;

                #region Fight
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
                    Debug.Log("<color==red> Lose </color>");
                }
                else
                    Debug.Log("<color==green> Win </color>");

                #endregion
            }
            //ShowInfo();
        }        
    }

    public void ShowInfo()
    {
        Debug.Log("Kalan Agent Say�s�: " + currentSpawnCount);
        Debug.Log("Kalan D��man Say�s�: " + enemyCount);
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
                item.GetComponent<Enemy>().AnimationTrigger();
        }
        isEndGame = true;
        GameState();
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

            // ��karma
            case "Cikarma":
                ArithmeticOperation.Cikarma(intData, agentObjectPool, DeadNpcParticles);
                break;

            // �arpma
            case "Carpma":
                ArithmeticOperation.Carpma(intData, agentObjectPool, position, spawnNpcParticles);
                break;

            // B�lme
            case "Bolme":
                ArithmeticOperation.Bolme(intData, agentObjectPool, DeadNpcParticles);
                break;
        }
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
                deadParticle.GetComponent<AudioSource>().Play();
                if (!state)
                    currentSpawnCount--;
                else
                    enemyCount--;
                break;
            }
        }

        if (Balyoz) // Balyoz
        {
            Vector3 offset = new(position.x, 0.005f, position.z);
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
            GameState();

    }

}
