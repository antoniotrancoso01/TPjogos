using UnityEngine;
using TMPro;

public class Porta : MonoBehaviour
{
    private bool portaAberta = false;
    private bool jogadorComChave = false;
    private bool jogadorPerto = false;

    [SerializeField] private TextMeshProUGUI mensagemChave; // Referência ao texto de mensagem

    private void Start()
    {
        // Certifique-se de que a mensagem está oculta no início
        if (mensagemChave != null)
        {
            mensagemChave.gameObject.SetActive(false);
        }
    }

    public void PegarChave()
    {
        jogadorComChave = true;
        Debug.Log("Chave foi coletada e a porta está pronta para abrir!");
    }

    private void Update()
    {
        // Verifica se o jogador está perto e pressionou "E"
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            if (jogadorComChave && !portaAberta)
            {
                AbrirPorta();
            }
            else
            {
                // Exibe a mensagem de que a chave está no celeiro
                if (mensagemChave != null)
                {
                    mensagemChave.gameObject.SetActive(true);

                    // Esconde a mensagem após 2 segundos
                    Invoke("EsconderMensagem", 2f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador entrou no TriggerArea
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            Debug.Log("Jogador está perto da porta");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quando o jogador sai da área da porta
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
        }
    }

    private void AbrirPorta()
    {
        portaAberta = true;
        Debug.Log("A porta foi aberta!");

        // Abra a porta e desative o Collider principal para permitir passagem
        transform.Rotate(0, 90, 0);
        GetComponent<Collider>().enabled = false; // Desativa o Collider principal
    }

    private void EsconderMensagem()
    {
        if (mensagemChave != null)
        {
            mensagemChave.gameObject.SetActive(false);
        }
    }
}
