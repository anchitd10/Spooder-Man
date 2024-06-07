using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grappling : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;
    private bool study;
    private DistanceJoint2D disJoint;
    private LineRenderer lnRenderer;
    private Vector3 tempPos;


    public Vector2 CheckPoint;

    public Voonm voonm;


    //movement

    public float jumpforce = 10f;
    public float walkforce = 5f;
    private Rigidbody2D rb;
    private bool isJumping = false;
    public float brakeFactor = 10f;

    //animation
    
    public Animator animator;
    public SpriteRenderer _spritePlayer;

    // music
    public AudioSource webaudio;
    public AudioSource checkaudio;



    void Start()
    {
        CheckPoint = new Vector3(1.2018f,5.1558f,-4.15f);
        cam = Camera.main;
        disJoint = GetComponent<DistanceJoint2D>();

        disJoint.enabled = false;
        study = true;

        lnRenderer = GetComponent<LineRenderer>();
        lnRenderer.positionCount = 0;

        // movement
        rb = GetComponent<Rigidbody2D>();

        //animation
        _spritePlayer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MousePos();
        if(Input.GetMouseButtonDown(button: 0) && study)
        {
            webaudio.Play();

            disJoint.enabled = true;
            disJoint.connectedAnchor = mousePos;
            lnRenderer.positionCount = 2;
            study = false;
            tempPos = mousePos;
        }

        else if(Input.GetMouseButtonDown(button: 0))
        {
            disJoint.enabled = false;
            study = true;
            lnRenderer.positionCount = 0;
        }

        DrawLine();

        if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.W)))&& !isJumping)
        {
            animator.SetFloat("Jump", rb.velocity.y);
            
            rb.AddForce(Vector3.up * jumpforce,ForceMode2D.Impulse);
            isJumping = true;
        }

        // if (Input.GetKeyDown(KeyCode.A) && !isJumping)
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(Vector3.left * walkforce, ForceMode2D.Impulse);
            _spritePlayer.flipX = true;
            // rb.position += Vector2.right;
        }

        // if (Input.GetKeyDown(KeyCode.D) && !isJumping)
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector3.right * walkforce, ForceMode2D.Impulse);
            _spritePlayer.flipX = false;
            // rb.position += Vector2.left;
        }    

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = Vector3.zero;
        }


        // animation

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("Jump", rb.velocity.y);

    }

    private void MousePos()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void DrawLine()
    {
        if(lnRenderer.positionCount <= 0) return;
        lnRenderer.SetPosition(index: 0, transform.position);
        lnRenderer.SetPosition(index: 1, tempPos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("mic"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("checkpoint"))
        {
            checkaudio.Play();
            CheckPoint = rb.position;
        }

        if (collision.gameObject.CompareTag("Boundary") || collision.gameObject.CompareTag("villain"))
        {
            transform.position = CheckPoint;
            rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("maxHeight"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (collision.gameObject.CompareTag("mic")){
            // Debug.Log("collided");
            voonm.Damagable = true;
        }
        else {
            voonm.Damagable = false;
        }
    }
}