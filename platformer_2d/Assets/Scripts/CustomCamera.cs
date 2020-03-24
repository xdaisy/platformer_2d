using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomCamera : MonoBehaviour {
    public Transform Player;
    public Tilemap BG;
    [SerializeField] private int offset;

    private Vector3 minPos;
    private Vector3 maxPos;

    private float halfWidth;
    private float halfHeight;

    // Start is called before the first frame update
    void Start() {
        // get lower left corner and upper right corner of the map
        minPos = BG.origin;
        maxPos = BG.origin + BG.size;

        // get the height and width of the camera view
        Camera cam = this.GetComponent<Camera>();
        float height = 2f * cam.orthographicSize;
        halfHeight = height / 2;
        halfWidth = height * cam.aspect / 2;
    }

    // Update is called once per frame
    void Update() {
        followPlayer();
    }

    /// <summary>
    /// Have the camera follow the player
    /// </summary>
    private void followPlayer() {
        transform.position = new Vector3(
            Mathf.Clamp(Player.position.x, minPos.x + halfWidth, maxPos.x - halfWidth),
            Mathf.Clamp(Player.position.y, minPos.y + halfHeight, maxPos.y - halfHeight),
            this.transform.position.z
        );
    }

    /// <summary>
    /// Get whether or not the object is within the camera view
    /// </summary>
    /// <param name="other">Transform of the object want to check if is within camera view</param>
    /// <param name="width">Width of the object</param>
    /// <param name="height">Height of the object</param>
    /// <returns>True if the object is within camera view, false otherwise</returns>
    public bool IsWithinView(Transform other, float width, float height) {
        // half width and height of other object
        float otherHalfWidth = width / 2;
        float otherHalfHeight = height / 2;

        // other lower and upper x position
        Vector3 otherLowerLeft = new Vector3(
            other.position.x - otherHalfWidth,
            other.position.y - otherHalfHeight,
            0
        );
        Vector3 otherUpperRight = new Vector3(
            other.position.x + otherHalfWidth,
            other.position.y + otherHalfHeight,
            0
        );

        // this lower and upper x position
        Vector3 thisLowerLeft = new Vector3(
            this.transform.position.x - this.halfWidth,
            this.transform.position.y - this.halfHeight,
            0
        );
        Vector3 thisUpperRight = new Vector3(
            this.transform.position.x + this.halfWidth,
            this.transform.position.y + this.halfHeight,
            0
        );

        return thisLowerLeft.x - offset <= otherUpperRight.x && 
            otherLowerLeft.x <= thisUpperRight.x + offset &&
            thisLowerLeft.y - offset <= otherUpperRight.y &&
            otherLowerLeft.y <= thisUpperRight.y + offset;
    }
}
