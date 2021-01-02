using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that handles the player's controls
/// </summary>
public class Player : MonoBehaviour {
    public float MoveSpeed;
    public float JumpMovement;
    public PauseMenu PauseMenu;

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
        bool isOpened = PauseMenu.IsOpen();
        if (isAlive && !EndStage.Instance.HasStageFinished() && Input.GetButtonDown("Pause")) {
            // pause the game
            if (isOpened) {
                // menu is open, close it
                PauseMenu.CloseMenu();
            } else {
                // menu is not open, open it
                PauseMenu.OpenMenu();
            }
        }
        if (isAlive && isGrounded() && Input.GetButtonDown("Jump")) {
            // jump if alive and on the ground
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
            AudioManager.Instance.PlaySFX(Constants.POWER_DOWN_SFX);
            poweredUp = false;
            invincibilityCoolDown = invincibilityFrame;
            StartCoroutine(playPowerDownAnim());
        } else if (invincibilityCoolDown <= 0f) {
            // get camera to stop following player
            CustomCamera cam = GameObject.FindObjectOfType<CustomCamera>();
            cam.StopFollowingPlayer();
            Destroy(myRigidBody);
            isAlive = false;
        }
    }

    /// <summary>
    /// Power up the Player
    /// </summary>
    public void PowerUp() {
        if (!poweredUp) {
            // power up
            AudioManager.Instance.PlaySFX(Constants.POWER_UP_SFX);
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
    /// Get whether or not the player is still alive
    /// </summary>
    /// <returns>True if the player is still alive, false otherwise</returns>
    public bool GetIsAlive() {
        return isAlive;
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
        AudioManager.Instance.PlaySFX(Constants.JUMP_SFX);
        myRigidBody.velocity = Vector2.up * JumpMovement;
    }

    /// <summary>
    /// Move the player
    /// </summary>
    private void handleMovement() {
        if (!isAlive && deathWaitTime < Constants.DEATH_WAIT_TIME) {
            // died so move player to left side of the screen
            deathWaitTime += Time.deltaTime;
            this.transform.position = this.transform.position + (Vector3.left * 0.25f);
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
            float moveX = isAlive ? myRigidBody.velocity.x : -1f;

            anim.SetFloat("xMove", moveX);
            anim.SetBool("poweredUp", poweredUp);

            if (moveX > 0.1 || moveX < -0.1) {
                anim.SetFloat("lastXMove", moveX);
            }

            bool isJumping = false;
            bool isMoving = false;

            if (isAlive && isGrounded()) {
                // is on the ground and is alive
                isMoving = moveX > 0.1 || moveX < -0.1;
            } else if (isAlive) {
                // is in the air and is alive
                isJumping = true;
            } else {
                // is dead
                isMoving = true;
            }

            anim.SetBool("isMoving", isMoving);
            anim.SetBool("isJumping", isJumping);
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
