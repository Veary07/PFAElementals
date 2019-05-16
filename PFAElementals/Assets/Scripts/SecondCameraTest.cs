using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraTest : MonoBehaviour
{

    public Transform m_Target1;
    public Transform m_Target2;

    [SerializeField]
    private float m_Height = 10f;

    [SerializeField]
    private float m_Distance = 20f;

    [SerializeField]
    private float m_Angle = 45f;

    [SerializeField]
    private float m_SmoothSpeed = 0.5f;

    private Vector3 refVelocity;

    public Vector3 offset;



    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    
    }


    protected virtual void HandleCamera()
    {
        
        if(!m_Target1 || !m_Target2)
        {
            return;
        }
      

        Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
        
        Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;

        //Vector3 flatTargetPosition = m_Target.position;
        Vector3 positionMid = new Vector3((m_Target1.transform.position.x + m_Target2.transform.position.x) / 2, 0, (m_Target1.transform.position.z + m_Target2.transform.position.z) / 2);
        //flatTargetPosition.y = 0f;
        Vector3 finalPosition = positionMid + rotatedVector;

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_SmoothSpeed);
        transform.LookAt(positionMid);
    }
}
