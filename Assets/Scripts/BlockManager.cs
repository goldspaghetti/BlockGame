using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public class BlockManager : MonoBehaviour
{
    //curr block, visualization of the block
    public static List<int[,]> possibleBlocks = new List<int[,]>();
    public int[,] currBlock;
    public GameObject blockSprite;
    private int xOffset = 12;
    private int yOffset = 5;
    private List<GameObject> visualBlocks = new List<GameObject>();
    public static void initPossibleBlocks(){
        int[,] squareBlock2 = {{0, 0}, {0, 1}, {1, 0}, {1, 1}};
        int[,] singleBlock = {{0, 0}};
        int[,] horizontal2 = {{0, 0}, {1, 0}};
        int[,] horizontal3 = {{0, 0}, {1, 0}, {2, 0}};
        int[,] horizontal4 = {{0, 0}, {1, 0}, {2, 0}, {3, 0}};
        int[,] vertical2 = {{0, 0}, {0, 1}};
        int[,] vertical3 = {{0, 0}, {0, 1}, {0, 2}};
        int[,] vertical4 = {{0, 0}, {0, 1}, {0, 2}, {0, 3}};
        int[,] leftUpsideDownL = {{0, 1}, {1, 1},{1, 0}};
        int[,] rightUpsideDownL = {{0, 0}, {0, 1}, {1, 1}};
        int[,] leftUpsideDownL2;
        int[,] rightUpsideDownL2;
        int[,] squareBlock3 = {{0, 0}, {1, 0}, {2, 0}, {0, 1}, {1, 1}, {2, 1}, {0, 2}, {1, 2}, {2, 2}};
        int[,] squareBlock4 = {{0, 0}, {1, 0}, {2, 0}, {3, 0}, {0, 1}, {1, 1}, {2, 1}, {3,1}, {0, 2}, {1, 2}, {2, 2}, {3, 2}, {0, 3}, {1, 3}, {2, 3}, {3, 3}};
        possibleBlocks.Add(squareBlock2);
        possibleBlocks.Add(singleBlock);
        possibleBlocks.Add(horizontal2);
        possibleBlocks.Add(horizontal3);
        possibleBlocks.Add(horizontal4);
        possibleBlocks.Add(vertical2);
        possibleBlocks.Add(vertical3);
        possibleBlocks.Add(vertical4);
        possibleBlocks.Add(leftUpsideDownL);
        possibleBlocks.Add(rightUpsideDownL);


        possibleBlocks.Add(squareBlock3);
        possibleBlocks.Add(squareBlock4);

    }
    public void removeVisual(){
        foreach(GameObject currBlock in visualBlocks){
            Destroy(currBlock);
        }
    }
    public void showNewVisual(){
        for (int i = 0; i < currBlock.GetLength(0); i++){
            GameObject currBlockSprite = Instantiate(blockSprite);
            currBlockSprite.transform.position = new Vector3(currBlock[i, 0] + xOffset, currBlock[i, 1] + yOffset, 1);
            visualBlocks.Add(currBlockSprite);
        }
    }
    public void setNewBlock(){
        removeVisual();
        currBlock = possibleBlocks[Random.Range(0, possibleBlocks.Count)];
        showNewVisual();
    }
}
