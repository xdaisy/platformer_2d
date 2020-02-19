using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float MoveSpeed;
    public float JumpMovement;

    [SerializeField] private LayerMask platformLayerMask; // which layer want to hit with raycast

    private Rigidbody2D myRigidBody;
    private BoxCollider2D boxCollider2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (isGrounded() && Input.GetButtonDown("Jump")) {
            myRigidBody.velocity = Vector2.up * JumpMovement;
        }

        handleMovement();
        handleAnimation();

    }

    public void Death() {
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
}
