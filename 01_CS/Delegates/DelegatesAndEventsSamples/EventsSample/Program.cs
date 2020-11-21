CarDealer dealer = new();

Consumer sebastian = new("Sebastian");
dealer.NewCarInfo += sebastian.NewCarIsHere;

dealer.NewCar("Williams");

Consumer max = new("Max");
dealer.NewCarInfo += max.NewCarIsHere;

dealer.NewCar("Aston Martin");

dealer.NewCarInfo -= sebastian.NewCarIsHere;

dealer.NewCar("Ferrari");
