using UnityEngine;

public class Mouse : MonoBehaviour {
    [SerializeField] private Transform[] waypoints; // array of the points in the path
    [SerializeField] private int moveSpeed; // movement speed of the mouse
    [SerializeField] private int score;

    private Animator anim;
    private int currentPoint;
    private bool moveForward;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        moveForward = true;
        currentPoint = 1;
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    /// <summary>
    /// Move the mouse
    /// </summary>
    private void Move() {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentPoint].transform.position, moveSpeed * Time.deltaTime);
        
        if (moveForward && currentPoint >= waypoints.Length - 1) {
            // start moving backward
            moveForward = false;
        } else if (!moveForward && currentPoint <= 0) {
            // start moving forward
            moveForward = true;
        }
        
        if (this.transform.position == waypoints[currentPoint].transform.position) {
            // increment/decrement to next point when reached current point
            if (moveForward) {
                currentPoint++;
                anim.SetFloat("Direction", 1f);
            } else {
                currentPoint--;
                anim.SetFloat("Direction", -1f);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Vector2 thisPos = this.transform.position;
            Vector2 playerPos = other.transform.position;
            float angle = Vector2.Angle(playerPos, thisPos);
            
            if (angle < 4f) {
                // enemy dies
                Destroy(this.gameObject);
                Inventory.Instance.IncrementScore(this.score);
            } else {
                // TODO: Implement player's death
                Player player = other.gameObject.GetComponent<Player>();
                player.Death();
            }
        }
    }
}
