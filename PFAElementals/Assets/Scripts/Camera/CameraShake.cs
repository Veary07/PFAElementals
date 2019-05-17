using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float lerpPerc;
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

    float elapsed = 0f;

        while (elapsed <= duration)
        {
            float x = Random.Range(-4f, 4f) * magnitude;

            transform.position = Vector3.Lerp(transform.position, new Vector3(x, originalPosition.y, originalPosition.z), Time.deltaTime * lerpPerc);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
