using UnityEngine;

public class ZombieDeathObserver : MonoBehaviour
{
    public GameObject capacetePrefab; // O prefab do capacete que será dropado

    private Vida vidaComponent; // Referência ao componente de vida do zumbi

    private void Start()
    {
        // Obtém o componente Vida no zumbi
        vidaComponent = GetComponent<Vida>();

        if (vidaComponent == null)
        {
            Debug.LogError($"ZombieDeathObserver: Nenhum componente 'Vida' encontrado no objeto {gameObject.name}.");
        }
    }

    private void Update()
    {
        if (vidaComponent != null && vidaComponent.isZombie && vidaComponent.vidaAtual <= 0)
        {
            // Quando o zumbi morrer, drope o capacete e destrua este script
            DropCapacete();
            Destroy(this);
        }
    }

    private void DropCapacete()
    {
        if (capacetePrefab != null)
        {
            // Instancia o capacete na posição do zumbi
            Instantiate(capacetePrefab, transform.position, Quaternion.identity);
            Debug.Log("Capacete dropado!");
        }
        else
        {
            Debug.LogWarning("ZombieDeathObserver: Prefab do capacete não atribuído.");
        }
    }
}