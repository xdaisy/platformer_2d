﻿using UnityEngine;

/// <summary>
/// Class that dictates the rommba's behavior
/// </summary>
public class Roomba : Enemy {
    private Transform player;
    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        move();
    }

    /// <summary>
    /// Roomba follows the player in its movement
    /// </summary>
    private void move() {
        Vector2 newPos = new Vector2(player.position.x, this.transform.position.y); // follow player
        int moveSpeed = this.GetMoveSpeed();

        transform.position = Vector2.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);

        float direction = player.position.x - this.transform.position.x;
        if (direction > 0) {
            // is moving right
            anim.SetFloat("Direction", 1f);
        } else {
            // is moving left
            anim.SetFloat("Direction", -1f);
        }
    }
}
