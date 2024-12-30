using TMPro;
using UnityEngine;

public class HordaController : MonoBehaviour
{
    public TextMeshProUGUI hordaUI;       // Texto para mostrar o n�mero da horda
    public TextMeshProUGUI zombiesUI;    // Texto para mostrar zombies restantes
    public TextMeshProUGUI mortosUI;
  

    public ZombieSpawner[] spawners; // Array de Spawners
    public int zombiesIniciais = 5;  // N�mero inicial de zombies na primeira horda
    public float intervaloHorda = 10f; // Tempo entre hordas (em segundos)
    public float fatorAumento = 1.5f;  // Fator pelo qual os zombies aumentam a cada horda
    private int pontosTotais = 0;
    private int zombiesRestantes = 0;
    private int numeroHorda = 1;      // N�mero atual da horda
    private int zombiesPorHorda;     // Zombies a spawnar nesta horda
    private bool hordaAtiva = false; // Controla se a horda est� em andamento

    void Start()
    {
        pontosTotais = 0;
        zombiesPorHorda = zombiesIniciais;
        IniciarHorda();
    }

    void IniciarHorda()
    {
        Debug.Log($"Iniciando Horda {numeroHorda} com {zombiesPorHorda} zombies!");
        hordaAtiva = true;
        // Reseta o contador de zombies mortos
        zombiesMortos = 0;
        //zombiesRestantes = zombiesPorHorda;
        zombiesRestantes = 0; // Reseta o contador de zombies restantes

        // Divide os zombies entre os spawners e ajusta o total de zombies
        int zombiesPorSpawner = Mathf.CeilToInt((float)zombiesPorHorda / spawners.Length);
        foreach (var spawner in spawners)
        {
            spawner.IniciarSpawn(zombiesPorSpawner);
            zombiesRestantes += zombiesPorSpawner; // Soma os zombies spawnados por este spawner
        }

        // Atualiza os textos do UI
        AtualizarUI();
    }

    void FinalizarHorda()
    {
        Debug.Log($"Horda {numeroHorda} finalizada!");
        hordaAtiva = false;

        // Incrementa o n�mero da horda
        numeroHorda++;

        // Aumenta o n�mero de zombies para a pr�xima horda
        zombiesPorHorda = Mathf.RoundToInt(zombiesPorHorda * fatorAumento);

        // Inicia a pr�xima horda ap�s um intervalo
        Invoke(nameof(IniciarHorda), intervaloHorda);
    }
    private int zombiesMortos = 0; // Contador de zombies mortos na horda atual

    public void ZombieEliminado()
    {
        zombiesMortos++;
        pontosTotais=pontosTotais+100;
        zombiesRestantes--;
        Debug.Log($"Zombie eliminado! Total eliminados nesta horda: {zombiesMortos}");

        // Atualiza os textos do UI
        AtualizarUI();

        // Verifica se todos os zombies da horda atual foram eliminados
        //if (zombiesMortos >= zombiesPorHorda)
        //{
        //    Debug.Log("Todos os zombies desta horda foram eliminados!");
        //    FinalizarHorda();
        //}
        if (zombiesRestantes==0)
        {
            Debug.Log("Todos os zombies desta horda foram eliminados!");
            FinalizarHorda();
        }
    }
    void AtualizarUI()
    {
        if (hordaUI != null)
            hordaUI.text = $"Horda: {numeroHorda}";

        if (zombiesUI != null)
            zombiesUI.text = $"Zombies Restantes: {zombiesRestantes}";

        if (mortosUI != null)
            mortosUI.text = $"Pontua�ao total: {pontosTotais}";
    }

}
