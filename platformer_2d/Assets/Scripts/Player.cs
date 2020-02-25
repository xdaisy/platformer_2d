using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that handles the player's controls
/// </summary>
public class Player : MonoBehaviour {
    public float MoveSpeed;
    public float JumpMovement;

    [SerializeField] private LayerMask platformLayerMask; // which layer want to hit with raycast
    [SerializeField] private float invincibilityFrame;

    private Rigidbody2D myRigidBody;
    private BoxCollider2D boxCollider2d;
    private Animator anim;

    private bool poweredUp;
    private float invincibilityCoolDown;

    // Start is called before the first frame update
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        poweredUp = false;
        invincibilityCoolDown = 0f;
    }

    // Update is called once per frame
    void Update() {
        if (isGrounded() && Input.GetButtonDown("Jump")) {
            myRigidBody.velocity = Vector2.up * JumpMovement;
        }

        handleMovement();
        handleAnimation();
        handleInvincibility();
    }

    /// <summary>
    /// What happens when player gets hit by an enemy
    /// </summary>
    public void Hit() {
        if (invincibilityCoolDown <= 0f && poweredUp) {
            poweredUp = false;
            invincibilityCoolDown = invincibilityFrame;
        } else if (invincibilityCoolDown <= 0f) {
            // TODO: Play death animation
            // decrement lives
            Inventory.Instance.Died();

            if (Inventory.Instance.GetNumLives() < 0) {
                // if run out of lives, go to game over scene
                SceneManager.LoadScene(Constants.GAME_OVER_SCENE);
            } else {
                // reset stage
                SceneManager.LoadScene(Constants.STAGE_SCENE);
            }
        }
    }

    /// <summary>
    /// Power up the Player
    /// </summary>
    public void PowerUp() {
        if (!poweredUp) {
            // power up
            poweredUp = true;
        }
    }

    /// <summary>
    /// Determines whether or not the player is grounded
    /// </summary>
    /// <returns>True if the player is on the ground, false otherwise</returns>
    private bool isGrounded() {
        RaycastHit2D rayCast2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        return rayCast2d;
    }

    /// <summary>
    /// Move the player
    /// </summary>
    private void handleMovement() {
        float moveX = Input.GetAxisRaw("Horizontal");

        myRigidBody.velocity = new Vector2(moveX * MoveSpeed, myRigidBody.velocity.y);
    }

    /// <summary>
    /// Determine which animation to play
    /// </summary>
    private void handleAnimation() {
        float moveX = myRigidBody.velocity.x;

        anim.SetFloat("xMove", moveX);

        if (moveX > 0.1 || moveX < -0.1) {
            anim.SetFloat("lastXMove", moveX);
        }

        if (isGrounded()) {
            // is on the ground
            anim.SetBool("isJumping", false);
            anim.SetBool("isMoving", moveX > 0.1 || moveX < -0.1);
        } else {
            // is in the air
            anim.SetBool("isMoving", false);
            anim.SetBool("isJumping", true);
        }
    }

    /// <summary>
    /// Handle the invincibility cool down
    /// </summary>
    private void handleInvincibility() {
        if (invincibilityCoolDown > 0f) {
            invincibilityCoolDown -= Time.deltaTime;
        }
    }
}
