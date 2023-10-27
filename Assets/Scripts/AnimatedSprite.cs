using UnityEngine;
//SCRIPT CANT RUN WITHOUT THE COMPONENT
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    //SETTING ALL THE VARIABLES
    public SpriteRenderer spriterenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public int animationFrame { get; private set; }
    public bool loop = true;
    //SETTING THE SPRITE RENDERER
    private void Awake()
    {
        this.spriterenderer = GetComponent<SpriteRenderer>();
    }
    //SETTING THE ANIMATIONS
    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }
    //LOOPS THE SPRITES IN THE LISTS UNTIL THE STATE CHANGES
    private void Advance()
    {
        if (!this.spriterenderer.enabled)
        {
            return;
        }

        this.animationFrame++;
        
        if (this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }

        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriterenderer.sprite = this.sprites[this.animationFrame];
        }
    }
    //RESETTING THE SPRITES ANIMATION
    public void Restart()
    {
        this.animationFrame = -1;

        Advance();
    }
}
