Authentication API to generate token
UserName : Test
Passwors : Test@123

Use APIVersion : 1


1.Create a RESTful API endpoint(s) in .NET Core that can be used to display a list of open brewery names,
cities and phone numbers using the following backend data source - https://www.openbrewerydb.org/documentation.  
 -> Brewery/GetAll api is created which is fetching all details

2. Implement the following technical specifications:
a.	Implement In-Memory storage
	-> Memory cache is used to store the data
	
b.	Implement Classes and Interfaces
	-> Business Logic layer contains the interface and it's implementation
	
c.	Implement Dependency Injection
	-> Used Ioc file to resolve the dependency and added to program file
	
d.	API should allow for sorting by brewery name, distance and city
	-> Brewery/Search httpPost method have requestModel where we can pass the sorting column and direction "asc","desc"
	
	
e.	API should allow for search functionality
	-> Brewery/Search httpPost method have requestModel where we can pass search string on any field
	
f.	API should map/transform source data to a generic data model 
	-> Within the business entity class : BreweryExternalModel is raw model and BreweryBusinessModel is generic data model 
	and mapping between the BreweryExternalModel and BreweryBusinessModel is created BreweryMappingProfile class and added on program file (Automapper is used)
	
g.	Cache results for 10 minutes (IE: only make the call to the source API every 10 minutes
	-> GetAll API result is set to caching for 10 minute. after every 10 minute it will make a new call and then set to cache again
	
h.	Architecture should implement SOLID principles
	-> S: Single responsibility : 2 controller is created one for brewery and one for Authentication and each have it's own signle resposibility
	   O: Open/Closed principle : for example with the brecwery manager search method is closed for change and open modification, here this method is used on 2 different methods and handled response own way 
	   L: Liskov substituion : 
	   I: Interface segregation priciple : break down the interface to smaller, so they don't force to implement the method
	   D: Dependency inversion : brewery manager is depends on IbreweryManager Interface
	
i.	Add error handling
	-> ErrorHandling Middleware is created and added to program file of API project


----> Bonus Tasks : -
1.	Implement autocomplete as part of the search functionality
	-> created a Brewery/Autocomplete API which can be used for dorpdown on frontend side
	
2.	Add API versioning
	-> API versioning is implemented and right now all API supported with APIVersion 1
	
3.	Add logging
	-> serilog is implemented and logs will strore with the file Logs/logsfile.txt
	
4.	Replace in-memory with a real database (IE: SQLite + EF Core)
	-> created th data access layer, use this layer when we want to connect to database, do not connect db with business layer
	-> 3 layer architectured design pattern is followed
	
5.	Add API authentication/security
	-> JWT token is implemented. Login is require first and then use the response token to other API
