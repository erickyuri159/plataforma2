using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Diagnostics;

public class MovimentoPersona : MonoBehaviour
{
    public static MovimentoPersona instance;
    public GameObject pontoDisparo;
    // pega o componente RigidiBody2D
    public Rigidbody2D Corpo;
    // recebe a velocidade do personagem
    public float velocidade;
    public float velExtra = 0;
    // pega o componente SpriteRenderer
    public SpriteRenderer ImagemPersonagem;
    // quantidade de pulos que meu personagem realizou
    public int qtd_pulo = 0;
    // controlar quando posso pular novamente 
    public float meuTempoPulo = 0;
    //booleana que me diz se posso pular
    public bool pode_pular = true;
    // vida do personagem
    public int vida = 10;
    public float meuTempoDano = 0;
    public bool pode_dano = true;
    // barra HP
    public Image BarraHp;
    // Start is called before the first frame update
    // add moeda
    public int moedas = 0;
    private Text Moeda_texto;
    //disparo bala
    public GameObject Bala;
    private float meuTempoTiro = 0;
    private bool pode_atirar = true;
    private Animator Animador;
    
    public int Municao = 5;
    private Text Municao_texto;
    //chance jogo
    private int Chances = 3;
    private Text Chances_texto;
    //variavel posição inical
    public Vector3 posInicial;
    float velX, velY;
    // controla o jogo
    private GerenciadorJogo GJ;
    public Vector3 PlayerPosition { get { return transform.position; } }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //recebe informaçao game objet
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorJogo>();
        //determinaçao da posição
        posInicial = new Vector3(-18.6f, -6.22f, 0);
        //muda a posição
        transform.position = posInicial;

