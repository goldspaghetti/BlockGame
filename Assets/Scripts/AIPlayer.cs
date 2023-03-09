using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public static int depth = 3;
    public GridManager gridManager;
    public BlockManager blockManager;
    public void calculateMove(){
        //curr block
        //curr board
        //possible blocks
        //int blocksRemaining;

        //generate possible moves

        Branch topBranch = new Branch(0, true, deepCloneBoard(gridManager.gridBlocks), blockManager.currBlock, gridManager);
        topBranch.calculatePoints();
        Debug.Log(topBranch.posX + " " + topBranch.posY);
        gridManager.placeBlock(topBranch.posX, topBranch.posY);
        /*int[,] currBlockCoors = blockManager.currBlock;
        List<int[]> possibleMoves = new List<int[]>();
        for (int i = 0; i < GridManager.gridHeight; i++){
            for (int j = 0; j < GridManager.gridHeight; j++){
                if (gridManager.checkPlacement(i, j, currBlockCoors)){
                    int[] currCoor = {i, j};
                    possibleMoves.Add(currCoor);
                }
            }
        }*/
    }
    public static bool[,] deepCloneBoard(bool[,] currBoard){
        bool[,] returnBoard = new bool[GridManager.gridLength, GridManager.gridHeight];
        for (int i = 0; i < GridManager.gridLength; i++){
            for (int j = 0; j < GridManager.gridHeight; j++){
                returnBoard[i,j] = currBoard[i, j];
            }
        }
        return returnBoard;
    }
}
//branches are created 
class Branch{
    List<Branch> subBrances;
    bool isDecision;
    float branchValue;
    float deathAmount;
    int currDepth;
    int[,] currBlock;
    bool[,] currBoard = new bool[GridManager.gridLength, GridManager.gridHeight];
    public int posX;
    public int posY;
    GridManager gridManager;
    Branch(int currDepth, bool isDecision, bool[,] currBoard, GridManager gridManager){
        this.currDepth = currDepth;
        this.isDecision = isDecision;
        this.currBoard = currBoard;
        this.gridManager = gridManager;
    }
    Branch(int currDepth, bool isDecision, bool[,] currBoard, int posX, int posY, GridManager gridManager){
        this.currDepth = currDepth;
        this.isDecision = isDecision;
        this.currBoard = currBoard;
        this.posX = posX;
        this.posY = posY;
        this.gridManager = gridManager;
    }
    public Branch(int currDepth, bool isDecision, bool[,] currBoard, int[,] currBlock, GridManager gridManager){
        this.currDepth = currDepth;
        this.isDecision = isDecision;
        this.currBoard = currBoard;
        this.currBlock = currBlock;
        this.gridManager = gridManager;
    }
    //have each move, each move creates a seperate branch

    //FIRST ONE IS LOSS PROBABILITY, SECOND INDEX IS BLOCKS REMAINING
    float[] createBlockBranch(){
        //currDepth++;
        //average of branches
        float[] returnValue = new float[2];
        for (int i = 0; i < BlockManager.possibleBlocks.Count; i++){
            Branch newBranch = new Branch(currDepth, true, AIPlayer.deepCloneBoard(currBoard), BlockManager.possibleBlocks[i], gridManager);
            float[] newBranchPoints = newBranch.calculatePoints();
            returnValue[0] += newBranchPoints[0];
            returnValue[1] += newBranchPoints[1];
        }
        //GET BEST MOVE
        returnValue[0] /=  (float)BlockManager.possibleBlocks.Count;
        returnValue[1] /= (float) BlockManager.possibleBlocks.Count;
        return returnValue;
    }
    float[] createDecisionBranch(){
        currDepth++;
        //if you die, add a die thing
        float[] returnValue = new float[2];
        if (gridManager.checkIfLost(currBlock)){
            returnValue[0] += 1;
            return returnValue;
        }
        //if got to wanted depth
        if (currDepth == AIPlayer.depth){
            //generate value
            int blockCount = 0;
            for (int i = 0; i < GridManager.gridLength; i++){
                for (int j = 0; j < GridManager.gridHeight; j++){
                    if (currBoard[i,j]){
                        blockCount++;
                    }
                }
            }
            returnValue[0] = 0;
            returnValue[1] = blockCount;
            
            return returnValue;
        }
        else{
            List<float[]> possibleReturnValues = new List<float[]>();
            List<Branch> branches = new List<Branch>();
            //generate all possible branches
            for (int i = 0; i < GridManager.gridLength; i++){
                for (int j = 0; j < GridManager.gridHeight; j++){
                    if (gridManager.checkPlacement(i, j, currBlock)){
                        //placement possible
                        //applying placement
                        for (int k = 0; k < currBlock.GetLength(0); k++){
                            //remember to check getlength
                            int newX = currBlock[k, 0] + i;
                            int newY = currBlock[k, 1] + j;
                            currBoard[newX, newY] = true;
                        }

                        //doing stuff
                        Branch newBranch = new Branch(currDepth, false, AIPlayer.deepCloneBoard(currBoard), i, j, gridManager);
                        possibleReturnValues.Add(newBranch.calculatePoints());
                        branches.Add(newBranch);

                        //possibleReturnValues.Add(newBranch.calculatePoints());
                    }
                }
            }
            //get best return value
            float[] currBestReturn = possibleReturnValues[0];
            int bestPosX = branches[0].posX;
            int bestPosY = branches[0].posY;
            for (int i = 0; i < possibleReturnValues.Count; i++){
                float[] currReturnValue = possibleReturnValues[i];
                int currPosX = branches[i].posX;
                int currPosY = branches[i].posY;
                
                //NORMAL!
                
                if (currReturnValue[0] < currBestReturn[0] || (currReturnValue[0] == currBestReturn[0] && currReturnValue[1] < currBestReturn[1])){
                    currBestReturn = currReturnValue;
                    bestPosX = branches[i].posX;
                    bestPosY = branches[i].posY;

                    if (currReturnValue[0] == currBestReturn[0] && currReturnValue[1] < currBestReturn[1]){
                        Debug.Log("using min blocks");
                    }
                }

                //USING MININNUM BLOCKS ONLY RIGHT NOW, CHANGE WHEN DONE
                /*if (currReturnValue[1] < currBestReturn[1]){
                    currBestReturn = currReturnValue;
                    bestPosX = branches[i].posX;
                    bestPosY = branches[i].posY;
                }*/

            }
            this.posX = bestPosX;
            this.posY = bestPosY;
            return currBestReturn;
        }
    }
    public float[] calculatePoints(){
        //amount of
        /*if (currDepth == AIPlayer.layers){
           return 3;
        }*/
        if (isDecision){
            //get average of  branches
            return createDecisionBranch();
        }
        else{
            //
            return createBlockBranch();
        }
    }
    bool checkIfValid(){
        return true;
    }

}
