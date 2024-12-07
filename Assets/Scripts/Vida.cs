using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaInicial = 100;       // Vida inicial do GameObject
    public bool fazerRespawn = false;  // Apenas para o jogador (ignorado pelos zombies)
    public Vector3 posicaoRespawn;     // Apenas para o jogador (ignorado pelos zombies)
    public bool isZombie = false;      // Identifica se o GameObject � um zombie

    private int vidaAtual;             // Vida atual do GameObject
    private Animator animator;         // Refer�ncia ao Animator (para zombies)

    // Start � chamado antes do primeiro frame update
    void Start()
    {
        vidaAtual = vidaInicial;

        // Guarda a posi��o inicial como respawn apenas para o jogador
        if (!isZombie)
        {
            posicaoRespawn = transform.position;
        }

        // Obt�m o Animator apenas se for um zombie
        if (isZombie)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning($"{gameObject.name} n�o tem Animator associado.");
            }
        }
    }

    // Fun��o p�blica para receber dano
    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log($"{gameObject.name} recebeu {dano} de dano. Vida atual: {vidaAtual}");

        if (vidaAtual <= 0)
        {
            if (isZombie)
            {
                MorrerZombie(); // L�gica para zombies
            }
            else
            {
                MorrerJogador(); // L�gica para o jogador
            }
        }
    }

    // L�gica para "morte" de zombies
    private void MorrerZombie()
    {
        if (animator != null)
        {
            animator.SetTrigger("isMorto"); // Ativa a anima��o de morte
        }

        Debug.Log($"{gameObject.name} morreu e desaparecer� em 30 segundos.");
        Invoke(nameof(Desaparecer), 30f); // Remove o zombie ap�s 30 segundos
    }

    // L�gica para "morte" do jogador
    private void MorrerJogador()
    {
        if (fazerRespawn)
        {
            Respawn();
        }
        else
        {
            gameObject.SetActive(false); // Desativa o jogador
            Debug.Log($"{gameObject.name} morreu e n�o far� respawn.");
        }
    }

    // L�gica de respawn para o jogador
    private void Respawn()
    {
        transform.SetPositionAndRotation(posicaoRespawn, Quaternion.identity);
        vidaAtual = vidaInicial; // Restaura a vida inicial
        Debug.Log($"{gameObject.name} fez respawn.");
    }

    // L�gica para desaparecer (usada pelos zombies)
    private void Desaparecer()
    {
        Destroy(gameObject); // Remove o zombie da cena
        Debug.Log($"{gameObject.name} foi destru�do.");
    }
}