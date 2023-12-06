using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pisoFalso : MonoBehaviour
{

    public float tempoDeCair;
    bool ativado = false;
    float contador = 0;

    Rigidbody2D rb;
    Vector3 PosInicial;
    float TempoRespaw = 5f;
    float ContadorRespaw;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PosInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (contador <= tempoDeCair && ativado == true)
        {
            contador += Time.deltaTime;
            if (contador >= tempoDeCair)
            {
                rb.gravityScale = 100;
            }
        }
        else 
        {
            contador = 0;
        }
        if (transform.position.y < PosInicial.y) 
        {
            ContadorRespaw += Time.deltaTime;
            if (ContadorRespaw >= TempoRespaw)
            {
                transform.position = PosInicial;
                rb.gravityScale = 0;
                rb.velocity = Vector3.zero;
                ativado = false;
                    
            }

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        { 
            ativado = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ativado = false;
        }
    }
    

}
