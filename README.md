# Decomposition
## Class Ship
### Properties:
- int damaged
- int undamaged
- boolean sunk
- int length

### Initializer:
```
PUBLIC Damamged AS INT get{damaged}
PUBLIC Undamaged AS INT get{undamaged}
PUBLIC Sunk AS BOOLEAN get{sunk}
```
### Constructor:
```
PUBLIC PROCEDURE Ship (INT d = 0, INT l = 0)
	damaged = d
	undamaged = l
	length = l
	sunk = false
```
### Methods:
- Checksunk
```
CREATE FUNCTION Checksunk()
	IF damaged = undamaged THEN
		sunk = true
	ENDIF
	RETURN sunk
ENDFUNCTION
```

## Class Gameboard
### Properties:
- int[10,10] board
- Ship[] ships

### Constructor:
```
PUBLIC Gameboard
	DECLARE len AS INT = board.Length 
	FOR INT i = 0 TO len
		FOR INT j = 0 TO len
			board[i,j] = 0
		ENDFOR 
	ENDFOR
	ships[0].Length = 2
	ships[1].Length = 3
	ships[2].Length = 3
	ships[3].Length = 4
	ships[4].Length = 5
```
### Methods:
- Place
	- In: int n //Length of the ship
	- Out: int board
```
CREATE PROEDURE Place(INT n,int[10,10] board)
	//Enter x coordinates 0-9
	DECLARE x AS INT = Program.CheckInput(USERINPUT)
	//Enter y coordinates 0-9
	DECLARE y AS INT = Program.CheckInput(USERINPUT)
	board = PlaceShip(n,x,y,board)
ENDPROCEDURE
```
- PlaceDirection
	- In: None
	- Out: STRING d
```
CREATE FUNCTION PlaceDirection()
	DECLARE d AS STRING
	DECLARE success AS BOOLEAN = false
	WHILE success = false
		OUTPUT "1:Vertical 2:Horizontal"
		d = USERINPUT
		IF d = "1" OR d = "2" THEN
			success = true
			return d
		ELSE
			OUTPUT "Wrong Direction"
		ENDIF
	ENDWHILE
ENDFUNCTION
```
- PlaceShip
	- In: INT n, INT x, INT y
	- Out: board
```
CREATE FUNCTION PlaceShip(INT n, INT x, INT y, INT[10,10] board)
	DECLARE s AS Ship = ships[n]
	DECLARE len AS INT = s.Length
	WHILE success = false 
		IF len+y > 9 OR len+x > 9 OR ValidShip(n,x,y,board) = false THEN
			OUTPUT "CANNOT PUT SHIP THERE"
			OUTPUT "ENTER x,y again"
			Place(n,board)
		ELSE 
			IF PLaceDirection() = "1" THEN
				FOR INT i = 0 To len - 1
					board[x,y+i] = ship[i]
				ENDFOR
			ELSE 
				FOR INT i = 0 To len - 1
					board[x+i,y] = 1
				ENDFOR
			ENDIF
			success = true
			RETURN board
		ENDIF
	ENDWHILE
ENDFUNCTION
```
- ValidShip
	- In: Int x, Int y, Int n, int[10,10] board
	- Out: boolean valid
```
CREATE FUNCTION ValidShip(int n, int x, int y, int[10,10] board)
	DECLARE valid AS BOOLEAN = false
	DECLARE count AS INT = 0
	FOR INT i = 0 To n - 1
		IF board[x,y+i] != 1 OR board[x+1,y] != 1 THEN
			count++
		ENDIF
	ENDFOR
	IF count = n NEXT
		valid = true
	ENDIF
	RETURN valid
ENDFUNCTION
```
- DisplayBoard
```
CREATE PROCEDURE DisplayBoard(Gameboard board)
	DECLARE b AS INT[,] = Gameboard.board[n]
	DECLARE len AS INT = b.Length
	FOR INT i = 0 To len - 1
		FOR INT j = 0 To len - 1
			OUTPUT "b[i,j]".PadRight
		ENDFOR
	ENDFOR
ENDPROCEDURE
```
- DisplayStatus
```
CREATE PROCEDURE DisplayStatus(Gameboard board)
	FOR i = 0 TO 4
		OUTPUT "{board.ship[i].Damaged}"
		OUTPUT "{board.ship[i].Undamaged}"
		IF board.ship[i].Sunk = false THEN
			OUTPUT "ship {ship[i].Length} Not Sunk"
		ELSE 
			OUTPUT "ship {ship[i].Length} SUNKED"
		ENDIF
	ENDFOR
ENDPROCEDURE
```
- MaskedBoard
```
CREATE FUNCTION MaskedBoard(Gameboard board)
	DECLARE b AS INT[,] = Gameboard.board[n]
	DECLARE len AS INT = b.Length
	FOR INT i = 0 To len - 1
		FOR INT j = 0 To len - 1
			OUTPUT "b[i,j]".PadRight
		ENDFOR
	ENDFOR
ENDFUNCTION
```


