using UnityEngine;

/// <summary>
/// Class that keeps track of the player's inventory
/// </summary>
public class Inventory : MonoBehaviour {
    private int NumCatGrass;
    private int NumLives;
    private int Score;

    // Start is called before the first frame update
    void Start() {
        NumCatGrass = 0;
        NumLives = 4;
        Score = 0;
    }

    /// <summary>
    /// Add n number of cat grass to inventory
    /// </summary>
    /// <param name="numCatGrass">Number of cat grass to be added</param>
    public void AddCatGrass(int numCatGrass) {
        NumCatGrass += numCatGrass;
        Score += 100 * numCatGrass;

        if (NumCatGrass >= 100) {
            // if get 100 cat grass, get an extra life
            NumCatGrass -= 100;
            NumLives++;
        }
    }
    
    /// <summary>
    /// Get the number of cat grass in inventory
    /// </summary>
    /// <returns>Number of cat grass</returns>
    public int GetNumCatGrass() {
        return NumCatGrass;
    }

    /// <summary>
    /// Decrement number of lives when player dies
    /// </summary>
    public void Died() {
        NumLives--;
    }
    
    /// <summary>
    /// Get the number of lives the player has
    /// </summary>
    /// <returns>Number of lives</returns>
    public int GetNumLives() {
        return NumLives;
    }

    /// <summary>
    /// Add to the score
    /// </summary>
    /// <param name="score">Amount to add to the score</param>
    public void IncrementScore(int score) {
        this.Score += score;
    }

    /// <summary>
    /// Get the score
    /// </summary>
    /// <returns>Score</returns>
    public int GetScore() {
        return this.Score;
    }

}
