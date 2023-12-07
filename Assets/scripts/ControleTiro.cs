using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleTiro : MonoBehaviour
{
    

    private float velocidade_bala = 0;
    // controla o jogo
    private GerenciadorJogo GJ;
    // Start is called before the first frame update
    void Start()
    {
        //recebe informaçao game objet
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorJogo>();
    }

    // Update is called once per frame
    void Update()

    {
        if (GJ.EstadoDoJogo() == true)
        {
            MoverBala();
        }
    }
   
    void MoverBala()
    {
        transform.position = new Vector3(transform.position.x + velocidade_bala, transform.position.y, transform.position.z);
    }
    // direcao da bala
    public void DirecaoBala(float direcao)
    {
        velocidade_bala = direcao;
    }
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.tag== "inimigo")
        {
            
            //outro objeto
            //Destroy(colisao.gameObject);
            colisao.gameObject.GetComponent<Esqueleto>().DanoEsqueletoPlayer();
            //esse objeto
            Destroy(this.gameObject);
        }
        if (colisao.gameObject.tag == "Boss")

        {
            
            // Destroy(colisao.gameObject);
            colisao.gameObject.GetComponent<BossUI>().DanoPlayer();
            Destroy(this.gameObject);
        }
        if (colisao.gameObject.tag == "BonecoBoss")

        {
            //Destroy(colisao.gameObject);
            colisao.gameObject.GetComponent<enemigos>().DanoPlayer();
            Destroy(this.gameObject);
        }
    }
   

}
