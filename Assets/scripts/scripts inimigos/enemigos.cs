using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;

public class enemigos : MonoBehaviour
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
    public static enemigos Instance;

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
        BarraHp = GameObject.FindGameObjectWithTag("BonecoHp").GetComponent<Image>();
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
        MoverParaPlayer();

        // Verificar se o jogador está à distância de ataque
        if (Vector2.Distance(transform.position, player.position) < distanciaAtaque)
        {
            // Atirar em direção ao jogador
            Atirar();
        }
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1);
        }
    }

    void MoverParaPlayer()
    {
        // Calcular a direção para o jogador
        Vector2 direcao = player.position - transform.position;

        // Normalizar a direção para manter uma velocidade constante
        direcao.Normalize();

        // Mover o boss na direção do jogador
        transform.Translate(direcao * velocidadeMovimento * Time.deltaTime);
    }

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
                        Animacao.SetTrigger("Dead");
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
                        Animacao.SetTrigger("Dead");
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
            Animacao.SetTrigger("Dead");
            Morto();
        }
        PerderHP();
    }
    public void PerderHP()
    {
        
        int vida_parabarra = hp * 97;
        BarraHp.rectTransform.sizeDelta = new Vector2(vida_parabarra, 40);

    }
    void Morto()
    {
       Animacao.SetTrigger("Dead");
       GetComponent<Collider2D>().enabled = false;
       GetComponent<Rigidbody2D>().gravityScale = 0;
       vivo = false;
        Destroy(BarraGrandeBoss);
       Destroy(gameObject, 1f);
    }
}

