using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static int gridHeight = 10;
    public static int gridLength = 10;
    public bool[,] gridBlocks = new bool[gridLength, gridHeight];
    //public Block currBlock;
    public GameObject tile;
    public GameObject blockSprite;
    public BlockManager blockManager;
    public int currTurn = 0;
    GameObject[,] blockSprites = new GameObject[gridLength, gridHeight];
    //public List<List<int[]>> possibleBlocks = new List<List<int[]>>();
    void Awake(){
        //init tiles
        createGrid();
        BlockManager.initPossibleBlocks();
        blockManager.setNewBlock();
    }

    public void createGrid(){
        //init tiles
        for (int i = 0; i < gridLength; i++){
            for (int j = 0; j < gridHeight; j++){
                Tile currTlie = Instantiate(tile).GetComponent<Tile>();
                currTlie.init(i, j, this);
                //UnityEngine.Object.Instantiate(tile);
            }
        }
    }

    public void placeBlock(int xCoor, int yCoor){
        //check if block can be placed
        bool works = true;
        for (int i = 0; i < blockManager.currBlock.GetLength(0); i++){

            //remember to check getlength
            int newX = blockManager.currBlock[i, 0] + xCoor;
            int newY = blockManager.currBlock[i, 1] + yCoor;
            if (newX >= gridLength || newY >= gridHeight || newX < 0 || newY < 0){
                Debug.Log("out of bounds");
                works = false;
                break;
            }
            if (gridBlocks[newX, newY]){
                Debug.Log("overlaps");
                works = false;
                break;
            }
        }
        if (works){
            for (int i = 0; i < blockManager.currBlock.GetLength(0); i++){
                //remember to check getlength
                int newX = blockManager.currBlock[i, 0] + xCoor;
                int newY = blockManager.currBlock[i, 1] + yCoor;
                gridBlocks[newX, newY] = true;
                GameObject newBlock = Instantiate(blockSprite);
                newBlock.transform.position = new Vector3(newX, newY, 1);
                blockSprites[newX, newY] = newBlock;
            }
            blockPlaced();
        }
    }

    public bool checkPlacement(int xCoor, int yCoor, int[,] newBlock){
        bool works = true;
        for (int i = 0; i < newBlock.GetLength(0); i++){

            //remember to check getlength
            int newX = newBlock[i, 0] + xCoor;
            int newY = newBlock[i, 1] + yCoor;
            if (newX >= gridLength || newY >= gridHeight || newX < 0 || newY < 0){
                works = false;
                break;
            }
            if (gridBlocks[newX, newY]){
                works = false;
                break;
            }
        }
        return works;
    }

    public void blockPlaced(){
        //get new block, 
        blockManager.setNewBlock();
        checkGrid();

        if (checkIfLost()){
            //LOST
            
            Debug.Log("LOST! RESIGN");
        }
        currTurn++;
        Debug.Log("turn: " + currTurn);
        //check if lost
        //!!!!

    }
    public void checkGrid(){
        //vertical
        for (int i = 0; i < gridLength; i++){
            bool currLine = true;
            for (int j = 0; j < gridHeight; j++){
                if (!gridBlocks[i, j]){
                    currLine = false;
                }
            }
            if (currLine){
                removeLine(i, true);
            }
        }

        //vertical
        for (int i = 0; i < gridHeight; i++){
            bool currLine = true;
            for (int j = 0; j < gridLength; j++){
                if (!gridBlocks[j,i]){
                    currLine = false;
                }
            }
            if (currLine){
                removeLine(i, false);
            }
        }
        //check if lost
        /*bool lost = true;
        for (int i = 0; i < gridLength; i++){
            for (int j = 0; j < gridHeight; j++){
                if (checkPlacement(i, j, currBlock.blockCoors)){
                    lost = false;
                }
            }
        }
        return lost;*/
    }
    public bool checkIfLost(){
        bool lost = true;
        for (int i = 0; i < gridLength; i++){
            for (int j = 0; j < gridHeight; j++){
                if (checkPlacement(i, j, blockManager.currBlock)){
                    lost = false;
                }
            }
        }
        return lost;
    }
    public bool checkIfLost(int[,] currBlockCoors){
        bool lost = true;
        for (int i = 0; i < gridLength; i++){
            for (int j = 0; j < gridHeight; j++){
                if (checkPlacement(i, j, currBlockCoors)){
                    lost = false;
                }
            }
        }
        return lost;
    }
    private void removeLine(int coor, bool isVertical){
        if (isVertical){
            //coor is x
            for (int i = 0; i < gridHeight; i++){
                gridBlocks[coor, i] = false;
                Destroy(blockSprites[coor, i]);
            }
        }
        else{
            //coor is y, horizontal
            for (int i = 0; i < gridHeight; i++){
                gridBlocks[i, coor] = false;
                Destroy(blockSprites[i, coor]);
            }
        }
    }
}
