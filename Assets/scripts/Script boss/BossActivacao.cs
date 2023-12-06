using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class BossActivacao : MonoBehaviour

{
    public GameObject BossGO;

    private void Start()
    {
        BossGO.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossGO.SetActive(true);
            BossUI.Instance.BossActivator();
            
            
            Destroy( gameObject );
        }

    }
   
    
        
       
    
}
