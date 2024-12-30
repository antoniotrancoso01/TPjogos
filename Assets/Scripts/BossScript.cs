using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public float vidaMaxima = 100f; // Vida máxima do zombie
    private float vidaAtual;

    public Slider barraDeVida; // Referência à barra de vida na interface
    public GameObject barraDeVidaUI; // GameObject que contém a barra de vida

    private void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarBarraDeVida();
    }

    public void LevarDano(float dano)
    {
        vidaAtual -= dano;
        if (vidaAtual <= 0)
        {
            vidaAtual = 0;
            Morrer();
        }

        AtualizarBarraDeVida();
    }

    private void AtualizarBarraDeVida()
    {
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual / vidaMaxima;
        }
    }

    private void Morrer()
    {
        Debug.Log("O zombie morreu!");
        Destroy(gameObject); // Destroi o zombie
        if (barraDeVidaUI != null)
        {
            barraDeVidaUI.SetActive(false); // Esconde a barra de vida
        }
    }

    public void ConfigurarBarraDeVida(Slider barra, GameObject barraUI)
    {
        barraDeVida = barra;
        barraDeVidaUI = barraUI;
        barraDeVidaUI.SetActive(true); // Ativa a barra de vida
    }
}

