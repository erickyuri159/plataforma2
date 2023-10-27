using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinhaMagia : MonoBehaviour
{
    private Rigidbody2D Corpo;
    public float lado;
    // Start is called before the first frame update
    void Start()
    {
        Corpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Corpo.velocity = new Vector2(2*lado, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inimigo")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Chao")
        {
            Destroy(this.gameObject);
        }
    }

}
