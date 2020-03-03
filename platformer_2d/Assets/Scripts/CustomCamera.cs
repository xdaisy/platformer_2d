using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomCamera : MonoBehaviour {
    public Transform Player;
    public Tilemap BG;

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
        halfHeight = 2f * cam.orthographicSize / 2;
        halfWidth = halfHeight * cam.aspect / 2;
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
    /// <param name="height">Height of the object</param>
    /// <param name="width">Width of the object</param>
    /// <returns>True if the object is within camera view, false otherwise</returns>
    public bool IsWithinView(Transform other, float height, float width) {
        float otherHalfHeight = height / 2;
        float otherHalfWidth = width / 2;
        Vector3 otherLowerLeft = new Vector3(
            other.position.x - otherHalfWidth,
            other.position.y - otherHalfHeight
        );
        Vector3 otherUpperRight = new Vector3(
            other.position.x + otherHalfWidth,
            other.position.y + otherHalfHeight
        );

        Vector3 thisLowerLeft = new Vector3(
            this.transform.position.x - this.halfWidth,
            this.transform.position.y - this.halfHeight
        );
        Vector3 thisUpperRight = new Vector3(
            this.transform.position.x + this.halfWidth,
            this.transform.position.y + this.halfHeight
        );

        //return thisLowerLeft.x < otherLowerLeft.x && otherLowerLeft.x < thisUpperRight.x &&
        //    thisLowerLeft.x < otherUpperRight.x && otherUpperRight.x < thisUpperRight.x &&
        //    thisLowerLeft.y < otherLowerLeft.y && otherLowerLeft.y < thisUpperRight.y &&
        //    thisLowerLeft.y < otherUpperRight.y && otherUpperRight.y < thisUpperRight.y;
        return thisLowerLeft.x < other.position.x && other.position.x < thisUpperRight.x &&
            thisLowerLeft.y < other.position.y && other.position.y < thisUpperRight.y;
    }
}
