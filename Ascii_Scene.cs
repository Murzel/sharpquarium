using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asciiquarium;

public class Ascii_Scene
{
	public char[][] Scene;
	public (int Width, int Height) Dimensions { get; private set; }

	public void Print_Information()
	{
		if (Scene.Length < 1)
		{
			Console.WriteLine($"Dimensions: Not init");
			return;
		}

		Console.WriteLine($"Dimensions: ({Dimensions.Width}|{Dimensions.Height})");
	}

	public Ascii_Scene()
	{
		Restart();
	}

	public Ascii_Scene(int width, int height)
	{
		Restart(width, height);
	}

	public void Clear()
	{
		for (int i = 0; i < Scene.Length; i++)
			Scene[i] = Enumerable.Repeat(' ', Dimensions.Width).ToArray();
	}

	public void Restart()
	{
		Restart(Console.WindowWidth, Console.WindowHeight);
	}

	public void Restart(int width, int height)
	{
		Dimensions = (width, height);
		Scene = new char[Dimensions.Height][];
		Clear();
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