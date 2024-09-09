# Side-Projects
Just random things I decided to write during my free time.
This started as a learning experience back when I was just getting started in software dev, so it's cool to see how far I've come.

## Tetfuza (C#)
- A CLI animated tetris clone
- Points and difficulty scale with level
- Z and X to rotate, <- and -> arrow keys to move

## Battleship (C#)
- Originally written in CPP (original code lost). Rewritten in C#
- The classic board game "Battleship" written as a console app
- Needs a rework, as writing this was my introduction to C#

## NumberGuesserConsoleGame (C#)
- CLI game where one player chooses a number and the other tries to guess
- Allows for computer and user to play either role (chooser or guesser)

## TextFileEncoder (C#)
- Some thought I had about shifting the values of each byte in a text file to obscure the original contents
- User specifies offset and new file is written with each byte shifted by that offset

## TestingGround (C#)
- Various little custom functions that may be useful at some point
- ### Framerate Stabilizer
  - Can be used within a main loop to force a specific run speed for a program
- ### FileExtensionRenamer
  - As the implies, contains a few methods to give files different extension types
  - Includes recursive renamer to give all files within a directory and its subdirectories a different extension
