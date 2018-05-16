# Summary
Solution constist of 2 projects: 
  * ParkingLibrary - .NET Standard library application which contain a parking logic 
  * ParkingWebAPI - ASP.NET Core WebAPI application which provide methods for working with parking
  
## REST API:
  * Cars:
      * GetCars - GET: api/cars
      * GetCar - GET: api/cars/number
      * AddCar -  GET: api/cars
      * DeleteCar - DELETE : api/cars/{number}
   * Parking:
      * FreePlaces - GET : api/parking/FreePlaces
      * OccupiedPlaces - GET : api/parking/OccupiedPlaces
      * Balance -  GET : api/parking/Balance
   * Transactions:
      * GetTransactionLog - GET : api/transactions/TransactionLog/
      * GetTransactions - GET : api/transactions/Transactions/
      * GetCarTransactions - GET : api/transactions/CarTransactions/{number}
      * AddCarBalance - PUT: api/transactions/AddCarBalance/{number}
      
## POST and GET body:
### body POST for AddCar
```
{
	"carNumber" : "test3",
	"balance" : 1260,
	"carType" : 3
}
```
carType:
* Passenger -0
* Truck - 1
* Bus - 2
* Motorcycle - 3

### body PUT for AddCarBalance
```
{
	"value":1000
}
```
