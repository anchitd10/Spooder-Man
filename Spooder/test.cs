// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor.VersionControl;
// using UnityEngine;

// public class Grappling : MonoBehaviour
// {
//     private Vector3 mousePos;
//     private Camera cam;
//     private bool study;
//     private DistanceJoint2D disJoint;
//     private LineRenderer lnRenderer;
//     private Vector3 tempPos;


//     //movement

//     public float jumpforce = 10f;
//     public float walkforce = 5f;
//     private Rigidbody2D rb;
//     private bool isJumping = false;
//     public float brakeFactor = 10f;



//     void Start()
//     {
//         cam = Camera.main;
//         disJoint = GetComponent<DistanceJoint2D>();

//         disJoint.enabled = false;
//         study = true;

//         lnRenderer = GetComponent<LineRenderer>();
//         lnRenderer.positionCount = 0;

//         // movement
//         rb = GetComponent<Rigidbody2D>();

//     }

//     void Update()
//     {
//         MousePos();
//         if(Input.GetMouseButtonDown(button: 0) && study)
//         {
//             disJoint.enabled = true;
//             disJoint.connectedAnchor = mousePos;
//             lnRenderer.positionCount = 2;
//             study = false;
//             tempPos = mousePos;
//         }

//         else if(Input.GetMouseButtonDown(button: 0))
//         {
//             disJoint.enabled = false;
//             study = true;
//             lnRenderer.positionCount = 0;
//         }

//         DrawLine();

//         if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.W)))&& !isJumping)
//         {
//             rb.AddForce(Vector3.up * jumpforce,ForceMode2D.Impulse);
//             isJumping = true;
//         }

//         if (Input.GetKeyDown(KeyCode.A) && !isJumping)
//         {
//             rb.AddForce(Vector3.left * walkforce, ForceMode2D.Impulse);
//             // rb.position += Vector2.right;
//         }

//         if (Input.GetKeyDown(KeyCode.D) && !isJumping)
//         {
//             rb.AddForce(Vector3.right * walkforce, ForceMode2D.Impulse);
//             // rb.position += Vector2.left;
//         }    

//         if (Input.GetKeyDown(KeyCode.S))
//         {
//             rb.velocity = Vector3.zero;
//         }
//     }

//     private void MousePos()
//     {
//         mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
//     }

//     private void DrawLine()
//     {
//         if(lnRenderer.positionCount <= 0) return;
//         lnRenderer.SetPosition(index: 0, transform.position);
//         lnRenderer.SetPosition(index: 1, tempPos);
//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isJumping = false;
//         }
//     }
// }