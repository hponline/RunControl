using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //public GameObject agentSpawnPoint;
    public GameObject agentTargetPoint;

    public static int currentSpawnCount = 1;

    public List<GameObject> agentObjectPool;

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
    public void AgentSpawnManager(string data, Transform position)
    {
        switch (data)
        {
            // Çarpma
            case "X2":
                int number = 0;
                foreach (var agent in agentObjectPool)
                {
                    if (number < currentSpawnCount)
                    {
                        if (!agent.activeInHierarchy)
                        {
                            agent.transform.position = position.transform.position;
                            agent.SetActive(true);
                            number++;
                        }
                    }
                    else
                    {
                        number = 0;
                        break;
                    }
                }
                currentSpawnCount *= 2;
                break;

            // Arttýrma
            case "+3":
                int number1 = 0;
                foreach (var agent in agentObjectPool)
                {
                    if (number1 < 3)
                    {
                        if (!agent.activeInHierarchy)
                        {
                            agent.transform.position = position.transform.position;
                            agent.SetActive(true);
                            number1++;
                        }
                    }
                    else
                    {
                        number1 = 0;
                        break;
                    }
                }
                currentSpawnCount += 3;
                break;

            // Çýkarma
            case "-4":

                // Mevcut sayýdan küçük mü diye kontrol ediyoruz
                if (currentSpawnCount < 4)
                {
                    foreach (var agent in agentObjectPool)
                    {
                        agent.transform.position = Vector3.zero;
                        agent.SetActive(false);
                    }
                    currentSpawnCount = 1; // Main Karakterimiz
                }
                else
                {
                    int number2 = 0;
                    foreach (var agent in agentObjectPool)
                    {
                        if (number2 != 4)
                        {
                            if (agent.activeInHierarchy)
                            {
                                agent.transform.position = Vector3.zero;
                                agent.SetActive(false);
                                number2++;
                            }
                        }
                        else
                        {
                            number2 = 0;
                            break;
                        }
                    }
                    currentSpawnCount -= 4;
                }
                break;

            // Bölme
            case "/2":

                // 2 den aþaðýsý bölünebiliyorsa hepsini false yapar
                if (currentSpawnCount <= 2)
                {
                    foreach (var agent in agentObjectPool)
                    {
                        agent.transform.position = Vector3.zero;
                        agent.SetActive(false);
                    }
                    currentSpawnCount = 1; // Main Karakterimiz
                }
                else
                {
                    int division = currentSpawnCount / 2; // Bölme sayýsý kadar döngü
                    int number2 = 0;
                    foreach (var agent in agentObjectPool)
                    {
                        if (number2 != division)
                        {
                            if (agent.activeInHierarchy)
                            {
                                agent.transform.position = Vector3.zero;
                                agent.SetActive(false);
                                number2++;
                            }
                        }
                        else
                        {
                            number2 = 0;
                            break;
                        }
                    }
                    // Asal sayý kontrol
                    if (currentSpawnCount % 2 == 0)                    
                        currentSpawnCount /=2;
                    else
                    {
                        currentSpawnCount /= 2;
                        currentSpawnCount++;
                    }
                }
                break;
        }
        ShowCurrentSpawnCount();
    }

    public void ShowCurrentSpawnCount()
    {
        Debug.Log($"Agent sayýsý: {GameManager.currentSpawnCount}");
    }
}
