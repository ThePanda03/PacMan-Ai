using UnityEngine;

public class Ghosts : MonoBehaviour
{
    //MAKING ALL THE VARIABLES
    public Movment movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightend frightened { get; private set; }
    public GhostBehaviour initialBehaviour;
    public Transform target;
    //SETTING THE POINTS FOR THE GHOSTS IF EATEN
    public int points = 200;
    //SETTING ALL THE VARIABLES
    private void Awake()
    {
        this.movement = GetComponent<Movment>();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightend>();
    }
    //MAKING SURE THE GAME ALWAYS START IN THE PROPER CONDITION
    public void Start()
    {
        ResetState();
    }
    //RESETTING ALL THE VARIABLES AND POSITIONS OF GHOST,PACMAN
    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        
        if (this.home != this.initialBehaviour)
        {
            this.home.Disable();
        }

        if (this.initialBehaviour != null)
        {
            this.initialBehaviour.Enable();
        }
    }
    //SETTING THE GAMESTATE FOR WHEN PACMAN RUNS INTO THE GHOSTS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PacMan"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostsEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
