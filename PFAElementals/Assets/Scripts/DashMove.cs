using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField] float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        dashTime = startDashTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (direction == 0)
        {

        }
        else if (direction <= 0)
        {
            direction = 0;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }
	}
}
