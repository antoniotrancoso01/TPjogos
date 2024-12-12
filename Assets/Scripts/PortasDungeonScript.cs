using UnityEngine;

public class PortasDungeonScript : MonoBehaviour
{
    public Animator portaAnimator; // Referência ao Animator da porta
    public ObjetivosUI objetivosUI; // Referência ao sistema de objetivos
    public string mensagemSemChave = "Você precisa da chave correta!";
    public string idChave; // Identificador da chave necessária
    public TeamSeguirPlayer npcSeguir; // Referência ao script de seguimento do NPC
    private bool estaAberta = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            var inventario = other.GetComponent<PlayerInventario>();

            if (inventario != null && inventario.TemChave(idChave) && !estaAberta)
            {
                AbrirPorta();
            }
            else if (inventario != null && !inventario.TemChave(idChave))
            {
                Debug.Log(mensagemSemChave);
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

        if (objetivosUI != null)
        {
            objetivosUI.MarcarProximoObjetivo();
        }

        if (npcSeguir != null)
        {
            npcSeguir.AtivarSeguimento(); // Ativa o seguimento do NPC
        }

        Debug.Log("Porta aberta!");
    }
}
