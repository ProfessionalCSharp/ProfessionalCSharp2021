using Wrox.ProCSharp.Delegates;

var dealer = new CarDealer();

var sebastian = new Consumer("Sebastian");
dealer.NewCarInfo += sebastian.NewCarIsHere;

dealer.NewCar("Williams");

var max = new Consumer("Max");
dealer.NewCarInfo += max.NewCarIsHere;

dealer.NewCar("Aston Martin");

dealer.NewCarInfo -= sebastian.NewCarIsHere;

dealer.NewCar("Ferrari");
