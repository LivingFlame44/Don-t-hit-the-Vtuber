using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    public bool canJump = false;
   
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if(gameObject.transform.position.y < -5)
        {
            BounceManager.instance.GameOver();
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            canJump = false;
            rb.AddForce(Vector3.up * jumpForce * 1000 * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    //private void OnBecameInvisible()
    //{
    //    BounceManager.instance.GameOver();
    //}
}