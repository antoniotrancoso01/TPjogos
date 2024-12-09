using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombieTemplate;  // O primeiro template de zombie
    public GameObject zombieTemplate2; // O segundo template de zombie
    public int maxZombies = 10;        // N�mero m�ximo de zombies ativos ao mesmo tempo
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
            // Verifica se � poss�vel spawnar mais zombies
            if (activeZombies + 2 <= maxZombies) // Adiciona espa�o para 2 zombies
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
