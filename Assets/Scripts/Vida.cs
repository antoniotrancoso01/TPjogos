using TMPro;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaInicial = 100;          // Vida inicial do jogador
    public int coleteInicial = 100;       // Colete inicial do jogador
    public int vidaMaxima = 100;          // Vida máxima
    public int coleteMaximo = 100;        // Colete máximo
    public bool fazerRespawn = false;     // Se o jogador pode fazer respawn
    public Vector3 posicaoRespawn;        // Posição de respawn do jogador
    public bool isZombie = false;         // Identifica se é um zombie

    public int vidaAtual;                // Vida atual
    private int coleteAtual;              // Colete atual
    private Animator animator;            // Apenas para zombies

    public GameObject caixaDeMunicaoPrefab;
    public GameObject caixaDeMunicaoPrefab2;

    // Referências para a UI
    public TextMeshProUGUI vidaTexto;
    public TextMeshProUGUI coleteTexto;

    void Start()
    {
        vidaAtual = vidaInicial;
        coleteAtual = coleteInicial;

        if (!isZombie)
        {
            posicaoRespawn = transform.position;
        }

        if (isZombie)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning($"{gameObject.name} não tem Animator associado.");
            }
        }

        AtualizarUI();
    }

    // Função pública para receber dano
    public void ReceberDano(int dano)
    {
        if (coleteAtual > 0)
        {
            int danoRestante = dano - coleteAtual;
            coleteAtual = Mathf.Max(0, coleteAtual - dano);

            if (danoRestante > 0)
            {
                vidaAtual = Mathf.Max(0, vidaAtual - danoRestante);
            }
        }
        else
        {
            vidaAtual = Mathf.Max(0, vidaAtual - dano);
        }

        Debug.Log($"{gameObject.name} recebeu {dano} de dano. Vida atual: {vidaAtual}, Colete atual: {coleteAtual}");
        AtualizarUI();

        if (vidaAtual <= 0)
        {
            if (isZombie)
            {
                MorrerZombie();
            }
            else
            {
                MorrerJogador();
            }
        }
    }

    // Função para aumentar a vida
    public void AumentarVida(int quantidade)
    {
        vidaAtual = Mathf.Min(vidaMaxima, vidaAtual + quantidade);
        AtualizarUI();
    }

    // Função para aumentar o colete
    public void AumentarColete(int quantidade)
    {
        coleteAtual = Mathf.Min(coleteMaximo, coleteAtual + quantidade);
        AtualizarUI();
    }

    // Atualiza a UI
    private void AtualizarUI()
    {
        if (vidaTexto != null)
        {
            vidaTexto.text = $"{vidaAtual}";
        }

        if (coleteTexto != null)
        {
            coleteTexto.text = $"{coleteAtual}";
        }
    }

    private void MorrerZombie()
    {
        DroparMunicao();
        FindObjectOfType<HordaController>().ZombieEliminado();
        if (animator != null)
        {
            animator.SetTrigger("isMorto");
        }
        DisableZombieBehavior();
        Invoke(nameof(Desaparecer), 30f);
    }

    private void MorrerJogador()
    {
        if (fazerRespawn)
        {
            Respawn();
        }
        else
        {
            Debug.Log($"{gameObject.name} morreu e não fará respawn.");
            // Pausar o jogo
            Time.timeScale = 0f;

            // Exibe a tela de morte
            var deathScreenManager = FindObjectOfType<DeathScreenManager>();
            if (deathScreenManager != null)
            {
                deathScreenManager.ShowDeathScreen();
            }
            else
            {
                Debug.LogError("DeathScreenManager não encontrado!");
            }
        }
    }

    private void Respawn()
    {
        transform.SetPositionAndRotation(posicaoRespawn, Quaternion.identity);
        vidaAtual = vidaInicial;
        coleteAtual = coleteInicial;
        AtualizarUI();
        Debug.Log($"{gameObject.name} fez respawn.");
    }

    private void Desaparecer()
    {
        Destroy(gameObject);
    }

    private void DisableZombieBehavior()
    {
        var seguirJogador = GetComponent<seguirOplayer>();
        if (seguirJogador != null)
        {
            seguirJogador.enabled = false;
        }

        var zombieAttack = GetComponent<zombiedamage>();
        if (zombieAttack != null)
        {
            zombieAttack.enabled = false;
        }

        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private void DroparMunicao()
    {
        if (caixaDeMunicaoPrefab != null && caixaDeMunicaoPrefab2 != null)
        {
            Vector3 posicaoDrop = transform.position + new Vector3(0, 0.5f, 0);

            if (Random.value < 0.5f)
            {
                Instantiate(caixaDeMunicaoPrefab, posicaoDrop, Quaternion.identity);
            }
            else
            {
                Quaternion rotacaoDrop = Quaternion.Euler(-90, 0, 0);
                Instantiate(caixaDeMunicaoPrefab2, posicaoDrop + new Vector3(0, 0.3f, 0), rotacaoDrop);
            }
        }
    }
}