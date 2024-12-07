using UnityEngine;
using System.Collections;
public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombieTemplate;  // O zombie existente na hierarquia
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
            if (activeZombies < maxZombies)
            {
                SpawnZombie();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnZombie()
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
