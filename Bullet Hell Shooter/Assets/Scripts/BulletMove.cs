using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float lifetime = 5f; // Tiempo en segundos antes de que la bala se destruya

    public float speed = 10f;

    public int damage = 10; // Daño que la bala hace al jugador

    void Start()
    {
        // Destruye la bala después de `lifetime` segundos
        Destroy(gameObject, lifetime);

        // Configura el material y color de la bala
        SetBulletMaterial();
    }

    void SetBulletMaterial()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            // Configura el color principal del material de la bala a rojo
            rend.material.shader = Shader.Find("Standard");
            rend.material.SetColor("_Color", Color.red);

            // Si deseas agregar un color especular, puedes hacerlo aquí
            rend.material.SetColor("_SpecColor", Color.red);
        }
        else
        {
            Debug.LogError("Renderer no encontrado en la bala");
        }
    }

    void OnDestroy()
    {
        BulletManager.DecrementBulletCount();  // Decrementar el contador de balas
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destruye la bala después de golpear al jugador
        }
    }
}
