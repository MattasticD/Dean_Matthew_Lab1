//------------------------------------------------------------------------------------------------
// Author: Matthew Dean
// Date: 1/10/2016
// Credit: Unity Scripting API
// Credit: Full Sail University
// Purpose: Script used for creating camera shake effect
//------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Camera CameraObject;
    public GameObject Player;

    private Vector3 movement = Vector3.zero;
    private float MoveSpeed = 5.0f;
    #endregion

    // Use this for initialization
    void Start()
    {

    }

    #region Alternate Shake
    Vector3 originPosition;
    Quaternion originRotation;


    float shake_decay;
    float shake_intensity;

    //void Update()
    //{
    //    if (shake_intensity > 0)
    //    {
    //        transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
    //        transform.rotation = new Quaternion(
    //                        originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
    //                        originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
    //                        originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
    //                        originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
    //        shake_intensity -= shake_decay;
    //        if (shake_intensity < 0.2)
    //        {
    //            //transform.position = originPosition;sdas
    //            //transform.rotation = originRotation;
    //        }
    //    }

    //    //if (Input.GetButtonDown("Fire1"))
    //    //{
    //    //    Shake();sss
    //    //}
    //    UpdatePosition();
    //}

    //public void Shake()
    //{
    //    originPosition = transform.position;
    //    originRotation = transform.rotation;
    //    shake_intensity = .001f;
    //    shake_decay = .005f;
    //}
    #endregion

    void UpdatePosition()
    {
        // Move the player laterally in the 'X' coordinate
        movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
        // Move the player laterally in the 'Y' coordinate
        movement.y = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;
        // Apply the movement vector to the game object's position
        Player.transform.Translate(movement);
        // Transform the 3D world position to a screen pixel location
        Vector3 screenPosition = CameraObject.WorldToScreenPoint(
                                    Player.transform.position);
        // X Axis Player Movement
        // Off screen to the RIGHT?
        if (screenPosition.x > Screen.width)
        {
            screenPosition.x = 0;
            // Transform clamped screen position to world space and
            // assign to player ship
            Player.transform.position =
                CameraObject.ScreenToWorldPoint(screenPosition);
        }
        // Off screen o the LEFT?d
        else if (screenPosition.x < 0)
        {
            // Clamp (reset) to the screen's left side
            screenPosition.x = 1350;
            // Transform clamped screen position to world space and
            // assign to player ship
            Player.transform.position =
                CameraObject.ScreenToWorldPoint(screenPosition);
        }
        // Y Axis Player Movement
        // Off screen to the TOP?
        if (screenPosition.y > Screen.height)
        {
            // Clamp (reset) to the screen's top side
            screenPosition.y = Screen.height;
            // Transform clamped screen position to world space and
            // assign to player ship
            Player.transform.position =
                CameraObject.ScreenToWorldPoint(screenPosition);

            Shake(1, 1);
        }
        // Off screen o the BOTTOM?
        else if (screenPosition.y < 0)
        {
            // Clamp (reset) to the screen's bottom side
            screenPosition.y = 0;
            // Transform clamped screen position to world space and
            // assign to player ship
            Player.transform.position =
                CameraObject.ScreenToWorldPoint(screenPosition);

            Shake(2, 3);
        }
    }

    #region RealShake

    public Camera mainCam;

    float shakeAmount = 0;

    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    void BeginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPosition = mainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPosition.x += offsetX;
            camPosition.y += offsetY;

            mainCam.transform.position = camPosition;
        }
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition = Vector3.zero;
    }

    #endregion

}
