//------------------------------------------------------------------------------------------------
// Author: Matthew Dean
// Date: 1/10/2016
// Credit: Unity Scripting API
// Credit: Full Sail University
// Purpose: Script used to control projectile movement and destruction
//------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class EnemyProjectileStrong : MonoBehaviour
{
    #region Variables
    //Used to control how fast the game object moves

    [SerializeField]
    [Tooltip("The speed of the projectile")]
    [Range(0, 20)]
    public float MoveSpeed;
    //[SerializeField]
    //[Tooltip("Time until projectile is destroyed")]
    //private float DestroyTime = 2.0f;
    public GameObject Ship;
    private float Frequency = 20.0f;
    private float Magnitude = 0.05f;
    private Vector3 axis;


    private ShipPlayerController ParentShip;

    #endregion

    // Use this for initialization
    void Start()
    {
        axis = transform.right;

        //Set the life-span until our object is destroyed.
        Destroy(gameObject, 3.0f);
        // Find the player ship game object name
        GameObject playerShip = GameObject.Find("PlayerShip");
        // Access the player ship's script component by type
        ParentShip = playerShip.GetComponent<ShipPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Projectile Movement Wave Pattern
        transform.position += transform.up * Time.deltaTime * MoveSpeed;
        transform.position = transform.position + axis * Mathf.Sin(Time.time * Frequency) * Magnitude;
    }

    // Called by the engine when this object collides
    // with another object containing collision
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            Destroy(gameObject);

            //ParentShip.ModScore(-1);

            //ParentShip.ModEnemyHits(-1);

        }
    }

    // Funtion to identify then destroy projectiles when they move off screen
    void OnBecameInvisible()
    {
        DestroyObject(gameObject);
    }

    // Funtion to identify projectiles when they move on screen
    void OnBecameVisible()
    {

    }
}
