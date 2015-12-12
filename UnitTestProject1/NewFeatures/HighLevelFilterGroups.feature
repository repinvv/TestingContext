Feature: HighLevelFilterGroups
	In order to write high level tests
	As a test writer
	I want to specify OR and NOT-AND groups

Background: 
	Given I have companies
	| Id | Name      |
	| 1  | Microsoft |
	| 2  | Adobe     |
	| 3  | Oracle    |
	| 4  | Kaspersky |
	| 5  | MeraRu    |
	And I have departments in companies
	 | Id | CompanyId | Name | Type          |
	 | 1  | 1         | Ms1  | Supplementary |
	 | 2  | 2         | Ad1  | Business      | 
	 | 3  | 3         | Or1  | Supplementary |
	 | 4  | 4         | Ka1  | Supplementary |
	 | 5  | 5         | Me1  | Business      |
	And I have empleyees in departments
	| Id | DepartmentId | Name    | EmploymentType |
	| 1  | 5            | Peter   | PartTime       |
	| 2  | 3            | Alex    | PartTime       |
	| 3  | 4            | Richard | FullTime       |
	And I have work projects in departments
	| Id | DepartmentId | Name  | Budget |
	| 1  | 5            | proj1 | 100    |
	| 2  | 4            | proj2 | 100    |
	| 3  | 2            | proj3 | 50     |	
	And I have companies property
	| Id | CompanyId | Name             | Type         |
	| 1  | 5         | Software of Peter | Intellectual |

Scenario: use OrGroup to find company with department with either employees, projects or department type
	When i search for company
	 And i need company to have a department
	 And i need department to either be "Business" type or have "PartTime" employee or have project with budjet 100 or more 	 
	Then companies "Adobe, Oracle, Kaspersky, MeraRu" should be found for company requirements

Scenario: use NotGroup to find company with department that is not qualified to have all three conditions
	When i search for company
	 And i need company to have a department
	 And i need department to NOT have all three, "Business" type, "PartTime" employee, project budjet 100 or more 	 
	Then companies "Microsoft, Adobe, Oracle, Kaspersky" should be found for company requirements

Scenario: have Exception, notifying that you should specify direct relation between property and NotGroup or between employee and property
	When i search for company
	 And i need company to have a department
	 And i need company to have "Intellectual" property
	 And i need department to NOT have all three, "Business" type, "PartTime" employee, project budjet 100 or more 
	 And I need employee name mentioned in company property
	Then searching for company should result exception mentioning "Not", "Employee" and "CompanyProperty"

Scenario: when explicitly stated, NotGroup should allow to find data
	When i search for company
	 And i need company to have a department
	 And i need company to have "Intellectual" property
	 And for company property i need department to either be "Business" type or have "PartTime" employee or have project with budjet 100 or more 	 	 
	 #this goes under "Or"
	 And I need employee name mentioned in company property	 
	Then companies "MeraRu" should be found for company requirements