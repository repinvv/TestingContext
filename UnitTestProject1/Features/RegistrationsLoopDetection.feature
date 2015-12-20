Feature: Registrations loop detection
	In order to understand why registrations do not work	
	I want to be presented with meaningful exception at registration time
	instead of "stack overflow" at execution time.

@loopDetection
Scenario: Filter dependency loop detection
	Given insurance B is taken from insurancesSource
	  And assignment B has more people than assignments C
	  And assignment C has more people than assignments D
	  And for insurance B exists an assignment B
	  And for insurance B exists an assignment C
	  And for insurance B exists an assignment D
	  And assignment D has more people than assignments C
	When i try resolving insurance B
	Then i should get an exception with information about Assignment "D"

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