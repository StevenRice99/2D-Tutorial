using UnityEngine;

/// <summary>
/// Basic 2D movement.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMover2D : MonoBehaviour
{
    /// <summary>
    /// How much force to move with.
    /// </summary>
    [Tooltip("How much force to move with.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float force = 10;
    
    /// <summary>
    /// How strong the jump should be.
    /// </summary>
    [Tooltip("How strong the jump should be.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float jump = 10;
    
    /// <summary>
    /// Control the movement of the character.
    /// </summary>
    [HideInInspector]
    [SerializeField]
    private Rigidbody2D rb;

    /// <summary>
    /// If the character is grounded or not.
    /// </summary>
    private bool _grounded;

    /// <summary>
    /// Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    /// </summary>
    private void OnValidate()
    {
        // Check if the rigidbody is not yet set.
        if (rb == null)
        {
            // Get the attached rigidbody.
            rb = GetComponent<Rigidbody2D>();
        }
    }
    
    /// <summary>
    /// Frame-rate independent MonoBehaviour. FixedUpdate message for physics calculations.
    /// </summary>
    private void FixedUpdate()
    {
        // Start with no movement.
        float movement = 0;

        // Check if wanting to move right.
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Add movement to go to the right.
            movement += 1;
        }
        
        // Check if wanting to move left.
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Subtract movement to go to the left.
            movement -= 1;
        }
        
        // Apply the movement.
        rb.AddForce(new(movement * force, 0));
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Check if on the ground and wanting to jump.
        if (_grounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Add an impulse to jump.
            rb.AddForce(new(0, jump), ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The collider in contact with.</param>
    private void OnCollisionStay2D(Collision2D other)
    {
        // Set that it is on the ground.
        _grounded = true;
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The collider no longer in contact with.</param>
    private void OnCollisionExit2D(Collision2D other)
    {
        // Set that it is not on the ground.
        _grounded = false;
    }
}