using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float health;

    Player player;
    private bool canHit;
    private float hitCooldown = 0.5f;

    private void Start()
    {
        canHit = true;
        player = FindObjectOfType<Player>();
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            if (canHit)
            {
                StartCoroutine(OnPlayerHit());
            }
        }
    }

    private IEnumerator OnPlayerHit()
    {
        Debug.Log("Hit Player");
        canHit = false;

        yield return new WaitForSeconds(0.5f);

        canHit = true;
    }
}
