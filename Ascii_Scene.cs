using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asciiquarium;

public class Ascii_Scene
{
	public char[][] Scene;

	public Ascii_Scene()
	{
		Restart();
	}

	public void Clear()
	{
		for (int i = 0; i < Scene.Length; i++)
			Scene[i] = Enumerable.Repeat(' ', Console.WindowWidth).ToArray();
	}

	public void Restart()
	{
		Scene = new char[Console.WindowHeight][];

		Clear();
	}

	public static void Print_Information()
	{
		Console.WriteLine($"Dimensions: ({Console.WindowWidth}|{Console.WindowHeight})");
	}

	public void Show()
	{
		StringBuilder sb = new();

		foreach (char[] line in Scene)
			sb.AppendLine(string.Join("", line));

		Console.SetCursorPosition(0, 0);
		Console.WriteLine(sb.ToString());
		Console.SetCursorPosition(0, 0);

		Clear();
	}
}