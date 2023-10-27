using UnityEngine;

public class PowerPellet : Pellet
{
    //SETS DURATION 
    public float duration = 8.0f;
    //LINKS SCRIPTS 
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PowerPellet(this);
    }
}
