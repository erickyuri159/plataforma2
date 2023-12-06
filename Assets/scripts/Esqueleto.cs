using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Esqueleto : MonoBehaviour
{
    public float  hp = 10;
    public bool podeTomarDano = true;
    private Animator Animacao;
    public float posInicial;
    public float posFinal;
    public bool frente = true;
    private GameObject Jogador;
    public bool vendoJogador = false;
    public GameObject MeuAtk;
    public bool vivo = true;
    // controla o jogo
    private GerenciadorJogo GJ;
    public GameObject drop;
    public SpriteRenderer BarraHp;


    void Start()
    {
        //recebe informaçao game objet
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorJogo>();
        Animacao = GetComponent<Animator>();
        Jogador = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        if (GJ.EstadoDoJogo() == true)
        {
            if (vivo == true)
            {
                Intel();
                PerdeEsqueletoHP();
            }
        }
        
    }

    void Intel()
    {
        if (Vector2.Distance(transform.position, Jogador.transform.position) <= 1.9f)
        {
            //Debug.Log("CHAMOU ATAQUE");
            Animacao.SetTrigger("Atacar");
            
        }else if (Vector2.Distance(transform.position, Jogador.transform.position) <= 2f)
        {
            vendoJogador = true;
            Animacao.SetBool("Andar", false);
        }else if (Vector2.Distance(transform.position, Jogador.transform.position) > 2f)
        {
            vendoJogador = false;
            Mover();
        }
    }

    void Mover()
    {
        Animacao.SetBool("Andar", true);
        if (frente == true)
        {
            //para Onde eu quero IR
            Vector2 destino = new Vector2(posFinal, transform.position.y);
            //Me deslocando
            transform.position = Vector2.MoveTowards(transform.position, destino, 0.01f);
            transform.localScale = new Vector3(1, 1, 1);
            if(Vector2.Distance(transform.position, destino) < 0.2f)
            {
                frente = false;
            }
        }
        if (frente == false)
        {
            //para Onde eu quero IR
            Vector2 destino = new Vector2(posInicial, transform.position.y);
            //Me deslocando
            transform.position = Vector2.MoveTowards(transform.position, destino, 0.01f);
            transform.localScale = new Vector3(-1, 1, 1);
            if (Vector2.Distance(transform.position, destino) < 0.2f)
            {
                frente = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        Debug.Log(colidiu.gameObject.tag);
        if(colidiu.gameObject.tag == "Ataque")
        {
            //if(podeTomarDano == true)
            //{
                hp--;
                //podeTomarDano = false;
                //Animacao.SetTrigger("TomouDano");
                if (hp <= 0)
                {
                    Animacao.SetBool("Morto", true);
                    Morto();
                }
            //}
            
        }
    }
    public void DanoEsqueletoPlayer()
    {
        hp--;
        if (hp <= 0)
        {
            Animacao.SetBool("Morto", true);
            Morto();
        }
        PerdeEsqueletoHP();
    }
    
    public void AcabouImunidade()
    {
        podeTomarDano = true;
    }


    public void AtivaAtk()
    {
        MeuAtk.SetActive(true);
    }

    public void DesativaAtk()
    {
        MeuAtk.SetActive(false);
    }
   public void  PerdeEsqueletoHP()
    {
        float vida_parabarra = hp * 0.125f;
        BarraHp.transform.localScale = new Vector3(vida_parabarra, 0.1f, 1f);
    }

    void Morto()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        vivo = false;
        Destroy(gameObject, 5f);
        Vector3 ponto = new Vector3(transform.position.x, transform.position.y + 1.7f, transform.position.z);
        GameObject BalaDispara = Instantiate(drop, ponto, Quaternion.identity);
    }

}