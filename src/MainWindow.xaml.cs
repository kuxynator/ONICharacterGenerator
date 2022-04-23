using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Kux.ONICharacterGenerator;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void GenerateButton_Click(object sender, RoutedEventArgs e) {
		var names=NamesNextBox.Text
			.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
			.Select(s=>s.Trim(' ','\t','\r','\n'))
			.Where(s=>!string.IsNullOrWhiteSpace(s))
			.ToArray();
		var itemTemplate = TemplateTextBox.Text.Trim();
		var output=new StringBuilder();
		output.AppendLine("{");
		for (int i = 0; i < names.Length; i++) {
			var name = names[i];
			var gender = i % 2 == 0 ? "Male" : "Female";
			var s = GenerateItem(itemTemplate, i+1, name, gender);
			output.AppendLine(s);
		}
		output.AppendLine("}");
		OutputTextBox.Text = output.ToString();
	}

	private string GenerateItem(string itemTemplate, int count, string name, string gender) {

		return itemTemplate
			.Replace("$COUNT$", $"{count}")
			.Replace("$NAME$", name)
			.Replace("$GENDER$", gender)
			.Replace("$BODY$", $"{Random.Body(gender)}")
			.Replace("$EYES$", $"{Random.Eyes(gender)}")
			.Replace("$HAIR$", $"{Random.Hair(gender)}")
			.Replace("$HEADSHAPE$", $"{Random.HeadShape(gender)}")
			.Replace("$STRESSTRAIT$", $"{Random.StressTrait()}")
			.Replace("$JOYTRAIT$", $"{Random.JoyTrait()}")
			;
	}

	public static class Random {

		private static System.Random R = new System.Random();

		private static string[] StressTraits = { "Aggressive", "StressVomiter", "UglyCrier", "BingeEater" };
		private static string[] JoyTraits = { "BalloonArtist", "SparkleStreaker", "StickerBomber", "SuperProductive" };

		private static Dictionary<string, int[]> HairDic = new Dictionary<string, int[]>() {
			{"Male"  , new int[] {1, 2, 3,    5,    7,    9,  11,     13,     15,     17, 18, 19,     21, 22, 23,     25,     27,     29,     31, 32}},
			{"Female", new int[] {1, 2, 3, 4, 5, 6,    8, 10,     12,     14,     16,     18,     20,     22,     24, 25, 26,     28, 29, 30,        33}},
		};

		private static Dictionary<string, int[]> EyesDic = new Dictionary<string, int[]>() {
			{"Male"  , new int[] {1, 2, 3, 5}},
			{"Female", new int[] {1, 2, 3, 4}},
		};

		public static int Body() {
			return R.Next(1, 4 + 1);
		}

		public static int Body(string gender) {
			return R.Next(1, 4 + 1);
		}

		public static int Eyes() {
			return R.Next(1, 5 + 1);
		}

		public static int Eyes(string gender) {
			var array = EyesDic[gender];
			return array[R.Next(0, array.Length)];
		}

		public static int Hair() {
			return R.Next(1, 33 + 1);
		}

		public static int Hair(string gender) {
			var array = HairDic[gender];
			return array[R.Next(0, array.Length)];
		}

		public static int HeadShape() {
			return R.Next(1, 4 + 1);
		}

		public static int HeadShape(string gender) {
			return R.Next(1, 4 + 1);
		}

		public static string StressTrait() {
			return StressTraits[R.Next(0, StressTraits.Length)];
		}

		public static string JoyTrait() {
			return JoyTraits[R.Next(0, JoyTraits.Length)];
		}
	}
}
