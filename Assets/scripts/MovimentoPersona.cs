using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;


public class MovimentoPersona : MonoBehaviour
{ // pega o componente RigidiBody2D
    public Rigidbody2D Corpo;
    // recebe a velocidade do personagem
    public float velocidade;
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
    void Start()
    {
        BarraHp = GameObject.FindGameObjectWithTag("hp_barra").GetComponent<Image>();
        Moeda_texto = GameObject.FindGameObjectWithTag("moeda_text_tag").GetComponent<Text>();
        Animador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        pular();
        Apontar();
        dano();
        Atirar();
        

       
    }
    void Mover()//responsavel por mover o personagem
    {
        velocidade = Input.GetAxis("Horizontal") * 3;
        Corpo.velocity = new Vector2(velocidade, Corpo.velocity.y);
    }
    void Apontar()
    {
        if (velocidade > 0)
        {
            ImagemPersonagem.flipX = false;
            Animador.SetBool("Correndo", true);
        }
        else if (velocidade < 0)
        {
            ImagemPersonagem.flipX = true;
            Animador.SetBool("Correndo", true);
        }
        else
        {
            Animador.SetBool("Correndo", false);
        }

    }
    void pular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pode_pular == true)
        {
            pode_pular = false;
            qtd_pulo--;
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
                PerderHP();
                pode_dano = false;
                ImagemPersonagem.color = UnityEngine.Color.red;
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
        }
    }
    void PerderHP()
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
                pode_atirar = false;
                Disparo();
            }
        }

        else
        {
            TemporizadorTiro();
        }

        void Disparo()// posiçao da bala que sai
        {
            if (ImagemPersonagem.flipX == false)
            {
                Vector3 pontoDisparo = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.4f, transform.position.z);
                GameObject BalaDisparada = Instantiate(Bala, pontoDisparo, Quaternion.identity);
                BalaDisparada.GetComponent<ControleTiro>().DirecaoBala(0.03f);
                //destruir bala 
                Destroy(BalaDisparada, 0.5f);

            }
            if (ImagemPersonagem.flipX == true)
            {
                Vector3 pontoDisparo = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.4f, transform.position.z);
                GameObject BalaDisparada = Instantiate(Bala, pontoDisparo, Quaternion.identity);
                BalaDisparada.GetComponent<ControleTiro>().DirecaoBala(-0.03f);
                //destruir bala 
                Destroy(BalaDisparada, 0.5f);

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
    }
}







