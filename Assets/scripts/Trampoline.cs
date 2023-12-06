using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 10f;  // Força inicial do pulo

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // Aplica uma força adicional para cima
                playerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                jumpForce += 5f; // Aumenta a força do trampolim a cada pulo
            }
        }
    }
}

