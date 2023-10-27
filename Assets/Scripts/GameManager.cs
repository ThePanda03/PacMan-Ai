using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //SETTING ALL VARIABLES AND ARRAYS
    public Ghosts[] ghosts;
    public Pacman PacMan_AI;
    public Transform pellets;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int ghostMultiplier { get; private set; } = 1;
    public bool hasEatenPill;

    //SETS EVERYTHING TO DEFAULT ON START
    private void Start()
    {
        NewGame();
    }
    //sTARTS NEW GAME AND ALSO SETS THE VARIABLES 
    public void NewGame()
    {
        SetScore(0);
        Setlives(3);
        NewRound();
    }
    //RESETS ALL THE PELLETS IN SCENE AND RESETS POSITIONS 
    private void NewRound()
    {
        foreach (Transform pellets in this.pellets)
        {
            pellets.gameObject.SetActive(true);
        }

        ResetState();
    }
    //RESETS PACMAN WHEN DEAD
    private void ResetState()
    {
        ResetghostMultiplier();

        for (int i = 0; i< this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        PacMan_AI.ResetState();
    }
    //TURNS OFF ALL GAMEOBJECTS WHEN PACMAN RUNS OUT OF LIVES AND RESETS GAME
    private void GameOver()
    {
        for (int i = 0; i< this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.PacMan_AI.gameObject.SetActive(false);        
    }

    //RESETS SCORE ON NEW GAME
    private void SetScore(int score)
    {
        this.score = score;
    }
    //SETS LIVES WHEN NEW GAME STARTS
    private void Setlives(int lives)
    {
        this.lives = lives;
    }
    //INCREASES POINTS IF GHOSTS GETS EATEN
    public void GhostsEaten(Ghosts ghosts)
    {
        int points = ghosts.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }
    //DECREASES LIVES AND RESETS POSITION OF GAMEOBJECTS
   
    public void PacmanEaten()
    {
        this.PacMan_AI.gameObject.SetActive(false);

        Setlives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }

        hasEatenPill = true;
    }


    //INCREASES SCORE AND STARTS NEW ROUND WHEN ALL PELLETS ARE EATEN
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);  
        SetScore(this.score + pellet.points);

        if (!RemaningPellets())
        {
            this.PacMan_AI.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    //PUTS GHOSTS INTO FRIGHTENED STATE
    public void PowerPellet(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++) 
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetghostMultiplier), pellet.duration);
    }
    //CHECKS HOW MANY PELLETS ARE LEFT
    private bool RemaningPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
    //RESETS GHOHST POINT MULTIPLIER WHEN POWERPELLET RUNS OUT 
    private void ResetghostMultiplier()
    {

    }
}
