using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   public void JogarMichael()
   {
        SceneManager.LoadScene(1);
   }

    public void JogarLeon()
    {
        SceneManager.LoadScene(2);
    }

   public void BotaoCreditos()
   {
       //SceneManager.LoadScene(2);
   }

   public void Sair()
   {
        Debug.Log("Quit");
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
