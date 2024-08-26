using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBulletMove : MonoBehaviour
{
    public float lifetime = 5f; // Tiempo en segundos antes de que la bala se destruya
    public float speed = 10f;   // Velocidad de la bala

    public int damage = 1;

    void Start()
    {
        // Configura la velocidad y dirección inicial de la bala
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;  // Aplica velocidad hacia adelante en la dirección del prefab
        }
        else
        {
            Debug.LogError("Rigidbody no encontrado en la bala"); // Si no hay Rigidbody, se enviará un error
        }

        Destroy(gameObject, lifetime); // Asegura que la bala se destruye tras el lifetime

        SetBulletMaterial();
    }

    void SetBulletMaterial()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            // Configura el color principal del material de la bala a azul
            rend.material.shader = Shader.Find("Standard");
            rend.material.SetColor("_Color", Color.blue);

            // Si deseas agregar un color especular, puedes hacerlo aquí
            rend.material.SetColor("_SpecColor", Color.blue);
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
        if (collision.gameObject.CompareTag("Boss")) // Asegúrate de que el jefe tenga el tag "Boss"
        {
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destruye la bala después de golpear al jefe
        }
    }
}