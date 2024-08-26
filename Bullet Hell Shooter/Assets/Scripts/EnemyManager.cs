using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRatePattern1 = 0.5f;  // Tasa de disparo para el patrón 1
    public float fireRatePattern2 = 1.0f;  // Tasa de disparo para el patrón 2

    private float switchTime = 30f;  // Duración de cada patrón

    void Start()
    {
        StartCoroutine(ShootingPattern());
    }

    IEnumerator ShootingPattern()
    {
        bool isPattern1 = true;

        while (true)
        {
            float startTime = Time.time;
            float endTime = startTime + switchTime;
            float fireRate = isPattern1 ? fireRatePattern1 : fireRatePattern2;

            while (Time.time < endTime)
            {
                if (isPattern1)
                {
                    ShootStarPattern(fireRate);
                }
                else
                {
                    Shoot(fireRate, Quaternion.Euler(0, 45, 0) * -Vector3.forward);
                    Shoot(fireRate, Quaternion.Euler(0, -45, 0) * -Vector3.forward);
                }
                yield return new WaitForSeconds(fireRate);
            }

            yield return new WaitForSeconds(10f);  // Pausa entre patrones
            isPattern1 = !isPattern1;  // Cambiar de patrón
        }
    }

    void Shoot(float fireRate, Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * 10f;  // Aplicar velocidad en la dirección especificada
            BulletManager.IncrementBulletCount();  // Incrementar el contador de balas
        }
        Debug.Log("Disparando bala en dirección: " + direction);
    }

    void ShootStarPattern(float fireRate)
    {
        int numberOfBullets = 5; // Número de balas en el patron de estrella
        float angleStep = 360f / numberOfBullets;
        for (int i = 0; i < numberOfBullets; i++)
        {
            float bulletDirection = i * angleStep;
            float bulletDirectionRadians = bulletDirection * Mathf.Deg2Rad; // Convertir grados a radianes

            Vector3 bulletVelocity = new Vector3(
                Mathf.Sin(bulletDirectionRadians),
                0f,
                Mathf.Cos(bulletDirectionRadians)
            );

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0, bulletDirection, 0));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bulletVelocity * 10f;  // Aplicar velocidad en la dirección calculada
            }
        }
    }
}
