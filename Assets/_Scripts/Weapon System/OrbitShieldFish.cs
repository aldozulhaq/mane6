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
        StartCoroutine(FadeInBlack());
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(FadeToOriginalColor());
        GetComponent<Collider>().enabled = true;
    }

    // Animate color to black
    IEnumerator FadeInBlack()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            spriteRenderer.material.color = Color.Lerp(originalColor, Color.black, t);
            yield return null;
        }
    }

    IEnumerator FadeToOriginalColor()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            spriteRenderer.material.color = Color.Lerp(Color.black, originalColor, t);
            yield return null;
        }
    }
}
