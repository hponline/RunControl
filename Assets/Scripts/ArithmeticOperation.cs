using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Arithmetich
{
    public class ArithmeticOperation : MonoBehaviour
    {


        public static void Toplama(int intData, List<GameObject> agentObjectPool, Transform position)
        {
            int number = 0;
            foreach (var agent in agentObjectPool)
            {
                if (number < intData)
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
            GameManager.currentSpawnCount += intData;            
        }

        public static void Cikarma(int intData, List<GameObject> agentObjectPool)
        {            
            if (GameManager.currentSpawnCount < intData)
            {
                foreach (var agent in agentObjectPool)
                {
                    agent.transform.position = Vector3.zero;
                    agent.SetActive(false);
                }
                GameManager.currentSpawnCount = 1; // Main Karakterimiz
            }
            else
            {
                int number = 0;
                foreach (var agent in agentObjectPool)
                {
                    if (number != intData)
                    {
                        if (agent.activeInHierarchy)
                        {
                            agent.transform.position = Vector3.zero;
                            agent.SetActive(false);
                            number++;
                        }
                    }
                    else
                    {
                        number = 0;
                        break;
                    }
                }
                GameManager.currentSpawnCount -= intData;
            }
        }

        public static void Carpma(int intData, List<GameObject> agentObjectPool, Transform position)
        {
            int numberOfCycles = (GameManager.currentSpawnCount * intData) - GameManager.currentSpawnCount;
            /*                                      10          *    3    -            =20
             * Gelen (number) sayýyý mevcut npc (currentSpawnCount) sayýmýz ile çarpýyoruz,
             * Sonra mevcut npc sayýmýzý çýkarýyoruz,
             * Bu sayý bize kaç defa döngüye girmemizi veriyor.
             */
            int number = 0;
            foreach (var agent in agentObjectPool)
            {
                if (number < numberOfCycles)
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
            GameManager.currentSpawnCount *= intData;
        }
        public static void Bolme(int intData, List<GameObject> agentObjectPool)
        {
            // Mevcut sayý bölenden daha küçükse hepsini false yapar.
            if (GameManager.currentSpawnCount <= intData)
            {
                foreach (var agent in agentObjectPool)
                {
                    agent.transform.position = Vector3.zero;
                    agent.SetActive(false);
                }
                GameManager.currentSpawnCount = 1; // Main Karakterimiz
            }
            else
            {
                int division = GameManager.currentSpawnCount / intData; // Bölme sayýsý kadar döngü
                int number = 0;
                foreach (var agent in agentObjectPool)
                {
                    if (number != division)
                    {
                        if (agent.activeInHierarchy)
                        {
                            agent.transform.position = Vector3.zero;
                            agent.SetActive(false);
                            number++;
                        }
                    }
                    else
                    {
                        number = 0;
                        break;
                    }
                }
                // Asal sayý kontrol
                if (GameManager.currentSpawnCount % intData == 0)
                    GameManager.currentSpawnCount /= intData;

                else if (GameManager.currentSpawnCount % intData == 1)
                {
                    GameManager.currentSpawnCount /= intData;
                    GameManager.currentSpawnCount++; 
                    // Burada arttýrma nedenimiz oyun içindeki toplam karakter sayýsýný eþitlemek içindir.
                    // Bunu yapmazsak MainKarakterimiz toplam karakter sayýsýna dahil edilmiyor.
                    // Oyun içinde Fazla/Eksik mevcut karakter sayýsý oluyor.
                }
                else if (GameManager.currentSpawnCount % intData == 2)
                {
                    GameManager.currentSpawnCount /= intData;
                    GameManager.currentSpawnCount +=2;
                }
                // 3e bölme Hatalý olabilir kontrol et
                // Bölenler 3e kadar devam eder daha üstü þuanlýk yok.
            }
        }
    }
}

