using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Scripts
    public GameObject sceneManager;
    public SceneManager sceneManagerScript;

    public bool isDead = false;

    [SerializeField] int health = 50;

    public SpecialEffects specialEffects;

    public bool isPlayer;
    public bool isEnemy;

    // Score
    ScoreKeeper scoreKeeper;
    [SerializeField] int score = 50;

    // Camera Shake
    CameraShake cameraShake;

    // Renderer&Invincible 
    [SerializeField] Renderer player_Renderer;
    private bool invincible = false;

    // Health Bar
    public Image healthBar;


    private void Start()
    {
        sceneManagerScript = FindObjectOfType<SceneManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        cameraShake = FindObjectOfType<CameraShake>();

    }

    // Wird aufgerufen, wenn ein anderer Collider2D in den Collider2d dieses GameObjects gelangt
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Hier nimmt man das Damage_dealer-Komponente vom Game Object des Colliders
        Damage_Dealer damage_Dealer = other.GetComponent<Damage_Dealer>();

        // Überprüft, ob das Damage_Dealer-Komponente gefunden wurde. Also nicht null ist.
        if (damage_Dealer != null)
        {

            if (!invincible && isPlayer)
            {
                // Nimmt Schaden basierend auf dem Schadenwert des Damage_Dealers
                TakeDamage(damage_Dealer.GetDamage());
            }
            if (isEnemy)
            {
                // Nimmt Schaden basierend auf dem Schadenwert des Damage_Dealers
                TakeDamage(damage_Dealer.GetDamage());
            }



            damage_Dealer.Hit();
        }
    }

    // Diese Methode reduziert die Gesundheit basierend auf dem übergebenen Schadenwert
    private void TakeDamage(int damage)
    {

            health -= damage;


        if (isPlayer)
        {
            cameraShake.PlayerHitShakeCam(); // Player Hit Camera Shake
            healthBar.fillAmount -= damage / 50f;
        }
        if (isEnemy)
        {
            cameraShake.EnemyHitShakeCam();
        }

        // Überprüft, ob die Gesundheit unter oder gleich 0 gefallen ist, und zerstört falls true dann das GameObject
        if (health <= 0)
        {
            isDead = true;
            specialEffects.PlayHitParticle();
            Death();
        }
        else if(isPlayer)
        {
            StartCoroutine(BlinkAndInvincibility(0.5f));
        }
    }

    public void Death()
    {
        if (!isPlayer)
        {
            scoreKeeper.Score(score);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {

        if (isPlayer)
        {
            Debug.Log("Game Over");
            sceneManagerScript.StartCoroutine(this.sceneManagerScript.LoadScene("GameOver"));
        }
    }

    IEnumerator BlinkAndInvincibility(float duration)
    {
        // Make the player invincible
        invincible = true;

        // Get the current sprite
        SpriteRenderer spriteRenderer = player_Renderer.GetComponent<SpriteRenderer>();
        Sprite currentSprite = spriteRenderer.sprite;

        // Blink the sprite for the duration
        float elapsed = 0;
        while (elapsed < duration)
        {
            // Toggle the sprite every second
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.025f;
        }

        // Make the sprite visible again
        spriteRenderer.enabled = true;

        // Make the player mortal again
        invincible = false;
    }



}
