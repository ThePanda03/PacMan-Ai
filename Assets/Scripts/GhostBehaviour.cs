using UnityEngine;
//CANT RUN WITHOUT THE COMPONENT
[RequireComponent(typeof(Ghosts))]
public abstract class GhostBehaviour : MonoBehaviour
{
    //SETS THE VARIABLES
    public Ghosts ghost { get; private set; }
    public float duration;
    //LINKS VARIABLE TO SCRIPT 
    private void Awake()
    {
        this.ghost = GetComponent<Ghosts>();
    }
    //MAKES SURE BEHAVIOURS ARE ON FOR DURATION OF THE GAME
    public void Enable()
    {
        Enable(this.duration);
    }
    //VIRTUAL VOID, OTHER SCRIPTS CAN CHANGE IT 
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }
    //DISABLES BEHAVIOURS WHEN NEEDED
    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }

}
