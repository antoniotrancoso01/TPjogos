using UnityEngine;

public class ObjetosVidaSpawn : MonoBehaviour
{
    public GameObject coletePrefab; // Prefab do GameObject "Colete"
    public GameObject caixaVidaPrefab; // Prefab do GameObject "CaixaVida"

    public Transform spawnPointColete; // Local específico para spawnar "Colete"
    public Transform spawnPointCaixaVida; // Local específico para spawnar "CaixaVida"

    public float intervaloSpawn = 60f; // Intervalo de spawn em segundos

    private void Start()
    {
        // Inicia os spawns repetidamente
        InvokeRepeating(nameof(SpawnItems), 0f, intervaloSpawn);
    }

    private void SpawnItems()
    {
        // Spawn do "Colete"
        if (coletePrefab != null && spawnPointColete != null)
        {
            Instantiate(coletePrefab, spawnPointColete.position, spawnPointColete.rotation);
        }

        // Spawn da "CaixaVida"
        if (caixaVidaPrefab != null && spawnPointCaixaVida != null)
        {
            Instantiate(caixaVidaPrefab, spawnPointCaixaVida.position, spawnPointCaixaVida.rotation);
        }
    }
}
