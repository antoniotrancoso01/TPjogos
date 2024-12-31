using UnityEngine;

public class CapacetePickup : MonoBehaviour
{
    public GameObject mensagemUI; // Referência ao painel da mensagem

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ativar o painel da mensagem
            if (mensagemUI != null)
            {
                mensagemUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None; // Liberar o cursor
                Cursor.visible = true;                 // Tornar o cursor visível
                Time.timeScale = 0f;                   // Pausar o jogo
            }

            // Desativar o capacete
            gameObject.SetActive(false);
        }
    }
}