using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public TrailRenderer[] trails;
    private CharacterController controller;
    public float baseSpeed = 10.0f;
    public float rotSpeedX = 3.0f;
    public float rotSpeedY = 1.5f;
    private Vector3 _endPosition;
    private Vector3 _offset;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        SetTrail();
    }

    private void SetTrail()
    {
        for (int i = 0; i < trails.Length; i++)
        {
           // trails[i].transform.localPosition = new Vector3(trails[i].transform.localPosition.x,0,0);
            trails[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            trails[i].transform.localScale = Vector3.one * 0.01f;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        float _mzCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        mousePoint.z = _mzCord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mousePosition = GetMouseWorldPos().x;
            Vector3 mouseDelta = Input.mousePosition - _endPosition;
            _endPosition = Input.mousePosition;

            Vector3 moveVector = transform.forward * baseSpeed;
            Vector3 yaw = mouseDelta.x * transform.right * rotSpeedX * Time.deltaTime;
            Vector3 pitch = mouseDelta.y * transform.up * rotSpeedY * Time.deltaTime;
            Vector3 dir = pitch + yaw;

            float maxX = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;
            if (maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290)
            {

            }
            else
            {
                moveVector += dir;
                transform.rotation = Quaternion.LookRotation(new Vector3(moveVector.x,moveVector.y,moveVector.z));
            }
            controller.Move(moveVector * Time.deltaTime);
        }
        else
        {
            _endPosition = Input.mousePosition;
            Vector3 moveVector = transform.forward * baseSpeed;
            controller.Move(moveVector * Time.deltaTime);
        }
    }
}
