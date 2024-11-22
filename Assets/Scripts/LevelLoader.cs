using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        //Se pressionar qualquer cena
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Mudar de cena
            StartCoroutine(CarregarFase("Fase1"));
        }
    }

    // Corrotina- Coroutine
    IEnumerator CarregarFase (string nomeFase)
    {
        // Iniciar a anima��o 
        transition.SetTrigger("Start");


        //Esperar o tempo de anima��o
        yield return new WaitForSeconds(transitionTime);

        //Carregar a cena 
        SceneManager.LoadScene (nomeFase);
    }
}