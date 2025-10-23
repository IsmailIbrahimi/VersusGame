using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    public float multiplier = 5f;
    public float duration = 5f;

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            StartCoroutine( Pickup(other) );
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Debug.Log("Player picked up Range!");

        AttacksController stats = player.GetComponent<AttacksController>();
        PlayerController movements = player.GetComponent<PlayerController>();

        stats.attackRange *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        stats.attackRange /= multiplier;

        Destroy(gameObject);
    
    }
}
