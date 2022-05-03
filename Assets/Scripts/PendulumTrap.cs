using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PendulumTrap : MonoBehaviour
{
    public Rigidbody2D rb;
    public float RotationSpeed;
    public float PendulumLeftCap;
    public float PendulumRightCap;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = RotationSpeed;
    }

    private void Update()
    {
        TrapRotation();
    }

    private void TrapRotation()
    {
        if (transform.rotation.z > 0 && transform.rotation.z < PendulumRightCap && rb.angularVelocity > 0 && rb.angularVelocity < RotationSpeed)
        {
            rb.angularVelocity = RotationSpeed;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z > PendulumLeftCap && rb.angularVelocity < 0 && rb.angularVelocity > -RotationSpeed *-1)
        {
            rb.angularVelocity = RotationSpeed * -1;
        }
    }
}
