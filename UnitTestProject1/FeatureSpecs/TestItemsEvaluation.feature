Feature: TestItemsEvaluation
	In order to find entity from existing data
	As a test writer
	I want to find entity by defined conditions

Scenario: Find a policy by name, date and covered
	Given Policy A is taken from policiesSource
	  And policy A is created before year 2013
	  And policy A has at least 2 covered rows
	  And policy A has at least 80 people covered
	Then policy A must exist
	  And policy A must have id 3
	  And policy A name must contain naem
