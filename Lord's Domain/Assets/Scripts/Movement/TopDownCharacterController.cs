using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed = 10f;
        private Rigidbody2D rigidBody;
        private Animator animator;
        private int facingCamera = 0;
        public bool isMoving = false;

        private void Start()
        {
            //animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();

        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                //animator.SetInteger("Direction", 3);
                //Left
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                //animator.SetInteger("Direction", 2);
                //Right
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                //animator.SetInteger("Direction", 1);
                //Up
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                //animator.SetInteger("Direction", 0);
                //Down
            }

            //Debug.Log(animator.GetInteger("Direction"));
            //Debug.Log(dir);

            dir.Normalize();
            //animator.SetBool("IsMoving", dir.magnitude > 0);
            if (dir.magnitude > 0)
            {
                isMoving = true;
            }


            rigidBody.velocity = speed * dir;
            //Debug.Log(rigidBody.velocity);
        }
    }
}
