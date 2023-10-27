using UnityEngine;
//CANT RUN WITHOUT THE COMPONENT ON GAMEOBJECT
[RequireComponent(typeof(Rigidbody2D))]
public class Movment : MonoBehaviour
{
    //SETTING VARIABLES 
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }
    //LINKING VARIABLES WITH SCRIPTS AND SETTING INITIAL DIRECTION
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }
    //RESETS GAME ON START
    private void Start()
    {
        ResetState();
    }
    //RESETS POSITION AND VARIABLES
    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }  
    }
    //CONSTANTLY MOVES THE GAMEOBJECT
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        
        rigidbody.MovePosition(position + translation);
    }
    //CHANGES DIRECTION ONLY WHEN TILE IS AVAILABLE 
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }
    //CHECKS IF TILE IS OCCUPIED 
    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, obstacleLayer);
        return hit.collider !=null;
    }
}
