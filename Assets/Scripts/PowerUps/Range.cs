using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    public float multiplier = 5f;
    public float duration = 10f;
    public Sprite effectSpritePlayer1;
    public Sprite effectSpritePlayer2;
    public float warningTime = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Debug.Log("Player picked up Range!");

        AttacksController stats = player.GetComponent<AttacksController>();
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

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration - warningTime);

        SpriteFlash.SimpleFlash flash = player.GetComponent<SpriteFlash.SimpleFlash>();
        if (flash != null)
        {
            flash.Flash();
        }

        yield return new WaitForSeconds(warningTime);

        stats.attackRange /= multiplier;

        playerSprite.sprite = originalSprite;

        Destroy(gameObject);

    }
}
