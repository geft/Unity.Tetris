using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tetronimo {
		
	public static int[][] MoveLeft (int[][] block, GameObject[,] cellGrid)
	{
		bool valid = true;
		int[][] temp = block;
		
		foreach (int[] c in temp)
		{
			if (c[1] == 0 || cellGrid[c[0], c[1]-1].GetComponent<MeshRenderer>().enabled)
				valid = false;
		}
		
		if (valid)
		{
			foreach (int[] c in block)
				c[1]--;
				
			block = temp;
		}
		
		return block;
	}
	
	public static int[][] MoveRight (int[][] block, GameObject[,] cellGrid)
	{
		bool valid = true;
		int[][] temp = block;
		
		foreach (int[] c in temp)
		{
			if (c[1] == 9 || cellGrid[c[0], c[1]+1].GetComponent<MeshRenderer>().enabled)
				valid = false;
		}
		
		if (valid)
		{
			foreach (int[] c in block)
				c[1]++;
				
			block = temp;
		}
	
		return block;
	}
	
	public static int[][] Rotate (int[][] block, GameObject[,] cellGrid)
	{
		bool horizontalI = false;
		int diff = 0;
		
		int[][] temp = RotateBlock(block, out horizontalI);
		
		if (horizontalI)
		{
			diff = temp[3][1] - 9;
			
			if (diff > 0)
			{
				foreach (int[] c in temp)
				{
					c[1] -= diff;
				}
			}
		}
		
		// avoid block collision
		foreach (int[] c in temp)
		{
			if (cellGrid[c[0], c[1]].GetComponent<MeshRenderer>().enabled)
				return block;
		}
		
		return temp;
	}
	
	public static int[][] Fall (int[][] block, GameObject[,] cellGrid)
	{
		bool valid = true;
		
		int[][] temp = block;
		
		while (valid)
		{
			foreach (int[] c in temp)
			{
				if (c[0] == 0 || cellGrid[c[0]-1, c[1]].GetComponent<MeshRenderer>().enabled)
					valid = false;
			}
			
			if (valid)
			{
				foreach (int[] c in block)
					c[0]--;
			}
		}
	
		return block;
	}
	
	public int[][] Generate (int num)
	{
		TetronimoBlocks tetronimoBlocks = new TetronimoBlocks();
		int[][] blockTransform = new int[4][];
	
		switch (num)
		{
			case 0:
				blockTransform = tetronimoBlocks.MakeL();
				break;
			case 1:
				blockTransform = tetronimoBlocks.MakeJ();
				break;
			case 2:
				blockTransform = tetronimoBlocks.MakeS();
				break;
			case 3:
				blockTransform = tetronimoBlocks.MakeZ();
				break;
			case 4:
				blockTransform = tetronimoBlocks.MakeI();
				break;
			case 5:
				blockTransform = tetronimoBlocks.MakeT();
				break;
			case 6:
				blockTransform = tetronimoBlocks.MakeO();
				break;
			default:
				break;
		}
		
		return blockTransform;
	}
	
	private static int[][] RotateBlock (int[][] block, out bool horizontalI)
	{
		bool isI = false;
		bool isVertical = false;
		
		isI = IsBlockI(block, out isVertical);
		
		horizontalI = isI & isVertical;
	
		if (isI)
		{
			if (isVertical)
			{
				return new int[][]{	new int[] {block[0][0], block[0][1]},
									new int[] {block[0][0], block[0][1] + 1},
									new int[] {block[0][0], block[0][1] + 2},
									new int[] {block[0][0], block[0][1] + 3},
									};
			}
			
			else
			{
				return new int[][]{	new int[] {block[0][0], block[0][1]},
									new int[] {block[0][0] + 1, block[0][1]},
									new int[] {block[0][0] + 2, block[0][1]},
									new int[] {block[0][0] + 3, block[0][1]},
									};
			}
		}
		
		else
		{
			int[] center = FindBlockCenter(block);
			int[][] temp = block;
		
			foreach (int[] c in temp)
			{
				if (c[0] < center[0])
				{
					c[0] = center[0] + center[1] - c[1];
					c[1] = center[1] - 1;
				}
				
				else if (c[0] > center[0])
				{
					c[0] = center[0] + center[1] - c[1];
					c[1] = center[1] + 1;
				}
				
				else if (c[1] < center[1])
				{
					c[0]++;
					c[1]++;
				}
				
				else if (c[1] > center[1])
				{
					c[0]--;
					c[1]--;
				}
			}			
			
			foreach (int[] c in temp)
			{
				if (c[1] < 0)
				{
					foreach (int[] d in temp)
						d[1]++;
				}
				
				if (c[1] > 9)
				{
					foreach (int[] d in temp)
						d[1]--;
				}
			}
																																										
			return temp;
		}
	}
	
	private static int[] FindBlockCenter (int[][] block)
	{
		int maxX = block[0][0];
		int minX = block[0][0];
		int maxY = block[0][1];
		int minY = block[0][1];
		
		foreach (int[] c in block)
		{
			if (c[0] > maxX)
				maxX = c[0];
				
			if (c[0] < minX)
				minX = c[0];
				
			if (c[1] > maxY)
				maxY = c[1];
			
			if (c[1] < minY)
				minY = c[1];
		}
		
		int centerX = Mathf.CeilToInt((maxX + minX) / 2);
		int centerY = Mathf.CeilToInt((maxY + minY) / 2);
		
		return new int[]{centerX, centerY};
	}
	
	private static bool IsBlockI (int[][] block, out bool isVertical)
	{
		bool isI = false;
		isVertical = false;

		if (block[0][1] == block[1][1] &&
		    block[0][1] == block[2][1] &&
		    block[0][1] == block[3][1])
		{
			isI = true;
			isVertical = true;
		}
		
		else if (block[0][0] == block[1][0] &&
		         block[0][0] == block[2][0] &&
		         block[0][0] == block[3][0])
		{
			isI = true;
		}
		
		return isI;
	}
}
