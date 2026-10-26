
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite upSprite;
    public Sprite downSprite;

    public float moveSpeed = 3f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Move the player based on WASD input
        Vector3 movement = new Vector3(horizontal, vertical, 0);
        transform.position += movement.normalized * moveSpeed * Time.deltaTime;

        // Change the sprite based on movement direction
        if (horizontal > 0)
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (vertical > 0)
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (vertical < 0)
        {
            spriteRenderer.sprite = downSprite;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }
    }
}
