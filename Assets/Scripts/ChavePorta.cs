using UnityEngine;

public class ChavePorta : MonoBehaviour
{
    public Animator portaAnimator; // Refer�ncia ao Animator
    public MensagemManager uiManager; // Refer�ncia ao gerenciador de UI
    public string mensagemSemChave = "Voc� precisa da chave correta!";
    public string idChave; // Identificador da chave necess�ria
    public bool ativaObjetivo2 = false; // Define se esta porta ativa o objetivo 2
    public GameObject tickObjetivo02; // Refer�ncia ao objeto "Tick" do objetivo 2
    private bool estaAberta = false;

    private void Start()
    {
        if (ativaObjetivo2 && tickObjetivo02 != null)
        {
            tickObjetivo02.SetActive(false); // Certifique-se de que o "Tick" est� desativado no in�cio
        }
    }

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
                uiManager.MostrarMensagem(mensagemSemChave);
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

        // Ativa o Tick do objetivo 2 apenas se esta porta for a correta
        if (ativaObjetivo2 && tickObjetivo02 != null)
        {
            tickObjetivo02.SetActive(true);
            Debug.Log("Objetivo 2 completo: Tick ativado!");
        }
    }
}
