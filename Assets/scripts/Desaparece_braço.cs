using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desaparece_braço : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDestroy()
    {
        gameObject.SetActive(false);
    }
}
