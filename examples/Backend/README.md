StringValidator
The StringValidator validates user input if the brackets are correctly set, that means if they are the same type and if they are correctly open and closed. 
Table of Contents
	•	Introduction
	•	Getting Started
	◦	Prerequisites
	◦	Installation
	•	API Endpoints
	•	Technologies Used

Introduction
The project provides a Backend Part (.NET 6) and a Frontend Part (Angular).  
The Backend projects are structured like following:
	•	Application	(configured to run with Swagger)
	•	Common	(has the REST Client)
	⁃	Models
	•	Library		(has the Controller and the logic and UnitTests)
	⁃	Controllers	(our Endpoints)
	⁃	Repository	(not fully implemented yet)
	⁃	Helper		(String Validator logic)
	⁃	Mappings	(maps the Database Model to the User Model and vice versa)
	⁃	Models (Database Models)
	•	Console	( for Testing purpose )
	•	Tests (UnitTests)
The Frontend project is the Monitor

API Endpoints
Validate Input
	•	Endpoint: /UserInput
	•	Method: POST
	•	Description: Validates a user input string.

Technologies Used
List the technologies, frameworks, and libraries used in the project.
	•	.NET 6
	•	ASP.NET Core
	•	Angular
	•	MSTest
