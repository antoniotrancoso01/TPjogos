using UnityEngine;

public class ObjetivosUI : MonoBehaviour
{
    public GameObject[] ticks; // Array que guarda as refer�ncias dos objetos "Tick"

    private int proximoObjetivo = 0; // �ndice do pr�ximo objetivo a ser marcado

    // Fun��o chamada ao completar um objetivo
    public void MarcarProximoObjetivo()
    {
        if (proximoObjetivo < ticks.Length)
        {
            ticks[proximoObjetivo].SetActive(true); // Ativa o tick do objetivo atual
            Debug.Log($"Objetivo {proximoObjetivo + 1} marcado!");
            proximoObjetivo++; // Incrementa para o pr�ximo objetivo
        }
        else
        {
            Debug.Log("Todos os objetivos j� foram marcados!");
        }
    }
}

