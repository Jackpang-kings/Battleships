using System;
namespace HelloWorld { 
class Program { 
static void Main(string[] args)
 { 
Console.WriteLine("Hello, World!"); 
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
} 
}
