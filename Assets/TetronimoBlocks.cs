using UnityEngine;
using System.Collections;

/*
	[0]	[1]	[2]	[3]
[3] *	*	*	*
[2]	*	*	*	*
[1]	*	*	*	*
[0]	*	*	*	*
*/

public class TetronimoBlocks {
	
	public int[][] MakeO ()
	{
		return new int[][] {new int[]{0, 0}, 
							new int[]{0, 1}, 
							new int[]{1, 0}, 
							new int[]{1, 1}};
	}
	
	public int[][] MakeT ()
	{
		return new int[][] {new int[]{0, 1}, 
							new int[]{1, 0}, 
							new int[]{1, 1}, 
							new int[]{1, 2}};
	}
	
	public int[][] MakeL ()
	{
		return new int[][] {new int[]{0, 0}, 
							new int[]{0, 1}, 
							new int[]{1, 0}, 
							new int[]{2, 0}};
	}
	
	public int[][] MakeJ ()
	{
		return new int[][] {new int[]{0, 0}, 
							new int[]{0, 1}, 
							new int[]{1, 1}, 
							new int[]{2, 1}};
	}
	
	public int[][] MakeS ()
	{
		return new int[][] {new int[]{0, 0}, 
							new int[]{0, 1}, 
							new int[]{1, 1}, 
							new int[]{1, 2}};
	}
	
	public int[][] MakeZ ()
	{
		return new int[][] {new int[]{0, 1}, 
							new int[]{0, 2}, 
							new int[]{1, 0}, 
							new int[]{1, 1}};
	}
	
	public int[][] MakeI ()
	{
		return new int[][] {new int[]{0, 1}, 
							new int[]{1, 1}, 
							new int[]{2, 1}, 
							new int[]{3, 1}};
	}
	

	
}
