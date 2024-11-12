using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

namespace VirusT {
	class main {
		
		static string ptf = Directory.GetCurrentDirectory() + @"\rules\field.txt";
		static string pts = Directory.GetCurrentDirectory() + @"\rules\settings.txt";
		public static Hashtable Settings = new Hashtable();
		
		static void Main() {
			Console.CursorVisible = false;

			Console.BufferHeight = 5000;
			Console.BufferWidth = 5000;
			
			
			StreamReader sr = new StreamReader(pts);

			string line;
			while((line = sr.ReadLine()) != null){
				int i = line.Length - 1;
				while(line[i] != ' '){
					i--;
				}
				 Settings.Add(line[0], line.Substring(i));
			}
			sr.Close();

			int turn4i;
			double time;
			ConsoleColor clr4back;
			ConsoleColor clr4font;
			int theme;
			char chr4h;
			char chr4i;
			char chr4r;
			//инициализация параметров

			turn4i = Convert.ToInt32(Settings['1']);
			try{
				time = Convert.ToDouble( Settings['2'].ToString().Replace(',', '.'));
			}catch{
				time = Convert.ToDouble( Settings['2'].ToString().Replace('.', ','));
			}
			clr4back = (ConsoleColor)Convert.ToInt32( Settings['3']);
			clr4font = (ConsoleColor)Convert.ToInt32( Settings['4']);
			theme = Convert.ToInt32(Settings['5']);
			chr4h =  Settings['6'].ToString().Trim().Length == 0 ? ' ' : Convert.ToChar( Settings['6'].ToString().Trim());
			chr4i =  Settings['7'].ToString().Trim().Length == 0 ? ' ' : Convert.ToChar( Settings['7'].ToString().Trim());
			chr4r =  Settings['8'].ToString().Trim().Length == 0 ? ' ' : Convert.ToChar( Settings['8'].ToString().Trim());
			

			switch(theme){
			case 0:
				Console.BackgroundColor = clr4back;
				Console.ForegroundColor = clr4font;
				break;

			case 1:
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				break;
			case 2:
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				break;

			case 3:
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				break;
			case 4:
				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
				break;
			}
			
			
			
			string[] fld = File.ReadAllLines(ptf);
			Console.SetWindowSize(fld[0].Length + 1, fld.Length);
			
			
			List<string> fld2 = new List<string>(fld);
			int[][] fldi = new int[fld.Length][];
			for(int i = 0; i < fld.Length; i++){
				fldi[i] = new int[fld[i].Length];
				for(int j = 0; j < fld[i].Length; j++){
					if(fld[i][j] == '@'){
						fldi[i][j] = 1;
					}else{
						fldi[i][j] = 0;
					}
				}
			}
			foreach(var el in fld2) {
				Console.WriteLine(el);
			}
			Console.SetCursorPosition(0, 0);

			Thread.Sleep((int)(1000 * time));

			while(true){
				for(int i = 1; i < fld.Length - 1; i++)
				{
					for(int j = 1; j < fld[i].Length - 1; j++)
					{
						if(fld[i][j] == chr4h)
						{
							if(Check(fld, i, j, chr4i))
							{
								fld2[i] = fld2[i].Remove(j, 1).Insert(j, chr4i.ToString());
								fldi[i][j] = 1;
							};
						} else if(fld[i][j] == chr4i)
						{
							if(fldi[i][j] < turn4i)
							{
								fldi[i][j]++;
							}
							if(fldi[i][j] == 3)
							{
								fldi[i][j] = 0;
								fld2[i] = fld2[i].Remove(j, 1).Insert(j, chr4h.ToString());//сначала они будут просто ресаться а потом можно добавить иммунитет
							}
						}
					}
				}
				fld = fld2.ToArray();
				foreach(var ln in fld){
					Console.WriteLine(ln);
				}
				Console.SetCursorPosition(0, 0);
				Thread.Sleep((int)(1000 * time));
				
			}

		}

		private static bool Check(string[] field, int row, int column, char chr){
			
			if(field[row - 1][column] == chr){
				return true;
			}
			if(field[row][column - 1] == chr){
				return true;
			}
			if(field[row - 1][column - 1] == chr){
				return true;
			}
			if(field[row - 1][column + 1] == chr){
				return true;
			}
			if(field[row + 1][column] == chr){
				return true;
			}
			if(field[row + 1][column - 1] == chr){
				return true;
			}
			if(field[row + 1][column + 1] == chr){
				return true;
			}
			if(field[row][column + 1] == chr){
				return true;
			}
			return false;
		}
		
	}
}
