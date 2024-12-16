using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaInicial = 100;       // Vida inicial do GameObject
    public bool fazerRespawn = false;  // Apenas para o jogador (ignorado pelos zombies)
    public Vector3 posicaoRespawn;     // Apenas para o jogador (ignorado pelos zombies)
    public bool isZombie = false;      // Identifica se o GameObject é um zombie

    private int vidaAtual;             // Vida atual do GameObject
    private Animator animator;         // Referência ao Animator (para zombies)

    public GameObject caixaDeMunicaoPrefab;
    public GameObject caixaDeMunicaoPrefab2;
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
        DroparMunicao();
        FindObjectOfType<HordaController>().ZombieEliminado();
        if (animator != null)
        {
            animator.SetTrigger("isMorto"); // Ativa a animação de morte
        }
        DisableZombieBehavior();
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
    private void DisableZombieBehavior()
    {
        var seguirJogador = GetComponent<seguirOplayer>();
        if (seguirJogador != null)
        {
            seguirJogador.enabled = false;  // Desativa o script
        }

        var zombieAttack = GetComponent<zombiedamage>();
        if (zombieAttack != null)
        {
            zombieAttack.enabled = false; // Desativa ataques do zombie
        }

        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // Opcional: desativa colisões físicas
        }
    }
    private void DroparMunicao()
    {
        if (caixaDeMunicaoPrefab != null && caixaDeMunicaoPrefab2 != null)
        {
            // Gera uma posição ligeiramente acima do zombie
            Vector3 posicaoDrop = transform.position + new Vector3(0, 0.5f, 0);

            // Decide aleatoriamente qual munição spawnar
            if (Random.value < 0.5f) // 50% de chance
            {
                // Dropa o primeiro tipo de munição
                Instantiate(caixaDeMunicaoPrefab, posicaoDrop, Quaternion.identity);
                Debug.Log("Tipo 1 de munição dropado.");
            }
            else
            {
                Quaternion rotacaoDrop = Quaternion.Euler(-90,0, 0); // Rotação de -90 no eixo Y
                Instantiate(caixaDeMunicaoPrefab2, posicaoDrop + new Vector3(0, 0.3f, 0), rotacaoDrop);
                Debug.Log("Tipo 2 de munição dropado com rotação.");
            }
        }
    }

}