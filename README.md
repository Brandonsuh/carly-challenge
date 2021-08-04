# carly-challenge
Carly Challenge

The main project repository is for Task 1

I have created an Azure SQL database and deployed the project on Azure.

The domain and IP Whitelisting can be done on the Azure/AWS platforms, e.g. firewall settings or domain/IP filtering. 

The following Restful APIs are implemented in Json format.

Get all the bookings

[HTTP GET]
https://carly-challenge.azurewebsites.net/api/bookings

Get the particular booking

[HTTP GET]
https://carly-challenge.azurewebsites.net/api/bookings/{id}

Create a booking

[HTTP POST]
https://carly-challenge.azurewebsites.net/api/bookings

Body example:
{

	"customerId": 10,

	"amount": 399.99,

	"created": "2021-08-01"

}

Update a booking

[HTTP PUT]
https://carly-challenge.azurewebsites.net/api/bookings/{id}

Body example:
{
	
	"bookingId":24,

	"customerId":10,

	"amount":33,

	"created":"2021-08-01"

}

Delete a booking

[HTTP DELETE]
https://carly-challenge.azurewebsites.net/api/bookings/{id}

Report API

[HTTP POST]
https://carly-challenge.azurewebsites.net/api/bookings/report

Body example:
{
	
	"startdate":"01/01/2021",

	"enddate":"01/01/2022"

}

Please kindly note the business logic for this Reporting API is in the controller method.
I have created a separate Report Store Procedure file just in case we would like to implement the logic in the database level.

Task2.sql is for Task 2