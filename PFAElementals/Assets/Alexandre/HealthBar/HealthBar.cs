using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    [SerializeField] private Transform cameraPos;

    // Start is called before the first frame update
    private void Awake()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    private void Update()
    {
        transform.LookAt(cameraPos);
    }

}
