using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;
    //WHEN GOING THROUGH THE TRIGGER GETS PUT AT THE OTHER SIDE WITH THE SAME ROTATION
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        other.transform.position = position;
    }
}
