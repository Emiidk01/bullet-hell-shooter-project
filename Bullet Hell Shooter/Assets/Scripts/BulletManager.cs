using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletManager : MonoBehaviour
{
    public static int bulletCount = 0; // Contador estático de balas, accesible desde cualquier script
    public TMP_Text bulletCounterText; // Referencia al TextMesh que muestra el contador

    void Update()
    {
        // Actualizar el texto del contador de balas cada frame
        if (bulletCounterText != null)
        {
            bulletCounterText.text = "Bullets: " + bulletCount;
        }
    }

    public static void IncrementBulletCount()
    {
        bulletCount++;
    }

    public static void DecrementBulletCount()
    {
        bulletCount--;
    }
}