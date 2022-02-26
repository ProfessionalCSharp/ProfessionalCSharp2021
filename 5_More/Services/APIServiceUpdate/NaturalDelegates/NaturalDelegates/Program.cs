Delegate d1 = new Func<int, bool>( x => x % 2 == 0);
Delegate d2 = (Func<int, bool>)(x => x % 2 == 0);

var d3 = new Func<int, bool>(x => x % 2 == 0);
var d4 = (Func<int, bool>)(x => x % x == 0);

// C# 10

Delegate d5 = (int x) => x % 2 == 0;
var d6 = (int x) => x % 2 == 0;

// specify a return type

var d7 = bool (int x) => x % 2 == 0;