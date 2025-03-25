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
                ArithmeticOperation.Toplama(intData, agentObjectPool, position);
                break;

            // ��karma
            case "Cikarma":
                ArithmeticOperation.Cikarma(intData, agentObjectPool);
                break;

            // �arpma
            case "Carpma":
                ArithmeticOperation.Carpma(intData, agentObjectPool, position);
                break;         

            // B�lme
            case "Bolme":
                ArithmeticOperation.Bolme(intData, agentObjectPool);
                break;
        }
        ShowCurrentSpawnCount();
    }

    public void ShowCurrentSpawnCount()
    {
        Debug.Log($"Agent say�s�: {GameManager.currentSpawnCount}");
    }
}
