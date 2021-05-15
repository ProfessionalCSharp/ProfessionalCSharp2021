CarDealer dealer = new();

Consumer sebastian = new("Sebastian");
dealer.NewCarCreated += sebastian.NewCarIsHere;

dealer.CreateANewCar("Williams");

Consumer max = new("Max");
dealer.NewCarCreated += max.NewCarIsHere;

dealer.CreateANewCar("Aston Martin");

dealer.NewCarCreated -= sebastian.NewCarIsHere;

dealer.CreateANewCar("Ferrari");
