using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject sliderPrefab;      // Prefab do Slider para a barra de vida
    public Transform spawnPoint;        // Ponto onde o slider será posicionado
    public Canvas canvas;               // Canvas onde o slider será colocado
    public int vidaMaxima = 100;        // Vida máxima do inimigo

    private int vidaAtual;              // Vida atual do inimigo
    private GameObject currentSlider;   // Instância atual do slider
    private Slider slider;              // Referência ao componente Slider

    void Start()
    {
        vidaAtual = vidaMaxima;         // Inicializa a vida do inimigo
        SpawnSlider();                  // Cria o slider no início
    }

    void Update()
    {
        if (currentSlider != null)
        {
            // Atualiza a posição do slider para estar acima do inimigo
            Vector3 position = Camera.main.WorldToScreenPoint(spawnPoint.position);
            currentSlider.transform.position = position;

            // Atualiza o valor do slider
            slider.value = (float)vidaAtual / vidaMaxima;
        }
    }

    private void SpawnSlider()
    {
        // Instancia o slider
        currentSlider = Instantiate(sliderPrefab, canvas.transform);
        slider = currentSlider.GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("Prefab do Slider não possui o componente Slider!");
        }
    }

    public void ReceberDano(int dano)
    {
        // Reduz a vida do inimigo
        vidaAtual = Mathf.Max(0, vidaAtual - dano);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        Destroy(currentSlider);   // Remove o slider
        Destroy(gameObject);      // Remove o inimigo
    }
}