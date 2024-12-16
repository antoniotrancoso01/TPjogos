using UnityEngine;

public class municaopickuppistola : MonoBehaviour
{
    public int quantidadeMunicao = 10; // Quantidade de muni��o fornecida

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador colidiu
        if (other.CompareTag("Player"))
        {
            // Procura o script Shotgun na arma do jogador
            pistola pistola = other.GetComponentInChildren<pistola>();

            if (pistola != null)
            {
                // Adiciona a muni��o � reserva do jogador
                pistola.AddAmmo(quantidadeMunicao);

                // Destroi a caixa de muni��o
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("N�o foi encontrado um script pistola na arma do jogador!");
            }
        }
    }
}
