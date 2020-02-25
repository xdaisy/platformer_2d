using UnityEngine;

/// <summary>
/// Class that dictates the mouse's behavior
/// </summary>
public class Mouse : Enemy {
    [SerializeField] private Transform[] waypoints; // array of the points in the path

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
        move();
    }

    /// <summary>
    /// Move the mouse
    /// </summary>
    private void move() {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentPoint].transform.position, this.GetMoveSpeed() * Time.deltaTime);
        
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
}
