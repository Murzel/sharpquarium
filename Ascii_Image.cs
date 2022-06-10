using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asciiquarium;

public class Ascii_Image
{
	#region "Properties"
	/// <summary>
	/// Reconstructs the Image from <see cref="FText"/>, but with whitespaces for left offset
	/// </summary>
	public string Text
	{
		get => string.Join('\n', FText.Select(x => new string(' ', x.left) + x.text));
	}
	/// <summary>
	/// Ascii Image trimmed and splitted by lines with information about left offset
	/// </summary>
	public List<(int left, string text)> FText { get; }
	
	/// <summary>
	/// Image with higher prio will get rendered when colliding
	/// </summary>
	public int Priority { get; init; }

	/// <summary>
	/// Draw from this position
	/// </summary>
	public (int x, int y) Position { get; set; }

	/// <summary>
	/// Size of Image
	/// </summary>
	public (int width, int height) Size { get; }
	#endregion

	public Ascii_Image(string text, int priority)
	{
		#region "Arguments checking"
		if (text == null)
			throw new ArgumentNullException(nameof(text));

		if (text.Trim() == string.Empty)
			throw new ArgumentException("Value cannot be empty.", nameof(text));
		#endregion

		Priority = priority;
		var textAsLines = text.Split('\n');
		string cLine; // current line
		var leftOffset = -1;
		var fText = new List<(int left, string text)>();
		var mostLeft = -1;
		var mostRight = 0;

		for (int i = 0; i < textAsLines.Length; i++)
		{
			cLine = textAsLines[i];
			for (int j = 0; j < cLine.Length; j++)
				if (cLine[j] != ' ')
				{
					leftOffset = j;
					break;
				}

			cLine = cLine.Trim();

			fText.Add((leftOffset, cLine));

			if (mostLeft < 0 || leftOffset < mostLeft)
				mostLeft = leftOffset;

			int calc = leftOffset + cLine.Length;

			if (mostRight < calc)
				mostRight = calc;
			
		}

		FText = fText;
		Size = (mostRight - mostLeft, FText.Count);
	}

	public bool TryRender(int left, int top, ref char[][] scene)
	{
		bool res = false;
		int cLeft, cTop; // current

		for (int i = 0; i < FText.Count; i++)
		{
			var line = FText[i];

			cLeft = left + line.left;
			cTop = top + i;

			if (!(cTop < scene.Length))
				break;

			for (int j = 0; cLeft < scene[cTop].Length && j < line.text.Length; cLeft++, j++)
			{
				if (cLeft < 0)
					continue;

				scene[cTop][cLeft] = line.text[j];
				res = true;
			}
		}

		return res;
	}

	public void Print_Information()
	{
		Console.WriteLine($"Image:\n{Text}\n");
		Console.WriteLine($"Size: ({Size.width}|{Size.height})");
		Console.WriteLine($"Priority: {Priority}");
		Console.WriteLine($"Position: ({Position.x}|{Position.y})");
	}
}

