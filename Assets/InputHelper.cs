using UnityEngine;
using System.Collections;

public class InputHelper : MonoBehaviour {
	
	private int _timer;
	private static bool _ready;
	
	void Start()
	{
		_timer = 0;
		_ready = true;
	}
	
	void Update()
	{
		if (!_ready)
		{
			if (_timer == 10)
			{
				_ready = true;
				_timer = 0;
			}
			
			_timer++;
		}
	}
	
	public static int[][] CheckInput(int[][] currBlock, GameObject[,] cellGrid)
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			if (_ready)
			{
				currBlock = Tetronimo.Rotate(currBlock, cellGrid);
				_ready = false;
			}
		}
		
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			if (_ready)
			{
				currBlock = Tetronimo.Fall(currBlock, cellGrid);
				_ready = false;
			}
		}
		
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (_ready)
			{
				currBlock = Tetronimo.MoveLeft(currBlock, cellGrid);
				_ready = false;
			}
		}
		
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			if (_ready)
			{
				currBlock = Tetronimo.MoveRight(currBlock, cellGrid);
				_ready = false;
			}
		}
		
		return currBlock;
	}
}
