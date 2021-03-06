using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Snake : MonoBehaviour {

    private enum Direction {
        Left,
        Right,
        Up,
        Down
    }
    
    private enum State {
        Alive,
        Dead,
        Win
    }

    private State state;
    private Vector2Int gridPosition;
    private Direction gridMoveDirection;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }
    private void Awake() {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimerMax = .05f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Direction.Right;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodySize = 0;

        snakeBodyPartList = new List<SnakeBodyPart>();
        state = State.Alive;
    }

    private void Update() { 
        switch (state)
        {
            case State.Alive:
                HandleInput();
                HandleGridMovement();
                break;
            // Scene back to main goes here
            case State.Dead:
                SceneManager.LoadScene("BattleGhost");
                break;
            // Scene change
            case State.Win:
                SceneManager.LoadScene("DemoStart");
                break;
        }

    }

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (gridMoveDirection != Direction.Down) {
                gridMoveDirection = Direction.Up;
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (gridMoveDirection != Direction.Up) {
                gridMoveDirection = Direction.Down;
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (gridMoveDirection != Direction.Right) {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (gridMoveDirection != Direction.Left) {
                gridMoveDirection = Direction.Right;
            }
        }
    }

    private void HandleGridMovement() {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax) {
            gridMoveTimer -= gridMoveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0) {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection) {
                default:
                case Direction.Right:
                    gridMoveDirectionVector = new Vector2Int(+1, 0);
                    break;
                case Direction.Left:
                    gridMoveDirectionVector = new Vector2Int(-1, 0);
                    break;
                case Direction.Up:
                    gridMoveDirectionVector = new Vector2Int(0, +1);
                    break;
                case Direction.Down:
                    gridMoveDirectionVector = new Vector2Int(0, -1);
                    break;
            }
            
            gridPosition += gridMoveDirectionVector;

            gridPosition = levelGrid.ValidateGridPosition(gridPosition);

            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood) {
                // snake ate food, grow body
                snakeBodySize++;
                CreateSnakeBody();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1) {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyParts();
            
            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition)
                {
                    // Game Over section
                    CMDebug.TextPopup("DEAD!", transform.position);
                    state = State.Dead;

                }
            }

            if (GameHandler.GetScore() == 1000)
            {
                // Win Section
                CMDebug.TextPopup("WIN!", transform.position);
                state = State.Win;
            }
            
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            // This is the direction that the sprite is facing - or + 90 to GetAngleFromVector function;
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) );

        }
    }

    private void CreateSnakeBody() {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    private void UpdateSnakeBodyParts() {
        for (int i = 0; i < snakeBodyPartList.Count; i++) {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }
    private float GetAngleFromVector(Vector2Int dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition() {
        return gridPosition;
    }

    // Return the full list of positions occupied by the snake: Head + Body
    public List<Vector2Int> GetFullSnakeGridPositionList() {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList) {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }

    private class SnakeBodyPart {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex) {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            // This is how you change position earlier of the sprite but I didn't really know how to face it correctly so I did random and thought it was funny might want to implement
            //snakeBodyGameObject.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition) {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection()) {
                default:
                case Direction.Up: // Currently going Up
                    angle = 0;
                    switch (snakeMovePosition.getPreviousDirection()) {
                        default:
                            angle = 0;
                            break;
                        case Direction.Left: // Previously going Left
                            angle = 0 + 45;
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Right: // Previously going Right
                            angle = 0 - 45;
                            break;
                    }
                    break;
                case Direction.Down: // Currently going Down
                    angle = 180;
                    switch (snakeMovePosition.getPreviousDirection()) {
                        default:
                            angle = 180;
                            break;
                        case Direction.Left: // Previously going Left
                            angle = 180 + 45;
                            break;
                        case Direction.Right: // Previously going Right
                            angle = 180 - 45;
                            break;
                    }
                    break;
                case Direction.Left: // Currently going left
                    angle = -90;
                    switch (snakeMovePosition.getPreviousDirection()) {
                        default:
                            angle = -90;
                            break;
                        case Direction.Down: // Previously going Down
                            angle = -45;
                            break;
                        case Direction.Up: // Previously going Up
                            angle = 45;
                            break;
                    }
                    break;
                case Direction.Right: // Currently going right
                    angle = 90;
                    switch (snakeMovePosition.getPreviousDirection()) {
                        default:
                            angle = 90;
                            break;
                        case Direction.Down: // Previously going Down
                            angle = 45;
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Up: // Previously going Up
                            angle = -45;
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                    }
                    break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }
         
        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
    }

    // Handles one move position from the snake

    private class SnakeMovePosition {

        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction) {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition() {
            return gridPosition;
        }

        public Direction GetDirection() {
            return direction;
        }

        public Direction getPreviousDirection() {
            if (previousSnakeMovePosition == null) {
                return Direction.Right;
            } else { return previousSnakeMovePosition.direction; }

        }
    }
}
