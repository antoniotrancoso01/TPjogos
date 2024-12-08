using UnityEngine;
using TMPro;

public class MensagemInicial : MonoBehaviour
{
    public TextMeshProUGUI mensagemTexto; // Referência ao texto da mensagem
    public string mensagem = "Oh não! O que se terá passado? Tenho de ir a casa ver se está tudo bem!"; // Mensagem a exibir
    public float tempoExibicao = 7f; // Tempo em segundos que a mensagem será exibida

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
        mensagemTexto.enabled = true; // Torna o texto visível

        yield return new WaitForSeconds(tempoExibicao); // Espera pelo tempo de exibição

        mensagemTexto.enabled = false; // Oculta o texto após o tempo
    }
}
