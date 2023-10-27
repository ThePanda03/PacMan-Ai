using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    //SETTING VARIABLES 
    public LayerMask obstacleLayer;
    public List<Vector2> availableDirections { get; private set; }
    //SETS AVAILABLE DIRECTION INTO LIST
    public void Start()
    {
        this.availableDirections = new List<Vector2>();

        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }
    //CHECKS WHICH DIRECTION IS AVAILABLE 
    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, obstacleLayer);

        if (hit.collider == null)
        {
            this.availableDirections.Add(direction);
        }
    }
}

