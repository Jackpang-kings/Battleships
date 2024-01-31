using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
namespace HelloWorld { 
class Program { 
    public static void Main(){ 
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
}    

    public static void SetupPhase(Player player){
        Console.WriteLine();
        Console.WriteLine();
        Console.Write($"{player.Name}:\n");
        int i = 0;
        while (i<5){
            string ch;
            Console.WriteLine($"1)Enter Ship |{i+1}| 2)Displayboard");
            ch = Console.ReadLine()!;
            if (ch=="1"){
                
                Console.WriteLine($"Length:{player.Gboard.Ships[i+1].Length}");
                Console.WriteLine("Hint: The x, y cooridinates are for the head of the ship");
                Console.WriteLine("Hint: Direction 1:Vertical 2:Horizontal");
                Console.Write($"Enter x,y,d:");
                string xyd = Console.ReadLine()!;
                if (ValidShipFormat(i,player,xyd)=="Correct command"){
                    //PlaceShip
                    player.Gboard.PlaceShip(i,xyd,true);
                    i++;
                }
            }else if (ch=="2"){
                Console.WriteLine(DisplayBoard(player.Gboard.Board));
            }else{
                Console.WriteLine("Wrong Input");
            }
        }
        Console.WriteLine(DisplayBoard(player.Gboard.Board));
    }
    
    public static void ShPhase(Player sh, Player nsh){
        Console.WriteLine($"{sh.Name} Shooting -> {nsh.Name}");
        // x,y is the coordinate of the gameboard
        Console.Write($"Enter x,y:");
        string xy = CheckInput4ShFormat(Console.ReadLine()!);
        bool result = false;
        if (xy!="Wrong command"){
            result=sh.Shoot(nsh,xy);
        }
        Console.WriteLine(DisplayBoard(nsh.Mboard));
        Console.WriteLine(DisplayOutcome(nsh,result));
        sh.CheckWin(sh,nsh);
    }  

    public static string DisplayOutcome(Player pl, bool result){
        string outcome;
        if (result){
            outcome = "Hitted\n";
        }else{
            outcome = "Nothing is Hitted\n";
        }
        outcome += pl.Gboard.DisplayStatus();
        return outcome;
    }

    public static string ValidShipFormat(int n,Player player,string xyd){
        // x is the coordinate of the gameboard, y is the coordinate of the gameboard, d is the direction of the ship  
        try{
            string[] commands = xyd.Split(",");
            int y;
            int count = 0;
            for(int i = 0;i<commands.Length-1;i++){
                if (int.TryParse(commands[i], out y)){
                    if (y<=9){
                        count++;
                    }
                }
            }
            if ((count == 2)&&(commands[2]=="1"||commands[2]=="2")&& player.Gboard.ValidShip(n,xyd)){
                return "Correct command";
            }else{
                return "Wrong command";
            }
        }catch{
            return "Wrong command";
        }
    }

    public static string CheckInput4ShFormat(string command){
        try{
            string[] commands = command.Split(",");
            int y;
            int count = 0;
            for(int i = 0;i<commands.Length;i++){
                if (int.TryParse(commands[i], out y)){
                    if (y<=9){
                        count++;
                    }
                }
            }
            if (count == 2){
                return command;
            }else{
                return "Wrong command";
            }
        }catch{
            return "Wrong command";
        }
    }

