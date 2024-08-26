using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidad normal de movimiento
    public float slowMoveSpeed = 2.5f; // Velocidad de movimiento lento
    public GameObject bulletPrefab; // Prefab de la bala para disparar
    public Transform bulletSpawnPoint; // Punto de donde las balas serán disparadas

    private float fireRate = 0.25f; // Tiempo entre disparos en segundos
    private float nextFireTime = 0f; // Momento en el que se permitirá el siguiente disparo

    private float currentSpeed; // Velocidad actual del jugador

    void Start()
    {
        currentSpeed = moveSpeed; // Iniciar con la velocidad normal
    }

    void Update()
    {
        MovePlayer(); // Manejar movimiento del jugador
        HandleShooting(); // Manejar el disparo

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = slowMoveSpeed; // Cambiar a velocidad lenta si LeftShift está presionado
        }
        else
        {
            currentSpeed = moveSpeed; // Regresar a velocidad normal si LeftShift no está presionado
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
    }

    void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Actualizar el momento del próximo disparo permitido
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab && bulletSpawnPoint)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            BulletManager.IncrementBulletCount();  // Incrementar el contador de balas
        }
    }
}
