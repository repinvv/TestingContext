Feature: TwoBranchItemsEvaluatuation
	In order to find entity from existing data
	As a test writer
	I want to find entity by defined conditions, describing a complex tree of entity interconnections	

@twobranch1
Scenario: Two branch find with one branch referencing the other
	Given policy B is taken from policiesSource
	  And policy B is created in year 2010
	  And for policy B exists a coverage B
	  And coverage B has type 'Dependent'
	  And coverages B have covered people
	  And for policy B exists a tax B
	  And tax B has type 'Federal'	  
	  And average payment per person in coverages B, specified in taxes B is over 10$
	Then policy B name must contain '@twobranch1'
