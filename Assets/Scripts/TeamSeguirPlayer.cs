using UnityEngine;
using UnityEngine.AI;

public class TeamSeguirPlayer : MonoBehaviour
{
    public Transform jogador; // Referência ao Transform do jogador
    public float distanciaMinima = 2f; // Distância mínima entre o NPC e o jogador
    private NavMeshAgent agente; // Referência ao NavMeshAgent

    private bool seguirJogador = false; // Define se o NPC deve seguir o jogador

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        if (agente == null)
        {
            Debug.LogError("NavMeshAgent não encontrado no NPC.");
        }
    }

    private void Update()
    {
        if (seguirJogador && jogador != null)
        {
            float distancia = Vector3.Distance(transform.position, jogador.position);

            if (distancia > distanciaMinima)
            {
                agente.SetDestination(jogador.position); // Define o destino do NPC como a posição do jogador
            }
            else
            {
                agente.ResetPath(); // Para de se mover quando está perto o suficiente
            }
        }
    }

    // Função para ativar o seguimento do jogador
    public void AtivarSeguimento()
    {
        seguirJogador = true;
    }
}