        BarraHp = GameObject.FindGameObjectWithTag("hp_barra").GetComponent<Image>();
        Moeda_texto = GameObject.FindGameObjectWithTag("moeda_text_tag").GetComponent<Text>();
        Animador = GetComponent<Animator>();
        Chances_texto = GameObject.FindGameObjectWithTag("Chance_texto_tag").GetComponent<Text>();
        Municao_texto = GameObject.FindGameObjectWithTag("Municao_texto_tag").GetComponent<Text>();
        Municao_texto.text = Municao.ToString();
        Chances_texto.text = "VIDAS: " + Chances.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GJ.EstadoDoJogo() == true)
        {
            Mover();
            pular();
            dano();
            Atirar();
            ataquePerto();
        }



    }
    void Mover()//responsavel por mover o personagem
    {
        float velX = Input.GetAxis("Horizontal")* (4 + velExtra);
       float velY = Corpo.velocity.y;
        
    
       Corpo.velocity = new Vector2 (velX, velY);
        
        if (velX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Animador.SetBool("Correndo", true);

        }
        else if (velX < 0)  
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Animador.SetBool("Correndo", true);
        }
        else
        {
            Animador.SetBool("Correndo", false);
        }
    }
    public void pular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pode_pular == true)
        {
            pode_pular = false;
            qtd_pulo++;
            if (qtd_pulo <= 2)
            {
                AcaoPulo();
            }
        }
        if (pode_pular == false)
        {
            TemporizadorPulo();
        }
    }
    void AcaoPulo()
    {
        Corpo.velocity = new Vector2(velocidade, 0);// zera velocidade de queda para o pulo
        Corpo.AddForce(transform.up * 300f); // adiciona força para pular

        Animador.SetTrigger("Pulo");

    }
    void OnTriggerEnter2D(Collider2D gatilho)//gatilhos
    {
        if (gatilho.gameObject.tag == "Pisavel")
        {
            qtd_pulo = 0;
            pode_pular = true;
            meuTempoPulo = 0;
        }
        if (gatilho.gameObject.tag == "moeda")
        {
            Destroy(gatilho.gameObject);
            moedas++;
            Moeda_texto.text = moedas.ToString();
        }
        if (gatilho.gameObject.tag == "Nova_municao")
        {
            Municao = Municao + 5;
            Municao_texto.text = Municao.ToString();
            Destroy(gatilho.gameObject);
        }
        if (gatilho.gameObject.tag == "Checkpoint")
        {
            posInicial = gatilho.transform.position;
            Destroy(gatilho.gameObject);
        }
        if (gatilho.gameObject.tag == "morte_imediata")
        {
            if (pode_dano == true)
            {
                pode_dano = false;
                vida = vida - 20;
                PerderHP();
                morrer();
            }
        }
       
        
        if (gatilho.gameObject.tag == "passarFase3")
        {
            GJ.fase3();
        }
        if (gatilho.gameObject.tag == "vitoria")
        {
            GJ.vitoria();
        }
    }
    void TemporizadorPulo()
    {
        meuTempoPulo += Time.deltaTime;
        if (meuTempoPulo > 0.5f)
        {
            pode_pular = true;
            meuTempoPulo = 0;
        }
    }
    //dano
    void dano()
    {
        if (pode_dano == false)
        {
            TemporizadorDano();
        }
    }
    void OnCollisionStay2D(Collision2D colisao)
    {
        if (colisao.gameObject.tag == "inimigo")
        {
            if (pode_dano == true)
            {
                vida--;
                Animador.SetBool("Dano", true);
                PerderHP();
                pode_dano = false;
                ImagemPersonagem.color = UnityEngine.Color.red;
                //morre se a vida for igual ou menor que 0
                if (vida <= 0)
                {
                   
                    morrer();
                }
            }
        }
        if (colisao.gameObject.tag == "Boss")
        {
            if (pode_dano == true)
            {
                vida--;
                Animador.SetBool("Dano", true);
                PerderHP();
                pode_dano = false;
                ImagemPersonagem.color = UnityEngine.Color.red;
                //morre se a vida for igual ou menor que 0
                if (vida <= 0)
                {
                    
                    morrer();
                }
            }
        }
        if (colisao.gameObject.tag == "BossTiro")
        {
            if (pode_dano == true)
            {
                vida--;
                Animador.SetBool("Dano", true);
                PerderHP();
                pode_dano = false;
                ImagemPersonagem.color = UnityEngine.Color.red;
                //morre se a vida for igual ou menor que 0
                if (vida <= 0)
                {
                    
                    morrer();
                }
            }
        }
    }
    void TemporizadorDano()
    {
        meuTempoDano += Time.deltaTime;
        if (meuTempoDano > 0.5f)
        {
            pode_dano = true;
            meuTempoDano = 0;
            ImagemPersonagem.color = UnityEngine.Color.white;
            Animador.SetBool("Dano", false);
        }
    }

  public  void DanoBoss()
    {
        vida--;
        PerderHP();
    }

    public void PerderHP()
    {

        int vida_parabarra = vida * 10;
        BarraHp.rectTransform.sizeDelta = new Vector2(vida_parabarra, 40);

    }
    //atirar bala
    void Atirar()
    {
        if (pode_atirar == true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (Municao > 0)
                {
                    Municao--;
                    Municao_texto.text = Municao.ToString();

                    //Braco_Personagem.SetActive(true);
                    pode_atirar = false;
                    //Disparo();
                    Animador.SetTrigger("atirar");
                }
            }
        }

        else
        {
            TemporizadorTiro();
        }
    }
    public void Disparo()// posiçao da bala que sai
    {

        if (transform.localScale.x > 0)
        {
            GameObject BalaDisparada = Instantiate(Bala, pontoDisparo.transform.position, Quaternion.identity);
            BalaDisparada.GetComponent<ControleTiro>().DirecaoBala(0.03f);
            //destruir bala 
            Destroy(BalaDisparada, 0.5f);

        }
        if (transform.localScale.x < 0)
        {
           
            GameObject BalaDisparada = Instantiate(Bala, pontoDisparo.transform.position, Quaternion.identity);
            BalaDisparada.GetComponent<ControleTiro>().DirecaoBala(-0.03f);
            //destruir bala 
            Destroy(BalaDisparada, 0.7f);

        }
    }
    void TemporizadorTiro()
    {
        meuTempoTiro += Time.deltaTime;
        if (meuTempoTiro > 0.5f)
        {
            pode_atirar = true;
            meuTempoTiro = 0;

        }
    }
    //morte
    void morrer()
    {
       // Animador.SetBool("Morte",true);
        Chances--;

        Chances_texto.text = "VIDAS: " + Chances.ToString();

        if (Chances <= 0)
        {
            //so reiniciara se as chances acaba
            GJ.PersonagemMorreu();

        }
        else
        {
            Inicializar();
        }
        void Inicializar()
        {
            //muda a posição
            transform.position = posInicial;
            //recuperar vida
            vida = 10;
            int vida_parabarra = vida * 10;
            BarraHp.rectTransform.sizeDelta = new Vector2(vida_parabarra, 40);


          
        }


    }

    public void ataquePerto()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Animador.SetBool("AtakPerto", true);
        }else
        {
            Animador.SetBool("AtakPerto", false);
        }

    }
}








