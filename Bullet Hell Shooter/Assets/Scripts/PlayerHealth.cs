using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // Salud inicial del jugador
    public TextMeshProUGUI healthText; // Referencia al TextMeshPro para la salud

    void Start()
    {
        UpdateHealthText(); // Actualizar el texto de salud inicialmente
    }

    void UpdateHealthText()
    {
        healthText.text = "Player Health: " + health; // Actualiza el texto de salud
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Disminuye la salud
        UpdateHealthText(); // Actualiza el texto de la salud

        if (health <= 0)
        {
            Die(); // Llama a la función Die si la salud es 0 o menos
        }
    }

    void Die()
    {
        Destroy(gameObject); // Destruye el objeto
        Debug.Log("Player defeated!");
    }
}
