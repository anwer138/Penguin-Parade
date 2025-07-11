using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalanceController : MonoBehaviour
{
    [Header("Leaning Thresholds")]
    public float movementThreshold = 0.15f;   // Start moving if above this
    public float fallThreshold = 0.35f;       // Fall if above this

    [Header("Body Tracking")]
    [SerializeField] private Transform hipsTransform;  // Assign hips joint from body tracking

    [Header("Leaning Settings")]
    public float leanThreshold = 0.15f;  // Minimum lean (in meters) to trigger movement
    public float moveSpeed = 1.5f;       // Speed of horizontal movement
    private float neutralX;              // Neutral center X position
    private float moveCooldown = 0.3f;
    private float lastMoveTime;
    [SerializeField] private Transform penguinObject; 

    void Start()
    {
        // Store the initial hips X position as the neutral center
        neutralX = hipsTransform.localPosition.x;
    }

    private void Update()
    {
        float leanAmount = hipsTransform.localPosition.x - neutralX;
    
        if (Time.time - lastMoveTime < moveCooldown)
            return; // too soon, skip
    
        if (Mathf.Abs(leanAmount) > fallThreshold)
        {
            TriggerFall();
        }
        else if (leanAmount > movementThreshold)
        {
            MoveRight();
            lastMoveTime = Time.time;
        }
        else if (leanAmount < -movementThreshold)
        {
            MoveLeft();
            lastMoveTime = Time.time;
        }
    }

    private void MoveLeft()
    {
        // You can replace this with actual penguin movement later
        penguinObject.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        Debug.Log("Leaning Left → Moving Left");
    }

    private void MoveRight()
    {
        penguinObject.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        Debug.Log("Leaning Right → Moving Right");
    }

    private void TriggerFall()
    {
        Debug.Log("⚠️ Player leaned too far — Fell!");

        // Example: respawn, restart level, play animation, etc.
        // For now:
        moveSpeed = 0f;

        // Optional: play animation or sound
        // GetComponent<Animator>().SetTrigger("Fall");
    }

    public void Calibrate()
    {
        neutralX = hipsTransform.localPosition.x;
        Debug.Log("🧭 Calibrated center to: " + neutralX);
    }



}
