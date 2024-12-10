using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private bool estaDentroDoCollider = false; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(estaDentroDoCollider)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(3);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Jogador")
        {
            estaDentroDoCollider=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Jogador")
        {
            estaDentroDoCollider = false;
        }
    }
}
