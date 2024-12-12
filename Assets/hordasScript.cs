using System.Collections;
using UnityEngine;

public class hordasScript : MonoBehaviour
{
    public Transform[] spawnPoints;      // Array de pontos de spawn
    public GameObject zombiePrefab;      // Prefab do zombie
    public int zombiesIniciais = 5;      // N�mero inicial de zombies na primeira horda
    public float intervaloEntreHordas = 10f; // Tempo entre hordas (em segundos)
    public float intervaloEntreSpawns = 0.5f; // Tempo entre spawns de zombies na mesma horda
    public float fatorCrescimento = 1.5f; // Multiplicador para aumentar a quantidade de zombies

    private int numeroHorda = 0;         // Contador da horda atual
    private int zombiesPorHorda;         // Quantidade de zombies na horda atual

    void Start()
    {
        zombiesPorHorda = zombiesIniciais; // Configura os zombies da primeira horda
        StartCoroutine(GerirHordas());
    }

    IEnumerator GerirHordas()
    {
        while (true) // Hordas infinitas
        {
            numeroHorda++;
            Debug.Log($"Horda {numeroHorda} est� a come�ar com {zombiesPorHorda} zombies!");
            StartCoroutine(SpawnHorda(zombiesPorHorda));

            // Aguarda at� a pr�xima horda
            yield return new WaitForSeconds(intervaloEntreHordas);

            // Aumenta a quantidade de zombies para a pr�xima horda
            zombiesPorHorda = Mathf.CeilToInt(zombiesPorHorda * fatorCrescimento);
        }
    }

    IEnumerator SpawnHorda(int quantidade)
    {
        for (int i = 0; i < quantidade; i++)
        {
            // Escolhe um spawn point aleat�rio
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instancia o zombie
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

            // Espera um intervalo antes de spawnar o pr�ximo zombie
            yield return new WaitForSeconds(intervaloEntreSpawns);
        }
    }
}

