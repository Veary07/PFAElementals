using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Decompte : MonoBehaviour
{

    private Text counterText;
    [SerializeField] private float startingTime;

    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<Text>() as Text;
    }

    // Update is called once per frame
    void Update()
    {
        startingTime -= Time.deltaTime;
        counterText.text = "" + Mathf.Round (startingTime);
    }

}
