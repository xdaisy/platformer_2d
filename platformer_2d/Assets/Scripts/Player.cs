using System.Collections;
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
    [SerializeField] private bool canMove = true;

    private Rigidbody2D myRigidBody;
    private BoxCollider2D boxCollider2d;
    private Animator anim;

    private bool isAlive = true;
    private float deathWaitTime = 0f;
    private bool poweredUp = false;
    private float invincibilityCoolDown = 0f;

    // Start is called before the first frame update
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (isGrounded() && Input.GetButtonDown("Jump")) {
            jump();
        }

        handleMovement();
        handleAnimation();
        handleInvincibility();
        handleDeath();
    }

    /// <summary>
    /// What happens when player gets hit by an enemy
    /// </summary>
    public void Hit() {
        if (invincibilityCoolDown <= 0f && poweredUp) {
            poweredUp = false;
            invincibilityCoolDown = invincibilityFrame;
            StartCoroutine(playPowerDownAnim());
        } else if (invincibilityCoolDown <= 0f) {
            // TODO: Play death animation

            // get camera to stop following player
            CustomCamera cam = GameObject.FindObjectOfType<CustomCamera>();
            cam.StopFollowingPlayer();
            isAlive = false;
        }
    }

    /// <summary>
    /// Power up the Player
    /// </summary>
    public void PowerUp() {
        if (!poweredUp) {
            // power up
            StartCoroutine(playPowerUpAnim());
            poweredUp = true;
        }
    }

    /// <summary>
    /// Tell the player to bounce
    /// </summary>
    public void ApplyBounce() {
        jump();
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
    /// Make the player jump
    /// </summary>
    private void jump() {
        myRigidBody.velocity = Vector2.up * JumpMovement;
    }

    /// <summary>
    /// Move the player
    /// </summary>
    private void handleMovement() {
        if (!isAlive && deathWaitTime < Constants.DEATH_WAIT_TIME) {
            // died so move player to left side of the screen
            deathWaitTime += Time.deltaTime;
            myRigidBody.velocity = new Vector2(
                -MoveSpeed * 2,
                myRigidBody.velocity.y
            );
        } else if (!EndStage.Instance.HasStageFinished()) {
            // if game is still continuing
            if (canMove) {
                float moveX = Input.GetAxisRaw("Horizontal");

                myRigidBody.velocity = new Vector2(moveX * MoveSpeed, myRigidBody.velocity.y);
            } else {
                // freeze the player in the current position they are in
                myRigidBody.velocity = Vector2.zero;
            }
        } else {
            // stage has been finished
            if (isGrounded()) {
                // if the player is grounded
                anim.SetBool("isMoving", false);
                anim.SetBool("isJumping", false);

                // play animation
                CustomCamera cam = GameObject.FindObjectOfType<CustomCamera>();
                EndStage.Instance.PlayEndAnimation(poweredUp);
                cam.StopFollowingPlayer();
                Destroy(this.gameObject);
            }
        }
    }

    private void handleDeath() {
        if (!isAlive && deathWaitTime >= Constants.DEATH_WAIT_TIME) {
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
    /// Determine which animation to play
    /// </summary>
    private void handleAnimation() {
        if (canMove) {
            float moveX = myRigidBody.velocity.x;

            anim.SetFloat("xMove", moveX);
            anim.SetBool("poweredUp", poweredUp);

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

    /// <summary>
    /// Handle the invincibility cool down
    /// </summary>
    private void handleInvincibility() {
        if (invincibilityCoolDown > 0f) {
            invincibilityCoolDown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Play the power up animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator playPowerUpAnim() {
        anim.SetBool("poweringUp", true);

        yield return new WaitForSeconds(Constants.ANIMATION_TIME);

        anim.SetBool("poweringUp", false);

        yield break;
    }

    /// <summary>
    /// Play the power down animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator playPowerDownAnim() {
        anim.SetBool("poweringDown", true);

        yield return new WaitForSeconds(Constants.ANIMATION_TIME);

        anim.SetBool("poweringDown", false);

        yield break;
    }
}
