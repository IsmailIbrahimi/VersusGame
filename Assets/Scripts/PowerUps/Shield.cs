// using System.Collections;
// using System.Linq;
// using UnityEngine;

// public class Shield : MonoBehaviour
// {
//     public float duration = 3f;
//     private bool used = false;

//     void OnTriggerEnter(Collider other)
//     {
//         if (used) return;

//         // Remonte au root du joueur (gère les colliders enfants)
//         Transform root = other.attachedRigidbody ? other.attachedRigidbody.transform : other.transform;
//         var pickerStats = root.GetComponentInParent<AttacksController>();
//         if (pickerStats == null) return;

//         used = true;
//         StartCoroutine(ApplyDebuffToOpponent(pickerStats));
//     }

//     IEnumerator ApplyDebuffToOpponent(AttacksController pickerStats)
//     {
//         // Masquer/désactiver le pickup
//         var rend = GetComponent<Renderer>(); if (rend) rend.enabled = false;
//         var col  = GetComponent<Collider>(); if (col)  col.enabled  = false;

//         // Trouver l'autre joueur (le premier AttacksController différent du ramasseur)
//         var all = FindObjectsOfType<AttacksController>();
//         var opponent = all.FirstOrDefault(a => a != pickerStats);
//         if (opponent == null) { Destroy(gameObject); yield break; }

//         // Sauvegarde + debuff total (force, upward, distance)
//         float f = opponent.knockbackForce;
//         float u = opponent.knockbackUpward;
//         float d = opponent.knockbackDistance;

//         opponent.knockbackForce = 0f;
//         opponent.knockbackUpward = 0f;
//         opponent.knockbackDistance = 0f;

//         yield return new WaitForSeconds(duration);

//         // Restaure puis détruis le pickup
//         opponent.knockbackForce = f;
//         opponent.knockbackUpward = u;
//         opponent.knockbackDistance = d;

//         Destroy(gameObject);
//     }
// }
