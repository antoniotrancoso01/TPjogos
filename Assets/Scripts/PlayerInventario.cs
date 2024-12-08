using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerInventario : MonoBehaviour
{
    public int totalChaves = 3; // Total de chaves necess�rias
    private int chavesColetadas = 0; // Contador de chaves coletadas
    public TextMeshProUGUI textoObjetivo; // Refer�ncia ao texto "Apanhar chaves X/4"
    public GameObject tickObjetivo01; // Refer�ncia ao objeto "Tick" do objetivo 1

    private HashSet<string> inventarioChaves = new HashSet<string>();

    private void Start()
    {
        AtualizarTextoObjetivo();
        if (tickObjetivo01 != null)
        {
            tickObjetivo01.SetActive(false); // Certifique-se de que o "Tick" est� desativado no in�cio
        }
    }

    public void AdicionarChave(string idChave)
    {
        inventarioChaves.Add(idChave);
        chavesColetadas++; // Incrementa o n�mero de chaves coletadas
        AtualizarTextoObjetivo();

        // Verifica se o jogador coletou 3 chaves e ativa o Tick do objetivo 1
        if (chavesColetadas >= 3 && tickObjetivo01 != null)
        {
            tickObjetivo01.SetActive(true);
            Debug.Log("Objetivo 1 completo: Tick ativado!");
        }

        Debug.Log($"Chave {idChave} coletada! Total: {chavesColetadas}/{totalChaves}");
    }

    public bool TemChave(string idChave)
    {
        return inventarioChaves.Contains(idChave);
    }

    private void AtualizarTextoObjetivo()
    {
        if (textoObjetivo != null)
        {
            textoObjetivo.text = $"Apanhar chaves {chavesColetadas}/{totalChaves}";
        }
    }
}
