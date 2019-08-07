using UnityEngine;

public class Player : MonoBehaviour {
    public float MoveSpeed;

    private Rigidbody2D myRigidBody;
    private Animator anim;

    private bool jump;

    // Start is called before the first frame update
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
    }

    private void FixedUpdate() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = 0f;

        if (jump) {
            moveY = 20f;
            jump = false;
        }

        myRigidBody.velocity = new Vector2(moveX, moveY) * MoveSpeed;

        anim.SetFloat("xMove", moveX);
        anim.SetFloat("lastXMove", moveX);
    }
}
