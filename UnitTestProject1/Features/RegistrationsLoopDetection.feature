Feature: Registrations loop detection
	In order to understand why registrations do not work	
	I want to be presented with meaningful exception at registration time
	instead of "stack overflow" at execution time.

@loopDetection
Scenario: Filter dependency loop detection
   	Given I have companies
	| Id | Name      |
    When i need a company
	 And i need company to have a department B
	 And i need company to have a department C
	 And i need company to have a department D

	 And i need department B to have higher headcount that departments C
	 And i need department C to have higher headcount that departments D
	 And i need department D to have higher headcount that departments B

    Then i should get a detailed exception trying to search for company
	 And Detailed exception should mention types "Department "B",Department "C",Department "D""

Scenario: Entity dependency loop detection
   	Given I have companies
	| Id | Name      |
	When i need a company
	 And i need company to have a department
	 And i specify that employee depends on department and project
	 And i specify that project depends on department and company property
	 And i specify that company property depends on company and employee
    Then i should get a detailed exception trying to search for company
	 And Detailed exception should mention types "Employee,WorkProject,CompanyProperty"