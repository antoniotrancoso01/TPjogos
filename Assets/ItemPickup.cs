using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum TipoItem
    {
        Colete,
        Vida
    }

    public TipoItem tipoItem;   // Define se � colete ou vida
    public int quantidade = 20; // Quantidade a ser adicionada
    public float tempoParaReaparecer = 60f; // Tempo para o item reaparecer

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var vidaScript = other.GetComponent<Vida>();
            if (vidaScript != null)
            {
                if (tipoItem == TipoItem.Colete)
                {
                    vidaScript.AumentarColete(quantidade);
                }
                else if (tipoItem == TipoItem.Vida)
                {
                    vidaScript.AumentarVida(quantidade);
                }

                // Desativa o item
                DesativarItem();
            }
        }
    }

    private void DesativarItem()
    {
        // Remove o item da hierarquia
        gameObject.SetActive(false);

        // Reativa o item ap�s o tempo definido
        Invoke(nameof(ReativarItem), tempoParaReaparecer);
    }

    private void ReativarItem()
    {
        // Adiciona o item de volta � hierarquia
        gameObject.SetActive(true);
    }
}
