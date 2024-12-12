using UnityEngine;



public class MunicaoPickup : MonoBehaviour
{
    public int quantidadeMunicao = 10; // Quantidade de muni��o fornecida

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador colidiu
        if (other.CompareTag("Player"))
        {
            // Procura o script Shotgun na arma do jogador
            shotgun shotgun = other.GetComponentInChildren<shotgun>();

            if (shotgun != null)
            {
                // Adiciona a muni��o � reserva do jogador
                shotgun.AdicionarMunicao(quantidadeMunicao);

                // Destroi a caixa de muni��o
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("N�o foi encontrado um script Shotgun na arma do jogador!");
            }
        }
    }
}

