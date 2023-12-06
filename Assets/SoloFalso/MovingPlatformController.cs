using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

    public float velocidade = 2f; // Velocidade de movimento da plataforma
    public float limiteSuperior = 4f; // Limite superior de movimento
    public float limiteInferior = 0f; // Limite inferior de movimento

    private bool movendoParaCima = true; // Indica se a plataforma est� se movendo para cima

    void Update()
    {
        // Movimento da plataforma
        MovePlataforma();

        // Verifica se a plataforma atingiu os limites
        VerificaLimites();
    }

    void MovePlataforma()
    {
        // Calcula a dire��o do movimento
        float direcao = movendoParaCima ? 1 : -1;
        // Calcula o deslocamento
        float deslocamento = direcao * velocidade * Time.deltaTime;

        // Aplica o deslocamento � posi��o da plataforma
        transform.Translate(0, deslocamento, 0);
    }

    void VerificaLimites()
    {
        // Se a plataforma atingir o limite superior, muda a dire��o para baixo
        if (transform.position.y >= limiteSuperior)
        {
            movendoParaCima = false;
        }
        // Se a plataforma atingir o limite inferior, muda a dire��o para cima
        else if (transform.position.y <= limiteInferior)
        {
            movendoParaCima = true;
        }
    }
}
