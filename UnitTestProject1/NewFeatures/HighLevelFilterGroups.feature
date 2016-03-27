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
	 | 6  | 4         | Ka2  | Venture       |
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
	| 1  | 5         | Software of Alex | Intellectual |

Scenario: use OrGroup to find company with department with either employees, projects or department type
	When i need a company
	 And i need company to have a department
	 And i need department to either be "Business" type or have "PartTime" employee or have project with budjet 100 or more 	 
	Then companies "Adobe, Oracle, Kaspersky, MeraRu" should be found for company requirements

Scenario: use OrGroup without child declarations
	When i need a company
	 And i need company to have a department
	 And i need department to either be "Business" or "Venture" type
	Then companies "Adobe, Kaspersky, MeraRu" should be found for company requirements

Scenario: use NotGroup to find company with department that is not qualified to have all three conditions
	When i need a company
	 And i need company to have a department
	 And i need department to NOT have all three, "Business" type, "PartTime" employee, project budjet 100 or more 	 
	Then companies "Microsoft, Adobe, Oracle, Kaspersky" should be found for company requirements

Scenario: have Exception, notifying that you should specify direct relation between property and NotGroup or between employee and property
	When i need a company
	 And i need company to have a department
	 And i need company to have "Intellectual" property
	 And i need department to NOT have all three, "Business" type, "PartTime" employee, project budjet 100 or more 
	 And I need employee name mentioned in company property
	Then i should get a detailed exception trying to search for company
	 And Detailed exception should mention types "Not,Employee,CompanyProperty"

Scenario: when explicitly stated, NotGroup should allow to find data
	When i need a company	
	 And i need company to have a department	 
	 And i need company to have "Intellectual" property
	 And for company property i need department to NOT have all three, "Business" type, "PartTime" employee, project budjet 100 or more 	 	 
	 #this goes under "Not"
	 And I need employee name mentioned in company property	 
	Then companies "MeraRu" should be found for company requirements