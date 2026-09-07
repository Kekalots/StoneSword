using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : EnemyBaseMove
{
    public SpriteRenderer sr;
    public Color color;
    public float fadeDuration = 0.5f;
    public float invisibleAlpha = 0.01f;
    public float visibleAlpha = 1f;
    public GameObject ghostHPBar;
    private Coroutine currentFadeCoroutine;
    protected override void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        color=sr.color;
        color.a = invisibleAlpha;
        sr.color=color;
        ghostHPBar.SetActive(false);

        base.Start();
        doPatrol = false;
        visionRadius = 5f;
        moveSpeed = 3f;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (shouldStopPatrol)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        bool canSee = CanSeePlayer();
        if (canSee && playerTrans != null)
        {
            StartFade(visibleAlpha, true);

            float dirX = Mathf.Sign(playerTrans.position.x - transform.position.x);
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            if (dirX != 0)
            {
                transform.localScale = new Vector3(dirX * Mathf.Abs(originLocalScale.x), originLocalScale.y, originLocalScale.z);
            }
        }
        else
        {
            StartFade(invisibleAlpha, false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void StartFade(float targetAlpha, bool showHud)
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }
        currentFadeCoroutine = StartCoroutine(FadeCoroutine(targetAlpha, showHud));
    }

    IEnumerator FadeCoroutine(float targetAlpha, bool showHud)
    {
        float timer = 0f;
        Color startColor = sr.color;
        ghostHPBar.SetActive(showHud);
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            float newA = Mathf.Lerp(startColor.a, targetAlpha, t);

            color.a = newA;
            sr.color = color;

            yield return null;
        }
        color.a = targetAlpha;
        sr.color = color;
        currentFadeCoroutine = null;
    }
}