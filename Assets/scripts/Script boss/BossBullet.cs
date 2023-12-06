using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(- speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }
    
    private void OnTriggerEnter2D(Collider2D colisao)
    {
        
        if (colisao.gameObject.tag == "Player")
        {
            //MovimentoPersona.instance.PerderHP();
            
            colisao.gameObject.GetComponent<MovimentoPersona>().DanoBoss();
            //outro objeto
            
            //esse objeto
            Destroy(this.gameObject);
        }
        
    }
}
