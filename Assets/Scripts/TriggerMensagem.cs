using UnityEngine;

public class TriggerMensagem : MonoBehaviour
{
    public MensagemTemporaria mensagemUI; // Referência ao script de mensagem

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mensagemUI != null)
        {
            mensagemUI.ExibirMensagem("Não acredito! Eles mataram os meus pais! Esta quinta é tudo o que me resta e eu vou defendê-la até ao fim!");
        }
    }
}
