# Diagrams

## Models


### Owned Entities

```mermaid
classDiagram
   Address *-- Location : Location
   Person *-- Address : BusinessAddress
   Person o-- Address : PrivateAddress
   
   class Location {
     +Country: string
     +City: string
   }
   class Address {
     +LineOne: string
     +LineTwo: string
   }
   class Person {
     +PersonId: int
     +FirstName: string
     +LastName: string
   }
```

```mermaid
erDiagram
  People {
    int PersonId
    string FirstName
    string LastName
    string BusinessAdressLineOne
    string BusinessAddressLineTwo
    string BusinessCity
    string BusinessCountry
    int PrivateAddressId
  }
  PrivateAddresses {
    string LineOne
    string LineTwo
    string City
    string Country
  }
```

### Table Splitting

```mermaid
classDiagram

   MenuItem "1" o--> "1" MenuDetails : Details
   
   class MenuDetails {
     +MenuItemId: int
     +KitchenInfo: string
     +MenusSold: int
   }
   class MenuItem {
     +MenuItemId: int
     +Title: string
     +Subtitle: string
     +Price: decimal
   }
```

```mermaid
erDiagram
  MenuItem {
    int MenuItemId
    string Title
    string Subtitle
    money Price
    string KitchenInfo
    int MenusSold
  }
```


## 

```mermaid
classDiagram

   MenuItem "1" o--> "1" MenuDetails : Details
   
   class MenuDetails {
     +MenuDetailsId: int
     +KitchenInfo: string
     +MenusSold: string
     +Price: decimal
   }
   class MenuItem {
     +MenuItemId: int
     +Title: string
     +Subtitle: string
     +Price: decimal
   }
   class MenuCard {
     +MenuCardId: int
     +Title: string
   }
   class Restaurant {
     +Name: string
   }
```

## Inheritance

## Table per Hierarchy (TPH)

TPH is a pattern where you have a base class and multiple derived classes. Each derived class has its own table. The base class table contains a discriminator column that identifies the type of the derived class. The derived class tables contain all the columns of the base class table plus their own columns.

```mermaid
classDiagram

    Payment <|-- CashPayment
    Payment <|-- CreditcardPayment
    class Payment {
        +PaymentId: int
        +Name: string
        +Amount: decimal
    }
    
    class CashPayment {
    }
    
    class CreditcardPayment {
        +CreditcardNumber: string
    }
```

```mermaid
erDiagram
  Payments {
    int PaymentId
    string PaymentType
    string Name
    money Amount
    string CreditcardNumber
  }
```

## Table per Type (TpT)

Table per type

```mermaid
erDiagram
  Payments {
    int PaymentId
    string PaymentType
    string Name
    money Amount

  }

  CashPayments {
    int PaymentId
  }

  CreditcardPayments {
    int PaymentId
    string CreditcardNumber
  }
```

## Table per concrete Type (TcT)

```mermaid
classDiagram

  Payment <|-- CashPayment
  Payment <|-- CreditcardPayment
        
  class Payment {
    <<abstract>>
    +PaymentId: int
    +Name: string
    +Amount: decimal
  }
        
  class CashPayment {
  }
        
  class CreditcardPayment {
    +CreditcardNumber: string
  }
```
    

```mermaid
erDiagram
  CashPayments {
    int PaymentId
    string Name
    money Amount
  }

  CreditcardPayments {
    int PaymentId
    string Name
    money Amount
    string CreditcardNumber
  }
```

## Many-to-many Relationships

```mermaid
classDiagram
  Book "*" <--> "*" Person
        
  class Book {
    +BookId: int
    +Title: string
    +Publisher: string
  }
        
  class Person {
    +PersonId: int
    +FirstName: string
    +LastName: string
  }
```

```mermaid
erDiagram
  Books {
    int BookId
    string Title
    string Publisher
  }

  People {
    int PersonId
    string FirstName
    string LastName
  }

  BookPeople {
    int BookId
    int PersonId
  }
```