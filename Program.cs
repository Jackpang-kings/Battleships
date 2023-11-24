using System;
namespace HelloWorld { 
class Program { 
static void Main()
 { 
    Console.WriteLine("Welcome to BattleShips");
    Player player1 = new Player();
    Player player2 = new Player();
    player1.Name = "Jack";
    player2.Name = "Don";
    TestSetupPhrase(player1);
    TestSetupPhrase(player2);
    TestShPhrase(player1,player2);
    TestShPhrase(player2,player1);
} 
public static int CheckInput(string x){
    bool success = false;
    int y = 0;
    while (success == false){
        if (int.TryParse(x, out y) == true){
            success = true;
        }else {
            Console.WriteLine("Wrong input, Enter again");
            string a = Console.ReadLine();
            x = a;
        }
    }
    return y;
} 
public static void SetupPhrase(Player player){
    for (int i=0;i<5;i++){
        // x is the coordinate of the gameboard
        Console.Write($"Enter x:");
        int x = CheckInput(Console.ReadLine());
        // y is the coordinate of the gameboard
        Console.Write($"Enter y:");
        int y = CheckInput(Console.ReadLine());
        player.Gboard.PlaceShip(i,x,y,"2");
    }
    Console.Write($"{player.Name}:\n");
    Console.Write("x is Horizontal\n");
    Console.WriteLine("y is Veritcal\n");
    player.Gboard.DisplayBoard();
}

public static void ShPhrase(Player shooter, Player nshooter){
    Console.WriteLine($"{shooter.Name} Shooting -> {nshooter.Name}");
    // x is the coordinate of the gameboard
    Console.Write($"Enter x:");
    int x = CheckInput(Console.ReadLine());
    // y is the coordinate of the gameboard
    Console.Write($"Enter y:");
    int y = CheckInput(Console.ReadLine());
    shooter.Mboard[x,y] = 1;
    shooter.MaskedBoard(shooter.Mboard);
    DisplayOutcome(shooter.Shoot(nshooter,x,y));
    shooter.CheckWin(shooter,nshooter);
}
static void DisplayOutcome(bool result){
    if (result){
        Console.WriteLine("Hitted");
    }else{
        Console.WriteLine("Nothing is hit");
    }
}

public static void TestSetupPhrase(Player player){
    for (int i = 0;i<5;i++){
        player.Gboard.PlaceShip(i,i,i,"2");
    }
    Console.Write($"{player.Name}:\n");
    Console.Write("x is Horizontal\n");
    Console.WriteLine("y is Veritcal\n");
    player.Gboard.DisplayBoard();
}
public static void TestShPhrase(Player shooter, Player nshooter){
    int count = 0;
    Console.WriteLine($"{shooter.Name} Shooting -> {nshooter.Name}");
    for (int x=0;x<6;x++){
        for (int y=0;y<6;y++){
            shooter.Mboard[x,y] = 1;
            if (shooter.Shoot(nshooter,x,y)==true){
                count++;
            }
            shooter.CheckWin(shooter,nshooter);
        }
    }
    shooter.MaskedBoard(shooter.Mboard);
    Console.WriteLine($"{count}");
    Console.WriteLine($"{nshooter.Name} Board:");
    nshooter.Gboard.DisplayBoard();
    Console.WriteLine($"{nshooter.Name} Ships:");
    nshooter.Gboard.DisplayStatus();
}
}
}