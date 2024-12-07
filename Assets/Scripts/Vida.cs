using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaInicial = 100;       // Vida inicial do GameObject
    public bool fazerRespawn = false;  // Apenas para o jogador (ignorado pelos zombies)
    public Vector3 posicaoRespawn;     // Apenas para o jogador (ignorado pelos zombies)
    public bool isZombie = false;      // Identifica se o GameObject é um zombie

    private int vidaAtual;             // Vida atual do GameObject
    private Animator animator;         // Referência ao Animator (para zombies)

    // Start é chamado antes do primeiro frame update
    void Start()
    {
        vidaAtual = vidaInicial;

        // Guarda a posição inicial como respawn apenas para o jogador
        if (!isZombie)
        {
            posicaoRespawn = transform.position;
        }

        // Obtém o Animator apenas se for um zombie
        if (isZombie)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning($"{gameObject.name} não tem Animator associado.");
            }
        }
    }

    // Função pública para receber dano
    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log($"{gameObject.name} recebeu {dano} de dano. Vida atual: {vidaAtual}");

        if (vidaAtual <= 0)
        {
            if (isZombie)
            {
                MorrerZombie(); // Lógica para zombies
            }
            else
            {
                MorrerJogador(); // Lógica para o jogador
            }
        }
    }

    // Lógica para "morte" de zombies
    private void MorrerZombie()
    {
        if (animator != null)
        {
            animator.SetTrigger("isMorto"); // Ativa a animação de morte
        }

        Debug.Log($"{gameObject.name} morreu e desaparecerá em 30 segundos.");
        Invoke(nameof(Desaparecer), 30f); // Remove o zombie após 30 segundos
    }

    // Lógica para "morte" do jogador
    private void MorrerJogador()
    {
        if (fazerRespawn)
        {
            Respawn();
        }
        else
        {
            gameObject.SetActive(false); // Desativa o jogador
            Debug.Log($"{gameObject.name} morreu e não fará respawn.");
        }
    }

    // Lógica de respawn para o jogador
    private void Respawn()
    {
        transform.SetPositionAndRotation(posicaoRespawn, Quaternion.identity);
        vidaAtual = vidaInicial; // Restaura a vida inicial
        Debug.Log($"{gameObject.name} fez respawn.");
    }

    // Lógica para desaparecer (usada pelos zombies)
    private void Desaparecer()
    {
        Destroy(gameObject); // Remove o zombie da cena
        Debug.Log($"{gameObject.name} foi destruído.");
    }
}