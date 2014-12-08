using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetrisGame : MonoBehaviour {

	public int FallSpeed { get; set; } // max is 30

	private int _cellGridRow;
	private int _cellGridCol;
	private int _timer;
	
	private bool _dropped;
	private bool _paused;
	
	private int[][] _currBlock;
	
	GameObject[,] cellGrid;

	void Start () 
	{
		FallSpeed = 5;
	
		_cellGridRow = 22;
		_cellGridCol = 10;
		_timer = 0;
		_dropped = false;
		_paused = false;
	
		GenerateCellGrid();
		GenerateBlock();
	}

	void Update () 
	{	
		if (_paused)
			return;
	
		if (_dropped)
		{
			RowClear();
			GenerateBlock();
			_dropped = false;
		}
			
		ProcessInput();
		
		CheckBottomCollision(_currBlock);
		
		FallBlock();
		
		CheckBottomCollision(_currBlock);
		CheckTopCollision(_currBlock);
	}
	
	private void GenerateCellGrid ()
	{	
		cellGrid = new GameObject[_cellGridRow, _cellGridCol];

		for (int row = 0; row < _cellGridRow; row++)
		{
			for (int col = 0; col < _cellGridCol; col++)
			{
				GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Plane);
				cell.name = "cell" + row.ToString() + "_" + col.ToString();
				cell.transform.parent = GameObject.Find("PlayField").transform;
				
				cell.transform.localPosition = new Vector3(-225f + (col * 50), -475f + (row * 50), -1f);
				cell.transform.rotation = Quaternion.AngleAxis(270, new Vector3(1, 0, 0));
				cell.transform.localScale = new Vector3(5, 5, 5);

				cell.renderer.material.color = Color.white;
				
				MeshRenderer mr = cell.GetComponent<MeshRenderer>();
				mr.castShadows = false;
				mr.receiveShadows = false;
				mr.enabled = false;
				
				cellGrid[row, col] = cell;
			}
		}
	}
	
	private void GenerateBlock ()
	{
		Tetronimo block = new Tetronimo();
	
		int[][] pos = block.Generate(Random.Range(0, 6));
		_currBlock = new int[4][];
		
		int count = 0;
		
		foreach (int[] c in pos)
		{
			int row = 18 + c[0];
			int col = 3 + c[1];
			
			_currBlock[count] = new int[]{row, col};
			count++;
		}
		
		RenderBlock(true, _currBlock);
	}
	
	private void FallBlock ()
	{	
		RenderBlock(false, _currBlock);
	
		if (_timer == 31 - FallSpeed)
		{
			if (!_dropped)
			{
				foreach (int[] c in _currBlock)
					c[0]--;
			}
			
			_timer = 0;
		}

		_timer++;
	}
	
	private void ProcessInput()
	{
		RenderBlock(false, _currBlock);
		_currBlock = InputHelper.CheckInput(_currBlock, cellGrid);
		RenderBlock(true, _currBlock);
	}
	
	private void RenderGrid(int row, int col, bool enable)
	{
		if (row < 20)
			cellGrid[row, col].GetComponent<MeshRenderer>().enabled = enable;
	}
	
	private void RenderBlock (bool visible, int[][] block)
	{
		foreach (int[] c in block)
		{
			RenderGrid(c[0], c[1], visible);
		}
	}
	
	private void CheckBottomCollision (int[][] block)
	{
		bool isDropped = false;
	
		RenderBlock(false, block);
	
		foreach (int[] c in block)
		{
			if (c[0] == 0 || cellGrid[c[0]-1, c[1]].GetComponent<MeshRenderer>().enabled)
			{
				isDropped = true;
				break;
			}
		}
		
		if (isDropped)
			_dropped = true;
		else
			_dropped = false;
		
		RenderBlock(true, block);
	}
	
	private void CheckTopCollision (int[][] block)
	{
		foreach (int[] c in block)
		{
			if (c[0] == _cellGridRow - 2 && _dropped)
			{
				print("Game Over");
				_paused = true;
				break;
			}
		}
	}
	
	private void RowClear ()
	{
		int count = 0;

		for (int i = 0; i < _cellGridRow - 2; i++)
		{
			for (int j = 0; j < _cellGridCol; j++)
			{
				if (!cellGrid[i, j].GetComponent<MeshRenderer>().enabled)
					break;
					
				count++;
			}
			
			if (count == 10)
			{	
				for (int row = i; row < _cellGridRow - 2; row++)
				{
					for (int col = 0; col < _cellGridCol; col++)
						cellGrid[row, col].GetComponent<MeshRenderer>().enabled = cellGrid[row+1, col].GetComponent<MeshRenderer>().enabled;
				}
				
				i--;
			}

			count = 0;
		}
	}
}
