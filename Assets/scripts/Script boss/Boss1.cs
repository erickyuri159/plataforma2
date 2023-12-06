using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public GameObject BossGO;
    public GameObject Chefao;

    private void Start()
    {
        //BossGO.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            BossGO.SetActive(true);
            Chefao.SetActive(true);
            //enemigos.Instance.BossActivator();


            Destroy(this.gameObject);
        }

    }
}