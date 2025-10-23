using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double : MonoBehaviour
{

    public float multiplier = 2f;
    public float duration = 5f;
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
        Debug.Log("Player picked up Double!");

        player.transform.localScale *= multiplier;
        AttacksController stats = player.GetComponent<AttacksController>();
        PlayerController movements = player.GetComponent<PlayerController>();

        movements.speed /= multiplier;
        stats.attackRange *= multiplier;
        stats.knockbackForce *= multiplier;
        stats.attackCooldown *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration - warningTime);

        SpriteFlash.SimpleFlash flash = player.GetComponent<SpriteFlash.SimpleFlash>();
        if (flash != null)
        {
            flash.Flash();
        }

        yield return new WaitForSeconds(warningTime);

        player.transform.localScale /= multiplier;
        movements.speed *= multiplier;
        stats.attackRange /= multiplier;
        stats.knockbackForce /= multiplier;
        stats.attackCooldown /= multiplier;

        Destroy(gameObject);

    }
}
