using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.PlatformGame.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        Rigidbody2D rigidbody;
        Animator animator;
        [SerializeField] Camera camera;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpPower;
        private float slopeSupport;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), Mathf.Abs(Input.GetAxis("Horizontal")) * slopeSupport, 0) * moveSpeed * Time.deltaTime;

            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            animator.SetFloat("WalkSpeed", Mathf.Abs(Input.GetAxis("Horizontal")));

            transform.position += move;
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rigidbody.velocity.y, 0))
            {
                rigidbody.AddForce(Vector2.up * jumpPower);
                animator.SetBool("Jump", true);
            }
            else if (Mathf.Approximately(rigidbody.velocity.y, 0))
                animator.SetBool("Jump", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Slope"))
                slopeSupport = 2;
            else if (collision.transform.CompareTag("Finish"))
            {
                camera.transform.position = new Vector3(18, 0, -10);
                transform.position = collision.transform.position;
                collision.gameObject.SetActive(false);
                //GameManager.Instance.AddOwnedGold();

            }
            else if(collision.transform.CompareTag("Gold"))
            {
                //GameManager.Instance.SetCollectedGold(1);
                collision.transform.gameObject.SetActive(false);
            }


        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Slope"))
                slopeSupport = 0;
        }
    }
}