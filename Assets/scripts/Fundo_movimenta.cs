using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fundo_movimenta : MonoBehaviour
{
    public GameObject Personagem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float posX = Personagem.transform.position.x * 1f;
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
