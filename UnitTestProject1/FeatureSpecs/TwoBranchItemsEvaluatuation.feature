Feature: TwoBranchItemsEvaluatuation
	In order to find entity from existing data
	As a test writer
	I want to find entity by defined conditions, describing a complex tree of entity interconnections	

@twobranch1
Scenario: Two branch search with condition between collections of those branches
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2010
	  And for insurance B exists an assignment B
	  And assignment B has type 'Dependent'
	  And assignments B have covered people
	  And for insurance B exists a tax B
	  And tax B has type 'Federal'	  
	  And average payment per person in assignments B, specified in taxes B is over 10$
	Then insurance B name must contain '@twobranch1'
	  And assignment B Id must be 7

@twobranch1
Scenario: Two branch search failure with condition between collections of those branches
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2010
	  And for insurance B exists an assignment B
	  And assignment B has type 'Dependent'
	  And assignments B have covered people
	  And for insurance B exists a tax B
	  And tax B has type 'Federal'	  
	  And average payment per person in assignments B, specified in taxes B is over 17$
	When insurance B resolves
	Then resolution logger must produce info for filter, mentioning 'Assignment "B"' and '17'