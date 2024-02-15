using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicingBlade : MonoBehaviour
{
    private bool slicing;
    private Collider BladeCollider;
    private Camera MainCamera;
    public Vector3 Direction { get; private set; }
    public float MinSliceVelocity = 0.01f;
    public float SlicedForce = 5f;
    private TrailRenderer BladeTrail;

    private void Awake()
    {
        MainCamera = Camera.main;
        BladeCollider = GetComponent<Collider>();
        BladeTrail = GetComponentInChildren<TrailRenderer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
            BladeCollider.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
            BladeCollider.enabled = false;
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void OnEnable()
    {
        StopSlicing();
    }


    private void OnDisable()
    {
        StopSlicing();
    }

    private void StartSlicing()
    {

        Vector3 NewPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        NewPosition.z = 0f;
        transform.position = NewPosition;
        slicing = true;
        BladeCollider.enabled = true;
        BladeTrail.enabled = true;
        BladeTrail.Clear();
    }
    private void StopSlicing()
    {
        slicing = false;
        BladeCollider.enabled = false;
        BladeTrail.enabled = false;

    }

    private void ContinueSlicing()
    {
        Vector3 NewPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        NewPosition.z = 0f;

        Direction = NewPosition - transform.position;

        float velocity = Direction.magnitude / Time.deltaTime;

        BladeCollider.enabled = velocity > MinSliceVelocity;
        transform.position = NewPosition;
    }
}
