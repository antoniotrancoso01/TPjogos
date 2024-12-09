using UnityEngine;
using TMPro;

public class MensagemInicial : MonoBehaviour
{
    public TextMeshProUGUI mensagemTexto; // Refer�ncia ao texto da mensagem
    public string mensagem = "Oh n�o! O que se ter� passado? Tenho de ir a casa ver se est� tudo bem!"; // Mensagem a exibir
    public float tempoExibicao = 7f; // Tempo em segundos que a mensagem ser� exibida

    private void Start()
    {
        if (mensagemTexto != null)
        {
            StartCoroutine(ExibirMensagem());
        }
    }

    private System.Collections.IEnumerator ExibirMensagem()
    {
        mensagemTexto.text = mensagem; // Define a mensagem
        mensagemTexto.enabled = true; // Torna o texto vis�vel

        yield return new WaitForSeconds(tempoExibicao); // Espera pelo tempo de exibi��o

        mensagemTexto.enabled = false; // Oculta o texto ap�s o tempo
    }
}
