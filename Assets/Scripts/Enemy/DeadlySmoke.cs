using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlySmoke : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float damageInterval = 1.2f;

    private bool isPlayerInTrigger = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerContorller playerController = other.GetComponent<PlayerContorller>();
        if (playerController != null)
        {
            isPlayerInTrigger = true;
            StartCoroutine(DamageOverTime(playerController));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerContorller playerController = other.GetComponent<PlayerContorller>();
        if (playerController != null)
        {
            isPlayerInTrigger = false;
            StopCoroutine(nameof(DamageOverTime));
        }
    }

    IEnumerator DamageOverTime(PlayerContorller playerController)
    {
        while (isPlayerInTrigger) 
        {
            playerController.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
