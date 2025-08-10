using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public enum CharacterState
{
    None,
    Idle,
    Walk,
    Jump,
    Hurt,
    Punch,
    Kick,
    Dead,
    TotallyDead
}

public class Player : MonoBehaviour
{
    private Animator animator;
    public float speed = 5.0f;
    private Vector3 originalScale;
    public PolygonCollider2D walkableArea;

    public CharacterState state;

    public LayerMask obstacleMask;
    public Collider2D FeetArea;
    
    private Rigidbody2D rb;
    
    Vector3 deltaMove = Vector3.zero;

    public float JumpForce = 3.0f;

    public float JumpTime = 1.0f;

    private float horizontal = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        Debug.Log("Update of Player");

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    IEnumerator StopPhysics()
    {
        yield return new WaitForSeconds(JumpTime);
        
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            Vector2 force = new Vector2(horizontal, JumpForce);
            
            rb.AddForce(force);
            
            StartCoroutine(StopPhysics());
        }

        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            // left click
            Debug.Log("Left click pressed");
            state = CharacterState.Punch;
            
            animator.SetTrigger(state.ToString());

        }
        if (Input.GetMouseButtonDown((int)MouseButton.Right))
        {
            // right click
            Debug.Log("Right click pressed");
        }

        if (IsAnimationRunning("Punch")) return;
            
         horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            Debug.Log(horizontal);
            
            deltaMove =  Vector3.right * (horizontal * speed * Time.deltaTime);
            if (CanMove(deltaMove))
                transform.position += deltaMove;// for Dima's sake
         
            transform.localScale = new Vector3(
                horizontal * Mathf.Abs(transform.localScale.x), 
                transform.localScale.y, 
                transform.localScale.z
                );
            
            state = CharacterState.Walk;

        }
        else
        {
            state = CharacterState.Idle;
        }
        
        
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            deltaMove = Vector3.up * vertical * speed * Time.deltaTime;
            if (CanMove(deltaMove))
                transform.position += deltaMove;

            state = CharacterState.Walk;
            Debug.Log(vertical);
        }

        if (vertical != 0 || horizontal != 0)
        {
            if (walkableArea.OverlapPoint(transform.position) == false)
            {
                Vector2 newPostion = walkableArea.ClosestPoint(transform.position);
                transform.position = new Vector3(newPostion.x, newPostion.y, transform.position.z);
            }
        }
        
        animator.Play(state.ToString());


    }

    bool IsAnimationRunning(string animationNAme)
    {
       AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
       if (currentStateInfo.IsName(animationNAme))
       {
           if (currentStateInfo.normalizedTime < 0.95f)
           {
               return true;
           }
       }

       return false;
    }

    bool CanMove(Vector3 deltaMove)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = obstacleMask;
        filter.useLayerMask = true;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        
        Vector2 direction = new Vector2(deltaMove.x, deltaMove.y);
        int hitCount = FeetArea.Cast(direction, filter, hits, direction.magnitude + 0.1f);
        return hitCount == 0;
    }
}
