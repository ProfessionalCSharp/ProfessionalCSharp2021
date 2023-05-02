namespace Codebreaker.Models;

public readonly partial record struct ColorResult(
    byte Correct, 
    byte WrongPosition);