    public static void Test(){
        Console.WriteLine("Welcome to BattleShips(test mode)");
        Player pla1 = new Player("Player1");
        Player pla2 = new Player("Player2");   
        TestSetupPhase(pla1);
        TestSetupPhase(pla2);
        // Both players has the same ships location
        TestShPhase(pla1,pla2);
        ForceWin(pla2,pla1);
        if (pla1.Win == true){
            Console.WriteLine($"{pla1.Name} Won");
        }else if (pla2.Win == true){
            Console.WriteLine($"{pla2.Name} Won");
        }else{
            Console.WriteLine("No player won");
        }

        static void TestSetupPhase(Player pl){
            Console.Write($"{pl.Name}:\n");
            Console.WriteLine(ValidShipFormat(0,pl,"4,4,1"));
            // Correct command
            pl.Gboard.PlaceShip(0,"4,4,1",true);
            // ship in row 4, column 4, placed Vertically
            Console.WriteLine(ValidShipFormat(1,pl,"0,0,2"));
            // Correct command
            pl.Gboard.PlaceShip(1,"0,0,2",true);
            // ship in row 0, column 0, placed Horizontally
            Console.WriteLine(ValidShipFormat(2,pl,"0,9,2"));
            // Correct command
            pl.Gboard.PlaceShip(2,"0,9,2",true);
            // ship in row 9, column 0, placed Horizontally
            Console.WriteLine(ValidShipFormat(3,pl,"0,9,1"));
            // Wrong Command
            Console.WriteLine(ValidShipFormat(3,pl,"0,5,1"));
            // Correct command
            pl.Gboard.PlaceShip(3,"0,5,1",true);
            // ship in row 5, column 0, placed Vertically
            Console.WriteLine(ValidShipFormat(4,pl,"5,9,2"));
            // Correct command
            pl.Gboard.PlaceShip(4,"5,9,2",true);
            // ship in row 9, column 5, placed Horizontally
            Console.WriteLine(ValidShipFormat(4,pl,"5,9,2"));
            // Wrong Command
            Console.WriteLine(DisplayBoard(pl.Gboard.Board));
            //Displays the Board
        }

        static void TestShPhase(Player sh, Player nsh){
            //sh for shooter and nsh for nshooter
            Console.WriteLine($"{sh.Name} Shooting -> {nsh.Name}");
            //Player1.Shoot(Player2,x,y) Player2 is the player who is getting shot and Player1 is the one shooting, x,y is column, row
            //shoot row 1, column 2
            sh.Shoot(nsh,"1,2");
            //shoot row 9, column 0-3
            sh.Shoot(nsh,"0,9");
            sh.Shoot(nsh,"1,9");
            sh.Shoot(nsh,"2,9");
            sh.Shoot(nsh,"3,9");
            //shoot row 5, column 0-4
            sh.Shoot(nsh,"0,5");
            sh.Shoot(nsh,"1,5");
            sh.Shoot(nsh,"2,5");
            sh.Shoot(nsh,"3,5");
            sh.Shoot(nsh,"4,5");
            //Display the MaskedBoard of the player getting shot as the board getting checked is it
            Console.WriteLine(DisplayBoard(nsh.Mboard));
            Console.WriteLine($"{nsh.Name} Board:");
            //Display the actual board of the player getting shot
            Console.WriteLine(DisplayBoard(nsh.Gboard.Board));
            Console.WriteLine($"{nsh.Name} Ships:");
            //Display the board status of the player getting shot
            Console.WriteLine(nsh.Gboard.DisplayStatus());
            sh.CheckWin(sh,nsh);
        }
        
        static void ForceWin(Player sh,Player nsh){
            //sh for shooter and nsh for nshooter
            Console.WriteLine($"{sh.Name} Shooting -> {nsh.Name}");
            //Shoots all the ship Cheat code
            for (int i=0;i<10;i++){
                for (int j=0;j<10;j++){
                    if (nsh.Gboard.Board[i,j]==1){
                        sh.Shoot(nsh,$"{j},{i}");
                    }
                }
            }
            Console.WriteLine(DisplayBoard(nsh.Mboard));
            Console.WriteLine($"{nsh.Name} Board:");
            //Display the actual board of the player getting shot
            Console.WriteLine(DisplayBoard(nsh.Gboard.Board));
            Console.WriteLine($"{nsh.Name} Ships:");
            //Display the board status of the player getting shot
            Console.WriteLine(nsh.Gboard.DisplayStatus());
            sh.CheckWin(sh,nsh);
        }

}
    
    public static string DisplayBoard(int[,] board){
    int len = board.GetLength(0);
    string getboardAsString = "";
    getboardAsString +=Line(len);
    for (int x = -1;x<len;x++){
        getboardAsString += "|".PadRight(2);
        getboardAsString += $"{x}".PadRight(2);
    }
    getboardAsString+="\n";
    getboardAsString +=Line(len);
    for (int i = 0;i<len;i++){
        getboardAsString += "|".PadRight(2);
        getboardAsString += $"{i}".PadRight(2);  
        for (int j = 0;j<len;j++){
            getboardAsString += "|".PadRight(2);
            getboardAsString += $"{board[i,j]}".PadRight(2);  
        }   
        getboardAsString+="|\n"; 
        getboardAsString +=Line(len); 
    }
    return getboardAsString;
}
    
    public static string Line(int len){
    string line="";
    for (int k=0;k<len+1;k++){
        line = line + "+---";
    }
    line = line + "+\n";
    return line;
}

}  
}