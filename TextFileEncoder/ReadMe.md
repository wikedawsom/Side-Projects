This is a crude text encryption idea I had when I saw that the .NET Core "File" class had a GetAllBytes() function.

This program reads a file and adds or subtracts an offset value from each byte of data, then writes the altered data to another file.

Goals:

Allow a user defined shift

More advanced encryption method