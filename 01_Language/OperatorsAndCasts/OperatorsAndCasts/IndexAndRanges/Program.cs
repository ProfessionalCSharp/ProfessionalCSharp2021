using System;

var ix3 = new Index(3);
var ixend3 = new Index(3, fromEnd: true);

Index ix4 = 4;
Index ixend4 = ^4;

var range1 = 3..5;
var range2 = new Range(ix3, ix4);
var range3 = Range.StartAt(4);
(var offset, var length) = range3.GetOffsetAndLength(10);
