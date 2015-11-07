Feature: NonEqualFilter
	In order to avoid comparing entity to itself
	As a test writer
	I want to specify filter that would fail if entity compares to itself and succeed if it does not

@NonEqualFilter1
Scenario: Non-equal filter
	Given insurance B is taken from insurancesSource	  
	  And for insurance B exists an assignment B	 
	  And for insurance B there is no insurance C in insurancesSource that meet requirements
	  And for insurance C exists an assignment C
	  And assignments C cover as much or more people than assignments B
	Then insurance B name must contain '@NoFilterLimitation1'
