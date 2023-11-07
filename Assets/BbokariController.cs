using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbokariController : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float jumpForce = 100;

    [SerializeField] 
    Transform groundCheck;

    bool mayJump = true;

    [SerializeField] 
    float groundRadius = 0.1f;

    [SerializeField] 
    LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 movementX = new Vector2(moveX, 0);

        transform.Translate(movementX * speed * Time.deltaTime);

        // Debug.DrawLine(transform.position, Vector3.zero, Color.green);

        //bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        Vector3 size = MakeGroundCheckSize();
        bool isGrounded = Physics2D.OverlapBox(groundCheck.position, size, 0, groundLayer);
        
        


        if (Input.GetAxisRaw("Jump") > 0 && mayJump == true && isGrounded == true)
        {
            
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 jump = Vector2.up * jumpForce;

            rb.AddForce(jump);

            mayJump = false;
        }

        if (Input.GetAxisRaw("Jump") == 0)
        {
            mayJump = true;
        }

    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);

        Vector3 size = MakeGroundCheckSize();
        Gizmos.DrawWireCube(groundCheck.position, size);
    }

    private Vector3 MakeGroundCheckSize() => new Vector3(2,groundRadius);
}
