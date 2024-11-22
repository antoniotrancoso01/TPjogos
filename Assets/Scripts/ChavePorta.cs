using UnityEngine;

public class ChavePorta : MonoBehaviour
{
    public Animator portaAnimator; // Refer�ncia ao Animator
    public MensagemManager uiManager; // Refer�ncia ao gerenciador de UI
    public string mensagemSemChave = "Voc� precisa da chave correta!";
    public string idChave; // Identificador da chave necess�ria
    private bool estaAberta = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            var inventario = other.GetComponent<PlayerInventario>();

            if (inventario != null && inventario.TemChave(idChave) && !estaAberta)
            {
                Debug.Log("Porta aberta!");
                AbrirPorta();
            }
            else if (inventario != null && !inventario.TemChave(idChave))
            {
                Debug.Log("Mensagem: " + mensagemSemChave);
                uiManager.MostrarMensagem(mensagemSemChave); // Mostra a mensagem na tela
            }
        }
    }

    private void AbrirPorta()
    {
        estaAberta = true;

        if (portaAnimator != null)
        {
            portaAnimator.SetTrigger("Abrir");
        }
    }
}