using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public float speed = 2f;
        public Rigidbody2D rb;
        private SpriteRenderer sr;

        // LayerMasks for the ground and wall layers
        public LayerMask groundLayer;
        public LayerMask wallLayer;

        public Transform groundCheck;

        bool isFacingLeft;

        // Raycasts to detect contact with pipes, block walls, and the edge of a platform or ground
        RaycastHit2D hitGround;
        RaycastHit2D hitWall;

        // Second try; uses Rigidbody2D with raycast and ground check game objects for detection of when boundary has been reached
        void Start()
        {
            isFacingLeft = true;
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            hitGround = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);
            hitWall = Physics2D.Raycast(groundCheck.position, -transform.right, 1f, wallLayer);
        }

        private void FixedUpdate()
        {
            if (hitGround.collider || hitWall.collider)
            {
                if (isFacingLeft)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
            }
            else
            {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Walls"))
            {
                if (sr.flipX == false)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }

                // Update current direction regardless of flipX status
                isFacingLeft = !isFacingLeft;
            }
        }
    }
}