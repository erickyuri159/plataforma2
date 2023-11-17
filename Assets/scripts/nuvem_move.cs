using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nuvem_move : MonoBehaviour
{
    public float velocidade = 0;
    public GameObject Jogador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nuvemVelocidade();
    }
    void nuvemVelocidade()
    {
        transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);
        if (transform.position.x < Jogador.transform.position.x - 9) 
        {
            float posY = Random.RandomRange(2.9f, 6.8f);
            transform.position = new Vector3(Jogador.transform.position.x + 9, posY, transform.position.z);
        }
    }
}
