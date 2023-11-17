using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esqueleto : MonoBehaviour
{
    public int hp = 100;
    public bool podeTomarDano = true;
    private Animator Animacao;
    public float posInicial;
    public float posFinal;
    public bool frente = true;
    private GameObject Jogador;
    public bool vendoJogador = false;
    public GameObject MeuAtk;
    public bool vivo = true;

    void Start()
    {
        Animacao = GetComponent<Animator>();
        Jogador = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(vivo == true)
        {
            Intel();
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
           // Animacao.SetBool("Andar", false);
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

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if(colidiu.gameObject.tag == "Ataque")
        {
            if(podeTomarDano == true)
            {
                hp--;
                podeTomarDano = false;
                Animacao.SetTrigger("TomouDano");
                if (hp <= 0)
                {
                    Animacao.SetBool("Morto", true);
                    Morto();
                }
            }
            
        }
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


    void Morto()
    {
        GetComponent<Collider2D>().enabled = false;
        vivo = false;
        Destroy(gameObject, 15f);
    }

}