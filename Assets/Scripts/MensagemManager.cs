using UnityEngine;
using TMPro;

public class MensagemManager : MonoBehaviour
{
    public TextMeshProUGUI mensagemTexto; // Referência ao texto da mensagem
    private float tempoExibicao = 2f; // Tempo em segundos para exibir a mensagem
    private float contadorTempo; // Contador para ocultar a mensagem

    private void Update()
    {
        if (contadorTempo > 0)
        {
            contadorTempo -= Time.deltaTime;
            if (contadorTempo <= 0)
            {
                OcultarMensagem();
            }
        }
    }

    public void MostrarMensagem(string mensagem)
    {
        mensagemTexto.text = mensagem;
        mensagemTexto.enabled = true; // Torna o texto visível
        contadorTempo = tempoExibicao; // Reinicia o contador
    }

    private void OcultarMensagem()
    {
        mensagemTexto.text = ""; // Limpa o texto
        mensagemTexto.enabled = false; // Torna o texto invisível
    }
}
