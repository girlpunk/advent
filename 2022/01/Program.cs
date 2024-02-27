using System.IO;

var groups = new List<int>();

var input = await File.OpenText("input2.txt").ReadToEndAsync();

var elves = input.Split("\n\n").Select(e => e.Split('\n').Where(f => !string.IsNullOrWhiteSpace(f)).Select(f => int.Parse(f)).Sum()).OrderDescending();

Console.WriteLine(elves.First());

Console.WriteLine(elves.Take(3).Sum());
