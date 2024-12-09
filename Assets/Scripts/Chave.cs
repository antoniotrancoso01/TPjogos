using UnityEngine;
using TMPro;

public class Chave : MonoBehaviour
{
    public string idChave; // Identificador da chave

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Supondo que o jogador tem a Tag "Player"
        {
            Debug.Log("Chave pega: " + idChave);
            var inventario = other.GetComponent<PlayerInventario>();
            if (inventario != null)
            {
                inventario.AdicionarChave(idChave); // Adiciona a chave ao inventário
            }
            Destroy(gameObject); // Remove a chave do mundo
        }
    }
}

