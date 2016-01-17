using UnityEngine;
using System.Collections;

public class EnemyControllerStrong : MonoBehaviour
{

    [SerializeField]
    private float MoveSpeedX;

    private ShipPlayerController PlayerShipCtrl;

    [SerializeField]
    private GameObject[] PickupTypes;

    [SerializeField]
    private float MaxHits;

    private float CurHits;

    private float Frequency = 1.0f;
    private float Magnitude = 0.03f;
    private Vector3 axis;

    // Projectile Game Object Variable
    [Tooltip("The Game Object used as the projectile")]
    public EnemyProjectile Bullet;

    // Use this for initialization
    void Start()
    {
        axis = transform.right;
        // Set the life-span until our object is destroyed
        Destroy(gameObject, 10.0f);
        // Find the player ship game object by name
        GameObject playerShip = GameObject.Find("PlayerShip");
        // Access the player ship's script component by type

        PlayerShipCtrl = playerShip.GetComponent<ShipPlayerController>();

        CurHits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position +=
        //    transform.up * Time.deltaTime * MoveSpeedX;

        transform.position += transform.up * Time.deltaTime * MoveSpeedX;
        transform.position = transform.position + axis * Mathf.Sin(Time.time * Frequency) * Magnitude;

        DoWeaponFire();
    }

    int iCounter = 0;
    void DoWeaponFire()
    {
        //yield return new WaitForSeconds(3);
        if (iCounter == 100)
        {
            Instantiate(Bullet, transform.position, transform.rotation);
            iCounter = 0;
        }
        iCounter++;
    }

    void OnTriggerEnter(Collider other)
    {
        // Hit the player's projectile?
        if (other.tag == "PlayerProjectile")
        {
            //A projectile instant kills us
            if (CurHits == MaxHits - 1)
            {
                Destroy(gameObject);
                SpawnPickup();
                CurHits = 0;
            }
            CurHits++;
        }
        else if (other.tag == "PlayerProjectileInstaKill")
        {
            DestroyObject(gameObject);
            SpawnPickup();
        }
        else if (other.tag == "PlayerShip")
        {
            // Wrecking with the player kills us
            Destroy(gameObject);
            // Remove points from the player for hitting this enemy
            PlayerShipCtrl.ModScore(-1);
        }
    }
    void SpawnPickup()
    {
        Instantiate(PickupTypes[0], transform.position, transform.rotation);
        Instantiate(PickupTypes[2], transform.position, transform.rotation);
        Instantiate(PickupTypes[3], transform.position, transform.rotation);
        Instantiate(PickupTypes[4], transform.position, transform.rotation);

        Instantiate(PickupTypes[5], transform.position, transform.rotation);
    }
}
