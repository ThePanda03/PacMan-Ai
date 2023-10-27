using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    //STARTS CHASE SCRIPT WHEN ITS DISABLED
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    //GHOSTS GO IN RANDOM DIRECTION WHEN SPACE IS AVAILABLE
    private void OnTriggerEnter2D(Collider2D other)
    {
        Nodes node = other.GetComponent<Nodes>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
