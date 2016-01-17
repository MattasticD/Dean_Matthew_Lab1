using UnityEngine;
using System.Collections;

public class PickupDamage : MonoBehaviour
{


    [SerializeField]
    float MoveSpeed = 9.0f;

    [SerializeField]
    string subTypeName = "UnknownPickup";

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 8.0f);
        GetComponent<Collider>().name = subTypeName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=
           transform.right * Time.deltaTime * MoveSpeed * Mathf.Sin(5);

        transform.Rotate(transform.forward);
    }
}
