using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PlayerInput : MonoBehaviour
{
    private float horizontal;
    
    private float speed = 2f;
    private float jumpingPower = 4f;
    private bool isFacingRight = true;
    private Animator anim;

    public AudioSource audioPlayer;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public BoxCollider2D regCol;
    public BoxCollider2D slideCol;
    public Vector3 respawnPoint;


    void Start()
    {
        //regCol.enabled = true;
        //slideCol.enabled = false;
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
        respawnPoint = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("IsAttacking", true);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("IsSlide", true);
            Slide();
            //anim.SetBool("IsSlide", false);
        }


        Flip();
    }

    private void Slide(){
        //regCol.enabled = false;
        //slideCol.enabled = true;
        speed = 5f;
        StartCoroutine(ResetSlide());
        //regCol.enabled = true;
        //slideCol.enabled = false;
    }

    IEnumerator ResetSlide()
    {
        yield return new WaitForSeconds(0.5f);
        speed = 2f;
        anim.SetBool("IsSlide", false);
        //regCol.enabled = true;
        //slideCol.enabled = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public void endAttack()
    {
        anim.SetBool("IsAttacking", false);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "FallDetector"){
            //what will happen when player enters fall detector zone
            transform.position = respawnPoint;
        }
        else if(other.tag == "Checkpoint"){
            //what will happen when player enters checkpoint zone
            respawnPoint = other.transform.position;
        }
        else if(other.tag == "Ice"){
            //what will happen when player enters ice zone
            speed = 3f;
        }
        else if(other.tag == "Normal"){
            //what will happen when player enters normal zone
            speed = 2f;
        }
        else if (other.tag == "Seal" && anim.GetBool("IsAttacking") == false){
            transform.position = respawnPoint;
        }
        else if(other.tag == "Final"){
            SceneManager.LoadScene(2);
        }
    }
}
