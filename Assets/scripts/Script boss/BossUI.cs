using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
//using static UnityEditor.PlayerSettings;
public class BossUI : MonoBehaviour

{
    public Transform player;
    public float velocidadeMovimento = 3f;
    public float distanciaAtaque = 10f;
    public float taxaDeTiro = 1f;
    public GameObject projetilPrefab;
    public Transform pontoDeTiro;
    public int hp = 10;
    public bool podeTomarDano = true;
    private Animator Animacao;
    public bool vivo = true;
    public SpriteRenderer ImagemPersonagem;
    public float tempoParaProximoTiro = 6f;
    public bool pode_dano = true;
    public Image BarraHp;
    public GameObject BossPanel;
    public static BossUI Instance;
    private GameObject Jogador;
    public bool vendoJogador = false;
    public float posInicial;
    public float posFinal;
    public bool frente = true;
    public GameObject drop;
    public GameObject BarraGrandeBoss;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        BarraHp = GameObject.FindGameObjectWithTag("hp_barra_noel").GetComponent<Image>();
        Animacao = GetComponent<Animator>();

        //BossPanel.SetActive(false);

    }

    public void BossActivator()
    {
        BossPanel.SetActive(true);

    }
    public void BossDeactivator()
    {
        BossPanel.SetActive(false);

    }

    void Update()
    {
        // Movimento do boss em direção ao jogador
        //  MoverParaPlayer();
        Mover();
        // Verificar se o jogador está à distância de ataque
        if (Vector2.Distance(transform.position, player.position) < distanciaAtaque)
        {
            Animacao.SetTrigger("Atacar");
            // Atirar em direção ao jogador
            Atirar();
           
            
        }
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }
    }
   
    void Mover()
    {
        Animacao.SetBool("Andar", true);
        if (frente == true)
        {
            //para Onde eu quero IR
            Vector2 destino = new Vector2(posFinal, -8.42767f);
            //Me deslocando
            transform.position = Vector2.MoveTowards(transform.position, destino, 0.01f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (Vector2.Distance(transform.position, destino) < 0.2f)
            {
                frente = false;
            }
        }
        if (frente == false)
        {
            //para Onde eu quero IR
            Vector2 destino = new Vector2(posInicial, -8.42767f);
            //Me deslocando
            transform.position = Vector2.MoveTowards(transform.position, destino, 0.01f);
            transform.rotation = Quaternion.Euler(0, -180, 0);
            if (Vector2.Distance(transform.position, destino) < 0.2f)
            {
                frente = true;
            }
        }
    }

   /* void MoverParaPlayer()
    {
        // Calcular a direção para o jogador
        Vector2 direcao = player.position - transform.position;

        // Normalizar a direção para manter uma velocidade constante
        direcao.Normalize();

        // Mover o boss na direção do jogador
        transform.Translate(direcao * velocidadeMovimento * Time.deltaTime);
    }*/

    void Atirar()
    {
        tempoParaProximoTiro += Time.deltaTime;
        // Verificar se é hora de atirar novamente
        if (tempoParaProximoTiro > 3f)
        {
            // Calcular a direção para o jogador
            Vector2 direcaoTiro = player.position - pontoDeTiro.position;


            // Normalizar a direção para manter uma velocidade constante
            direcaoTiro.Normalize();

            // Instanciar o projetil
            GameObject projetil = Instantiate(projetilPrefab, pontoDeTiro.position, Quaternion.identity);


            // Aplicar força ao projetil na direção do jogador
            projetil.GetComponent<Rigidbody2D>().velocity = new Vector2(direcaoTiro.x, direcaoTiro.y) * 5f;

            // Atualizar o tempo para o próximo tiro
            tempoParaProximoTiro = 0;
            Destroy(projetil, 3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {



        if (other.gameObject.tag == "BalaBoss")
        {
            if (vivo == true)
            {

                UnityEngine.Debug.Log("Morri");

                if (podeTomarDano == true)
                {
                    hp--;

                    pode_dano = false;
                    ImagemPersonagem.color = UnityEngine.Color.red;
                    //morre se a vida for igual ou menor que 0
                    if (hp <= 0)
                    {
                        Animacao.SetTrigger("Morte");
                        Morto();
                    }
                }

            }
            if (other.gameObject.tag == "Ataque")
            {
                if (podeTomarDano == true)
                {
                    hp--;

                    pode_dano = false;
                    //ImagemPersonagem.color = UnityEngine.Color.red;
                    //morre se a vida for igual ou menor que 0
                    if (hp <= 0)
                    {
                        Animacao.SetTrigger("Morte");
                        Morto();
                    }
                }

            }
        }
    }


    public void DanoPlayer()
    {
        hp--;
        if (hp <= 0)
        {
            Animacao.SetTrigger("Morte");
            Morto();
        }
        PerderHP();
    }
    public void PerderHP()
    {

        int vida_parabarra = hp * 170;
        BarraHp.rectTransform.sizeDelta = new Vector2(vida_parabarra, 40);

    }
    void Morto()
    {
        Animacao.SetTrigger("Morte");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        vivo = false;
        Destroy(BarraGrandeBoss);
        Destroy(gameObject, 1f);
        Vector3 ponto = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
        GameObject BalaDispara = Instantiate(drop, ponto, Quaternion.identity);
    }
}