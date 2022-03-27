using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    private Vector2 direction = Vector2.right;

    [SerializeField]
    private GameObject snakeSegmentPrefab;

    private List<GameObject> snakeSegments;

    //change this shit
    [SerializeField]
    private bool enemy = false;
    //
    private void Start() {
        snakeSegments = new List<GameObject>();
        snakeSegments.Add(this.gameObject);
    }

    private void Update() {
        Control();
    }

    private void FixedUpdate() {

        for(int i = snakeSegments.Count - 1; i > 0; i--) {
            snakeSegments[i].gameObject.transform.position = snakeSegments[i - 1].gameObject.transform.position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }

    private void Control() {
        if (enemy == false) {
            if (Input.GetKeyDown(KeyCode.W)) {
                direction = Vector2.up;
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                direction = Vector2.down;
            }
            if (Input.GetKeyDown(KeyCode.A)) {
                direction = Vector2.left;
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                direction = Vector2.right;
            }
            //change this shit
        } else {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                direction = Vector2.up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                direction = Vector2.down;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = Vector2.left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = Vector2.right;
            }
        }
        //
    }

    private void Grow() {
        GameObject segment = Instantiate(this.snakeSegmentPrefab);
        //think about this shit
        if(enemy) {
            segment.GetComponent<SpriteRenderer>().color = Color.red;
        }
        //
        segment.gameObject.transform.position = snakeSegments[snakeSegments.Count - 1].gameObject.transform.position;

        snakeSegments.Add(segment);
    }

    private void ResetGame() {
        for(int i = 1; i < snakeSegments.Count; i++) {
            Destroy(snakeSegments[i].gameObject);
        }
        snakeSegments.Clear();
        snakeSegments.Add(this.gameObject);
        //change and think about this shit
        if (enemy) {
            this.transform.position = Vector3.zero;
        }
        else {
            this.transform.position = new Vector3(10f, 10f, 0.0f);
        }
        //
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Food") {
            Grow();
        }
        //colliders not properly works with other snake
        //think and change this shit
        if(collision.tag == "Obstacle" || collision.tag == "Snake") {
            ResetGame();
        }
    }
}
