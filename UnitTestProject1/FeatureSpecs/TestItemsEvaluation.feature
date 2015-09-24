Feature: TestItemsEvaluation
	In order to find entity from existing data
	As a test writer
	I want to find entity by defined conditions

@simpleEvaluation1
Scenario: Single find and assert
	Given policy B is taken from policiesSource
	  And policy B is created before year 2014
	Then policy B must exist
	  And policy B name must contain '2013'

@simpleEvaluation2
Scenario: Two entities in cascaded find and assert
	Given policy B is taken from policiesSource
	  And policy B is created before year 2015
	  And for policy B exists a coverage B
	  And coverage B has type 'Dependent'
	Then policy B must exist
	  And policy B name must contain '2014'
	  And coverage B must exist
	  And coverage B Id must be 3
	  