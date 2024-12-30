using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutSceneManager : MonoBehaviour
{
    public float fadeDuration = 1.5f; // Duração do fade
    public string nextSceneName;      // Nome da próxima cena
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // Inicia o fade-in
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        // Gradualmente reduz o alpha para 0
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        // Inicia a contagem para trocar de cena
        yield return new WaitForSeconds(10f);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        canvasGroup.blocksRaycasts = true;

        // Gradualmente aumenta o alpha para 1
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1;

        // Carrega a próxima cena
        SceneManager.LoadScene(nextSceneName);
    }
}
