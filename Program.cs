using System.Text;

namespace asciiquarium;

public static class Program
{
	private static int height = Console.WindowHeight;
	private static int width = Console.WindowWidth;

	public static void Main()
	{
		string castle_image =
        @"               T~~
               |
              /^\
             /   \
 _   _   _  /     \  _   _   _
[ ]_[ ]_[ ]/ _   _ \[ ]_[ ]_[ ]
|_=__-_ =_|_[ ]_[ ]_|_=-___-__|
 | _- =  | =_ = _    |= _=   |
 |= -[]  |- = _ =    |_-=_[] |
 | =_    |= - ___    | =_ =  |
 |=  []- |-  /| |\   |=_ =[] |
 |- =_   | =| | | |  |- = -  |
 |_______|__|_|_|_|__|_______|";

		Console.CursorVisible = false;
		Ascii_Scene scene = new();
		Ascii_Image img = new(castle_image, 100);
		Ascii_Image img2 = new(castle_image, 101);

		int i = 1 - img.Size.width, j = Console.WindowWidth - 1;



		for (;;)
		{
			EvalInput();
			CheckResize(ref scene);

			img.TryRender(width - img.Size.width - 2, height - img.Size.height - 1, ref scene.Scene);

			scene.Show();
			Thread.Sleep(50);
		}

		scene.Show();
		Thread.Sleep(50);
	}

	private static void CheckResize(ref Ascii_Scene scene)
	{
		if(WindowStateChanged())
		{
			Console.Clear();

			while (WindowStateChanged())
			{
				Thread.Sleep(200);
				Console.Clear();
			}
		}	
	}

	public static bool WindowStateChanged()
	{
		var state_changed = false;

		if (height != Console.WindowHeight)
		{
			height = Console.WindowHeight;
			state_changed = true;
		}

		if (width != Console.WindowWidth)
		{
			width = Console.WindowWidth;
			state_changed = true;
		}

		return state_changed;
	}

	public static void EvalInput()
	{
		if (Console.KeyAvailable)
		{
			ConsoleKeyInfo key = Console.ReadKey(true);

			switch (key.Key)
			{
				case ConsoleKey.Q:
					Console.Clear();
					Environment.Exit(0);
					break;
			}
		}
	}
}