## Class Player
### Properties#
- Gameboard gboard
- int[10,10] mboard
- string name
- boolean win

### Initializer:
```
PUBLIC Name AS STRING {get(name),set(name = value)}
PUBLIC Gboard AS Gameboard {get(gboard),set(gboard = value)}
```

### Constructor:
```
PUBLIC Player(string n = "player", w = false)
	name = n
	win = w
	DECLARE len AS INT = mboard.Length 
	FOR INT i = 0 TO len
		FOR INT j = 0 TO len
			mboard[i,j] = 0
		ENDFOR 
	ENDFOR
```

### Methods:
- Shoot
```
CREATE FUNCTION Shoot(player.Gboard AS nshooter, int x, int y)
	DECLARE outcome AS BOOLEAN = false
	IF nshooter.Gboard.board[x,y] = 1 THEN
		nshooter.Gboard.board[x,y] = nshooter.Gboard.board[x,y] - 2
		outcome = true
	ENDIF
	RETURN outcome
ENDFUNCTION
```
- CheckWin
```
CREATE FUNCTION CheckWin()
	DECLARE result AS BOOLEAN
		
	return result
ENDFUNCTION
```
## Class Program
### Methods
- Main()
```
DECLARE player1 AS Player
DECLARE player2 AS Player
OUTPUT "ENTER 1st Player name"
DECLARE player1.Name AS STRING = USERINPUT
OUTPUT "ENTER 2nd Player name"
DECLARE player2.Name AS STRING = USERINPUT
SetPhase(player1)
SetPhase(player2)

ShPhase(player1,player2)
ShPhase(player2,player1)
```
- SetupPhase
	- In: Player
	- Out: None
```
CREATE PROCEDURE SetPhase(player AS Player)
	OUTPUT "{player.Name} Setup your board:"
	FOR i = 0 TO 4
		Place(player.Gboard.ship[i].Length,player.Gboard.board)
		i++
	ENDFOR
ENDPROCEDURE
```

- ShootingPhase
	- In: player AS Player
	- Out: None
```
CREATE PROCEDURE ShPhase(shooter AS Player,nshooter AS Player)
	OUTPUT "{shooter.Name}"
	OUTPUT "ENTER the x coordinates to shoot"
	DECLARE x AS INT = CheckInput(USERINPUT)
	OUTPUT "ENTER the y coordinates to shoot"
	DECLARE y AS INT = CheckInput(USERINPUT)
	shooter.mboard[x,y] = 1
	DisplayOutcome(nshooter.Shoot(x,y))
	player.Gboard.Maskboard()
ENDPROCEDURE
```
- CheckInput 
	- In: None _Whatever needs to be converted into INT 
	- Out: INT y
```
CREATE FUNCTION CheckInput(STRING x)
	DECLARE success AS BOOLEAN = false
	WHILE success = false
		IF CONVERTTOINT(x, out y) = true THEN
			success = true
			RETURN y
		Else //ask again for input
			OUTPUT "Wrong INPUT, Enter again"
			x = USERINPUT
			CheckInput(x)
		ENDIF
	ENDWHILE
ENDFUNCTION
```
- DisplayOutcome
```
CREATE PROCEDURE DisplayOutcome(boolean result)
	IF result = true
		OUTPUT "Hitted"
	ELSE
		OUTPUT "Nothing is hit"
ENDPROCEDURE
```