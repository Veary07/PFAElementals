using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    private Transform camera;

    // Start is called before the first frame update
    private void Awake()
    {
        bar = transform.Find("Bar");
        camera = transform.Find("Camera");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    private void Update()
    {
        transform.LookAt(camera);
    }

}
