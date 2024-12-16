using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaInicial = 100;       // Vida inicial do GameObject
    public bool fazerRespawn = false;  // Apenas para o jogador (ignorado pelos zombies)
    public Vector3 posicaoRespawn;     // Apenas para o jogador (ignorado pelos zombies)
    public bool isZombie = false;      // Identifica se o GameObject � um zombie

    private int vidaAtual;             // Vida atual do GameObject
    private Animator animator;         // Refer�ncia ao Animator (para zombies)

    public GameObject caixaDeMunicaoPrefab;
    public GameObject caixaDeMunicaoPrefab2;
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
        DroparMunicao();
        FindObjectOfType<HordaController>().ZombieEliminado();
        if (animator != null)
        {
            animator.SetTrigger("isMorto"); // Ativa a anima��o de morte
        }
        DisableZombieBehavior();
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
            collider.enabled = false; // Opcional: desativa colis�es f�sicas
        }
    }
    private void DroparMunicao()
    {
        if (caixaDeMunicaoPrefab != null && caixaDeMunicaoPrefab2 != null)
        {
            // Gera uma posi��o ligeiramente acima do zombie
            Vector3 posicaoDrop = transform.position + new Vector3(0, 0.5f, 0);

            // Decide aleatoriamente qual muni��o spawnar
            if (Random.value < 0.5f) // 50% de chance
            {
                // Dropa o primeiro tipo de muni��o
                Instantiate(caixaDeMunicaoPrefab, posicaoDrop, Quaternion.identity);
                Debug.Log("Tipo 1 de muni��o dropado.");
            }
            else
            {
                Quaternion rotacaoDrop = Quaternion.Euler(-90,0, 0); // Rota��o de -90 no eixo Y
                Instantiate(caixaDeMunicaoPrefab2, posicaoDrop + new Vector3(0, 0.3f, 0), rotacaoDrop);
                Debug.Log("Tipo 2 de muni��o dropado com rota��o.");
            }
        }
    }

}