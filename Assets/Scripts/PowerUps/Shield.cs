using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float duration = 5f;
    public Sprite effectSpritePlayer1;
    public Sprite effectSpritePlayer2;
    public float warningTime = 0.5f;
    public float lifetimeDuration = 5f;

    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetimeDuration);

        if (gameObject != null)
        {
            Debug.Log(gameObject.name + " disappeared!");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            StopAllCoroutines();
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Debug.Log("Player picked up Shield! Protected!");

        PlayerController movements = player.GetComponent<PlayerController>();
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        Sprite originalSprite = playerSprite.sprite;

        if (player.CompareTag("Player1"))
        {
            playerSprite.sprite = effectSpritePlayer1;
        }
        else if (player.CompareTag("Player2"))
        {
            playerSprite.sprite = effectSpritePlayer2;
        }

        ShieldProtection shield = player.gameObject.AddComponent<ShieldProtection>();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration - warningTime);

        SpriteFlash.SimpleFlash flash = player.GetComponent<SpriteFlash.SimpleFlash>();
        if (flash != null)
        {
            flash.Flash();
        }

        yield return new WaitForSeconds(warningTime);

        if (player != null && shield != null)
        {
            Destroy(shield);
        }

        if (player != null && playerSprite != null)
        {
            playerSprite.sprite = originalSprite;
        }

        Debug.Log("Shield ended!");

        Destroy(gameObject);
    }
}

public class ShieldProtection : MonoBehaviour
{
    // Laisser vide, sert juste de marqueur
}