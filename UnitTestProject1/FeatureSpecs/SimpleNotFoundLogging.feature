Feature: SimpleNotFoundLogging
	In order to find out which filter filtered out last entity
	As a test writer
	I want to be told the filter expression and entity definition the filter is specified for

@notFoundLogging1
Scenario: Single failed find should print a filter
	Given policy B is taken from policiesSource
	  And policy B is created in year 2005
	When policy B resolves
	Then resolution logger produces info for filter, mentioning 'Policy "B"' and '2005'

@notFoundLogging2
Scenario: Two entities in cascade failed find should print a filter
	Given policy B is taken from policiesSource
	  And policy B is created in year 2006
	  And for policy B exists a coverage B
	  And coverage B has type 'Undefined'
	When policy B resolves
	Then resolution logger produces info for filter, mentioning 'Coverage "B"' and 'Undefined'

@notFoundLogging3
Scenario: Two entities in cascade failed find should print an "empty source" message
	Given policy B is taken from policiesSource
	  And policy B is created in year 2009
	  And for policy B exists a coverage B
	  And coverage B has type 'Undefined'
	When policy B resolves
	Then resolution logger produces info for filter, mentioning 'Coverage "B"' and 'Source was null or empty'
