using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Jam
{
    public class GridController : MonoBehaviour
    {

        private int numRows;
        private int numCols;
        private Tile[,] tiles;

        // Start is called before the first frame update

        void Awake()
        {
            SetupTilesArray();

            // Testing
            // ActivateTile(Random.Range(0, numRows), Random.Range(0, numCols)); 
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetupTilesArray()
        {
            Tile[] temp = GetComponentsInChildren<Tile>();

            numRows = temp.Max(tile => tile.row) + 1;
            numCols = temp.Max(tile => tile.col) + 1;
            tiles = new Tile[numRows, numCols];

            for (int iTile = 0; iTile < temp.Length; ++iTile)
            {
                tiles[temp[iTile].row, temp[iTile].col] = temp[iTile];
            }

            // Testing
            //foreach(Tile tile in tiles)
            //{
            //    Debug.Log("Pos: " + tile.row + " , " + tile.col);
            //}
        }

        //public void ActivateTile(int row, int col)
        //{
        //    tiles[row, col].ActivateTile();
        //}

        //public void DeactivateTile(int row, int col)
        //{
        //    tiles[row, col].DeactivateTile();
        //}
    }
}
