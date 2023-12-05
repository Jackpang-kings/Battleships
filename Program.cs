using System;
using System.Xml.XPath;
namespace HelloWorld { 
class Program { 
static void Main(){ 
    bool resume = true;
    string opt;
    while(resume == true){
        Console.WriteLine("1)Game mode 2)Test mode 3)Quit");
        opt = Console.ReadLine()!;
        if (opt == "1"){
            Game();
        }else if (opt == "2"){
            Test();
        }else{
            resume = false;
        }
    }
} 
public static void Game(){
    Console.WriteLine("Welcome to BattleShips");
    Console.Write("Enter name of player 1:");
    Player pla1 = new Player(Console.ReadLine()!);
    Console.Write("Enter name of player 2:");
    Player pla2 = new Player(Console.ReadLine()!);
    SetupPhase(pla1);
    SetupPhase(pla2);
    while(pla1.Win == false && pla2.Win == false){
        ShPhase(pla1,pla2);
        ShPhase(pla2,pla1);
    }
    if (pla1.Win == true){
        Console.WriteLine($"{pla1.Name} Won");
    }else{
        Console.WriteLine($"{pla2.Name} Won");
    }
    static void SetupPhase(Player player){
        Console.WriteLine();
        Console.WriteLine();
        Console.Write($"{player.Name}:\n");
        int i = 0;
        while (i<5){
            string ch;
            bool valid;
            Console.WriteLine($"1)Enter Ship |{i+1}| 2)Displayboard");
            ch = Console.ReadLine()!;
            if (ch=="1"){
                valid = player.Gboard.Place(i);
                if (!valid){
                    Console.WriteLine("Ship not Valid");
                    DisplayBoard(player.Gboard.Board);
                }else{
                    Console.WriteLine("Ship Valid");
                    i++;
                }
            }else if (ch=="2"){
                DisplayBoard(player.Gboard.Board);
            }else{
                Console.WriteLine("Wrong Input");
            }
        }
        DisplayBoard(player.Gboard.Board);
    }
    static void ShPhase(Player sh, Player nsh){
        Console.WriteLine($"{sh.Name} Shooting -> {nsh.Name}");
        // x is the coordinate of the gameboard
        Console.Write($"Enter x:");
        int x = CheckInput(Console.ReadLine()!);
        // y is the coordinate of the gameboard
        Console.Write($"Enter y:");
        int y = CheckInput(Console.ReadLine()!);
        bool result;
        result=sh.Shoot(nsh,x,y);
        DisplayBoard(nsh.Mboard);
        DisplayOutcome(nsh,result);
        sh.CheckWin(sh,nsh);
    }  
    static void DisplayOutcome(Player pl, bool result){
        if (result){
            Console.WriteLine("Hitted");
        }else{
            Console.WriteLine("Nothing is hit");
        }
        pl.Gboard.DisplayStatus();
    }
}    
public static void Test(){
    Console.WriteLine("Welcome to BattleShips(test mode)");
    Player pla1 = new Player("Jack");
    Player pla2 = new Player("Don");   
    TestSetupPhase(pla1);
    TestSetupPhase(pla2);
    TestShPhase(pla1,pla2);
    ForceWin(pla2,pla1);
    if (pla1.Win == true){
        Console.WriteLine($"{pla1.Name} Won");
    }else{
        Console.WriteLine($"{pla2.Name} Won");
    }
    static void TestSetupPhase(Player pl){
        Console.Write($"{pl.Name}:\n");
        pl.Gboard.ValidShip(0,4,4,"1");
        //return Ship Valid, ship in row 4, column 4, placed Vertically
        pl.Gboard.ValidShip(1,0,0,"2");
        //return Ship Valid, ship in row 0, column 0, placed Horizontally
        pl.Gboard.ValidShip(2,0,9,"2");
        //return Ship Valid, ship in row 9, column 0, placed Horizontally
        pl.Gboard.ValidShip(3,0,9,"1");
        //return Ship not Valid
        pl.Gboard.ValidShip(3,0,5,"1");
        //return Ship Valid, ship in row 5, column 0, placed Vertically
        pl.Gboard.ValidShip(4,5,9,"2");
        //return Ship Valid, ship in row 9, column 5, placed Horizontally
        pl.Gboard.ValidShip(4,5,9,"2");
        //return Ship not Valid
        DisplayBoard(pl.Gboard.Board);
        //Displays the Board
    }
    static void TestShPhase(Player sh, Player nsh){
        //sh for shooter and nsh for nshooter
        Console.WriteLine($"{sh.Name} Shooting -> {nsh.Name}");
        //Player1.Shoot(Player2,x,y) Player2 is the player who is getting shot and Player1 is the one shooting, x,y is column, row
        //I know it should be row, column for int[row,column], but don't have to effort to do that
        //shoot row 1, column 2 and row 9, column 0-3
        sh.Shoot(nsh,1,2);
        sh.Shoot(nsh,0,9);
        sh.Shoot(nsh,1,9);
        sh.Shoot(nsh,2,9);
        sh.Shoot(nsh,3,9);
        //Display the MaskedBoard of the player getting shot as the board getting checked is it
        DisplayBoard(nsh.Mboard);
        Console.WriteLine($"{nsh.Name} Board:");
        //Display the actual board of the player getting shot
        DisplayBoard(nsh.Gboard.Board);
        Console.WriteLine($"{nsh.Name} Ships:");
        //Display the board status of the player getting shot
        nsh.Gboard.DisplayStatus();
        sh.CheckWin(sh,nsh);
    }
    static void ForceWin(Player sh,Player nsh){
        //sh for shooter and nsh for nshooter
        Console.WriteLine($"{sh.Name} Shooting -> {nsh.Name}");
        //Shoots all the ship Cheat code
        for (int i=0;i<10;i++){
            for (int j=0;j<10;j++){
                if (nsh.Gboard.Board[i,j]==1){
                    sh.Shoot(nsh,j,i);
                }
            }
        }
        DisplayBoard(nsh.Mboard);
        Console.WriteLine($"{nsh.Name} Board:");
        //Display the actual board of the player getting shot
        DisplayBoard(nsh.Gboard.Board);
        Console.WriteLine($"{nsh.Name} Ships:");
        //Display the board status of the player getting shot
        nsh.Gboard.DisplayStatus();
        sh.CheckWin(sh,nsh);
    }
}
public static int CheckInput(string x){
        bool success = false;
        int y = 0;
        while (success == false){
            if (int.TryParse(x, out y) == true){
                if (y<=9){
                    success = true;
                }
            }else{
                Console.WriteLine("Wrong input, Enter again");
                x = Console.ReadLine()!;
            }
        }
        return y;
}
public static void DisplayBoard(int[,] board){
    int len = board.GetLength(0);
    line(len);
    for (int x = -1;x<len;x++){
        Console.Write("|".PadRight(2));
        Console.Write($"{x}".PadRight(2));
    }
    Console.Write("|\n");
    line(len);
    for (int i = 0;i<len;i++){
        Console.Write("|".PadRight(2));
        Console.Write($"{i}".PadRight(2));    
        for (int j = 0;j<len;j++){
            Console.Write("|".PadRight(2));
            Console.Write($"{board[i,j]}".PadRight(2));
        }   
        Console.Write("|\n"); 
        line(len);     
    }
}
public static void line(int len){
    for (int k=0;k<len+1;k++){
        Console.Write("+---");
    }
    Console.Write("+");
    Console.Write("\n");
}
}  
}