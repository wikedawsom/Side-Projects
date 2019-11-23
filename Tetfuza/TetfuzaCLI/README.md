# Tetfuza
## A tetris clone originally written by myself in C#

To play:
- The game only registers one key at a time.
- Arrow key left and right move the current piece left and right (respectively)
- Keyboard keys "Z" and "X" rotate the current piece counterclockwise and clockwise (respectively)
- Arrow key down causes the piece to move down one space immediately



### Backend logic for those interested:

Each Piece is a 7x7 2-D List of char(s)

- The blank spaces are FuzaPiece.BLANK_CHAR
- Filled spaces (referred to in the code as fuza(s)) are defined by FuzaPiece.FUZA_CHAR
- FuzaPieceType is an enum that has values 0-6 for each type of piece
- Coordinate is a struct that has an x positon and a y position (as normal grid coordinated do)
- _blocks is a List of Coordinate(s) where each coordinate defines the offset of a single fuza from the center of the Piece
  - FuzaPieceType is used to determine what Coordinate(s) are present in the _blocks list

The Board is a 10x22 2-D List of char(s)

- The blank spaces are TetfuzaBoard.EMPTY_CHAR
- The current piece's center is defined by the Coordinate stored in "_pieceCenter"
- Spaces occupied by the current piece's fuza are TetfuzaBoard.MOVING_CHAR
- Spaces occupied by previously placed pieces are TetfuzaBoard.LOCKDOWN_CHAR

Bits and pieces

- Run() is the entry point and will execute the main loop until CheckTopOut() returns true
- Piece type is randomly rolled using RNG from the Random class
- CurrentPiece is the FuzaPiece that is currently being dropped and controlled
- AfterPiece is the FuzaPiece that will start falling after the current one locks down
