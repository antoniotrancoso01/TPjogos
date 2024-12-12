using UnityEngine;
using UnityEngine.AI;

public class TeamSeguirPlayer : MonoBehaviour
{
    public Transform jogador; // Refer�ncia ao Transform do jogador
    public float distanciaMinima = 2f; // Dist�ncia m�nima entre o NPC e o jogador
    private NavMeshAgent agente; // Refer�ncia ao NavMeshAgent

    private bool seguirJogador = false; // Define se o NPC deve seguir o jogador

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        if (agente == null)
        {
            Debug.LogError("NavMeshAgent n�o encontrado no NPC.");
        }
    }

    private void Update()
    {
        if (seguirJogador && jogador != null)
        {
            float distancia = Vector3.Distance(transform.position, jogador.position);

            if (distancia > distanciaMinima)
            {
                agente.SetDestination(jogador.position); // Define o destino do NPC como a posi��o do jogador
            }
            else
            {
                agente.ResetPath(); // Para de se mover quando est� perto o suficiente
            }
        }
    }

    // Fun��o para ativar o seguimento do jogador
    public void AtivarSeguimento()
    {
        seguirJogador = true;
    }
}
