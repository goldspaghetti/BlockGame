using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    private int xCoor;
    private int yCoor;
    GridManager gridManager;
    void Awake(){

    }
    public void init(int xCoor, int yCoor){
        this.xCoor = xCoor;
        this.yCoor = yCoor;
        transform.position = new Vector3(xCoor, yCoor, 1);
        gridManager = FindObjectOfType<GridManager>();
    }
    
    public void init(int xCoor, int yCoor, GridManager gridManager){
        this.xCoor = xCoor;
        this.yCoor = yCoor;
        transform.position = new Vector3(xCoor, yCoor, 1);
        this.gridManager = gridManager;
    }
    void OnMouseOver(){
        //Debug.Log("A");
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("placing block");
            gridManager.placeBlock(xCoor, yCoor);
            /*
            if (xCoor == 0 && yCoor == 0){
                Debug.Log("wtf");
            }
            Debug.Log("processing input");
            Debug.Log(xCoor + " " + yCoor);
            gridManager.placeBlock(xCoor, yCoor);*/
        }
    }
}
