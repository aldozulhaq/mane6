using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Point : MonoBehaviour
{
    [SerializeField] float pickRadius;
    [SerializeField] float flySpeed;

    [Header("Event Channels")]
    [SerializeField] EventChannel onGetPoint;

    private Action<Point> destroyAction;

    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        /*if (Vector3.Distance(player.transform.position, transform.position) <= pickRadius)
        {
            MoveToPlayer();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        transform.DOMove(player.transform.position, flySpeed).OnComplete(() => {
            Debug.Log("Move End");
            onGetPoint.Invoke();
            DestroyPoint(); 
        });
    }

    private void DestroyPoint()
    {
        // Destroy
        Debug.Log("Destroy");
        destroyAction(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, pickRadius);
    }

    public void Init(Action<Point> _destroyAction)
    {
        destroyAction = _destroyAction;
    }
}