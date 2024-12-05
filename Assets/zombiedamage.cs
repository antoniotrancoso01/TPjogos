using UnityEngine;

public class zombiedamage : MonoBehaviour
{
    public int dano = 10; // Quantidade de dano que o zombie causa
    public float intervaloDeDano = 1.0f; // Intervalo entre os ataques

    private float tempoUltimoDano = 0f; // Controla o tempo do último ataque

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica se o objeto que entrou no trigger tem o script de vida
            Vida vidaScript = other.GetComponent<Vida>();

            if (vidaScript != null && Time.time >= tempoUltimoDano + intervaloDeDano)
            {
                // Aplica dano ao jogador
                vidaScript.ReceberDano(dano);

                // Atualiza o tempo do último dano
                tempoUltimoDano = Time.time;
            }
        }
    }
}
