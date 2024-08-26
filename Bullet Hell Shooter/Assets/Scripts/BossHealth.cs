using UnityEngine;
using TMPro;

public class BossHealth : MonoBehaviour
{
    public int health = 50; // Salud inicial del jefe
    public TextMeshProUGUI healthText;

    void Start()
    {
        UpdateHealthText(); // Actualizar texto inicialmente
    }

    void UpdateHealthText()
    {
        healthText.text = "Boss Health : " + health; // Actualiza el texto
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Disminuye la salud
        UpdateHealthText(); // Actualiza el texto

        if (health <= 0)
        {
            Die(); // Llama al metodo Die
            Debug.Log("Boss defeated!");
        }
    }

    void Die()
    {
        Destroy(gameObject); // Destruye el objeto
    }
}
