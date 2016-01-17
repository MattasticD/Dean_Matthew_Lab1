using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    string subTypeName = "UnknownPickup";

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 8.0f);
        GetComponent<Collider>().name = subTypeName;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position +=
            transform.up * Time.deltaTime * MoveSpeed;
	}
}
