using UnityEngine;

//NEED COMPONENT TO WORK 
[RequireComponent(typeof(Movment))]
public class Pacman : MonoBehaviour
{
    //SETTING VARIABLES
    public Movment movment { get; private set; }
    //LINKS VARIABLE TO SCRIPT 
    private void Awake()
    {
        this.movment = GetComponent<Movment>();
    }
    //MOVES AND ROTATES PACMAN IN DIRECTION
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movment.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movment.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movment.SetDirection(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movment.SetDirection(Vector2.down);
        }

        float angle = Mathf.Atan2(movment.direction.y, movment.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    //RESETS POSITION AND MAKES PACMAN VISIBLE 
    public void ResetState()
    { 
        this.movment.ResetState();
        this.gameObject.SetActive(true);
    }
}
