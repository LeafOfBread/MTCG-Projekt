// See https://aka.ms/new-console-template for more information
using SWE.Models;

Console.WriteLine("Hello, World!");

string Description = "This unit has a big dick";
string Name = "Blue Eyes White Dragon";

Card myCard = new SWE.Models.Card(Description, Name);


Console.WriteLine(myCard.Name, "\n", Name);