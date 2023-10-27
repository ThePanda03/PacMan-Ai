using UnityEngine;

public class Pellet : MonoBehaviour
{
    //SETS POINTS 
    public int points = 10;
    //LINKS SCRIPTS 
    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }
    //ONLY CHANGES WHEN PACMAN EATS PELLETS 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PacMan"))
        {
            Eat();
        }
    }
}
