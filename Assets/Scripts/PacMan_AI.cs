using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PacMan_AI : Agent
{
    public GameManager gameManager;
    public void Awake()
    {
        gameManager.GetComponent<GameManager>();
    }

    [SerializeField] private Transform targettransform;

    public override void OnEpisodeBegin()
    {
        gameManager.GetComponent<GameManager>().NewGame();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targettransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float movespeed = 8f;
        transform.position += new Vector3(moveX, moveY, 0) * Time.deltaTime * movespeed;
    }

    private void Update()
    {
        if (gameManager.GetComponent<GameManager>().hasEatenPill) 
        { 
            AddReward(+1f); 
            gameManager.GetComponent<GameManager>().hasEatenPill = false; 
        }
    }
}
