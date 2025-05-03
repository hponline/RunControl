using System.Collections.Generic;
using UnityEngine;



namespace Arithmetich
{
    public class ArithmeticOperation : MonoBehaviour
    {


        public static void Toplama(int intData, List<GameObject> agentObjectPool, Transform position, List<GameObject> spawnNpcParticles)
        {
            int number = 0;
            foreach (var agent in agentObjectPool)
            {
                if (number < intData)
                {
                    if (!agent.activeInHierarchy)
                    {
                        foreach (var deadParticle in spawnNpcParticles) // npc �l�m particle effect
                        {
                            if (!deadParticle.activeInHierarchy)
                            {
                                deadParticle.SetActive(true);
                                deadParticle.transform.position = position.transform.position;
                                deadParticle.GetComponent<ParticleSystem>().Play();
                                break;
                            }
                        }

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
        public static void Carpma(int intData, List<GameObject> agentObjectPool, Transform position, List<GameObject> spawnNpcParticles)
        {
            int numberOfCycles = (GameManager.currentSpawnCount * intData) - GameManager.currentSpawnCount;
            /*                                      10          *    3    -            =20
             * Gelen (number) say�y� mevcut npc (currentSpawnCount) say�m�z ile �arp�yoruz,
             * Sonra mevcut npc say�m�z� ��kar�yoruz,
             * Bu say� bize ka� defa d�ng�ye girmemizi veriyor.
             */
            int number = 0;
            foreach (var agent in agentObjectPool)
            {
                if (number < numberOfCycles)
                {
                    if (!agent.activeInHierarchy)
                    {
                        foreach (var deadParticle in spawnNpcParticles) // npc �l�m particle effect
                        {
                            if (!deadParticle.activeInHierarchy)
                            {                                
                                deadParticle.SetActive(true);
                                deadParticle.transform.position = position.transform.position;
                                deadParticle.GetComponent<ParticleSystem>().Play();
                                break;
                            }
                        }

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

        public static void Cikarma(int intData, List<GameObject> agentObjectPool, List<GameObject> deadNpcParticles)
        {            
            if (GameManager.currentSpawnCount < intData)
            {
                foreach (var agent in agentObjectPool) // npc klonlar�
                {
                    foreach (var deadParticle in deadNpcParticles) // npc �l�m particle effect
                    {
                        if (!deadParticle.activeInHierarchy)
                        {
                            Vector3 pos = new(agent.transform.position.x, 0.23f, agent.transform.position.z);

                            deadParticle.SetActive(true);
                            deadParticle.transform.position = pos;
                            deadParticle.GetComponent<ParticleSystem>().Play();
                            break;
                        }
                    }

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
                            foreach (var deadParticle in deadNpcParticles) // npc �l�m particle effect
                            {
                                if (!deadParticle.activeInHierarchy)
                                {
                                    Vector3 pos = new(agent.transform.position.x, 0.23f, agent.transform.position.z);

                                    deadParticle.SetActive(true);
                                    deadParticle.transform.position = pos;
                                    deadParticle.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }

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

        public static void Bolme(int intData, List<GameObject> agentObjectPool, List<GameObject> deadNpcParticles)
        {
            // Mevcut say� b�lenden daha k���kse hepsini false yapar.
            if (GameManager.currentSpawnCount <= intData)
            {
                foreach (var agent in agentObjectPool)
                {
                    foreach (var deadParticle in deadNpcParticles) // npc �l�m particle effect
                    {
                        if (!deadParticle.activeInHierarchy)
                        {
                            Vector3 pos = new(agent.transform.position.x, 0.23f, agent.transform.position.z);

                            deadParticle.SetActive(true);
                            deadParticle.transform.position = pos;
                            deadParticle.GetComponent<ParticleSystem>().Play();
                            break;
                        }
                    }
                    agent.transform.position = Vector3.zero;
                    agent.SetActive(false);
                }
                GameManager.currentSpawnCount = 1; // Main Karakterimiz
            }
            else
            {
                int division = GameManager.currentSpawnCount / intData; // B�lme say�s� kadar d�ng�
                int number = 0;
                foreach (var agent in agentObjectPool)
                {
                    if (number != division)
                    {
                        if (agent.activeInHierarchy)
                        {
                            foreach (var deadParticle in deadNpcParticles) // npc �l�m particle effect
                            {
                                if (!deadParticle.activeInHierarchy)
                                {
                                    Vector3 pos = new(agent.transform.position.x, 0.23f, agent.transform.position.z);

                                    deadParticle.SetActive(true);
                                    deadParticle.transform.position = pos;
                                    deadParticle.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }

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
                // Asal say� kontrol
                if (GameManager.currentSpawnCount % intData == 0)
                    GameManager.currentSpawnCount /= intData;

                else if (GameManager.currentSpawnCount % intData == 1)
                {
                    GameManager.currentSpawnCount /= intData;
                    GameManager.currentSpawnCount++; 
                    // Burada artt�rma nedenimiz oyun i�indeki toplam karakter say�s�n� e�itlemek i�indir.
                    // Bunu yapmazsak MainKarakterimiz toplam karakter say�s�na dahil edilmiyor.
                    // Oyun i�inde Fazla/Eksik mevcut karakter say�s� oluyor.
                }
                else if (GameManager.currentSpawnCount % intData == 2)
                {
                    GameManager.currentSpawnCount /= intData;
                    GameManager.currentSpawnCount +=2;
                }
                // 3e b�lme Hatal� olabilir kontrol et
                // B�lenler 3e kadar devam eder daha �st� �uanl�k yok.
            }
        }
    }
}

