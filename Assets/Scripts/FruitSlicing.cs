using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSlicing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Whole;
    public GameObject Sliced;

    private Rigidbody FruitRb;
    private Collider FruitCollider;

    private ParticleSystem Juice;

    public int Points = 1;

    private void Awake()
    {
        FruitRb = GetComponent<Rigidbody>();
        FruitCollider = GetComponent<Collider>();
        Juice = GetComponentInChildren<ParticleSystem>();
    }
    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreasingScore(Points);

        Whole.SetActive(false);
        Sliced.SetActive(true);

        FruitCollider.enabled = false;
        Juice.Play();

        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        Sliced.transform.rotation = Quaternion.Euler(0f,0f,angle);

        Rigidbody[ ] Slices = Sliced.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody Slice in Slices)
        {
            Slice.velocity = FruitRb.velocity;
            Slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SlicingBlade blade = other.GetComponent<SlicingBlade>();
            Slice(blade.Direction, blade.transform.position, blade.SlicedForce);
        }
       
    }

}
