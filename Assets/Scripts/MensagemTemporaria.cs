using UnityEngine;
using TMPro;

public class MensagemTemporaria : MonoBehaviour
{
    public TextMeshProUGUI mensagemTexto; // Refer�ncia ao texto
    public CanvasGroup canvasGroup;      // Para controlar a visibilidade
    public float duracaoMensagem = 7f;   // Dura��o da mensagem

    private void Start()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0; // Certifique-se de que o texto est� invis�vel no in�cio
        }
    }

    public void ExibirMensagem(string mensagem)
    {
        StopAllCoroutines(); // Interrompe mensagens anteriores, se houver
        StartCoroutine(MostrarMensagemCoroutine(mensagem));
    }

    private System.Collections.IEnumerator MostrarMensagemCoroutine(string mensagem)
    {
        if (mensagemTexto != null && canvasGroup != null)
        {
            mensagemTexto.text = mensagem;
            canvasGroup.alpha = 1; // Torna o texto vis�vel
            yield return new WaitForSeconds(duracaoMensagem);
            canvasGroup.alpha = 0; // Torna o texto invis�vel novamente
        }
    }
}
