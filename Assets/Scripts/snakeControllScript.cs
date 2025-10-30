using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class snakeControllScript : MonoBehaviour
{

    public GameObject prefabBodyPart; // Prefab for body parts, assign in inspector if needed

    public List<GameObject> bodyParts = new List<GameObject>(); // List to hold body parts, max 100 parts

    internal Vector3 direction = new Vector3(1, 0, 0); //initial direction to the right
    private bool canChangeDirection = true; // Flag to control direction change

    void Start()
    {
        bodyParts.Add(gameObject); // Add the head of the snake as the first body part

        // Start position in the middle of the screen and start the main loop
        transform.position = new Vector3(0, 0, 0);
        StartCoroutine(mainLoop());

    } // End of Start()

    private void Update()
    {
        if (!canChangeDirection) return;


        // Change direction based on arrow key input, but prevent reversing direction
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != new Vector3(0, -1, 0))
        {
            direction = new Vector3(0, 1, 0);
            canChangeDirection = false;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != new Vector3(0, 1, 0))
        {
            direction = new Vector3(0, -1, 0);
            canChangeDirection = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != new Vector3(1, 0, 0))
        {
            direction = new Vector3(-1, 0, 0);
            canChangeDirection = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != new Vector3(-1, 0, 0))
        {
            direction = new Vector3(1, 0, 0);
            canChangeDirection = false;
        }

    }// End of Update()

    private IEnumerator mainLoop()
    {
        // Main game loop, moves the snake in the current direction every 0.5 seconds
        while (true)
        {
            // Move the head in the current direction
            transform.position += direction;

            yield return new WaitForSeconds(0.1f);

            // Move each body part to the position of the part in front of it
            for (int i = bodyParts.Count - 1; i > 0; i--)
            {
                bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
            }

            canChangeDirection = true;

        }

    }// End of mainLoop()

    public void PickedApple()
    {
        // Add a new body part at the position of the last body part
        bodyParts.Add(Instantiate(prefabBodyPart, bodyParts[bodyParts.Count - 1].transform.position, Quaternion.identity));

    }// End of PickedApple()
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "BodyPart" && bodyParts.Count > 2)
        {
            // Restart the game by reloading the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Debug.Log("Game Over");
        }
    }
    
}// End of snakeControllScript class
