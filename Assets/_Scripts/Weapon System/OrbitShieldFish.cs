using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitShieldFish : MonoBehaviour
{
    public float cooldown = 0.5f;
    [SerializeField] SpriteRenderer spriteRenderer;

    Color originalColor;
    

    private void Start()
    {
        originalColor = spriteRenderer.material.color;
    }


    public void TakeHit(float damage)
    {
        StartCoroutine(OnTakingHit());
    }

    // Disable collider and turn renderer to black
    private IEnumerator OnTakingHit()
    {
        GetComponent<Collider>().enabled = false;
        spriteRenderer.material.DOColor(Color.black, 0.5f); //turn black
        yield return new WaitForSeconds(cooldown); // cooldown
        spriteRenderer.material.DOColor(originalColor, 0.5f); //turn back to original color
        GetComponent<Collider>().enabled = true;
    }
}
