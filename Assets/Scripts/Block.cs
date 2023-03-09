using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    //public List<int[]> blockCoors = new List<int[]>();
    public int[,] blockCoors;

    public static List<int[,]> possibleBlocks = new List<int[,]>();
    public static void initBlocks(){
        int[,] testCoor = {{0, 0}, {0, 1}};
        possibleBlocks.Add(testCoor);
        //possibleBlocks.Add();


    }
    public void init(int blockType){
        this.blockCoors = possibleBlocks[blockType];
    }
    public void init(){
        //gen randomly
        this.blockCoors = possibleBlocks[Random.Range(0, possibleBlocks.Count)];
    }

    public void applyToGrid(){

    }
}
