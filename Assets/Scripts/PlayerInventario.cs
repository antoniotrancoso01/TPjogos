using System.Collections.Generic;
using UnityEngine;

public class PlayerInventario : MonoBehaviour
{
    private HashSet<string> chavesColetadas = new HashSet<string>(); // Armazena as chaves coletadas

    // Adiciona uma chave ao invent�rio
    public void AdicionarChave(string idChave)
    {
        chavesColetadas.Add(idChave);
    }

    // Verifica se o jogador tem a chave com o identificador espec�fico
    public bool TemChave(string idChave)
    {
        return chavesColetadas.Contains(idChave);
    }
}
