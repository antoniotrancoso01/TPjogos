//using UnityEngine;
//using System.Collections;

//public class ZombieSpawner : MonoBehaviour
//{
//    public GameObject zombieTemplate;  // O primeiro template de zombie
//    public GameObject zombieTemplate2; // O segundo template de zombie
//    public int maxZombies = 10;        // N�mero m�ximo de zombies ativos ao mesmo tempo
//    public float spawnInterval = 2f;  // Intervalo de spawn (em segundos)
//    public float spawnRadius = 3f;    // Raio da �rea onde os zombies podem spawnar

//    private int activeZombies = 0;    // Contador de zombies ativos

//    void Start()
//    {
//        StartCoroutine(SpawnZombies());
//    }

//    IEnumerator SpawnZombies()
//    {
//        while (true)
//        {
//            // Verifica se � poss�vel spawnar mais zombies
//            if (activeZombies + 2 <= maxZombies) // Adiciona espa�o para 2 zombies
//            {
//                SpawnZombie(zombieTemplate);
//                SpawnZombie(zombieTemplate2);
//            }
//            yield return new WaitForSeconds(spawnInterval);
//        }
//    }

//    void SpawnZombie(GameObject zombieTemplate)
//    {
//        if (zombieTemplate != null)
//        {
//            // Adiciona uma posi��o aleat�ria dentro de um c�rculo
//            Vector3 randomOffset = new Vector3(
//                Random.Range(-spawnRadius, spawnRadius),
//                0,
//                Random.Range(-spawnRadius, spawnRadius)
//            );

//            // Define a nova posi��o de spawn
//            Vector3 spawnPosition = transform.position + randomOffset;

//            // Clona o zombieTemplate na nova posi��o
//            GameObject newZombie = Instantiate(zombieTemplate, spawnPosition, Quaternion.identity);

//            // Garante que o clone est� ativo e vis�vel
//            newZombie.SetActive(true);

//            activeZombies++;

//            // Verifica se o zombie � destru�do
//            StartCoroutine(CheckZombieDestroyed(newZombie));
//        }
//        else
//        {
//            Debug.LogError("Zombie Template n�o foi atribu�do no Inspector!");
//        }
//    }

//    IEnumerator CheckZombieDestroyed(GameObject zombie)
//    {
//        // Aguarda at� o zombie ser destru�do
//        yield return new WaitUntil(() => zombie == null);
//        activeZombies--;
//    }
//}
using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombieTemplate;  // O primeiro template de zombie
    public GameObject zombieTemplate2; // O segundo template de zombie
    public float spawnInterval = 2f;   // Intervalo de spawn (em segundos)
    public float spawnRadius = 3f;     // Raio da �rea onde os zombies podem spawnar

    private int zombiesParaSpawnar = 0; // Zombies que ainda precisam de ser spawnados
    private int activeZombies = 0;     // Contador de zombies ativos

    public void IniciarSpawn(int quantidade)
    {
        zombiesParaSpawnar += quantidade; // Adiciona a quantidade de zombies � fila
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (zombiesParaSpawnar > 0)
        {
            // Spawn do primeiro tipo de zombie
            SpawnZombie(zombieTemplate);
            zombiesParaSpawnar--;

            if (zombiesParaSpawnar > 0)
            {
                // Spawn do segundo tipo de zombie
                SpawnZombie(zombieTemplate2);
                zombiesParaSpawnar--;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnZombie(GameObject zombieTemplate)
    {
        if (zombieTemplate != null)
        {
            // Adiciona uma posi��o aleat�ria dentro de um c�rculo
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,
                Random.Range(-spawnRadius, spawnRadius)
            );

            // Define a nova posi��o de spawn
            Vector3 spawnPosition = transform.position + randomOffset;

            // Clona o zombieTemplate na nova posi��o
            GameObject newZombie = Instantiate(zombieTemplate, spawnPosition, Quaternion.identity);

            // Garante que o clone est� ativo e vis�vel
            newZombie.SetActive(true);

            activeZombies++;

            // Verifica se o zombie � destru�do
            StartCoroutine(CheckZombieDestroyed(newZombie));
        }
        else
        {
            Debug.LogError("Zombie Template n�o foi atribu�do no Inspector!");
        }
    }

    IEnumerator CheckZombieDestroyed(GameObject zombie)
    {
        // Aguarda at� o zombie ser destru�do
        yield return new WaitUntil(() => zombie == null);
        activeZombies--;
    }
}
