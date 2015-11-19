Feature: SimpleItemsEvaluation
	In order to find entity from existing data
	As a test writer
	I want to find entity by defined conditions

@simpleEvaluation1
Scenario: Single find and assert
	Given insurance B is taken from insurancesSource
	  And insurance B is created in year 2006
	Then insurance B must exist
	  And insurance B name must contain '@simpleEvaluation1'

@simpleEvaluation2
Scenario: Two entities in cascaded find and assert
	Given insurance B is taken from insurancesSource
	  And insurance B is created in year 2007
	  And for insurance B exists an assignment B
	  And assignment B has type 'Dependent'
	  And assignment B covers less people than maximum dependendts specified in insurance B
	Then insurance B must exist
	  And insurance B name must contain '2014'
	  And assignment B must exist
	  And assignment B Id must be 3
	  