using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPapaiNoel : MonoBehaviour
{
    public float velocidade = 5f; // Ajuste conforme necessário
    public float tempoEsperaMin = 1f;
    public float tempoEsperaMax = 3f;

    void Start()
    {
        // Iniciar o movimento aleatório
        StartCoroutine(MoverAleatoriamente());
    }

    IEnumerator MoverAleatoriamente()
    {
        while (true)
        {
            float direcao = Random.Range(-1f, 1f);
            Flip(direcao); // Inverter o sprite se estiver indo para a esquerda

            float tempoEspera = Random.Range(tempoEsperaMin, tempoEsperaMax);
            yield return new WaitForSeconds(tempoEspera);

            // Movimento horizontal
            float movimentoHorizontal = direcao * velocidade * Time.deltaTime;
            transform.Translate(new Vector3(movimentoHorizontal, 0, 0));
        }
    }

    void Flip(float direcao)
    {
        // Inverter o sprite se estiver indo para a esquerda
        if (direcao > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direcao < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}


