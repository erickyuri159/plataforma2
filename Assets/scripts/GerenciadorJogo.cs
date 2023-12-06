using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviour
{
    //verifica se o jogo esta logado ou nao
    public bool GameLigado = false;
    public GameObject TelaGameOver;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "fase1")
        {

        GameLigado=false;
        Time.timeScale = 0; 
        }else
        {
            GameLigado = true;
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool EstadoDoJogo()
    { 
        return GameLigado;
    }
    public void LigarJogo() 
    {
        GameLigado=true;
        Time.timeScale = 1;
    }
    public void PersonagemMorreu()
    {
        
       TelaGameOver.SetActive(true);
        GameLigado = false;
        Time.timeScale = 0;
    }
    //reiniciar
    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }
    
    public void fase3()
    {
        SceneManager.LoadScene(1);
    }
    public void vitoria()
    {
        SceneManager.LoadScene(2);
    }
}
