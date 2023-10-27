using UnityEngine;

public class GhostChase : GhostBehaviour
{   
    //TURNS ON SCRIPT WHEN SCATTER IS DISABLED
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }
    //CHASES PACMAN SHORTEST POSSIBLE ROUTE WITH RAYCASTS
    private void OnTriggerEnter2D(Collider2D other)
    {
        Nodes node = other.GetComponent<Nodes>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 NewPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.ghost.target.position - NewPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}
