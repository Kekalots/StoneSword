using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;
    public float smoothSpeed = 5f;
    public Vector2 offset = new Vector2(0, 1f);

    public float shakeForce = 0.1f;
    public float shakeTime = 0.1f;
    public Vector3 shakeOffset;

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(
            playerTarget.position.x + offset.x,
            playerTarget.position.y + offset.y,
            transform.position.z 
        );
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime)+shakeOffset;
    }
    public void Shake()
    {
        StartCoroutine(Shaking());
    }
    public IEnumerator Shaking()
    {
        float t = 0;
        while(t<shakeTime)
        {
            shakeOffset.x = Random.Range(-shakeForce, shakeForce);
            shakeOffset.y = Random.Range(-shakeForce, shakeForce);
            t+= Time.deltaTime;
            yield return null;
        }
        shakeOffset = Vector3.zero;
    }
}
