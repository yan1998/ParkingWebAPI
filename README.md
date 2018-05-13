# Summary
Solution constist of 2 projects: 
  * ParkingLibrary - .NET Standard library application which contain a parking logic 
  * ParkingWebAPI - ASP.NET Core WebAPI application which provide methods for working with parking
  
## REST API:
  * Cars:
      * GetCars - GET: api/Cars/GetCars
      * GetCar - GET: api/Cars/GetCar/{number}
      * AddCar -  GET: api/Cars/AddCar
      * DeleteCar - DELETE : api/Cars/DeleteCar/{number}
   * Parking:
      * FreePlaces - GET : api/Parking/FreePlaces
      * OccupiedPlaces - GET : api/Parking/OccupiedPlaces
      * Balance -  GET : api/Parking/Balance
   * Transactions:
      * GetTransactionLog - GET : api/Transactions/GetTransactionLog/
      * GetTransactions - GET : api/Transactions/GetTransactions/
      * GetCarTransactions - GET : api/Transactions/GetCarTransactions/{number}
      * AddCarBalance - PUT: api/Transactions/AddCarBalance/{number}
      
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
