using UnityEngine;

public class municaopickuppistola : MonoBehaviour
{
    public int quantidadeMunicao = 10; // Quantidade de munição fornecida

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador colidiu
        if (other.CompareTag("Player"))
        {
            // Procura o script Shotgun na arma do jogador
            pistola pistola = other.GetComponentInChildren<pistola>();

            if (pistola != null)
            {
                // Adiciona a munição à reserva do jogador
                pistola.AddAmmo(quantidadeMunicao);

                // Destroi a caixa de munição
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Não foi encontrado um script pistola na arma do jogador!");
            }
        }
    }
}
