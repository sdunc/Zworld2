using Zworld;

GameWorld world = new GameWorld();

string command = string.Empty; 

while ((_ = Console.ReadLine()) != "exit")
{
    world.NextTurn();
}