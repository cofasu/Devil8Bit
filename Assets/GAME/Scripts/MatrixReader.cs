using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MatrixReader {

	

	public static int[,] GetLevelMatrix(string path)
	{
		string matrix = File.ReadAllText(Application.streamingAssetsPath + path);

		string[] temp = matrix.Split('{', '}');
		string matrixNoBracket = temp[1];
		string[] matrixCorchete = matrixNoBracket.Split(':');
		string[] matrixNoCorchete = matrixCorchete[1].Split('[', ']');

		List<string> matrixValues = new List<string>();
		for (int i = 0; i < matrixNoCorchete.Length; i++)
		{
			if (i == 0 || i % 2 != 0 || i == matrixNoCorchete.Length-1)
				continue;

			matrixValues.Add(matrixNoCorchete[i]);			
		}

		int[,] realMatrix = new int[11, 23];

		int column = 0;
		int row = 22;

		for (int matrixValue = 0; matrixValue < matrixValues.Count; matrixValue++)
		{
			string temporal = matrixValues[matrixValue];
			string[] temporalSplitted = temporal.Split(',',' ');
			for (int i = 0; i < temporalSplitted.Length; i++)
			{
				if(i % 2 == 0)
				{
					realMatrix[column, row] = int.Parse(temporalSplitted[i]);
					column++;
				}
			}
			column = 0;
			row--;
		}
		return realMatrix;
	}
}
