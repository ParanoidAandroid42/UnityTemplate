using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Transform lookat;

    private Vector3 desiredPosiiton;
    private float offset = 1.5f;
    private float distance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        desiredPosiiton = lookat.position + (-transform.forward * distance) + (transform.up * offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosiiton, 0.03f);
        transform.LookAt(lookat.transform, Vector3.up * offset);
    }
}
