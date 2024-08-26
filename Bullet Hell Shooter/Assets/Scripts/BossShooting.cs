using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform bulletSpawnPoint; // El punto desde donde se disparan las balas
    public float bulletSpeed = 10f;
    public float moveSpeed = 5f; // Velocidad de movimiento del jefe
    public float moveRange = 10f; // Rango de movimiento en el eje X
    public float timeBetweenShots = 0.1f; // Tiempo entre disparos
    public float shootingDuration = 10f; // Duración de cada patron

    private Vector3 startPosition; // Posicion inicial del jefe
    private int direction = 1; // 1 para derecha, -1 para izquierda
    private float patternTimeElapsed = 0f; // Tiempo transcurrido en el patrón actual
    private int currentPattern = 1; // Patrones actuales (1, 2, 3, etc.)
    private bool canMove = true; // Variable para controlar si el jefe puede moverse



    void Start()
    {
        startPosition = transform.position; // Guardar la posicion inicial
        StartCoroutine(RunPatterns());
    }

    void Update()
    {
        patternTimeElapsed += Time.deltaTime;

        // Control del movimiento
        if (canMove)
        {
            // Mover el jefe en el eje X
            transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

            // Cambiar de dirección si llega al limite del rango de movimiento
            if (Mathf.Abs(transform.position.x - startPosition.x) > moveRange)
            {
                direction *= -1; // Invertir la dirección
            }
        }

        // Cambio de patrones
        if (patternTimeElapsed >= shootingDuration)
        {
            patternTimeElapsed = 0f; // Reinicia el temporizador
            currentPattern++;

            // Reinicia el ciclo de patrones
            if (currentPattern > 3)
            {
                currentPattern = 1;
            }

            StopAllCoroutines();
            StartCoroutine(RunPatterns());
        }
    }

    IEnumerator RunPatterns()
    {
        // Controla si el jefe puede moverse según el patrón actual
        canMove = (currentPattern == 1);

        switch (currentPattern)
        {
            case 1:
                yield return StartCoroutine(Pattern1());
                break;
            case 2:
                yield return StartCoroutine(Pattern2());
                break;
            case 3:
                yield return StartCoroutine(Pattern3());
                break;
        }
    }

    IEnumerator Pattern1()
    {
        float startTime = Time.time;

        while (Time.time < startTime + shootingDuration)
        {
            float centralAngle = 0f;
            float angleOffset = 10f;

            Vector3 direction1 = Quaternion.Euler(0, -angleOffset, 0) * Vector3.back;
            Vector3 direction2 = Vector3.back;
            Vector3 direction3 = Quaternion.Euler(0, angleOffset, 0) * Vector3.back;

            ShootBullet(direction1);
            ShootBullet(direction2);
            ShootBullet(direction3);

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    IEnumerator Pattern2()
    {
        float startTime = Time.time;
        float angle = 0f;

        while (Time.time < startTime + shootingDuration)
        {
            angle += 45f;

            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));

            ShootBullet(direction);

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator Pattern3()
    {
        float startTime = Time.time;
        float angle = 0f;

        while (Time.time < startTime + shootingDuration)
        {
            angle += 30f;

            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));

            ShootBullet(direction);

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }


    void ShootBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(direction * bulletSpeed, ForceMode.VelocityChange);

            BulletManager.IncrementBulletCount();  // Incrementar el contador de balas
        }
        else
        {
            Debug.LogError("No se encontró un Rigidbody en la bala.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            // Anula la fuerza de la colisión
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }


}
