using UnityEngine;

public class ObjetivosUI : MonoBehaviour
{
    public GameObject[] ticks; // Array que guarda as referências dos objetos "Tick"

    private int proximoObjetivo = 0; // Índice do próximo objetivo a ser marcado

    // Função chamada ao completar um objetivo
    public void MarcarProximoObjetivo()
    {
        if (proximoObjetivo < ticks.Length)
        {
            ticks[proximoObjetivo].SetActive(true); // Ativa o tick do objetivo atual
            Debug.Log($"Objetivo {proximoObjetivo + 1} marcado!");
            proximoObjetivo++; // Incrementa para o próximo objetivo
        }
        else
        {
            Debug.Log("Todos os objetivos já foram marcados!");
        }
    }
}

