using UnityEngine;

public class PortasDungeonScript : MonoBehaviour
{
    public Animator portaAnimator;       // Refer�ncia ao Animator da porta
    public ObjetivosUI objetivosUI;      // Refer�ncia ao sistema de objetivos
    public string mensagemSemChave = "Voc� precisa da chave correta!";
    public string idChave;               // Identificador da chave necess�ria
    public TeamSeguirPlayer npcSeguir;   // Refer�ncia ao script de seguimento do NPC
    public Animator npcAnimator;         // Refer�ncia ao Animator do NPC

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

        // Ativar a anima��o da porta
        if (portaAnimator != null)
        {
            portaAnimator.SetTrigger("Abrir");
        }

        // Marcar o pr�ximo objetivo
        if (objetivosUI != null)
        {
            objetivosUI.MarcarProximoObjetivo();
        }

        // Ativar o seguimento do NPC
        if (npcSeguir != null)
        {
            npcSeguir.AtivarSeguimento();
        }

        // Ativar a anima��o de Walking
        if (npcAnimator != null)
        {
            npcAnimator.SetBool("IsWalking", true); // Ativa o par�metro "IsWalking"
        }

        Debug.Log("Porta aberta!");
    }
}