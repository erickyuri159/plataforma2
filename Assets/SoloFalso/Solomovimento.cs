using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solomovimento : MonoBehaviour
{
    public float velocidade = 2f;
    public float limiteDireita = 5f; // Defina o limite direito onde o piso ir� inverter a dire��o
    public float limiteEsquerda = -5f; // Defina o limite esquerdo onde o piso ir� inverter a dire��o

    void Update()
    {
        // Movimento para a direita
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);

        // Verifica se atingiu o limite direito
        if (transform.position.x >= limiteDireita)
        {
            InverterDirecao();
        }

        // Verifica se atingiu o limite esquerdo
        if (transform.position.x <= limiteEsquerda)
        {
            InverterDirecao();
        }
    }

    void InverterDirecao()
    {
        // Inverte a dire��o multiplicando a velocidade por -1
        velocidade *= -1;
    }
}
