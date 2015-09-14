Feature: TestItemsEvaluation
	In order to find entity from existing data
	As a test writer
	I want to find entity by defined conditions

Scenario: Find a policy by name, date and covered
	Given Policy B is taken from policiesSource
	  And policy B is created before year 2014
	  And policy B has at least 2 covered rows
	  And policy B has at least 80 people covered
	Then policy B must exist
	  And policy B must have id 3
	  And policy B name must contain naem
