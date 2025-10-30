using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<snakeControllScript>().PickedApple();

            SpawnApple();
            Destroy(gameObject);
        }
    }

    private void SpawnApple()
    {
        int x = Random.Range(-17, 17);
        int y = Random.Range(-9, 9);
        Vector3 applePos = new Vector3(x, y, 0);
        snakeControllScript snake = FindObjectOfType<snakeControllScript>();
        foreach (var part in snake.bodyParts)
        {
            if (part.transform.position == applePos)
            {
                // If the position collides with the snake, try again
                SpawnApple();
                return;
            }
        }

        Instantiate(gameObject, new Vector3(x, y, 0), Quaternion.identity);
    }
}

