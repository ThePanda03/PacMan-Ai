using UnityEngine;

public class GhostFrightend : GhostBehaviour
{
    //SETTING VARIABLES
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten { get; private set; }
    //SETTING SPRITES WEHN SCRIPT GETS ENABLED
    public override void Enable(float duration)
    {
        base.Enable(duration);
        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }
    //RESETS SPRITES WHEN ITS DISABLED
    public override void Disable()
    {
        base.Disable();
        
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;       
    }
    //FLASHES THE SPRITES WHEN TIME IS RUNNING OUT 
    private void Flash()
    {
        if (!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<AnimatedSprite>().Restart();
        }
    }
    //SETS POSITION AND SPRITES TO DEFAULT IF EATEN
    private void Eaten()
    {
        this.eaten = true;
        
        Vector3 position = this.ghost.home.homeTransform.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        this.ghost.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false; 
    }
    //SETTING SPEED MULTIPLIER
    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }
    //COLLISION FOR PACMAN AND GHOSTS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PacMan"))
        {
            if (this.enabled)
            {
                Eaten();
            }
        }
    }
    //RUNS AWAY FROM PACMAN WHEN FRIGHTENED OPPOSITE TO CHASE 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Nodes node = other.GetComponent<Nodes>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 NewPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.ghost.target.position - NewPosition).sqrMagnitude;

                if (distance < maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}
