using UnityEngine;

public class TriggerMensagem : MonoBehaviour
{
    public MensagemTemporaria mensagemUI; // Refer�ncia ao script de mensagem

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mensagemUI != null)
        {
            mensagemUI.ExibirMensagem("N�o acredito! Eles mataram os meus pais! Esta quinta � tudo o que me resta e eu vou defend�-la at� ao fim!");
        }
    }
}
