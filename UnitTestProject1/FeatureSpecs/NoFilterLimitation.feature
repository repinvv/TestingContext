Feature: NoFilterLimitation
	In order to create advanced search
	As a test writer
	I want to have no limitation in filter creation

@NoFilterLimitation1
Scenario: Filter, that was previously forbidden
	Given insurance B is taken from insurancesSource	  
	  And for insurance B exists an assignment B	 
	  And for insurance B there is no insurance C in insurancesSource that meet requirements
	  And for insurance C exists an assignment C
	  And assignments C cover more people than assignments B
	Then insurance B name must contain '@NoFilterLimitation1'
