using UnityEngine;
using UnityEngine.AI;

public class TeamSeguirPlayer : MonoBehaviour
{
    public Transform jogador; // Referência ao Transform do jogador
    public float distanciaMinima = 2f; // Distância mínima entre o NPC e o jogador
    private NavMeshAgent agente; // Referência ao NavMeshAgent
    private Animator npcAnimator; // Referência ao Animator do NPC

    private bool seguirJogador = false; // Define se o NPC deve seguir o jogador

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        npcAnimator = GetComponentInChildren<Animator>();


        if (agente == null)
        {
            Debug.LogError("NavMeshAgent não encontrado no NPC.");
        }

        if (npcAnimator == null)
        {
            Debug.LogError("Animator não encontrado no NPC.");
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
                npcAnimator.SetBool("IsWalking", true); // Ativa a animação de Walking
                npcAnimator.SetFloat("Distancia", distancia);
            }
            else
            {
                agente.ResetPath(); // Para de se mover quando está perto o suficiente
                npcAnimator.SetBool("IsWalking", false); // Volta para Idle
                npcAnimator.SetFloat("Distancia", distancia);
            }
        }
    }

    // Função para ativar o seguimento do jogador
    public void AtivarSeguimento()
    {
        seguirJogador = true;
    }
}
