using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject sliderPrefab;      // Prefab do Slider para a barra de vida
    public Transform spawnPoint;        // Ponto onde o slider ser� posicionado
    public Canvas canvas;               // Canvas onde o slider ser� colocado
    public int vidaMaxima = 100;        // Vida m�xima do inimigo

    private int vidaAtual;              // Vida atual do inimigo
    private GameObject currentSlider;   // Inst�ncia atual do slider
    private Slider slider;              // Refer�ncia ao componente Slider

    void Start()
    {
        vidaAtual = vidaMaxima;         // Inicializa a vida do inimigo
        SpawnSlider();                  // Cria o slider no in�cio
    }

    void Update()
    {
        if (currentSlider != null)
        {
            // Atualiza a posi��o do slider para estar acima do inimigo
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
            Debug.LogError("Prefab do Slider n�o possui o componente Slider!");
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