Feature: SimpleNotFoundLogging
	In order to find out which filter filtered out last entity
	As a test writer
	I want to be told the filter expression and entity definition the filter is specified for

@notFoundLogging1
Scenario: Single failed find should print a filter
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2005
	When insurance B resolves
	Then resolution logger must produce info for filter, mentioning 'Insurance "B"' and '2005'

@notFoundLogging2
Scenario: Two entities in cascade failed find should print a filter
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2006
	  And for insurance B exists an assignment B
	  And assignment B has type 'Undefined'
	When insurance B resolves
	Then resolution logger must produce info for filter, mentioning 'Assignment "B"' and 'Undefined'

@notFoundLogging3
Scenario: Two entities in cascade failed find should print an "empty source" message
	Given insurance B is taken from policiesSource
	  And insurance B is created in year 2009
	  And for insurance B exists an assignment B
	  And assignment B has type 'Undefined'
	When insurance B resolves
	Then resolution logger must produce info for filter, mentioning 'Assignment "B"' and 'Any'
	 And resolution logger must produce info for filter, mentioning 'Assignment "B"' and 'MeetsCondition'
