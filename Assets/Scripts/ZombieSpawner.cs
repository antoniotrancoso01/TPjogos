using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombieTemplate;  // O primeiro template de zombie
    public GameObject zombieTemplate2; // O segundo template de zombie
    public int maxZombies = 10;        // Número máximo de zombies ativos ao mesmo tempo
    public float spawnInterval = 2f;  // Intervalo de spawn (em segundos)

    private int activeZombies = 0;    // Contador de zombies ativos

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            // Verifica se é possível spawnar mais zombies
            if (activeZombies + 2 <= maxZombies) // Adiciona espaço para 2 zombies
            {
                SpawnZombie(zombieTemplate);
                SpawnZombie(zombieTemplate2);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnZombie(GameObject zombieTemplate)
    {
        if (zombieTemplate != null)
        {
            // Clona o zombieTemplate
            GameObject newZombie = Instantiate(zombieTemplate, transform.position, Quaternion.identity);

            // Garante que o clone está ativo e visível
            newZombie.SetActive(true);

            activeZombies++;

            // Verifica se o zombie é destruído
            StartCoroutine(CheckZombieDestroyed(newZombie));
        }
        else
        {
            Debug.LogError("Zombie Template não foi atribuído no Inspector!");
        }
    }

    IEnumerator CheckZombieDestroyed(GameObject zombie)
    {
        // Aguarda até o zombie ser destruído
        yield return new WaitUntil(() => zombie == null);
        activeZombies--;
    }
}
