using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    //SETTING VARIABLES
    public Transform homeTransform;
    public Transform exitTransfrom;
    //STOPS ALL ROUTINES 
    private void OnEnabled()
    {
        StopAllCoroutines();
    }
    //STARTS THE ROUTINE TO LEAVE HOME 
    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }
    //MAKES GHOSTS BOUNCE AROUND IN HOME 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }
    //CHANGES GHOST RIGIDBODY AND CHANGES THEIR POSITION TO LEAVE
    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        //SETTING NEW VARIABLES
        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;
        //IN HOME
        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.homeTransform.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;
        //OUTSIDE
        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.homeTransform.position, this.exitTransfrom.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        //RETURNS RIGIDBODY TO NORMAL
        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    }
}
