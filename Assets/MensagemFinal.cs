using UnityEngine;
using UnityEngine.SceneManagement;
public class MensagemFinal : MonoBehaviour
{
    public GameObject painelMensagem; // Painel que contém a mensagem e o botão

    public void ExibirMensagem(string mensagem)
    {
        if (painelMensagem != null)
        {
            painelMensagem.SetActive(true); // Ativa o painel
            var textoMensagem = painelMensagem.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (textoMensagem != null)
            {
                textoMensagem.text = mensagem; // Define o texto no painel
            }
        }
    }

    public void OcultarMensagem()
    {
        if (painelMensagem != null)
        {
            painelMensagem.SetActive(false); // Desativa o painel
        }
    }
    public void VoltarParaMenu()
    {
        Time.timeScale = 1f;                     // Retomar o jogo
        Cursor.lockState = CursorLockMode.Locked; // Travar o cursor novamente
        Cursor.visible = false;                  // Ocultar o cursor
        SceneManager.LoadScene("SceneMenu");     // Carregar a cena do menu
    }
}